using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTPackage.UI
{
    public class NTButtonState : NTButtonEffect
    {
        public Transform OffState;
        public List<Transform> States;
        public List<Action> ActionState;

        public void _Onclick(){
            if(!OffState.gameObject.activeSelf){
                for (int i = 0; i < States.Count-1; i++)
                {
                    if(this.States[i].gameObject.activeSelf){
                        this.States[i].gameObject.SetActive(false);
                        this.States[i+1].gameObject.SetActive(true);
                        try
                        {
                            this.ActionState[i+1].Invoke();
                        }
                        catch (System.Exception){}
                        return;
                    }
                }
                this.States[this.States.Count-1].gameObject.SetActive(false);
                this.States[0].gameObject.SetActive(true);
                try
                {
                    this.ActionState[0].Invoke();
                }
                catch (System.Exception){}
                return;
            }else{
                this.Off();
                this.OffState.gameObject.SetActive(false);
                this.States[0].gameObject.SetActive(true);
                try
                {
                    this.ActionState[0].Invoke();
                }
                catch (System.Exception){}
            }
        }

        public void Off(){
            this.OffState.gameObject.SetActive(true);
            foreach (Transform item in States)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}