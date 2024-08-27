using AAAShare.BsModules;
using AAAShare.BsModules.Param;
using AAAShare.BsPublic.Agent;

namespace AAAShare.BsPublic.Param
{
    public class MPCountDown : IMissionParam
    {
        public string tip;
        public float internalTime = 1f;
        public int repeatTime = 1;

        public string Des
        {
            get => "CountDown";
        }

        public IMissionAgent CreateAgent()
        {
            return new MACountDown();
        }
    }
}