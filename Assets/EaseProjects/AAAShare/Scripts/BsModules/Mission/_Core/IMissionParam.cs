namespace AAAShare.BsModules.Param
{
    public interface IMissionParam
    {
        public string Des { get; }
        IMissionAgent CreateAgent(); // 创建代理
    }
}