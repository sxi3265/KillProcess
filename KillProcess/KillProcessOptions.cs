using System;
using System.Collections.Generic;

namespace KillProcess
{
    public class KillProcessOptions
    {
        /// <summary>
        /// ִ�м�� ��λ����
        /// </summary>
        public int ExecutionInterval { get; set; }

        /// <summary>
        /// ������������ʱ������ֵ�
        /// </summary>
        public Dictionary<string,int> ProcessStartTimeDic { get; set; }
    }
}