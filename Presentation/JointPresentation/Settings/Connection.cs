namespace JointPresentation.Settings
{
    public static class Connection
    {
        public static string GetConfiguration(string defaultConnection)
        {
            if (!string.IsNullOrEmpty(defaultConnection))
                return defaultConnection;

            string envString = Environment.GetEnvironmentVariable("DATA_CON");

            return envString;
        }
    }
}
