using Ease.Core;

namespace Ease.UI
{
    /// <summary>
    /// </summary>
    public interface IUIManager : IModule,ILife
    {
        //打开某个UI;
        void OpenWindow(string windowName,BaseWindowParam param);

        //关闭某个UI;
        void CloseWindow(string windowName);

        //关闭所有UI;
        void CloseAllWindow();
    }
}