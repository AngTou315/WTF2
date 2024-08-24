using System;
using System.Collections.Generic;
using System.Linq;
using Ease.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AAAShare.Adapter
{
    public class SceneLoadingInfo
    {
        public int index;
        public string name;
        public LoadSceneMode loadSceneMode;
        public AsyncOperation asyncOperation;
        public Action start;
        public Action end;
        public Action<float> progress;

        public void Reset()
        {
            index = -1;
            name = "";
            loadSceneMode = default;
            asyncOperation = null;
            start = null;
            end = null;
            progress = null;
        }
    }

    public class UnitySeneManager : ISceneManager
    {
        public List<SceneLoadingInfo> LoadingInfos = new List<SceneLoadingInfo>();

        public void ChangeScene(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name, loadSceneMode);
        }

        public void ChangeScene(int index, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(index, loadSceneMode);
        }

        public void ChangeSceneAsync(string name, LoadSceneMode loadSceneMode, Action start, Action end, Action<float> progress)
        {
            if (LoadingInfos.FirstOrDefault(x => x.name == name) != null)
                return;
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, loadSceneMode);
            start?.Invoke();
            SceneLoadingInfo loadingInfo = new SceneLoadingInfo();
            loadingInfo.index = -1;
            loadingInfo.name = name;
            loadingInfo.asyncOperation = operation;
            loadingInfo.start = start;
            loadingInfo.end = end;
            loadingInfo.progress = progress;
            LoadingInfos.Add(loadingInfo);
        }

        public void ChangeSceneAsync(int index, LoadSceneMode loadSceneMode, Action start, Action end, Action<float> progress)
        {
            if (LoadingInfos.FirstOrDefault(x => x.index == index) != null)
                return;
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index, loadSceneMode);
            start?.Invoke();
            SceneLoadingInfo loadingInfo = new SceneLoadingInfo();
            loadingInfo.index = index;
            loadingInfo.name = $"Scene_{index}";
            loadingInfo.asyncOperation = operation;
            loadingInfo.start = start;
            loadingInfo.end = end;
            loadingInfo.progress = progress;
            LoadingInfos.Add(loadingInfo);
        }

        public void OnUpdate(float time, float realtime)
        {
            List<string> doneList = new List<string>();
            foreach (var sceneLoadingInfo in LoadingInfos)
            {
                sceneLoadingInfo.progress?.Invoke(sceneLoadingInfo.asyncOperation.progress);
                if (sceneLoadingInfo.asyncOperation.isDone)
                {
                    sceneLoadingInfo.end?.Invoke();
                    sceneLoadingInfo.Reset();
                    doneList.Add(sceneLoadingInfo.name);
                }
            }

            //移除完成的异步操作。
            LoadingInfos.RemoveAll(x => doneList.Contains(x.name));
            doneList.Clear();
        }

        public void OnClose()
        {
            foreach (var sceneLoadingInfo in LoadingInfos)
            {
                sceneLoadingInfo.Reset();
            }

            LoadingInfos.Clear();
        }
    }
}