using System;
using System.Collections.Generic;

namespace Ease.Core
{
    public static class ModuleManager
    {
        public static Dictionary<Type, IModule> Modules = new Dictionary<Type, IModule>();

        public static void SetModule<T>(IModule module) where T : class
        {
            if (!Modules.ContainsKey(typeof(T)))
            {
                Modules.Add(typeof(T), module);
            }
            else
            {
                throw new Exception($"ModuleManager:SetModule Repetitive");
            }
        }

        public static T GetModule<T>() where T : class
        {
            if (Modules.TryGetValue(typeof(T), out var module))
            {
                return (T)module;
            }

            return null;
        }

        public static bool RemoveModule<T>() where T : class
        {
            var type = typeof(T);
            if (Modules.ContainsKey(type))
            {
                Modules.Remove(type);
                return true;
            }

            return false;
        }

        #region 生命周期

        public static void OnUpdate(float time, float realtime)
        {
            foreach (var keyValuePair in Modules)
            {
                if (keyValuePair.Value is ILife life)
                    life.OnUpdate(time, realtime);
            }
        }

        public static void OnClose()
        {
            foreach (var keyValuePair in Modules)
            {
                if (keyValuePair.Value is ILife life)
                    life.OnClose();
            }

            Modules.Clear();
        }

        #endregion
    }
}