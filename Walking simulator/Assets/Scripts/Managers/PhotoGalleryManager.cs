using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Molioo.Simulator.Photos
{
    public class PhotoGalleryManager : MonoBehaviour
    {
        private const string SAVE_FILE_NAME = "Photos.json";

        public static string SaveFilePath { get { return Application.persistentDataPath + "/" + SAVE_FILE_NAME; } }

        public static List<PhotoEntry> Photos = new List<PhotoEntry>();

        public static void SavePhoto(Texture2D photoTexture, string photoName)
        {
            PhotoEntry photoEntry = new PhotoEntry(photoTexture, photoName);
            Photos.Add(photoEntry);

            byte[] _bytes = photoTexture.EncodeToPNG();
        }

        public static void SavePhotosGalleryData()
        {
            StreamWriter file = new StreamWriter(SaveFilePath, false);
            string content = JsonUtility.ToJson(new PhotoGalleryWrapper(Photos));
            file.Write(content);
            file.Close();
            Debug.Log("Saved photo gallery");
        }

        public static void LoadPhotoGallery()
        {
            try
            {
                string jsonContent = File.ReadAllText(SaveFilePath);
                if (string.IsNullOrEmpty(jsonContent))
                {
                    Debug.Log("File was empty or nulled");
                    Photos = new List<PhotoEntry>();
                    return;
                }
                else
                {
                    PhotoGalleryWrapper wrapper = JsonUtility.FromJson<PhotoGalleryWrapper>(jsonContent);
                    if (wrapper.PhotoEntries != null)
                    {
                        Photos = new List<PhotoEntry>(wrapper.PhotoEntries);
                    }
                    LoadTextureForPhotos();
                }
            }
            catch (Exception e)
            {
                Debug.Log("Something went wrong when reading file : " + e.Message);
                Photos = new List<PhotoEntry>();
                return;
            }
        }

        private static void LoadTextureForPhotos()
        {
            for (int i = 0; i < Photos.Count; i++)
            {
                try
                {
                    byte[] photoBytes = File.ReadAllBytes(Application.persistentDataPath + "/" + Photos[i].PhotoName + ".png");
                    Texture2D photoTexture = new Texture2D(Photos[i].Width, Photos[i].Height);
                    photoTexture.LoadImage(photoBytes);
                    photoTexture.Apply();
                    Photos[i].PhotoTexture = photoTexture;
                }
                catch (Exception e)
                {
                    Debug.LogError("Photo wasn't loaded correctly : " + e.Message);
                }
            }
        }


        public class PhotoGalleryWrapper
        {
            public List<PhotoEntry> PhotoEntries;

            public PhotoGalleryWrapper(List<PhotoEntry> photoEntries)
            {
                PhotoEntries = new List<PhotoEntry>(photoEntries);
            }
        }
    }
}