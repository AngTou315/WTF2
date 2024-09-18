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
    
    /// <summary>
    /// 卡片初始位子
    /// </summary>
    [SerializeField]
    private Transform kapianstartPoint;

    /// <summary>
    /// 卡片移出位子
    /// </summary>
    [SerializeField]
    private Transform kapianendPoint;

    /// <summary>
    /// 所有卡片显示位子(黑色)
    /// </summary>
    [SerializeField]
    private List<Transform> kapianPoint1_heise;

    #region 排序
    /// <summary>
    /// 排序卡片位子左
    /// </summary>s
    [SerializeField]
    private Transform daPoint;
    /// <summary>
    /// 排序卡片位子(右)
    /// </summary>
    [SerializeField]
    private Transform xiaoPoint;
    //
    [SerializeField]
    private Transform paixu;
    /// <summary>
    /// 大于号
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
    /// 大于号还是小于号(点击大于号按钮控制:true大于号  false 小于号)
    /// </summary>
    private bool dayuOrxiaoyu;
    /// <summary>
    /// 排序确定按钮
    /// </summary>
    [SerializeField]
    private Transform queding_Button;
    [SerializeField]
    private YanJing yanJing;
    [SerializeField]
    private KaPian kapian2DUI;
    #endregion
    

    //黑色2d镜片按钮显示到达中间位置
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
    //黑色2d镜片按钮显示到达结束位置
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

    #region 随机排序
    //抽取两个随机镜片 排序大小
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
        //点击镜片0
        jingpianButtons_heise[suiJiIndex0].transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            JingPianPaiXuDianJiChaKan_heiSe(jingpianButtons_heise[suiJiIndex0], jingpianButtons_heise[suiJiIndex1], suiJiIndex0);
        });
        //点击镜片1
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
                //正确
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
                    jingpianButtons_heise[suiJiIndex0].GetChild(0).localPosition = new Vector3(0, 10, 0); //镜片0放下
                    jingpianButtons_heise[suiJiIndex1].GetChild(0).localPosition = new Vector3(0, 10, 0); //镜片1放下
                    kapian2DUI.YinChangBiaoJi();    //隐藏卡片上的标记
                    kapian2DUI.JingPianYingChang(); //隐藏卡片上的镜片
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
        jingpian0.transform.GetComponent<Button>().enabled = false;  //关闭镜片0点击按钮
        jingpian1.transform.GetComponent<Button>().enabled = false;  //关闭镜片1点击按钮
        queding_Button.GetComponent<Button>().enabled = false;
        jingpian1.GetChild(0).localPosition = new Vector3(0, 10, 0); //另一个镜片放下
        JingPianXuanZhong(jingpian0).Play(); //镜片拿起
        kapian2DUI.XianShiJingPian_HeiSe();
        yanJing.JingPian3DXianShi(yanJing.jingPian_point0, 0, 0f).Play(); //3D镜片隐藏
        yanJing.JingPian3DXianShi(yanJing.jingPian_point1, 1, 0.3f).Play();//3D镜片显示
        kapian2DUI.GuanChaPlay(0, index * 0.3f + 0.9f);
        
        jingpian1.transform.GetComponent<Button>().enabled = true;  //打开镜片1点击按钮
        queding_Button.GetComponent<Button>().enabled = true;
    }

    private void JingPianPaiXuDianJiChaKan_hongse(Transform jingpian0, Transform jingpian1, int index)
    {
        kapian2DUI.YinChangBiaoJi();
        jingpian0.transform.GetComponent<Button>().enabled = false;  //关闭镜片0点击按钮
        jingpian1.transform.GetComponent<Button>().enabled = false;  //关闭镜片1点击按钮
        queding_Button.GetComponent<Button>().enabled = false;
        jingpian1.GetChild(0).localPosition = new Vector3(0, 10, 0); //另一个镜片放下
        JingPianXuanZhong(jingpian0).Play(); //镜片拿起
        kapian2DUI.XianShiJingPian_HongSe();
        yanJing.JingPian3DXianShi(yanJing.jingPian_point0, 0, 0f).Play(); //3D镜片隐藏
        yanJing.JingPian3DXianShi(yanJing.jingPian_point1, 1, 0.3f).Play();//3D镜片显示
        kapian2DUI.GuanChaPlay(0, index * 0.3f + 0.9f);
        
        jingpian1.transform.GetComponent<Button>().enabled = true;  //打开镜片1点击按钮
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
    //选中镜片动画
    private Tween JingPianXuanZhong(Transform obj)
    {
        var tweens = DOTween.Sequence();
        var tween2 = obj.GetChild(0).transform.DOLocalMoveY(40, 0.3f);
        tweens.Insert(0f, tween2);
        return tweens;
    }
    //刷新黑色镜片按钮位置
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
    /// 黑色镜片依次显示动画
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
    /// 黑色镜片依次隐藏动画
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
