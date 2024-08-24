using System;
using System.Collections.Generic;
using Ease.Config;
using Ease.Core;
using Ease.Event;
using Ease.Procedure;
using Ease.Resource;
using Ease.Scene;
using Ease.Sound;
using Ease.UI;
using Ease.Version;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAAShare.BsPublic
{
    public partial class Entry : MonoBehaviour
    {
        protected virtual void Start()
        {
            DontDestroyOnLoad(this);
            InitCommon();
        }

        protected virtual void Update()
        {
            ModuleManager.OnUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        protected virtual void OnDestroy()
        {
            ResetCommon();
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

        private void InitCommon()
        {
            Ease.Logger.PrefixLog = "EASE";
            Ease.Logger.PrefixError = "EASE";
            Ease.Logger.PrefixWarning = "EASE";
            Ease.Logger.eventLog += Debug.Log;
            Ease.Logger.eventLogError += Debug.LogError;
            Ease.Logger.eventLogWarning += Debug.LogWarning;
        }

        private void ResetCommon()
        {
            Ease.Logger.Reset();
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