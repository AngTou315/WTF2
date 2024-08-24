using AAAShare.BsModules;
using AAAShare.BsModules.Agent;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;
using UnityEngine;
using UnityEngine.UI;

namespace AAAShare.BsPublic.Param
{
    public class MPSetJuLi : IMissionParam
    {
        
        public Transform shilika3D20_30;

        public Text dangqian_cm;

        public Button queDing_Button;

        public Slider slider;

        public Transform juLiTiShi;
        public GameObject trans;
        public string Des
        {
            get => "调试距离";
        }

        public IMissionAgent CreateAgent()
        {
            return new MASetJuLi();
        }
    }
}