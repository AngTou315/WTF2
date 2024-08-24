using System;
using System.Collections;
using System.Collections.Generic;
using AAAShare.Adapter;
using Ease.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AAAShare.BsPublic
{
    public class UITipWindowParam : BaseWindowParam
    {
        public string title;
        public string content;
        public Action callback1;
        public Action callback2;
    }

    public class UITip : BaseUILogic
    {
        public TextMeshProUGUI textTitle;
        public TextMeshProUGUI textContent;
        public Button btn1;
        public Button btn2;
        private Action btn1CallBack;
        private Action btn2CallBack;
        private UITipWindowParam uiTipWindowParam;


        protected override void OnOpen()
        {
            uiTipWindowParam = param as UITipWindowParam;
            Open(uiTipWindowParam.title, uiTipWindowParam.content, uiTipWindowParam.callback1, uiTipWindowParam.callback2);
            btn1.onClick.AddListener(OnClickBtn1);
            btn2.onClick.AddListener(OnClickBtn2);
        }

        protected override void OnClose()
        {
            btn1.onClick.RemoveListener(OnClickBtn1);
            btn2.onClick.RemoveListener(OnClickBtn2);
            uiTipWindowParam = null;
        }


        #region Private

        private void Open(string title, string content, Action callback1, Action callback2)
        {
            textTitle.text = title;
            textContent.text = content;
            btn1CallBack = callback1;
            btn2CallBack = callback2;
            if (btn2CallBack == null)
                btn2.gameObject.SetActive(false);
            else
                btn2.gameObject.SetActive(true);
        }

        private void OnClickBtn1()
        {
            btn1CallBack?.Invoke();
        }

        private void OnClickBtn2()
        {
            btn2CallBack?.Invoke();
        }

        #endregion
    }
}