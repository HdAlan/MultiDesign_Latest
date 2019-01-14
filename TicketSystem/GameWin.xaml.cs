using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicketSystem
{
    /// <summary>
    /// GameWin.xaml 的交互逻辑
    /// </summary>
    public partial class GameWin : Window
    {
        private Process process;
        public IntPtr childHandle;
        public GameWin()
        {
            InitializeComponent();
            string path = Environment.CurrentDirectory + @"\AngryBird\AngryBird.exe";
            IntPtr hostHandle = unityHost.Handle;
            process = new Process();
            process.StartInfo.FileName = path;
            //process.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            //process.WaitForInputIdle();

            childHandle = process.MainWindowHandle;
            while (childHandle == IntPtr.Zero)
            {
                childHandle = process.MainWindowHandle;
            }
            uint oldStyle = Win32Helper.GetWindowLong(childHandle, Win32Helper.GWL_STYLE);
            //Win32Helper.SetWindowLong(childHandle, Win32Helper.GWL_STYLE, (oldStyle | WS_CHILD) & ~WS_BORDER);
            //Win32Helper.SetWindowLong(childHandle, Win32Helper.GWL_STYLE, oldStyle & ~WS_BORDER);//去除边框
            Win32Helper.SetParent(childHandle, hostHandle);//设为子窗体
            Win32Helper.MoveWindow(childHandle, -2, -30, unityHost.Width + 4, unityHost.Height + 4, true);//移动窗口位置
        }
    }
}
