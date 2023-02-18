using System;
using UnityEngine;

namespace Molioo.Simulator.Photos
{
    [System.Serializable]
    public class PhotoEntry
    {
        [NonSerialized]
        public Texture2D PhotoTexture;

        public string PhotoName;

        public int Width;

        public int Height;

        public PhotoEntry(Texture2D texture, string name)
        {
            PhotoTexture = texture;
            PhotoName = name;
            Width = texture.width;
            Height = texture.height;
        }

        public string GetPath()
        {
            return Application.persistentDataPath + "/" + PhotoName + ".png";
        }
    }
}