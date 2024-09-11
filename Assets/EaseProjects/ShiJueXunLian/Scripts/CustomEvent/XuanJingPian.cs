using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class XuanJingPian : MonoBehaviour
{
    /// <summary>
    /// ���к�ɫ��Ƭ��ť
    /// </summary>
    [SerializeField]
    private List<Transform> jingpianButtons_heise;

    /// <summary>
    /// ���к�ɫ��ƬText0(��ǰֵ)
    /// </summary>
    [SerializeField]
    private List<Transform> jingButtons_text_heise_known;

    /// <summary>
    ///  ���к�ɫ��ƬText1(?)
    /// </summary>
    [SerializeField]
    private List<Transform> jingButtons_text_heise_Unknown;
    
    /// <summary>
    /// ��Ƭ��ʼλ��
    /// </summary>
    [SerializeField]
    private Transform kapianstartPoint;

    /// <summary>
    /// ��Ƭ�Ƴ�λ��
    /// </summary>
    [SerializeField]
    private Transform kapianendPoint;
    
    /// <summary>
    /// ���п�Ƭ��ʾλ��(��ɫ)
    /// </summary>
    [SerializeField]
    private List<Transform> kapianPoint1_hongse;

    /// <summary>
    /// ���п�Ƭ��ʾλ��(��ɫ)
    /// </summary>
    [SerializeField]
    private List<Transform> kapianPoint1_heise;

    public void heiseJingPianXianShi(Action OnOVer)
    {
        for (int i = 0; i < jingpianButtons_heise.Count; i++)
        {
            jingpianButtons_heise[i].transform.localPosition = kapianstartPoint.localPosition;
            jingpianButtons_heise[i].transform.GetComponent<Button>().onClick.RemoveAllListeners();
        }
        for (int i = 0; i < jingpianButtons_heise.Count; i++)
        {
            jingpianButtons_heise[i].gameObject.SetActive(true);
        }
        JingPianXianShi_Heise().Play().OnComplete(() =>
        {
            OnOVer?.Invoke();
        });
    }
    public void XuanZhong_Hei_1(Action OnOVer)
    {
        jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.RemoveAllListeners();
            JingPianXuanZhong(jingpianButtons_heise[0]).Play();
            OnOVer?.Invoke();
        });
    }
    public void XuanZhong_Hei_2(Action OnOVer)
    {
        jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.RemoveAllListeners();
            JingPianXuanZhong(jingpianButtons_heise[0]).Play();
            OnOVer?.Invoke();
        });
    }

    //ѡ�о�Ƭ����
    private Tween JingPianXuanZhong(Transform obj)
    {
        var tweens = DOTween.Sequence();
        var tween = obj.GetChild(0).transform.DOLocalMoveY(40, 0.3f);
        tweens.Insert(0, tween);
        return tweens;
    }
    
    /// ��ɫ��Ƭ������ʾ����
    private Tween JingPianXianShi_Heise()
    {
        var tweens = DOTween.Sequence();
        var tween = jingpianButtons_heise[0].transform.DOMove(kapianPoint1_heise[0].position,0.3f);
        tweens.Insert(0, tween);
        tween = jingpianButtons_heise[1].transform.DOMove(kapianPoint1_heise[1].position, 0.3f);
        tweens.Insert(0.1f, tween);
        tween = jingpianButtons_heise[2].transform.DOMove(kapianPoint1_heise[2].position, 0.3f);
        tweens.Insert(0.2f, tween);
        tween = jingpianButtons_heise[3].transform.DOMove(kapianPoint1_heise[3].position, 0.3f);
        tweens.Insert(0.3f, tween);
        tween = jingpianButtons_heise[4].transform.DOMove(kapianPoint1_heise[4].position, 0.3f);
        tweens.Insert(0.4f, tween);
        return tweens;
    }

    
    
}
