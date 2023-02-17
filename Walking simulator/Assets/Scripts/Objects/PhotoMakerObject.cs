using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Molioo.Simulator.Photos;

public class PhotoMakerObject : MonoBehaviour
{
    [SerializeField]
    private Camera _cameraComponent = null;

    [SerializeField]
    private Image _photoImage = null;

    [SerializeField]
    private float _waitTimeBeforePhoto = 3f;

    private WaitForEndOfFrame _waitForFrame;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        InitializeObject();   
    }

    private void InitializeObject()
    {
        _waitForFrame = new WaitForEndOfFrame();
        _waitForSeconds = new WaitForSeconds(1f);
    }

    public void TakePhoto()
    {
        Debug.Log("Taking photo");
        StartCoroutine(TakePhotoRoutine());
    }

    private IEnumerator TakePhotoRoutine()
    {
        yield return new WaitForSeconds(_waitTimeBeforePhoto);

        SwitchCamera(true);
        GameUIManager.Instance.SwitchAllUIVisibility(false);

        yield return _waitForFrame;

        TakeScreenShot();
        _photoImage.ChangeAlpha(1);
        yield return _waitForSeconds;
        _photoImage.ChangeAlpha(0);

        SwitchCamera(false);
        GameUIManager.Instance.SwitchAllUIVisibility(true);
    }

    private void SwitchCamera(bool enablePhotoCamera)
    {
        FPSCameraController.Instance.gameObject.SetActive(!enablePhotoCamera);
        _cameraComponent.enabled = enablePhotoCamera;
    }

    private void TakeScreenShot()
    {
        Texture2D screenShot = ScreenCapture.CaptureScreenshotAsTexture();

        // All the following is necessary due to a Unity bug when working in Linear color space:
        Texture2D newScreenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        newScreenShot.SetPixels(screenShot.GetPixels());
        newScreenShot.Apply();

        _photoImage.sprite = Sprite.Create(newScreenShot, new Rect(0, 0, Screen.width,Screen.height), new Vector2(0, 0));
        PhotoGalleryManager.SavePhoto(newScreenShot, Guid.NewGuid().ToString());
        PhotoGalleryManager.SavePhotosGalleryData();
    }

    private void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
    }
}
