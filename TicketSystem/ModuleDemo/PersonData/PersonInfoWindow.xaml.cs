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
using TicketSystem.ModuleDemo.Login;
namespace TicketSystem.ModuleDemo.PersonData
{
    /// <summary>
    /// PersonInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PersonInfoWindow : Windowz
    {
        public static int userId;
        public static string userLoginEmail;
        private Button lastButton;
        public PersonInfoWindow()
        {
            InitializeComponent();
            Title = "用户信息界面";


            //同步用户，为与登录时的用户一致，使用邮箱检索，确定userId
            userLoginEmail = Login.Login.instrance.accountNumber.Text.ToString();
            using(var c = new ticketEntities())
            {
                var q = from t in c.user where t.loginEmail == userLoginEmail select t;
                userId = q.FirstOrDefault().uid;
            }


            // 设置背景图片
            ImageBrush b = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("../../Resources/Images/bg.jpg", UriKind.Relative)),
                Stretch = Stretch.Fill
            };
            this.Background = b;
        }

        /// <summary>
        /// 按钮点击事件(个人信息\购票信息)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lastButton != null)
            {
                lastButton.Foreground = Brushes.Black;
            }
            if (lastButton == null)
            {
                person_info.Foreground = Brushes.Black;
            }
            Button btn = e.Source as Button;
            btn.Foreground = Brushes.Blue;

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
                info_frame.Refresh();
            }
            else
                info_frame.Source = uri;
            lastButton = btn;
        }
    }
}
