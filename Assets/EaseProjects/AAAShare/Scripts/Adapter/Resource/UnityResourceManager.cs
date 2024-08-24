using System;
using System.Collections;
using System.Collections.Generic;
using Ease.Resource;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AAAShare.Adapter
{
    public class RequstInfo
    {
        public string path;
        public ResourceRequest resourceRequst;
        public Action start;
        public Action<Object> end;
        public Action<float> progress;

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

        public Object Load(string path)
        {
            return Resources.Load(path);
        }

        public void LoadAsync(string path, Action start, Action<Object> end, Action<float> progress)
        {
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
            foreach (var item in dic)
            {
                if (item.Value.resourceRequst.isDone)
                {
                    item.Value.end?.Invoke(item.Value.resourceRequst.asset);
                    item.Value.Reset();
                    DoneList.Add(item.Key);
                }
                else
                {
                    item.Value.progress?.Invoke(item.Value.resourceRequst.progress);
                }
            }

            foreach (var item in DoneList)
            {
                dic.Remove(item);
            }

            DoneList.Clear();
        }
        
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