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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpfz;
using TicketSystem.ModuleDemo.PersonData;
using TicketSystem.ModuleDemo.PurchaseModule.Ticket_purchase;
using TicketSystem.ModuleDemo.Login;
using System.IO;

namespace TicketSystem.ModuleDemo.Others
{
    /// <summary>
    /// ModuleDemo.Others.UserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserWindow : Windowz
    {
        public UserWindow()
        {
            InitializeComponent();
            Width = 900;
            Height = 500;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SourceInitialized += UserWindow_SourceInitialized;
            ResizeMode = ResizeMode.CanMinimize;
            //加载用户主界面时，初始化优惠券减免值
            Loaded += delegate
            {
                string path = @"discount.txt";
                File.WriteAllText(path, "0");
            };
        }

        private void UserWindow_SourceInitialized(object sender, EventArgs e)
        {
            string userName;
            using(var c = new ticketEntities())
            {
                var q = from t in c.user where t.loginEmail == Login.Login.instrance.accountNumber.Text.ToString() select t;
                userName = q.FirstOrDefault().userName;
            }
            ShowUserMainpageName.Text = "欢迎您，"+userName;

        }


        private void btn1_Click(object sender, RoutedEventArgs e)
        {
        //    bus  bus = new bus();
        //    bus.ShowDialog();
            Ticket_purchase bus = new Ticket_purchase("train");
            bus.ShowDialog();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            //train train = new train();
            //train.ShowDialog();
            Ticket_purchase train = new Ticket_purchase("car");
            train.ShowDialog();
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            var v= MessageBoxz.ShowQuestion("确定要注销吗？","提示");
            if (v == true)
            {
                Login.Login login = new Login.Login();
                Close();
                login.Show();
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PersonInfoWindow info = new PersonInfoWindow();
            info.ShowDialog();
        }

        private void Game_Click(object sender, RoutedEventArgs e)
        {
            GameWin gameWin = new GameWin();
            gameWin.ShowDialog();
        }
    }
}
