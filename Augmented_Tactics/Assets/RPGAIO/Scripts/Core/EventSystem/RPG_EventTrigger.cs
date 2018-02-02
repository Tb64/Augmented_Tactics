using System;
using System.Linq;
using System.Reflection;
using LogicSpawn.RPGMaker.Core;
using LogicSpawn.RPGMaker.Generic;
using UnityEngine;

namespace LogicSpawn.RPGMaker.Core
{
    public class RPG_EventTrigger: MonoBehaviour
    {
        //todo: custom inspector links to EventNodeBank events
        public string EventID;
        public InteractType InteractType;
        public float Distance;
        private Transform _myTransform;
        private bool triggerHandled = false;
        public bool AllowRetrigger = false;

        void OnEnable()
        {
            _myTransform = transform;
        }

        void OnMouseDown()
        {
            if (triggerHandled) return;

            if(InteractType == InteractType.Click && !triggerHandled)
            {
                PerformEvent();
                if (!AllowRetrigger) triggerHandled = true;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (triggerHandled) return;
            if (InteractType == InteractType.Collide)
            {
                if (other.CompareTag("Player"))
                {
                    PerformEvent();
                    if (!AllowRetrigger) triggerHandled = true;
                }
            }
        }

        void Update()
        {
            if(!triggerHandled && InteractType == InteractType.NearTo)
            {
                if(Vector3.Distance(_myTransform.position, GetObject.PlayerMono.transform.position) < Distance)
                {
                    PerformEvent();
                    if(!AllowRetrigger) triggerHandled = true;
                }
            }
        }

        void PerformEvent()
        {
            var eventRun = GetObject.EventHandler.RunEvent(EventID);
        }
    }
}