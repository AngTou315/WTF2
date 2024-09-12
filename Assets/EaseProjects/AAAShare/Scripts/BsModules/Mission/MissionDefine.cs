using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AAAShare.BsModules.Param;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
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

    //任务参数的管理
    //MissionConst它的主要作用是提供任务参数的所有子类，并将这些子类实例作为选项返回。
    public static class MissionConst
    {
        //这个方法用于获取所有IMissionParam接口的子类，
        //并返回一个包含这些子类实例的下拉列表 (ValueDropdownList)，供任务参数选择使用。
        public static IEnumerable GetAllParams()
        {
            var list = GetSubClass(typeof(IMissionParam));
            
            var list2 = new ValueDropdownList<IMissionParam>();
            //这是一个特定于 奥丁插件 的下拉列表类，用于展示给用户的可选项。
            foreach (var type in list)
            {
                var instance = (IMissionParam)Activator.CreateInstance(type);
                //Activator.CreateInstance(type)使用反射来创建每个子类的实例。
                list2.Add(new ValueDropdownItem<IMissionParam>(instance.Des, instance));
                //每个实例的描述信息 (instance.Des) 和实例对象都会被添加到下拉列表中。
            }
            return list2;
        }

        //通过这个方法获取实现了传递进来IMissionParam接口的所有子类类型。
        private static List<Type> GetSubClass(Type superClassType)
        {
            List<Type> list = new List<Type>();
            Assembly a = Assembly.GetAssembly(superClassType);
            //获取 IMissionParam 接口所在的程序集。
            foreach (Type t in a.GetTypes())
            {
                //是否是类
                if (t.IsClass)
                {
                    //是否是superClassType类的派生类
                    if (t.IsSubclassOf(superClassType))
                    {
                        list.Add(t);
                    }
                    else if (superClassType.IsAssignableFrom(t))//或实现了这个接口superClassType
                    {
                        list.Add(t);
                    }
                }
            }
            return list;
            //最终返回一个包含所有符合条件的子类类型的列表。
        }
    }
}