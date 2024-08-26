using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XuanJingPian : MonoBehaviour
{
    /// <summary>
    /// 所有黑色镜片按钮
    /// </summary>
    [SerializeField]
    private List<Transform> jingpianButtons_heise;

    /// <summary>
    /// 所有黑色镜片Text0(当前值)
    /// </summary>
    [SerializeField]
    private List<Transform> jingpianButtons_text_heise0;

    /// <summary>
    ///  所有黑色镜片Text1(?)
    /// </summary>
    [SerializeField]
    private List<Transform> jingpianButtons_text_heise1;

    public void Dianjiheisejingpian1()
    {
        jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.RemoveAllListeners();
            JingPianXuanZhong(jingpianButtons_heise[0]).Play();
        });
    }

    private Tween JingPianXuanZhong(Transform obj)
    {
        var tweens = DOTween.Sequence();
        var tween = obj.GetChild(0).transform.DOLocalMoveY(40, 0.3f);
        tweens.Insert(0, tween);
        return tweens;
    }

}
