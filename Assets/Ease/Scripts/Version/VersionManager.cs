namespace Ease.Version
{
    public class VersionManager : IVersionManager
    {
        private IVersionProvider versionProvider;

        public string GetVersion()
        {
            return versionProvider.Version;
        }

        public void SetVersionProvider(IVersionProvider versionProvider)
        {
            this.versionProvider = versionProvider;
        }
    }
}