using AAAShare.Adapter;
using Ease.Event;
using Ease.UI;
using EaseProjects.Template.Event;

namespace AAAShare.BsPublic
{
    public class UISettlement : BaseUILogic
    {
        protected override void OnOpen()
        {
        }

        protected override void OnClose()
        {
        }

        public void OnClickBtnClose()
        {
            Entry.GetModule<IEventManager>().FireNow(this, new SettleOverArgs());
            Entry.GetModule<IUIManager>().CloseWindow("UISettlement");
        }
    }
}