using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AAAShare.BsModules.Param;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AAAShare.BsModules
{
    public enum MissionState
    {
        INACTIVATED, //未激活
        RUNNING, //进行中
        OVER, //结束。
    }

    public enum MissionManagerState
    {
        INACTIVATED, //未激活
        RUNNING, //运行中
        PAUSE, //暂停
        OVER, //结束
    }

    public enum MissionManagerRunMode
    {
        NORMAL, //正常
        AUTO, //自动
        QUICK, //快进
        BACK, //回退
    }

    public static class MissionConst
    {
        public static IEnumerable GetAllParams()
        {
            var list = GetSubClass(typeof(IMissionParam));
            var list2 = new ValueDropdownList<IMissionParam>();
            var index = 0;
            foreach (var type in list)
            {
                index++;
                var instance = (IMissionParam)Activator.CreateInstance(type);
                list2.Add(new ValueDropdownItem<IMissionParam>(instance.Des, instance));
            }

            return list2;
        }

        private static List<Type> GetSubClass(Type superClassType)
        {
            List<Type> list = new List<Type>();
            Assembly a = Assembly.GetAssembly(superClassType);
            foreach (Type t in a.GetTypes())
            {
                //是否是類
                if (t.IsClass)
                {
                    //是否是當前類的派生類
                    if (t.IsSubclassOf(superClassType))
                    {
                        list.Add(t);
                    }
                    else if (superClassType.IsAssignableFrom(t))
                    {
                        list.Add(t);
                    }
                }
            }

            return list;
        }
    }
}