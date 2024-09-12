namespace AAAShare.BsModules
{
    /// <summary>
    /// 实现了IMssion，让agent去代替执行
    /// </summary>
    public class Mission : IMssion
    {
        public MissionState State { get; set; }
        //任务的状态，通过 OnEnable() 和 OnDisable() 方法改变任务的状态。
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
            // 创建代理，把参数同步给代理
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