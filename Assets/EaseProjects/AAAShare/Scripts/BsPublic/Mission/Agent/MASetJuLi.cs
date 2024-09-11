using System;
using AAAShare.BsModules;
using AAAShare.BsPublic.Event;
using AAAShare.BsPublic.Param;
using Ease.Event;
using Ease.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace AAAShare.BsPublic.Agent
{
    public class MASetJuLi : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;
        private float dangqianfloat;

        public void OnEnable()
        {
            dangqianfloat = 30;
            var param = Data.Param as MPSetJuLi;
            param.trans.SetActive(true);
            param.queDing_Button.onClick.AddListener(() =>
            {
                if (Mathf.Abs(dangqianfloat - 40) < 2)
                {
                    param.trans.SetActive(false);
                    param.shilika3D20_30.localPosition = new Vector3(param.shilika3D20_30.localPosition.x, param.shilika3D20_30.localPosition.y, 0.4f);
                    dangqianfloat = 40;
                    param.dangqian_cm.text = dangqianfloat.ToString("F2") + "cm";
                    OnOVer?.Invoke();
                }
                else
                {
                    Debug.LogError("值不对啊");
                }
            });
            param.slider.value = 0.3f;
            param.slider.onValueChanged.AddListener((value) =>
            {
                Debug.Log(value);
                param.shilika3D20_30.localPosition = new Vector3(param.shilika3D20_30.localPosition.x, param.shilika3D20_30.localPosition.y, value);
                dangqianfloat = value * 100;
                RectTransform rectran =  param.juLiTiShi.transform as RectTransform;
                rectran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dangqianfloat * 10);
                param.dangqian_cm.text = dangqianfloat.ToString("F2") + "cm";
            });
        }

        public void OnDisable()
        {
            var param = Data.Param as MPSetJuLi;
            param.queDing_Button.onClick.RemoveAllListeners();
            param.slider.onValueChanged.RemoveAllListeners();
        }

        public void OnUpdate()
        {
        }
    }
}