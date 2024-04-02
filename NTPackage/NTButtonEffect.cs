using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NTPackage.Functions;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace NTPackage.UI{
    public class NTButtonEffect : NTBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Vector3 SizeUp = new Vector3(1.05f, 1.05f, 1.05f);
        public Vector3 SizeDown = new Vector3(0.95f, 0.95f, 0.95f);
        public Transform Main;

        public Transform Light;  
        public Transform Dark;  

        public UnityEvent Onclick;
        public UnityEvent Onhold;

        public bool IsClick = false;
        public bool IsHold = false;

        public Vector2 MouseDownPos;

        public Coroutine HoldCor;
        public bool IsIn = false;

        public void OnPointerDown(PointerEventData eventData){
            this.IsClick = true;
            this.IsHold = false;
            this.MouseDownPos = Input.mousePosition;
            if(this.Main == null) this.Main = transform.parent;
            this.Main.DOComplete();
            this.Main.DOScale(this.SizeDown, 0.1f);
            StartCoroutine(this.Holding());
        }

        IEnumerator Holding(){
            this.IsHold = true;
            yield return new WaitForSeconds(0.75f);
            if(this.IsClick && this.IsHold){
                this.IsClick = false;
                this.IsHold = false;
                CheckIn();
                if (this.IsIn) this.Onhold.Invoke();
            }
        }
        
        public void OnPointerUp(PointerEventData eventData){
            CheckIn();
            if(this.Main == null) this.Main = transform.parent;
            this.Main.DOComplete();
            this.Main.DOScale(this.SizeUp, 0.1f).OnComplete(()=>{
                this.Main.DOScale(new Vector3(1f,1f,1f), 0.05f);
                if(this.IsClick){
                    this.IsClick = false;
                    this.IsHold = false;
                    if (this.IsIn) this.Onclick.Invoke();
                }
            });
        }
    
        public void Chose(){
            if(this.Light != null && this.Dark != null){
                this.Light.gameObject.SetActive(true);
                this.Dark.gameObject.SetActive(false);
            }
        }
        public void UnChose(){
            if(this.Light != null && this.Dark != null){
                this.Dark.gameObject.SetActive(true);
                this.Light.gameObject.SetActive(false);
            }
        }

        public bool IsChose()
        {
            if (this.Light == null) return false;
            return this.Light.gameObject.activeSelf;
        }

        public void CheckIn()
        {
            Vector2 pos = Input.mousePosition;
            if (Mathf.Abs(pos.x - this.MouseDownPos.x) > 1) { this.IsIn = false; return; }
            if (Mathf.Abs(pos.y - this.MouseDownPos.y) > 1) { this.IsIn = false; return; }
            this.IsIn = true;
        }
    }
}
