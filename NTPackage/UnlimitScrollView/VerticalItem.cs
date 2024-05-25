using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.Functions
{
    public class VerticalItem : MonoBehaviour
    {
        public int Index;
        public VerticalScroll VerticalScroll;

        public int X;
        public int Y;
        public int posX;
        public int posY;

        public void SetData(int index, VerticalScroll verticalScroll)
        {
            this.Index = index;
            this.VerticalScroll = verticalScroll;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(verticalScroll.SizeX, verticalScroll.SizeY);
            int x = this.Index % verticalScroll.amount;
            int y = this.Index / verticalScroll.amount;
            this.X = x;
            this.Y = y;
            this.transform.localPosition = new Vector2(x * verticalScroll.SizeX, -y * verticalScroll.SizeY);
            this.posX = (int)this.transform.localPosition.x;
            this.posY = (int)this.transform.localPosition.y;
            this.UpdateData();
        }

        public virtual void UpdateData()
        {

        }
    }
}