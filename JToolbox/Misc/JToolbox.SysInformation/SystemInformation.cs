using System;
using System.Linq;
using System.Management;

namespace JToolbox.SysInformation
{
    public static class SystemInformation
    {
        public static string UserName => Environment.UserName;
        public static string UserDomainName => Environment.UserDomainName;
        public static string MachineName => Environment.MachineName;

        public static ManagementObject GetManagementObject(string query)
        {
            return new ManagementObjectSearcher(query)
               .Get()
               .Cast<ManagementObject>()
               .First();
        }

        public static ManagementObject GetSystemManagementObject()
        {
            return GetManagementObject("select * from Win32_OperatingSystem");
        }

        public static ManagementObject GetCPUManagementObject()
        {
            return GetManagementObject("select * from Win32_Processor");
        }

        public static OSInfo GetOSInfo()
        {
            var mo = GetSystemManagementObject();
            return new OSInfo
            {
                Name = Convert.ToString(mo["Caption"]).Trim(),
                Version = Convert.ToString(mo["Version"]),
                Architecture = Convert.ToString(mo["OSArchitecture"]),
                BuildNumber = Convert.ToString(mo["BuildNumber"]),
                Type = Convert.ToString(mo["OSType"]),
                Manufacturer = Convert.ToString(mo["Manufacturer"]),
                Status = Convert.ToString(mo["Status"])
            };
        }

        public static MemoryInfo GetMemoryInfo()
        {
            var mo = GetSystemManagementObject();
            return new MemoryInfo
            {
                FreePhysicalMemory = Convert.ToUInt64(mo["FreePhysicalMemory"]),
                FreeVirtualMemory = Convert.ToUInt64(mo["FreeVirtualMemory"]),
                TotalVirtualMemory = Convert.ToUInt64(mo["TotalVirtualMemorySize"]),
                TotalVisibleMemory = Convert.ToUInt64(mo["TotalVisibleMemorySize"]),
            };
        }

        public static CPUInfo GetCPUInfo()
        {
            var mo = GetCPUManagementObject();
            return new CPUInfo
            {
                Caption = Convert.ToString(mo["Caption"]),
                MaxClockSpeed = Convert.ToInt32(mo["MaxClockSpeed"]),
                Name = Convert.ToString(mo["Name"]),
                NumberOfCores = Convert.ToInt32(mo["NumberOfCores"]),
                NumberOfEnabledCores = Convert.ToInt32(mo["NumberOfEnabledCore"]),
                NumberOfLogicalProcessors = Convert.ToInt32(mo["NumberOfLogicalProcessors"]),
                Manufacturer = Convert.ToString(mo["Manufacturer"])
            };
        }
    }
}