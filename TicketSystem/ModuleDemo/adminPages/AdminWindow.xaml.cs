using System;
using System.Collections.Generic;
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
using Wpfz;
using TicketSystem.ModuleDemo.Login;

namespace TicketSystem.ModuleDemo.adminPages
{
    /// <summary>
    /// AdminWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AdminWindow : Windowz
    {
        private Button lastButton;
        public AdminWindow()
        {
            InitializeComponent();
            TBWEL.Text= "管理员："+Login.Login.instrance.accountNumber.Text;
            this.Width = 900;
            this.Height = 500;
            this.ResizeMode = ResizeMode.CanMinimize;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lastButton == null)
            {
                user.Foreground = Brushes.Black;
                user.Background = Brushes.Transparent;

            }
            if (lastButton != null)
            {

                lastButton.Foreground = Brushes.Black;
                lastButton.Background = Brushes.Transparent;

            }

            Button btn = e.Source as Button;
            btn.Foreground = Brushes.Blue;
            btn.Background = Brushes.Orange;



            Uri uri = new Uri(btn.Tag.ToString(), UriKind.Relative);
            try
            {
                Object obj = Application.LoadComponent(uri);
            }
            catch
            {
                MessageBox.Show("未找到" + uri.OriginalString, "出错了");
                return;
            }

            if (lastButton == btn)
            {
                frame.Refresh();
            }
            else
                frame.Source = uri;
            lastButton = btn;
        }

        private void Buttonz_Click(object sender, RoutedEventArgs e)
        {
            bool question = MessageBoxz.ShowQuestion("确定要注销吗？", "提示");
            if (question)
            {
                Login.Login login = new Login.Login();
                Close();
                login.Show();
            }
            else
            {
                return;
            }
            
        }
    }
}
