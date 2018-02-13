using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ram.SAPI
{
    class SystemInfo
    {
        [DllImport("kernel32")]
        public static extern void GetWindowsDirectory(StringBuilder WinDir, int count);
        [DllImport("kernel32")]
        public static extern void GetSystemDirectory(StringBuilder SysDir, int count);
        [DllImport("kernel32")]
        public static extern void GetSystemInfo(ref CPU_INFO cpuinfo);
        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
        [DllImport("kernel32")]
        public static extern void GetSystemTime(ref SYSTEMTIME_INFO stinfo);
        //定义CPU的信息结构
        [StructLayout(LayoutKind.Sequential)]
        public struct CPU_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }
        //定义内存的信息结构
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }
        //定义系统时间的信息结构
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME_INFO
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        PerformanceCounter _oPerformanceCounter;
        MEMORY_INFO MemInfo;

        public SystemInfo()
        {
            _oPerformanceCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            MemInfo = new MEMORY_INFO();
            GlobalMemoryStatus(ref MemInfo);
        }

        public string getCPUUsed()
        {
            float nVal = _oPerformanceCounter.NextValue();
            return nVal.ToString("0");
        }

        public string getMemoryUsed()
        {
            float mVal = MemInfo.dwMemoryLoad;
            return mVal.ToString("0");
        }

        public void other()
        {
            /*
            //调用GetWindowsDirectory和GetSystemDirectory函数分别取得Windows路径和系统路径
            const int nChars = 128;
            StringBuilder Buff = new StringBuilder(nChars);
            GetWindowsDirectory(Buff, nChars);
            WindowsDirectory.Text = "Windows路径：" + Buff.ToString();
            GetSystemDirectory(Buff, nChars);
            SystemDirectory.Text = " 系统路径：" + Buff.ToString();
            //调用GetSystemInfo函数获取CPU的相关信息
            CPU_INFO CpuInfo;
            CpuInfo = new CPU_INFO();
            GetSystemInfo(ref CpuInfo);
            NumberOfProcessors.Text = "本计算机中有" + CpuInfo.dwNumberOfProcessors.ToString() + "个CPU";
            ProcessorType.Text = "CPU的类型为" + CpuInfo.dwProcessorType.ToString();
            ProcessorLevel.Text = "CPU等级为" + CpuInfo.dwProcessorLevel.ToString();
            OemId.Text = "CPU的OEM ID为" + CpuInfo.dwOemId.ToString();
            PageSize.Text = "CPU中的页面大小为" + CpuInfo.dwPageSize.ToString();
            //调用GlobalMemoryStatus函数获取内存的相关信息
            MEMORY_INFO MemInfo;
            MemInfo = new MEMORY_INFO();
            GlobalMemoryStatus(ref MemInfo);
            MemoryLoad.Text = MemInfo.dwMemoryLoad.ToString() + "%的内存正在使用";
            TotalPhys.Text = "物理内存共有" + MemInfo.dwTotalPhys.ToString() + "字节";
            AvailPhys.Text = "可使用的物理内存有" + MemInfo.dwAvailPhys.ToString() + "字节";
            TotalPageFile.Text = "交换文件总大小为" + MemInfo.dwTotalPageFile.ToString() + "字节";
            AvailPageFile.Text = "尚可交换文件大小为" + MemInfo.dwAvailPageFile.ToString() + "字节";
            TotalVirtual.Text = "总虚拟内存有" + MemInfo.dwTotalVirtual.ToString() + "字节";
            AvailVirtual.Text = "未用虚拟内存有" + MemInfo.dwAvailVirtual.ToString() + "字节";
            //调用GetSystemTime函数获取系统时间信息
            SYSTEMTIME_INFO StInfo;
            StInfo = new SYSTEMTIME_INFO();
            GetSystemTime(ref StInfo);
            Date.Text = StInfo.wYear.ToString() + "年" + StInfo.wMonth.ToString() + "月" + StInfo.wDay.ToString() + "日";
            Time.Text = (StInfo.wHour + 8).ToString() + "点" + StInfo.wMinute.ToString() + "分" + StInfo.wSecond.ToString() + "秒";
             * */
        }
    }
}
