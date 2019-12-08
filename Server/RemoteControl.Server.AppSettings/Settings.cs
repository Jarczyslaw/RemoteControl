namespace RemoteControl.Server.AppSettings
{
    public class Settings
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public bool StartMinimized { get; set; }
        public int InactiveTime { get; set; }
        public int RemoveTime { get; set; }
    }
}