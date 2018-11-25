using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldnew.Utils
{
    class CommandHelper
    {
        public static string ExecuteCmd(string program, string arguments)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = program,
                    Arguments =  arguments,
                    UseShellExecute = false,           // 是否使用操作系统shell启动
                    RedirectStandardInput = false,     // 接受来自调用程序的输入信息
                    RedirectStandardOutput = true,     // 由调用程序获取输出信息
                    RedirectStandardError = true,      // 重定向标准错误输出
                    CreateNoWindow = true              // 不显示程序窗口
                },
            };

            p.Start();//启动程序
            string ret = p.StandardOutput.ReadToEnd();

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            return ret;

        }
    }
}
