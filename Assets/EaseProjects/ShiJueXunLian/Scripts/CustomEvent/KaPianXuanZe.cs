using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using AAAShare.BsPublic;
using Ease.UI;
using UnityEditor.Experimental.GraphView;

public class KaPianXuanZe : MonoBehaviour
{
    /// 光线节点
    [SerializeField]
    private Button trueBtn;
    /// 位子物体
    [SerializeField]
    private Button falseBtn1;
    /// 镜片
    [SerializeField]
    private Button falseBtn2;
    
    public void DianJiTrueBtn(Action OnOVer)
    {
        trueBtn.onClick.AddListener(() =>
        {
            trueBtn.onClick.RemoveAllListeners();
            OnOVer?.Invoke();
            falseBtn1.onClick.RemoveAllListeners();
            falseBtn2.onClick.RemoveAllListeners();
            falseBtn1.gameObject.SetActive(false);
            falseBtn2.gameObject.SetActive(false);
            trueBtn.gameObject.SetActive(false);
        });
        falseBtn1.onClick.AddListener(ShowTip);
        falseBtn2.onClick.AddListener(ShowTip);
    }
    private void ShowTip()
    {
        var param = new UITipWindowParam();
        param.title = "提示";
        param.content = "回答错误,为了提高训练效率,应该优先选择20/30的字母卡,如果患者视觉非常弱,看不清楚时可适当降低难度,选择20/40或20/50的卡片。";
        param.callback1 = (()=>{ Entry.GetModule<IUIManager>().CloseWindow("UITip"); });
        Entry.GetModule<IUIManager>().OpenWindow("UITip", param);
    }
}
