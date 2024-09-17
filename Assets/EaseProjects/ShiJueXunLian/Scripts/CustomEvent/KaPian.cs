using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
using Unity.VisualScripting;

public class KaPian : MonoBehaviour
{
    ///格子
    [SerializeField]
    private List<Transform> gezi;
    ///环
    [SerializeField]
    private List<Image> biaoJi0;
    ///勾
    [SerializeField]
    private List<Image> biaoJi1;
    ///镜片父物体
    [SerializeField]
    private Transform jingpian;
    /// 跳过按钮
    [SerializeField]
    private Button tiaoguo;
    
    public void Show_GuanChaJingPian_Hei(Action OnOVer)
    {
        XianShiJingPian_HeiSe();
        OnOVer?.Invoke();
    }
    public void YueDuKaPian_05_0(Action OnOVer)
    {
        var tween1 = ChanChaPlayAll( 0.9f).Play().OnComplete(()=>
        {
            OnOVer?.Invoke();
        });
        tiaoguo.gameObject.SetActive(true); //显示跳过按钮
        tiaoguo.onClick.AddListener(() =>
        {
            tiaoguo.onClick.RemoveAllListeners();
            tiaoguo.gameObject.SetActive(false); //关闭跳过按钮
            tween1.Pause();
            Tiaoguo();
            tween1.Kill();
            jingpian.gameObject.SetActive(false);
            jingpian.localPosition = gezi[0].localPosition;
            OnOVer?.Invoke();
        });
    }
    public void YueDuKaPian_1_0(Action OnOVer)
    {
        var tween1 = ChanChaPlayAll(2f).Play().OnComplete(() =>
        {
            OnOVer?.Invoke();
        });
        tiaoguo.gameObject.SetActive(true);
        tiaoguo.onClick.AddListener(() =>
        {
            tiaoguo.onClick.RemoveAllListeners();
            tiaoguo.gameObject.SetActive(false);
            tween1.Pause();
            Tiaoguo();
            tween1.Kill();
            jingpian.gameObject.SetActive(false);
            jingpian.localPosition = gezi[0].localPosition;
            OnOVer?.Invoke();
        });
    }
    public void RestKaPianBiaoJi(Action OnOVer)
    {
        ResetBiaoJi();
        OnOVer?.Invoke();
    }
    /// <summary>
    /// 卡片上显示黑色放大镜
    /// </summary>
    public void XianShiJingPian_HeiSe()
    {
        jingpian.GetChild(0).gameObject.SetActive(true);
        jingpian.GetChild(1).gameObject.SetActive(false);
        jingpian.gameObject.SetActive(true);
        jingpian.localPosition = gezi[0].localPosition;
    }
    //阅读卡片格子动画
    public Tween GuanChaPlay(int index,float timer)
    {
        var tweens = DOTween.Sequence();
        Tween tween;
        tween = jingpian.DOLocalMove(gezi[index].localPosition,0.3f);
        tweens.Append(tween);
        tween = DOTween.To(() => biaoJi0[index].fillAmount, x => biaoJi0[index].fillAmount = x, 1, timer);
        tweens.Append(tween);
        tween = DOTween.To(() => biaoJi1[index].fillAmount, x => biaoJi1[index].fillAmount = x, 1, 0.3f);
        tweens.Append(tween);
        tween = DOTween.To(() => biaoJi0[index].fillAmount, x => biaoJi0[index].fillAmount = x, 0, 0);
        tweens.Append(tween);
        return tweens;
    }
    //阅读所有卡片格子动画
    private Tween ChanChaPlayAll(float timer)
    {
        var tweens = DOTween.Sequence();
        Tween tween;
        for (int i = 0; i < 40; i++)
        {
            tween = GuanChaPlay(i, timer);
            tweens.Append(tween);
        }
        return tweens;
    }
    private void Tiaoguo()
    {
        for (int i = 0; i < 40; i++)
        {
            biaoJi0[i].fillAmount = 0;
            biaoJi1[i].fillAmount = 1;
        }
    }
    //刷新完成标记
    private void ResetBiaoJi()
    {
        for (int i = 0; i < 40; i++)
        {
            biaoJi1[i].fillAmount = 0;
        }
    }
}
