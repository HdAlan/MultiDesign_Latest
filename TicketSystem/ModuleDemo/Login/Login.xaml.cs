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

namespace TicketSystem.ModuleDemo.Login
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Windowz
    {

        public static Login instrance;
        public Login()
        {
            InitializeComponent();
            instrance = this;
            new Others.AddData();//添加vehicle数据
        }

        //点击出来注册
        object obj1;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button b = e.Source as Button;
            Uri uri = new Uri(b.Tag.ToString(), UriKind.Relative);
            obj1 = Application.LoadComponent(uri);

            var w = obj1 as Window;
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.ShowDialog();
            
            return;
        }

        private void land_Click(object sender, RoutedEventArgs e)
        {
            if (cb_yonghu.IsSelected)
            {
                Boolean vis = false;
                using (var context = new ticketEntities())
                {
                    var q1 =
                            from t1 in context.user
                            where t1.loginEmail == accountNumber.Text
                            select new
                            {
                            };
                    var q2 =
                            from t1 in context.user
                            where t1.loginPWD == password.Password
                            select new
                            {
                            };

                    if (accountNumber.Text == "" && password.Password == "")
                    {
                        tb1.Visibility = Visibility.Visible;
                        tb2.Visibility = Visibility.Visible;
                        return;
                    }
                    else if (accountNumber.Text == "" && password.Password != "")
                    {
                        tb1.Visibility = Visibility.Visible;
                        tb2.Visibility = Visibility.Hidden;
                        return;
                    }
                    else if (accountNumber.Text != "" && password.Password == "")
                    {
                        tb1.Visibility = Visibility.Hidden;
                        tb2.Visibility = Visibility.Visible;
                        return;
                    }

                    if (q1.Count() == 0 || q2.Count() == 0)
                    {

                        MessageBoxz.ShowError("账户名或密码错误");
                    }
                    else if (q1.Count() != 0 || q2.Count() != 0)
                    {
                        vis = true;
                    }
                    tb1.Visibility = Visibility.Hidden;
                    tb2.Visibility = Visibility.Hidden;

                }

                if (vis)
                {
                    Object obj2;
                    Button b = e.Source as Button;
                    Uri uri = new Uri(b.Tag.ToString(), UriKind.Relative);
                    obj2 = Application.LoadComponent(uri);
                    if (obj2 is Window)
                    {
                        var w = obj2 as Window;
                        w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        w.Show();
                        Close();
                    }
                }
            }
            else if(cb_guanliyuan.IsSelected)
            {
                using(var c = new ticketEntities())
                {
                    var q = from t in c.admin where t.adminName == accountNumber.Text.ToString() where t.adminPwd.ToString() == password.Password.ToString() select t;
                    if(q.Count() == 0)
                    {
                        MessageBoxz.ShowError("此管理员不存在,或密码错误");
                    }
                    else
                    {
                        Button btn = e.Source as Button;
                        Uri uri = new Uri(btn.Tag.ToString(), UriKind.Relative);
                        object obj = Application.LoadComponent(uri);
                        var w = obj as Window;
                        w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        w.Show();
                        Close();
                    }
                }
            }
            
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            bd1.Visibility = Visibility.Hidden;
            bd2.Visibility = Visibility.Hidden;
            tb1.Visibility = Visibility.Hidden;
            tb2.Visibility = Visibility.Hidden;

        }

        private void accountNumber_MouseEnter(object sender, MouseEventArgs e)
        {
            bd1.Visibility = Visibility.Visible;
        }

        private void bd2_MouseEnter(object sender, MouseEventArgs e)
        {
            bd2.Visibility = Visibility.Visible;
        }
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            LoginTitle.Text = "管理员登录";
            if (cb_guanliyuan.IsSelected)
            {
                button_register.Visibility = Visibility.Hidden;
                land.Tag = "ModuleDemo/adminPages/AdminWindow.xaml";
            }
        }

        private void cb_yonghu_Selected(object sender, RoutedEventArgs e)
        {
            LoginTitle.Text = "用户登录";
            if (cb_yonghu.IsSelected)
            {
                button_register.Visibility = Visibility.Visible;
                land.Tag = "ModuleDemo/Others/UserWindow.xaml";
            }
        }
    }
}
