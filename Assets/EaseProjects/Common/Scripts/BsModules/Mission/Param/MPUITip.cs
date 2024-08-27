using AAAShare.BsModules.Agent;

namespace AAAShare.BsModules.Param
{
    /// <summary>
    /// 提示UI
    /// </summary>
    public class MPUITip : IMissionParam
    {
        public string title;
        public string content;


        public string Des { get=>"UI"; }

        public IMissionAgent CreateAgent()
        {
            return new MAUITip();
        }
    }
}