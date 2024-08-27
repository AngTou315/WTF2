using AAAShare.BsPublic;
using Ease.Core;
using Ease.Event;
using Ease.FSM;
using Ease.Procedure;
using Ease.Scene;
using Ease.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EaseProjects.Template.Procedure
{
    public class ProcedureChangeScene : BaseProcedure
    {
        private string newScene = "";

        public override void OnEnter(Fsm<ProcedureManager> fsm)
        {
            newScene = (string)fsm.GetData("newScene");
            Entry.GetModule<ISceneManager>().ChangeSceneAsync(newScene, LoadSceneMode.Single, OnStartLoad, OnEndLoad, OnLoadProgress);
        }

        public override void OnQuit(Fsm<ProcedureManager> fsm)
        {
            Debug.Log("OnQuit");
        }

        public override void OnUpdate(Fsm<ProcedureManager> fsm, float time, float realTime)
        {
            Debug.Log("ProcedureChangeScene OnUpdate");
            if (SceneManager.GetActiveScene().name.Equals(newScene) && newScene.Equals("Login"))
            {
                ChangeProcedure<ProcedureLogin>(fsm);
            }
            else if (SceneManager.GetActiveScene().name.Equals(newScene) && newScene.Equals("Main"))
            {
                ChangeProcedure<ProcedureMain>(fsm);
            }
        }

        private void OnStartLoad()
        {
            Entry.GetModule<IUIManager>().OpenWindow("UILoading", null);
        }

        private void OnEndLoad()
        {
            Entry.GetModule<IUIManager>().CloseWindow("UILoading");
        }

        private void OnLoadProgress(float progress)
        {
        }
    }
}