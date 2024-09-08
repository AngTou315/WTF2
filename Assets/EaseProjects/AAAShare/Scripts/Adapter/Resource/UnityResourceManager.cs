using System;
using System.Collections;
using System.Collections.Generic;
using Ease.Resource;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AAAShare.Adapter//这是一个特定的适配器，专门用于适配 Unity 资源管理的机制。
{
    //这个类是用于存储单个资源请求的信息。包含以下字段
    public class RequstInfo
    {
        public string path;
        public ResourceRequest resourceRequst;
        public Action start;//在资源开始加载时执行的回调
        public Action<Object> end;
        public Action<float> progress;
        //在资源加载过程中执行的回调，传入加载进度（float）
        public void Reset()
        {
            path = null;
            resourceRequst = null;
            start = null;
            end = null;
            progress = null;
        }
    }

    /// <summary>
    /// 基于Unity自带的Resource实现。
    /// </summary>
    public class UnityResourceManager : IResourceManager
    {
        public Dictionary<string, RequstInfo> dic = new Dictionary<string, RequstInfo>();
        //用于存储所有当前正在加载的资源请求信息，键为资源路径，值为对应请求信息。

        public Object Load(string path)
        {
            return Resources.Load(path);
            //该方法用于同步加载资源
        }

        public void LoadAsync(string path, Action start, Action<Object> end, Action<float> progress)
        {
            //如果当前资源路径已经在dic字典中存在（意味着正在异步加载），
            //则会将传入的回调绑定到现有的RequstInfo对象上，并立即调用start回调。
            if (dic.TryGetValue(path, out var info))
            {
                start?.Invoke();
                if (start != null)
                    info.start += start;
                if (end != null)
                    info.end += end;
                if (progress != null)
                    info.progress += progress;
                return;
            }
            //如果是新的资源请求，则创建一个新的ResourceRequest对象，开始异步加载资源，
            //并将该请求相关的信息（包括这些回调）存储到RequstInfo对象中，加入到dic字典中。
            var request = Resources.LoadAsync(path);
            start?.Invoke();
            var requstInfo = new RequstInfo() { path = path, resourceRequst = request };
            if (start != null)
                requstInfo.start += start;
            if (end != null)
                requstInfo.end += end;
            if (progress != null)
                requstInfo.progress += progress;
            dic.Add(path, requstInfo);
        }
        
        public void OnUpdate(float time, float realtime)
        {
            List<string> DoneList = new List<string>();
            foreach (var item in dic)//每帧检查资源加载的状态。它遍历所有正在加载的资源请求
            {
                if (item.Value.resourceRequst.isDone) //如果资源加载完成（isDone为true），调用end回调
                {
                    item.Value.end?.Invoke(item.Value.resourceRequst.asset);
                    item.Value.Reset();
                    DoneList.Add(item.Key);//定义一个变量表来存储加载完的资源，在最后从字典删除
                }
                else
                {
                    //如果资源还在加载中，调用progress回调，传递当前的加载进度。
                    item.Value.progress?.Invoke(item.Value.resourceRequst.progress);
                }
            }

            foreach (var item in DoneList)
            {
                dic.Remove(item);
            }

            DoneList.Clear();
        }
        //关闭管理器时调用，清除所有未完成的资源请求，
        //并调用每个RequstInfo的 Reset() 方法清理资源和回调。
        public void OnClose()
        {
            foreach (var keyValuePair in dic)
            {
                keyValuePair.Value.Reset();
            }
            dic.Clear();
        }
    }
}