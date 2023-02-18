using Molioo.Simulator.Photos;
using System;
using System.Collections;
using UnityEngine;

public class PhotoMakerItem : Item
{
    private bool _isMakingPhoto = false;

    public override void UpdateItemBehaviour()
    {
        base.UpdateItemBehaviour();
        CheckForMakePhotoInput();
    }

    private void CheckForMakePhotoInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !_isMakingPhoto)
        {
            MakePhoto();
        }
    }

    private void MakePhoto()
    {
        _isMakingPhoto = true;
        StartCoroutine(TakePhotoRoutine());
    }

    private IEnumerator TakePhotoRoutine()
    {
        GameUIManager.Instance.SwitchAllUIVisibility(false);

        yield return new WaitForEndOfFrame();

        TakeScreenShot();
        yield return new WaitForEndOfFrame();

        GameUIManager.Instance.SwitchAllUIVisibility(true);
        _isMakingPhoto = false;

    }

    private void TakeScreenShot()
    {
        Texture2D screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        // All the following is necessary due to a Unity bug when working in Linear color space:
        Texture2D newScreenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        newScreenShot.SetPixels(screenShot.GetPixels());
        newScreenShot.Apply();

        //_photoImage.sprite = Sprite.Create(newScreenShot, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));
        PhotoGalleryManager.SavePhoto(newScreenShot, Guid.NewGuid().ToString());
        PhotoGalleryManager.SavePhotosGalleryData();
    }
}
