using System;
using AAAShare.Adapter;
using AAAShare.BsPublic;
using AAAShare.BsPublic.Project;
using Ease.Config;
using Ease.Core;
using Ease.Event;
using Ease.Procedure;
using Ease.UI;
using EaseProjects.Template.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EaseProjects.Template.UI
{
    public class UILogin : BaseUILogic
    {
        public TextMeshProUGUI textTitle;
        public TextMeshProUGUI textSchool;


        protected override void OnOpen()
        {
            textTitle.text = Entry.GetModule<IConfigManager>().GetConfig<ProjectConfig>().title;
            textSchool.text = Entry.GetModule<IConfigManager>().GetConfig<ProjectConfig>().school;
        }

        protected override void OnClose()
        {
        }

        public void OnEnterGame(int difficulty)
        {
            //Entry.GetModule<IProjectManager>().difficulty = (Difficulty)difficulty;
            Entry.GetModule<IEventManager>().FireNow(this, new ToMainEventArgs());
        }
    }
}