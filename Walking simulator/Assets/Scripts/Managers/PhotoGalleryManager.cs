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

        private static bool _loadedPhotoGalleryFromFile = false;

        public static void SavePhoto(Texture2D photoTexture, string photoName)
        {
            PhotoEntry photoEntry = new PhotoEntry(photoTexture, photoName);
            Photos.Add(photoEntry);

            byte[] _bytes = photoTexture.EncodeToPNG();
            File.WriteAllBytes(photoEntry.GetPath(), _bytes);
        }

        public static void SavePhotosGalleryData()
        {
            StreamWriter file = new StreamWriter(SaveFilePath, false);
            string content = JsonUtility.ToJson(new PhotoGalleryWrapper(Photos));
            file.Write(content);
            file.Close();
        }

        public static void LoadPhotoGallery()
        {
            if (_loadedPhotoGalleryFromFile)
                return;

            try
            {
                string jsonContent = File.ReadAllText(SaveFilePath);
                if (string.IsNullOrEmpty(jsonContent))
                {
                    Debug.Log("File was empty or nulled");
                    Photos = new List<PhotoEntry>();
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
            }

            _loadedPhotoGalleryFromFile = true;
        }

        private static void LoadTextureForPhotos()
        {
            for (int i = 0; i < Photos.Count; i++)
            {
                try
                {
                    byte[] photoBytes = File.ReadAllBytes(Photos[i].GetPath());
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