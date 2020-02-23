namespace JToolbox.SysInformation
{
    public class CPUInfo
    {
        public string Caption { get; set; }
        public int MaxClockSpeed { get; set; }
        public string Name { get; set; }
        public int NumberOfCores { get; set; }
        public int NumberOfEnabledCores { get; set; }
        public int NumberOfLogicalProcessors { get; set; }
        public string Manufacturer { get; set; }
    }
}