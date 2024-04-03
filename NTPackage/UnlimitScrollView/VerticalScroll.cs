using System.Collections;
using System.Collections.Generic;
using NTFunctions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NTFunctions
{
    public class VerticalScroll : LoadBehaviour
    {
        public RectTransform Content;

        public float SizeX;
        public float SizeY;
        public int amount;
        public int Max;

        public Vector3 OriginalPos;

        public List<VerticalItem> VerticalItems;

        public override void LoadComponents()
        {
            base.LoadComponents();
            this.OriginalPos = this.Content.localPosition;
            this.VerticalItems.Clear();
            foreach (Transform item in this.Content)
            {
                if (item.TryGetComponent<VerticalItem>(out VerticalItem verticalItem))
                {
                    this.VerticalItems.Add(verticalItem);
                    item.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
                }
            }
        }
        [Button]
        public void OnUI(int max)
        {
            this.Content.localPosition = this.OriginalPos;
            this.Max = max;
            int index = 0;
            this.Content.sizeDelta = new Vector2(this.Content.sizeDelta.x, (int)(this.Max / amount + 1) * this.SizeY);
            foreach (Transform item in this.Content)
            {
                if (index < 0 || index >= this.Max)
                {
                    item.gameObject.SetActive(false);
                }
                else
                {
                    item.GetComponent<VerticalItem>().SetData(index, this);
                    item.gameObject.SetActive(true);
                }
                index++;
            }
        }

        public int Top = 4;
        public int Bot = 2;

        protected override void Update()
        {
            while (true)
            {
                VerticalItem firstVerticalIcon = this.Content.GetChild(0).GetComponent<VerticalItem>();
                VerticalItem lastVerticalIcon = this.Content.GetChild(this.Content.childCount - 1).GetComponent<VerticalItem>();

                float num = (this.Content.localPosition.y + firstVerticalIcon.GetComponent<RectTransform>().localPosition.y) / 150;

                if (num > Top)
                {
                    if (lastVerticalIcon.Index >= this.Max - 1) return;
                    firstVerticalIcon.SetData(lastVerticalIcon.Index + 1, this);
                    firstVerticalIcon.transform.SetAsLastSibling();
                    continue;
                }

                if (num < Bot)
                {
                    if (firstVerticalIcon.Index <= 0) return;
                    lastVerticalIcon.SetData(firstVerticalIcon.Index - 1, this);
                    lastVerticalIcon.transform.SetAsFirstSibling();
                    continue;
                }
                break;
            }

        }

        public virtual void UpdateData(){
            foreach (VerticalItem item in this.VerticalItems)
            {
                item.UpdateData();
            }
        }
    }
}