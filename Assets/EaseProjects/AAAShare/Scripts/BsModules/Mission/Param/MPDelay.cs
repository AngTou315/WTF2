using AAAShare.BsModules.Agent;

namespace AAAShare.BsModules.Param
{
    public class MPDelay: IMissionParam
    {
        public string Des { get=>"延迟"; }
        public float delay = 1.0f;
        public IMissionAgent CreateAgent()
        {
            return new MADelay();
        }
    }
}