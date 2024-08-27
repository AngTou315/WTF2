using AAAShare.BsModules;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;

namespace AAAShare.BsPublic.Param
{
    public class MPcustom : IMissionParam
    {
        public string Des => "自定义事件";

        public IMissionAgent CreateAgent()
        {
            return new MAClickObject();
        }
    }
}