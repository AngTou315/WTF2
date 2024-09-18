using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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
    private List<Transform> jingpianButtons_text_heise0;

    /// <summary>
    ///  ���к�ɫ��ƬText1(?)
    /// </summary>
    [SerializeField]
    private List<Transform> jingpianButtons_text_heise1;
    
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
    private List<Transform> kapianPoint1_heise;

    #region ����
    /// <summary>
    /// ����Ƭλ����
    /// </summary>s
    [SerializeField]
    private Transform daPoint;
    /// <summary>
    /// ����Ƭλ��(��)
    /// </summary>
    [SerializeField]
    private Transform xiaoPoint;
    //
    [SerializeField]
    private Transform paixu;
    /// <summary>
    /// ���ں�
    /// </summary>
    [SerializeField]
    private Button dayuhao;
    [SerializeField]
    private Image dui;

    [SerializeField]
    private Image cuo_0;

    [SerializeField]
    private Image cuo_1;
    /// <summary>
    /// ���ںŻ���С�ں�(������ںŰ�ť����:true���ں�  false С�ں�)
    /// </summary>
    private bool dayuOrxiaoyu;
    /// <summary>
    /// ����ȷ����ť
    /// </summary>
    [SerializeField]
    private Transform queding_Button;
    [SerializeField]
    private YanJing yanJing;
    [SerializeField]
    private KaPian kapian2DUI;
    #endregion
    

    //��ɫ2d��Ƭ��ť��ʾ�����м�λ��
    public void heiseJingPianXianShi(Action OnOVer)
    {
        for (int i = 0; i < jingpianButtons_heise.Count; i++)
        {
            jingpianButtons_heise[i].transform.localPosition = kapianstartPoint.localPosition;
            jingpianButtons_heise[i].transform.GetComponent<Button>().onClick.RemoveAllListeners();
            jingpianButtons_heise[i].gameObject.SetActive(true);
        }
        JingPianXianShi_Heise().Play().OnComplete(() => {OnOVer?.Invoke();});
    }
    //��ɫ2d��Ƭ��ť��ʾ�������λ��
    public void heiseJingPianYinCang(Action OnOVer)
    {
        JingPianYinCang_Heise().Play().OnComplete(() =>
        {
            for (int i = 0; i < jingpianButtons_heise.Count; i++)
            {
                jingpianButtons_heise[i].gameObject.SetActive(false);
            }
            OnOVer?.Invoke();
        });
    }

    #region �������
    //��ȡ���������Ƭ �����С
    public void SuiJiChouXuan_heise(Action OnOVer)
    {
        SuiJiJingZhi(jingpianButtons_heise);
        jingpianButtons_text_heise0[suiJiIndex0].gameObject.SetActive(false);
        jingpianButtons_text_heise1[suiJiIndex0].gameObject.SetActive(true);
        jingpianButtons_text_heise0[suiJiIndex1].gameObject.SetActive(false);
        jingpianButtons_text_heise1[suiJiIndex1].gameObject.SetActive(true);
        jingpianButtons_heise[suiJiIndex0].DOMove(daPoint.position, 0.3f);
        jingpianButtons_heise[suiJiIndex1].DOMove(xiaoPoint.position, 0.3f).OnComplete(() =>
        {
            paixu.gameObject.SetActive(true);
            OnOVer?.Invoke();
        });
    }
    private int suiJiIndex0;
    private int suiJiIndex1;
    private List<int> xiabiaos;
    private void SuiJiJingZhi(List<Transform> jingpianButton)
    {
        xiabiaos = new List<int>();
        for (int i = 0; i < jingpianButton.Count; i++)
        {
            xiabiaos.Add(i); //0,1,2,3,4
        }
        int index = GetRandNum(0, xiabiaos.Count);
        suiJiIndex0 = xiabiaos[index];
        xiabiaos.Remove(xiabiaos[index]);
        index = GetRandNum(0, xiabiaos.Count);
        suiJiIndex1 = xiabiaos[index];

        Debug.Log("suiJiIndex0:" + suiJiIndex0);
        Debug.Log("suiJiIndex1:" + suiJiIndex1);
    }
    public int GetRandNum(int min, int max)
    {
        System.Random r = new System.Random(Guid.NewGuid().GetHashCode());
        return r.Next(min, max);
    }
    
    public void PaiXu_heise(Action OnOVer)
    {
        dayuhao.onClick.AddListener(() =>
        {
            dayuOrxiaoyu = !dayuOrxiaoyu;
            if (dayuOrxiaoyu)
            {
                dayuhao.GetComponentInChildren<Text>().text = ">";
            }
            else
            {
                dayuhao.GetComponentInChildren<Text>().text = "<";
            }
        });
        //�����Ƭ0
        jingpianButtons_heise[suiJiIndex0].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            JingPianPaiXuDianJiChaKan_heiSe(jingpianButtons_heise[suiJiIndex0], jingpianButtons_heise[suiJiIndex1], suiJiIndex0);
        });
        //�����Ƭ1
        jingpianButtons_heise[suiJiIndex1].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            JingPianPaiXuDianJiChaKan_heiSe(jingpianButtons_heise[suiJiIndex1], jingpianButtons_heise[suiJiIndex0], suiJiIndex1);
        });
        float jing0 = float.Parse(jingpianButtons_text_heise0[suiJiIndex0].GetComponent<Text>().text);
        float jing1 = float.Parse(jingpianButtons_text_heise0[suiJiIndex1].GetComponent<Text>().text);
        queding_Button.GetComponent<Button>().onClick.AddListener(() =>
        {
            dayuhao.GetComponent<Button>().enabled = false;
            queding_Button.GetComponent<Button>().enabled = false;
            if ((jing0 > jing1 && dayuOrxiaoyu) || (jing0 < jing1 && !dayuOrxiaoyu))
            {
                //��ȷ
                jingpianButtons_text_heise0[suiJiIndex0].gameObject.SetActive(true);
                jingpianButtons_text_heise1[suiJiIndex0].gameObject.SetActive(false);
                jingpianButtons_text_heise0[suiJiIndex1].gameObject.SetActive(true);
                jingpianButtons_text_heise1[suiJiIndex1].gameObject.SetActive(false);
                queding_Button.GetComponent<Button>().onClick.RemoveAllListeners();
                jingpianButtons_heise[suiJiIndex0].transform.GetComponent<Button>().onClick.RemoveAllListeners();
                jingpianButtons_heise[suiJiIndex1].transform.GetComponent<Button>().onClick.RemoveAllListeners();
                TiShi_Dui().Play().OnComplete(() =>
                {
                    dayuhao.GetComponent<Button>().enabled = true;
                    queding_Button.GetComponent<Button>().enabled = true;
                    jingpianButtons_heise[suiJiIndex0].GetChild(0).localPosition = new Vector3(0, 10, 0); //��Ƭ0����
                    jingpianButtons_heise[suiJiIndex1].GetChild(0).localPosition = new Vector3(0, 10, 0); //��Ƭ1����
                    kapian2DUI.YinChangBiaoJi();    //���ؿ�Ƭ�ϵı��
                    kapian2DUI.JingPianYingChang(); //���ؿ�Ƭ�ϵľ�Ƭ
                    OnOVer?.Invoke();
                    dayuhao.onClick.RemoveAllListeners();
                });
            }
            else
            {
                TiShi_Cuo().Play().OnComplete(() =>
                {
                    dayuhao.GetComponent<Button>().enabled = true;
                    queding_Button.GetComponent<Button>().enabled = true;
                });
            }

        });
    }
    private void JingPianPaiXuDianJiChaKan_heiSe(Transform jingpian0 , Transform jingpian1,int index)
    {
        kapian2DUI.YinChangBiaoJi();
        jingpian0.transform.GetComponent<Button>().enabled = false;  //�رվ�Ƭ0�����ť
        jingpian1.transform.GetComponent<Button>().enabled = false;  //�رվ�Ƭ1�����ť
        queding_Button.GetComponent<Button>().enabled = false;
        jingpian1.GetChild(0).localPosition = new Vector3(0, 10, 0); //��һ����Ƭ����
        JingPianXuanZhong(jingpian0).Play(); //��Ƭ����
        kapian2DUI.XianShiJingPian_HeiSe();
        yanJing.JingPian3DXianShi(yanJing.jingPian_point0, 0, 0f).Play(); //3D��Ƭ����
        yanJing.JingPian3DXianShi(yanJing.jingPian_point1, 1, 0.3f).Play();//3D��Ƭ��ʾ
        kapian2DUI.GuanChaPlay(0, index * 0.3f + 0.9f);
        
        jingpian1.transform.GetComponent<Button>().enabled = true;  //�򿪾�Ƭ1�����ť
        queding_Button.GetComponent<Button>().enabled = true;
    }

    private void JingPianPaiXuDianJiChaKan_hongse(Transform jingpian0, Transform jingpian1, int index)
    {
        kapian2DUI.YinChangBiaoJi();
        jingpian0.transform.GetComponent<Button>().enabled = false;  //�رվ�Ƭ0�����ť
        jingpian1.transform.GetComponent<Button>().enabled = false;  //�رվ�Ƭ1�����ť
        queding_Button.GetComponent<Button>().enabled = false;
        jingpian1.GetChild(0).localPosition = new Vector3(0, 10, 0); //��һ����Ƭ����
        JingPianXuanZhong(jingpian0).Play(); //��Ƭ����
        kapian2DUI.XianShiJingPian_HongSe();
        yanJing.JingPian3DXianShi(yanJing.jingPian_point0, 0, 0f).Play(); //3D��Ƭ����
        yanJing.JingPian3DXianShi(yanJing.jingPian_point1, 1, 0.3f).Play();//3D��Ƭ��ʾ
        kapian2DUI.GuanChaPlay(0, index * 0.3f + 0.9f);
        
        jingpian1.transform.GetComponent<Button>().enabled = true;  //�򿪾�Ƭ1�����ť
        queding_Button.GetComponent<Button>().enabled = true;
    }
    private Tween TiShi_Dui()
    {
        var tweens = DOTween.Sequence();
        var tween = DOTween.To(() => dui.fillAmount, x => dui.fillAmount = x, 1, 0.3f);
        tweens.Insert(0,tween);
        tween = DOTween.To(() => dui.fillAmount, x => dui.fillAmount = x, 0, 0f);
        tweens.Insert(1.5f, tween);
        return tweens;
    }

    private Tween TiShi_Cuo()
    {
        var tweens = DOTween.Sequence();
        var tween = DOTween.To(() => cuo_0.fillAmount, x => cuo_0.fillAmount = x, 1, 0.3f);
        tweens.Insert(0, tween);
        tween = DOTween.To(() => cuo_1.fillAmount, x => cuo_1.fillAmount = x, 1, 0.3f);
        tweens.Insert(0.3f, tween);
        tween = DOTween.To(() => cuo_0.fillAmount, x => cuo_0.fillAmount = x, 0, 0f);
        tweens.Insert(1.5f, tween);
        tween = DOTween.To(() => cuo_1.fillAmount, x => cuo_1.fillAmount = x, 0, 0f);
        tweens.Insert(1.5f, tween);
        return tweens;
    }

    public void paiXuJieShu_heise(Action OnOVer)
    {
        jingpianButtons_heise[suiJiIndex0].transform.DOMove(kapianendPoint.position, 0.3f);
        jingpianButtons_heise[suiJiIndex1].transform.DOMove(kapianendPoint.position, 0.3f).OnComplete(() =>
        {
            OnOVer?.Invoke();
        });
    }
    
    #endregion
    

    public void XuanZhong_Hei_1(Action OnOVer)
    {
        jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            jingpianButtons_heise[0].transform.GetComponent<Button>().onClick.RemoveAllListeners();
            ReastBlackBtnTrans().Play();
            JingPianXuanZhong(jingpianButtons_heise[0]).Play();
            OnOVer?.Invoke();
        });
    }
    public void XuanZhong_Hei_2(Action OnOVer)
    {
        jingpianButtons_heise[1].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            jingpianButtons_heise[1].transform.GetComponent<Button>().onClick.RemoveAllListeners();
            ReastBlackBtnTrans().Play();
            JingPianXuanZhong(jingpianButtons_heise[1]).Play();
            OnOVer?.Invoke();
        });
    }
    //ѡ�о�Ƭ����
    private Tween JingPianXuanZhong(Transform obj)
    {
        var tweens = DOTween.Sequence();
        var tween2 = obj.GetChild(0).transform.DOLocalMoveY(40, 0.3f);
        tweens.Insert(0f, tween2);
        return tweens;
    }
    //ˢ�º�ɫ��Ƭ��ťλ��
    private Tween ReastBlackBtnTrans()
    {
        var tweens = DOTween.Sequence();
        foreach (var item in jingpianButtons_heise)
        {
            var tween = item.GetChild(0).transform.DOLocalMoveY(10, 0.3f);
            tweens.Insert(0, tween);
        }
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
    /// ��ɫ��Ƭ�������ض���
    private Tween JingPianYinCang_Heise()
    {
        var tweens = DOTween.Sequence();
        var tween = jingpianButtons_heise[0].transform.DOMove(kapianendPoint.position, 0.3f);
        tweens.Insert(0, tween);
        tween = jingpianButtons_heise[1].transform.DOMove(kapianendPoint.position, 0.3f);
        tweens.Insert(0.1f, tween);
        tween = jingpianButtons_heise[2].transform.DOMove(kapianendPoint.position, 0.3f);
        tweens.Insert(0.2f, tween);
        tween = jingpianButtons_heise[3].transform.DOMove(kapianendPoint.position, 0.3f);
        tweens.Insert(0.3f, tween);
        tween = jingpianButtons_heise[4].transform.DOMove(kapianendPoint.position, 0.3f);
        tweens.Insert(0.4f, tween);
        return tweens;
    }



}
