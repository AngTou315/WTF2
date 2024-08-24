namespace AAAShare.BsModules
{
    /// <summary>
    /// 
    /// </summary>
    public class Mission : IMssion
    {
        public MissionState State { get; set; }
        public MissionData Data { get; set; }
        public IMissionAgent Agent { get; set; }

        public Mission(MissionData data)
        {
            this.Data = data;
            State = MissionState.INACTIVATED;
        }

        public void OnEnable()
        {
            State = MissionState.RUNNING;
            Agent = Data.Param.CreateAgent();
            Agent.Data = Data;
            Agent.OnOVer += OnOVer;
            Agent.OnEnable();
        }

        public void OnDisable()
        {
            State = MissionState.INACTIVATED;

            Data = null;

            Agent.OnDisable();
            Agent.OnOVer -= OnOVer;
            Agent = null;
        }

        public void OnUpdate()
        {
            Agent.OnUpdate();
        }

        private void OnOVer()
        {
            State = MissionState.OVER;
        }
    }
}