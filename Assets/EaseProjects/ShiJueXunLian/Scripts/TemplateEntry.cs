using AAAShare;
using AAAShare.Adapter;
using AAAShare.BsModules;
using AAAShare.BsPublic;
using AAAShare.BsPublic.Expm;
using AAAShare.BsPublic.Project;
using Ease.Config;
using Ease.Core;
using Ease.Event;
using Ease.Procedure;
using Ease.Resource;
using Ease.Scene;
using Ease.UI;
using EaseProjects.Template.Procedure;
using UnityEngine.EventSystems;

namespace EaseProjects.Template.Scripts
{
    public class TemplateEntry : Entry
    {
        protected override void Start()
        {
            base.Start();
            ////事件系统；
            AddModule<IEventManager>(new DefaultEventManager());
            ////资源管理器
            AddModule<IResourceManager>(new UnityResourceManager());
            ////设置场景管理器。
            AddModule<ISceneManager>(new UnitySeneManager());
            //设置配置表管理器;
            AddModule<IConfigManager>(GetComponentInChildren<BaseConfigManager>());
            //设置UI管理器
            AddModule<IUIManager>(GetComponentInChildren<DefaultUIManager>());


            //设置项目管理器
            AddModule<IProjectManager>(GetComponentInChildren<ProjectManager>());
            //设置expm管理器
            AddModule<IExpmManager>(new ExpmManager());


            //设置流程
            IProcedure[] procedures = new IProcedure[4];
            procedures[0] = new ProcedureLaunch();
            procedures[1] = new ProcedureChangeScene();
            procedures[2] = new ProcedureLogin();
            procedures[3] = new ProcedureMain();
            AddModule<IProcedureManager>(new ProcedureManager(procedures));

            
            //设置高亮:
            AddModule<IHightLightManager>(new HightLightManager());

            //开始项目；
            GetModule<IProcedureManager>().ChangeProcedure<ProcedureLaunch>();
        }
    }
}