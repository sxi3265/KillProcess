using System;
using System.Collections.Generic;

namespace KillProcess
{
    public class KillProcessOptions
    {
        /// <summary>
        /// 执行间隔 单位毫秒
        /// </summary>
        public int ExecutionInterval { get; set; }

        /// <summary>
        /// 进程名和启动时间毫秒字典
        /// </summary>
        public Dictionary<string,int> ProcessStartTimeDic { get; set; }
    }
}