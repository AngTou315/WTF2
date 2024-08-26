using AAAShare;
using AAAShare.Adapter;
using AAAShare.BsModules;
using AAAShare.BsPublic;
using AAAShare.BsPublic.Expm;
using AAAShare.BsPublic.ObjectInfo;
using AAAShare.BsPublic.Project;
using Ease.Config;
using Ease.Core;
using Ease.Event;
using Ease.Procedure;
using Ease.Resource;
using Ease.Scene;
using Ease.Sound;
using Ease.UI;
using EaseProjects.AAAShare.BsModules.Scheduler;
using EaseProjects.AAAShare.BsModules.Score;
using EaseProjects.Template.Procedure;
using UnityEngine.EventSystems;

namespace EaseProjects.Template.Scripts
{
    public class TemplateEntry : Entry
    {
        protected override void Start()
        {
            base.Start();
            //事件系统；
            Entry.SetModule<IEventManager>(new DefaultEventManager());
            //资源管理器
            Entry.SetModule<IResourceManager>(new UnityResourceManager());
            //声音管理器////////////////////////////////////////////////////////
            Entry.SetModule<ISoundManager>(new ThirdSoundManager2());
            //设置场景管理器。
            Entry.SetModule<ISceneManager>(new UnitySeneManager());
            //设置流程
            IProcedure[] procedures = new IProcedure[5];
            procedures[0] = new ProcedureLaunch();
            procedures[1] = new ProcedureChangeScene();
            procedures[2] = new ProcedureLogin();
            procedures[3] = new ProcedureMain();
            procedures[4] = new ProcedureSettlement();
            Entry.SetModule<IProcedureManager>(new ProcedureManager(procedures));
            //设置UI管理器
            Entry.SetModule<IUIManager>(GetComponentInChildren<DefaultUIManager>());
            //设置配置表管理器;
            Entry.SetModule<IConfigManager>(GetComponentInChildren<BaseConfigManager>());
            //设置项目管理器
            Entry.SetModule<IProjectManager>(GetComponentInChildren<ProjectManager>());
            //设置expm管理器
            Entry.SetModule<IExpmManager>(new ExpmManager());
            //设置物品指示器
            Entry.SetModule<IObjectInfoManager>(new DefaultObjectInfoManager());
            //设置高亮:
            Entry.SetModule<IHightLightManager>(new HightLightManager());
            //设置调度管理器:
            Entry.SetModule<IScheduler>(new Scheduler());
            //设置成绩:
            Entry.SetModule<IScoreManager>(new AIScoreManager());
            //开始项目；
            Entry.GetModule<IProcedureManager>().ChangeProcedure<ProcedureLaunch>();
        }
    }
}