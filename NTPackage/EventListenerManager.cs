using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NTPackage.EventDispatcher
{
    using System.Linq;
    using Functions;
    using NTFunctions;

    public enum EventCode{
        ReciveChatGlobal,
        ReciveChatFriend,
        ViewInforMobOn,
        EndCombat,
        ChangeNormalView,
        ChangeRegionView,
        OffAttackSound,
        CompanionUpdate,
        CompanionEquipUpdate,
        UpdateMail,
    }

    public class EventListenerManager : NTBehaviour
    {
        public NTDictionary<NTDictionary<Action<object>>> ActionsDictionary = new NTDictionary<NTDictionary<Action<object>>>();

        public static EventListenerManager instance;
        protected override void Awake()
        {
            base.Awake();
            if (EventListenerManager.instance != null)
            {
                Debug.LogWarning("Only 1 instance allow");
                return;
            }
            EventListenerManager.instance = this;
        }

        public void PostEvent(EventCode eventCode,object data = null){
            NTDictionary<Action<object>> actions = this.ActionsDictionary.Get(eventCode.ToString());
            if(actions == null || actions.Dictionary.Count == 0) return;
            foreach (String item in actions.Dictionary.Keys.ToList())
            {
                try
                {
                    actions.Dictionary[item].Invoke(data);
                }
                catch (System.Exception)
                {
                    actions.Remove(item);
                }
            }
        }

        public void PostEvent(EventCode eventCode, string key, object data = null){
            NTDictionary<Action<object>> actions = this.ActionsDictionary.Get(eventCode.ToString());
            if(actions == null || actions.Dictionary.Count == 0) return;
            try
            {
                actions.Dictionary[key].Invoke(data);
            }
            catch (System.Exception)
            {
                actions.Remove(key);
            }
        }

        public void Register(EventCode eventCode, string key,Action<object> callback){
            
            NTDictionary< Action<object>> actions = this.ActionsDictionary.Get(eventCode.ToString());
            if(actions == null){
                actions = new NTDictionary<Action<object>>();
                this.ActionsDictionary.Add(eventCode.ToString(), actions);
            }
            actions.Add(key, callback);
        }
    }
}

