using System;
using AAAShare.BsModules;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Param;
using EaseProjects.AAAShare.BsPublic.Component;

namespace AAAShare.BsPublic.Agent
{
    public class MAClickObject : IMissionAgent
    {
        public MissionData Data { get; set; }
        public event Action OnOVer;
        private ClickableObject clickableObject = null;

        public void OnEnable()
        {
            var param = Data.Param as MPClickObject;
            var target = param.target;
            clickableObject = target.GetComponent<ClickableObject>();
            clickableObject.OnClick += OnClick;
            Entry.GetModule<IHightLightManager>().Show(clickableObject.gameObject);
        }

        public void OnDisable()
        {
            Entry.GetModule<IHightLightManager>().Hide(clickableObject.gameObject);
            clickableObject.OnClick -= OnClick;
            clickableObject = null;
        }

        public void OnUpdate()
        {
        }

        private void OnClick()
        {
            OnOVer?.Invoke();
        }
    }
}