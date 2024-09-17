using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class YanJing : MonoBehaviour
{
    /// 光线节点
    [SerializeField]
    private List<Transform> joints;
    /// 位子物体
    [SerializeField]
    private List<Transform> points;
    /// 镜片
    [SerializeField]
    private Transform jingpian;
    /// 镜片位子0
    [SerializeField]
    public Transform jingPian_point0;
    /// 镜片位子1
    [SerializeField]
    public Transform jingPian_point1;
    ///玻璃体
    [SerializeField]
    private Transform boliti;
    
    public void JingPianShow(Action OnOVer)
    {
        jingpian.gameObject.SetActive(true);
        jingpian.transform.position = jingPian_point0.position;
        JingPian3DXianShi(jingPian_point1, 1, 0.3f).Play();
        OnOVer?.Invoke();
    }
    
    private Tween JingPian3DXianShi(Transform point,float scal, float timer)
    {
        var tweens = DOTween.Sequence();
        var tween = jingpian.DOMove(point.position,timer);
        tweens.Insert(0,tween);
        tween = jingpian.DOScale(scal, timer);
        tweens.Insert(0, tween);
        return tweens;
    }
}
