﻿using System.Text.RegularExpressions;
using UnityEngine;

namespace EventVisualizer.Base
{
    [System.Serializable]
    public class EventCall
    {
        public Object Sender { get; private set; }
        public Object Receiver { get; private set; }
        public string EventName { get; private set; }
        public string Method { get; private set; }
        public string ReceiverComponentName { get; private set; }

        public string MethodFullPath
        {
            get
            {
                return ReceiverComponentName + Method;
            }
        }

        private static Regex parenteshesPattern = new Regex(@"\((.*)\)");

        public EventCall(Object sender, Object receiver, string eventName, string method)
        {
            Sender = sender as Component ? (sender as Component).gameObject : sender ;
            Receiver = receiver as Component ? (receiver as Component).gameObject : receiver;
            EventName = eventName;
            Method =  method;

            UpdateReceiverComponentName(receiver);
        }

        private void UpdateReceiverComponentName(Object component)
        {
            if (Receiver != null)
            {
                MatchCollection matches = parenteshesPattern.Matches(component.ToString());
                if (matches != null && matches.Count > 0)
                {
                    ReceiverComponentName = matches[matches.Count - 1].Value;
                    if(ReceiverComponentName.Length > 1)
                    {
                        ReceiverComponentName = ReceiverComponentName.Remove(0,1);
                    }
                    ReceiverComponentName = ReceiverComponentName.Replace(")", ".");
                }
            }
        }
    }
}