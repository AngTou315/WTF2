using System;
using System.Collections.Generic;

namespace Ease.Core
{
    public static class ModuleManager
    {
        //这里，Type 用作字典的键,建是唯一的嘛，也就确保每种模块只会有一个实例，保证了模块的唯一性。
        public static Dictionary<Type, IModule> Modules = new Dictionary<Type, IModule>();

        public static void AddModule<T>(IModule module) where T : class
            //这是一个泛型类型参数，允许该方法处理不同类型的模块。而后面的where约束了这个类型
            //class 引用类型 类，接口，数组，委托 和 字符串
        {
            if (!Modules.ContainsKey(typeof(T)))
            {
                Modules.Add(typeof(T), module);
            }
            else
            {
                throw new Exception("模块重复");
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
            //检查是否实现了接口，如果检查成功，则 life 会被赋值为 keyValuePair.Value 的 ILife 接口类型的引用。
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