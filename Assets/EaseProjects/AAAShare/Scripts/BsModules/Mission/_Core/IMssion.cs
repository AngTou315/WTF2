namespace AAAShare.BsModules
{

    
    /// <summary>
    /// 任务;
    /// </summary>
    public interface IMssion
    {
        MissionState State { get; set; }
        MissionData Data { get; set; }
        IMissionAgent Agent { get; set; }
        void OnEnable();
        void OnDisable();
        void OnUpdate();
    }
}