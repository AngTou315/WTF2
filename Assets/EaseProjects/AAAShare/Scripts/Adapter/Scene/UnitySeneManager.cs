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
        //用于处理异步操作的类。它通常用于管理和跟踪异步任务的状态
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
            loadingInfo.index = -1;//意味着它不在当前加载的场景中。
            loadingInfo.name = name;
            loadingInfo.asyncOperation = operation;
            loadingInfo.start = start;
            loadingInfo.end = end;
            loadingInfo.progress = progress;
            LoadingInfos.Add(loadingInfo);
        }
        //该方法用于启动异步场景加载操作。通过传入场景索引、加载模式以及开始、结束、进度的回调函数，
        //来处理场景加载的不同状态。场景加载的状态被保存在 SceneLoadingInfo 类的实例中。
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
        //该方法负责定期检查所有正在加载的场景的进度。如果场景加载完成，它将调用 end 回调，并将场景从正在加载的列表中移除。
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
        // 当管理器关闭时，会清理所有未完成的加载任务，并重置所有场景加载信息。
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