using System;
using Ease.Core;
using UnityEngine.SceneManagement;

namespace Ease.Scene
{
    public interface ISceneManager : IModule,ILife
    {
        void ChangeScene(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
        void ChangeScene(int index, LoadSceneMode loadSceneMode = LoadSceneMode.Single);
        void ChangeSceneAsync(string name, LoadSceneMode loadSceneMode, Action start, Action end, Action<float> progress);
        void ChangeSceneAsync(int index, LoadSceneMode loadSceneMode, Action start, Action end, Action<float> progress);
    }
}