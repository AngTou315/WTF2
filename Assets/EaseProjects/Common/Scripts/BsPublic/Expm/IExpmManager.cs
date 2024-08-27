using AAAShare.BsModules;
using Ease.Core;

namespace AAAShare.BsPublic.Expm
{
    /// <summary>
    /// 负责加载与卸载
    /// 事件的绑定与解绑
    /// </summary>
    public interface IExpmManager : IModule
    {
        public IMissionLogic missionLogic { get; set; }

        //加载;
        public void LoadExpm();

        //卸载；
        public void UnLoadExpm();
    }
}