using System;
using System.Collections.Generic;
using System.Linq;
using Ease.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace AAAShare.Adapter
{
    public enum WindowLayer
    {
        TIP, //框架提示层
        LOADING, //框架加载
        TOP, //业务顶
        MID, //业务中
        BOTTOM, //业务底
    }

    [Serializable]
    public class DefaultWindowConfig : BaseWindowConfig
    {
        //层
        public WindowLayer Layer;

        //预制体。
        public GameObject prefab;
    }

    public class DefaultUIManager : MonoBehaviour, IUIManager
    {
        //配置表；
        public List<DefaultWindowConfig> configs = new List<DefaultWindowConfig>();

        //层
        public List<Transform> layers = new List<Transform>();

        //打开的UI;
        public List<BaseUILogic> uiList = new List<BaseUILogic>();

        //param缓存;
        public Dictionary<string, BaseWindowParam> dicParam = new Dictionary<string, BaseWindowParam>();

        #region UNITY

        private void Awake()
        {
            layers.Clear();
            var canvas = GetComponentsInChildren<Canvas>();
            for (int i = 0; i < canvas.Length; i++)
                layers.Add(canvas[i].transform);
        }

        #endregion

        public void OnUpdate(float time, float realtime)
        {
        }

        public void OnClose()
        {
        }

        public void OpenWindow(string windowName, BaseWindowParam param)
        {
            if (uiList.FirstOrDefault(x => x.config.name.Equals(windowName)) != null)
                return;
            //找到配置表。
            var config = configs.FirstOrDefault(x => x.name == windowName);
            //保存参数
            dicParam.Remove(config.name);
            dicParam.Add(config.name, param);
            //根据配置表生成GameObject;
            var uiGo = Instantiate(config.prefab, layers[(int)config.Layer]);
            var uiLogic = uiGo.GetComponent<BaseUILogic>();
            uiLogic.gameObject.SetActive(true);
            uiLogic.enabled = true;
            uiLogic.config = config;
            uiLogic.param = param;
            uiLogic.Open();
            uiList.Add(uiLogic);
        }

        public void CloseWindow(string windowName)
        {
            //找到配置表。
            var config = configs.FirstOrDefault(x => x.name == windowName);
            //删除UI;
            var ui = uiList.FirstOrDefault(x => x.config.name.Equals(windowName));
            ui.Close();
            uiList.Remove(ui);
            if (ui != null)
            {
                Destroy(ui.gameObject);
            }
        }

        public void CloseAllWindow()
        {
            //关闭所有UI;
            foreach (var baseUILogic in uiList)
            {
                baseUILogic.Close();
                Destroy(baseUILogic.gameObject);
            }
        }
    }
}