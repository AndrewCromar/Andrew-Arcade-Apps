namespace ONYX
{
    [System.Serializable]
    public class AppProfileList
    {
        public AppProfile[] apps;
    }

    [System.Serializable]
    public class AppProfile
    {
        public string title;
        public string developer;
        public string icon;
        public string launchCommand;
    }
}