using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpfz;
namespace TicketSystem.ModuleDemo.SignUpModule
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SignUpWin : Windowz
    {

        private static int uid = 0;
        private string userName; 
        private string loginEmail;
        private string loginPWD;
        private string IDnumber = null;        //默认身份证号为null，用户注册后可自主实名制

        public SignUpWin()
        {
            InitializeComponent();
            ////此段代码用于清空数据库。请谨慎操作
            //using (ticketEntities context = new ticketEntities())
            //{
            //    var qw = from t in context.user select t;
            //    foreach (var v in qw)
            //    {
            //        context.user.Remove(v);
            //    }
            //    try
            //    {
            //        context.SaveChanges();
            //    }
            //    catch(Exception e)
            //    {
            //        MessageBoxz.ShowError(e.Message);
            //    }
            //}
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            //点击“重置”按钮，数据重置，如果用户点击确定，则清空输入框，否则返回，取消操作
            if (btn.Name == "BClearAll")
            {
                if (MessageBoxz.ShowQuestion("数据将清空，确定继续吗？", "警告") == true){ ClearAll(); }else{ return; }
            }

            //点击“确认注册”，进行用户注册，需要先检查每个输入框是否均有输入，然后再检查是否输入格式均正确，然后检查用户是否已存在，若不存在，则注册用户，若存在，自动清空输入框
            if (btn.Name == "BConfirmSignUp")
            {
                userName = TxtName.Text.ToString();
                loginEmail = TxtEmail.Text.ToString();
                loginPWD = PbPwd.Password.ToString();
                //检查是否有未输入的项
                if (!ChangeBorder()) { return; }
                //检查两次密码输入是否一致
                if (PbPwdConfirm.Password != PbPwd.Password)
                {
                    TBPwdCon.Visibility = Visibility.Visible;
                    return;
                }
                //检查邮箱输入是否合法
                if (!EmailFormatInsure(loginEmail))
                {
                    HTBErrEmail.Visibility = Visibility.Visible;
                    return;
                }
                //注册用户

                using (ticketEntities context = new ticketEntities())
                {
                    //先检查是否已经存在此用户
                    var q = from t in context.user
                            where t.loginEmail == loginEmail
                            select new
                            {
                                t.uid,
                                t.userName,
                                t.IDnumber
                            };
                    if (q.Count() != 0)
                    {
                        MessageBoxz.ShowError("此用户已存在，请用其他用户名");
                        //MessageBox.Show("此用户已存在，请用其他用户名", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                         ClearAll();           //清空输入框
                        context.Dispose();
                        return;
                    }
                    //确定不存在后，插入此用户
                    user user = new user()
                    {
                        uid = uid++,
                        userName = userName,
                        loginEmail = loginEmail,
                        loginPWD = loginPWD,
                        IDnumber = IDnumber,
                    };
                    context.user.Add(user);
                    try
                    {
                        context.SaveChanges();
                        MessageBoxz.ShowInfo("注册成功，将返回登录");
                        //MessageBox.Show("注册成功，将返回登录", "提示");
                        context.Dispose();
                        Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBoxz.ShowError("错误" + ex.ToString());
                        //MessageBox.Show("错误" + ex.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// 为输入框添加样式，鼠标移过，变为亮绿色，后变为灰色
        /// </summary>
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = e.Source as Border;
            border.MouseEnter += delegate {
                border.BorderBrush = Brushes.LightGreen;

            };
            border.MouseLeave += delegate
            {
                if (border.Name == "BtnConSignBor" || border.Name == "BtnClearBor")
                {
                    border.BorderBrush = Brushes.LightSkyBlue;
                }
                else
                {
                    border.BorderBrush = Brushes.Gray;
                }
            };
        }
        /// <summary>
        /// 为邮箱输入框设置样式，如果有输入，则不再显示提示信息
        /// </summary>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            HTBErrEmail.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 为密码输入框设置样式，如果有输入，则不再显示提示信息
        /// </summary>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pw = e.Source as PasswordBox;
            if (pw.Name == "PbPwd")
            {
                TBPwd.Visibility = Visibility.Hidden;
            }
            else
            {
                TBPwdCon.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// 清空所有输入框的内容
        /// </summary>
        private void ClearAll()
        {
            TxtEmail.Text = null;
            TxtName.Text = null;
            PbPwd.Password = null;
            PbPwdConfirm.Password = null;
        }
        /// <summary>
        ///  判断是否有输入
        /// </summary>
        private bool ChangeBorder()
        {
            if (TxtEmail.Text.ToString().Length == 0 || TxtName.Text.ToString().Length == 0)
            {
                return false;
            }
            if (PbPwd.Password.ToString().Length == 0)
            {
                TBPwd.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查邮箱是否合法
        /// </summary>
        /// <param name="Em">邮箱</param>
        /// <returns>返回一个布尔值，true为正确，false为不正确</returns>
        private bool EmailFormatInsure(string Em)
        {
            if (!(Em.EndsWith("com") || Em.EndsWith("cn") || Em.EndsWith("org")))
            {
                return false;
            }
            string EM = null;
            if (Em.Contains("qq"))
            {
                EM = @"^[1-9]\d{4,10}@qq\.com$";
            }
            else
            {
                EM = @"[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
            }
            Regex eReg = new Regex(EM);
            return eReg.IsMatch(Em);
        }
    }
}
