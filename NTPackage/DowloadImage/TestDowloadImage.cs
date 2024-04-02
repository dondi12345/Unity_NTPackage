using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NTPackage.DowloadImage
{
    public class TestDowloadImage : MonoBehaviour
    {
        public Image textureImage;
        public string imageURL;
        public string fileName;

        [ContextMenu("GetImage")]
        public void GetImage()
        {
            StartCoroutine(NTDowloadImage.LoadSpriteFromUrl(this.imageURL, (sprite) =>
            {
                this.textureImage.sprite = sprite;
            }));
        }
        [ContextMenu("SaveImage")]
        public void SaveImage()
        {
            NTDowloadImage.SaveSpriteToDisk(this.textureImage.sprite, this.fileName);
        }

        [ContextMenu("LoadImage")]
        public void LoadImage()
        {
            this.textureImage.sprite = NTDowloadImage.LoadSpriteFromDisk(this.fileName);
        }
    }

}
