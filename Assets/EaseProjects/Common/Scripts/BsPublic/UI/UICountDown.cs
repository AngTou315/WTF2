using AAAShare.Adapter;
using AAAShare.BsPublic.Event;
using Ease.Event;
using Ease.UI;
using EaseProjects.AAAShare.BsModules.Scheduler;
using TMPro;

namespace AAAShare.BsPublic
{
    public class UICountDownParam : BaseWindowParam
    {
        public string title;
        public float internalTime;
        public int repeatTime;
    }

    public class UICountDown : BaseUILogic
    {
        public TextMeshProUGUI textTitle;
        public TextMeshProUGUI textCountDown;
        private UICountDownParam uiCountDownParam;
        private int index = 0;

        protected override void OnOpen()
        {
            uiCountDownParam = param as UICountDownParam;
            Entry.GetModule<IScheduler>().Delay(OnDelay, uiCountDownParam.internalTime, uiCountDownParam.repeatTime, OnOver);
            textTitle.text = uiCountDownParam.title;
            textCountDown.text = $"{uiCountDownParam.repeatTime}";
        }

        protected override void OnClose()
        {
            Entry.GetModule<IEventManager>().FireNow(this, new UICloseEventArgs() { uiName = "UICountDown" });
        }

        private void OnDelay()
        {
            index++;
            textCountDown.text = $"{uiCountDownParam.repeatTime - index}";
        }

        private void OnOver()
        {
            Entry.GetModule<IUIManager>().CloseWindow("UICountDown");
        }
    }
}