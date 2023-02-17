using UnityEngine;

namespace Molioo.Simulator.Photos
{
    public class PhotoObject : MonoBehaviour
    {
        [SerializeField]
        private Renderer _photoRenderer = null;


        // Start is called before the first frame update
        void Start()
        {
            PhotoGalleryManager.LoadPhotoGallery();
            TryToSetPhotoTexture();
        }

        private void TryToSetPhotoTexture()
        {
            if (PhotoGalleryManager.Photos.Count == 0)
                return;

            //As for now, this just loads last photo but this will be changed in future, glad it works
            Material photoMaterial = _photoRenderer.material;
            photoMaterial.mainTexture = PhotoGalleryManager.Photos.Last().PhotoTexture;
        }
    }
}