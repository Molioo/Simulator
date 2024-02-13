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
    private float _waitTimeBeforePhoto = 3f;

    [Header("Photo spawning")]
    [SerializeField]
    private bool _spawnPhoto = false;

    [SerializeField]
    private PhotoObject _photoObjectPrefab = null;

    [SerializeField]
    private Transform _photoSpawnTransform = null;

    private WaitForEndOfFrame _waitForFrame;
    private WaitForSecondsRealtime _waitForSeconds;

    private void Awake()
    {
        InitializeObject();   
    }

    private void InitializeObject()
    {
        _waitForFrame = new WaitForEndOfFrame();
        _waitForSeconds = new WaitForSecondsRealtime(1f);
    }

    public void TakePhoto()
    {
        StartCoroutine(TakePhotoRoutine());
    }

    private IEnumerator TakePhotoRoutine()
    {
        yield return new WaitForSeconds(_waitTimeBeforePhoto);

        SwitchCamera(true);
        GameUIManager.Instance.SwitchAllUIVisibility(false);

        Time.timeScale = 0.001f;
        yield return _waitForFrame;

        TakeScreenShot();


        yield return _waitForSeconds;

        SpawnPhoto();

        SwitchCamera(false);
        Time.timeScale = 1f;

        GameUIManager.Instance.SwitchAllUIVisibility(true);
    }

    private void SpawnPhoto()
    {
        if(_spawnPhoto)
        {
            PhotoObject photo = Instantiate(_photoObjectPrefab, null);
            photo.transform.SetPositionAndRotation(_photoSpawnTransform.position, _photoSpawnTransform.rotation);
            photo.TryToSetPhotoTexture();
        }
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

        //_photoImage.sprite = Sprite.Create(newScreenShot, new Rect(0, 0, Screen.width,Screen.height), new Vector2(0, 0));
        PhotoGalleryManager.SavePhoto(newScreenShot, Guid.NewGuid().ToString());
        PhotoGalleryManager.SavePhotosGalleryData();
    }
}
