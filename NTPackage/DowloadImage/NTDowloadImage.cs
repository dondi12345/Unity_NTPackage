using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace NTPackage.DowloadImage
{
    public class ByteImage
    {
        public byte[] Data;
    }

    public class NTDowloadImage : MonoBehaviour
    {
        public static IEnumerator LoadSpriteFromUrl(string url, Action<Sprite> callback)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Texture2D loadedTexture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
                callback(sprite);
            }
        }

        public static Sprite LoadSpriteFromDisk(string spriteName)
        {
            ByteImage byteImage = JsonUtility.FromJson<ByteImage>(PlayerPrefs.GetString(spriteName, JsonUtility.ToJson(new ByteImage())));
            if (byteImage == null || byteImage.Data.Length == 0) return null;
            Texture2D loadedTexture = new Texture2D(0, 0);
            loadedTexture.LoadImage(byteImage.Data);
            Sprite sprite = Sprite.Create(loadedTexture, new Rect(0f, 0f, loadedTexture.width, loadedTexture.height), Vector2.zero);
            return sprite;

        }

        public static bool SaveSpriteToDisk(Sprite sprite, string spriteName)
        {
            if (sprite == null || spriteName.Length == 0) return false;
            byte[] textureBytes = sprite.texture.EncodeToPNG();
            ByteImage byteImage = new ByteImage();
            byteImage.Data = textureBytes;
            PlayerPrefs.SetString(spriteName, JsonUtility.ToJson(byteImage));
            return true;
        }
    }
}
