using System;
using System.Collections.Generic;
using Ease.Config;
using Ease.Core;
using Ease.Event;
using Ease.Procedure;
using Ease.Resource;
using Ease.Scene;
using Ease.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAAShare.BsPublic
{
    public class Entry : MonoBehaviour
    {
        protected virtual void Start()
        {
            DontDestroyOnLoad(this);
        }

        protected virtual void Update()
        {
            ModuleManager.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        protected virtual void OnDestroy()
        {
            ModuleManager.OnClose();
        }

        protected virtual void OnApplicationQuit()
        {
            Destroy(this);
        }

        public static void SetModule<T>(IModule module) where T : class
        {
            ModuleManager.SetModule<T>(module);
        }

        public static T GetModule<T>() where T : class
        {
            return ModuleManager.GetModule<T>();
        }

#if UNITY_EDITOR
        [SerializeField][ReadOnly] private List<string> modules = new List<string>();
        [Button]
        private void RefreshModules()
        {
            if (Application.isPlaying)
            {
                modules.Clear();
                foreach (var keyValuePair in ModuleManager.Modules)
                {
                    var str = $"[{keyValuePair.Key.Name}]:{keyValuePair.Value.GetType().FullName}";
                    modules.Add(str);
                }
            }
        }
#endif
    }
}