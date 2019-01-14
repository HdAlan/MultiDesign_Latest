using Wpfz;
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

namespace TicketSystem.ModuleDemo.PurchaseModule.Ticket_purchase
{    
    /// <summary>
    /// Confirm_info.xaml 的交互逻辑
    /// </summary>
    public partial class Confirm_info : Window
    {

        public string err_message = "";
        public double price=5;
        public int sum_user=1;
        public double sum_price=5;
        public static Confirm_info Instance;
        int add_user_id = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">交通工具类型：true为火车，false为汽车</param>
        public Confirm_info(Boolean type,double t_price)
        {
            price = t_price;
            sum_price = price;
            InitializeComponent();
            Instance = this;
            if (type)
            {
                //type_icon.Text = "&#116;";
                type_icon.Text = "t";   //火车
            }
            else
            {
                //type_icon.Text = "&#118;";

                type_icon.Text = "v";   //汽车
            }
            num.Text = "总计"+sum_user+"人";
            total_sum.Text = "共"+sum_price+"元";
        }

        private void add_user_Click(object sender, RoutedEventArgs e)
        {
            switch (add_user_id)
            {
                case 0:
                    if (user_name_0.Text == "" || user_phone_0.Text == "" || user_id_0.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能添加新的用户！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_0.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_0.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message+"请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    user_border_1.Visibility = Visibility.Visible;
                    sum_user++;
                    num.Text = "总计" + sum_user + "人";
                    sum_price = price * sum_user;
                    total_sum.Text = "共" +sum_price + "元";
                    break;
                case 1:
                    if (user_name_1.Text == "" || user_phone_1.Text == "" || user_id_1.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能添加新的用户！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_1.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_1.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    reduce_1.Visibility = Visibility.Hidden;
                    user_border_2.Visibility = Visibility.Visible;
                    sum_user++;
                    num.Text = "总计" + sum_user + "人";
                    sum_price = price * sum_user;
                    total_sum.Text = "共" + sum_price + "元";
                    break;
                case 2:
                    if (user_name_2.Text == "" || user_phone_2.Text == "" || user_id_2.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能添加新的用户！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_2.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_2.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    reduce_2.Visibility = Visibility.Hidden;
                    user_border_3.Visibility = Visibility.Visible;
                    sum_user++;
                    num.Text = "总计" + sum_user + "人";
                    sum_price = price * sum_user;
                    total_sum.Text = "共" + sum_price + "元";
                    break;
                case 3:
                    if (user_name_3.Text == "" || user_phone_3.Text == "" || user_id_3.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能添加新的用户！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_3.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_3.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    reduce_3.Visibility = Visibility.Hidden;
                    user_border_4.Visibility = Visibility.Visible;
                    sum_user++;
                    num.Text = "总计" + sum_user + "人";
                    sum_price = price * sum_user;
                    total_sum.Text = "共" + sum_price + "元";
                    break;
                case 4:
                    if (user_name_4.Text == "" || user_phone_4.Text == "" || user_id_4.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能添加新的用户！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_4.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_4.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    reduce_4.Visibility = Visibility.Hidden;
                    user_border_5.Visibility = Visibility.Visible;
                    sum_user++;
                    num.Text = "总计" + sum_user + "人";
                    sum_price = price * sum_user;
                    total_sum.Text = "共" + sum_price + "元";
                    break;
                case 5:
                    if (user_name_5.Text == "" || user_phone_5.Text == "" || user_id_5.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能添加新的用户！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_5.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_5.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    MessageBoxz.ShowError("用户上限，不能再次添加！");
                    break;
                default:
                    break;
            }
            add_user_id++;
        }

        private void reduce_1_Click(object sender, RoutedEventArgs e)
        {
            user_border_1.Visibility = Visibility.Hidden;
            user_name_1.Text = "";
            user_phone_1.Text = "";
            user_id_1.Text = "";
            sum_user--;
            num.Text = "总计" + sum_user + "人";
            sum_price = price * sum_user;
            total_sum.Text = "共" + sum_price + "元";
            add_user_id--;
        }
        private void reduce_2_Click(object sender, RoutedEventArgs e)
        {
            user_border_2.Visibility = Visibility.Hidden;
            user_name_2.Text = "";
            user_phone_2.Text = "";
            user_id_2.Text = "";
            reduce_1.Visibility = Visibility.Visible;
            sum_user--;
            num.Text = "总计" + sum_user + "人";
            sum_price = price * sum_user;
            total_sum.Text = "共" + sum_price + "元";
            add_user_id--;
        }
        private void reduce_3_Click(object sender, RoutedEventArgs e)
        {
            user_border_3.Visibility = Visibility.Hidden;
            user_name_3.Text = "";
            user_phone_3.Text = "";
            user_id_3.Text = "";
            reduce_2.Visibility = Visibility.Visible;
            sum_user--;
            num.Text = "总计" + sum_user + "人";
            sum_price = price * sum_user;
            total_sum.Text = "共" + sum_price + "元";
            add_user_id--;
        }
        private void reduce_4_Click(object sender, RoutedEventArgs e)
        {
            user_border_4.Visibility = Visibility.Hidden;
            user_name_4.Text = "";
            user_phone_4.Text = "";
            user_id_4.Text = "";
            reduce_3.Visibility = Visibility.Visible;
            sum_user--;
            num.Text = "总计" + sum_user + "人";
            sum_price = price * sum_user;
            total_sum.Text = "共" + sum_price + "元";
            add_user_id--;
        }
        private void reduce_5_Click(object sender, RoutedEventArgs e)
        {
            user_border_5.Visibility = Visibility.Hidden;
            user_name_5.Text = "";
            user_phone_5.Text = "";
            user_id_5.Text = "";
            reduce_4.Visibility = Visibility.Visible;
            sum_user--;
            num.Text = "总计" + sum_user + "人";
            sum_price = price * sum_user;
            total_sum.Text = "共" + sum_price + "元";
            add_user_id--;
        }
        private void confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            switch (add_user_id)
            {
                case 0:
                    if (user_name_0.Text == "" || user_phone_0.Text == "" || user_id_0.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能提交订单！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_0.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_0.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    break;
                case 1:
                    if (user_name_1.Text == "" || user_phone_1.Text == "" || user_id_1.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能提交订单！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_1.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_1.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    break;
                case 2:
                    if (user_name_2.Text == "" || user_phone_2.Text == "" || user_id_2.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能提交订单！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_2.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_2.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    break;
                case 3:
                    if (user_name_3.Text == "" || user_phone_3.Text == "" || user_id_3.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能提交订单！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_3.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_3.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    break;
                case 4:
                    if (user_name_4.Text == "" || user_phone_4.Text == "" || user_id_4.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能提交订单！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_4.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_4.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    break;
                case 5:
                    if (user_name_5.Text == "" || user_phone_5.Text == "" || user_id_5.Text == "")
                    {
                        MessageBoxz.ShowError("当前用户信息尚未填写完毕，不能提交订单！");
                        return;
                    }
                    if (!Common.Verificate.IsHandset(user_phone_5.Text))
                    {
                        err_message += " 【手机号码】格式错误！\n";
                    }
                    if (!Common.Verificate.CheckIDCard18(user_id_5.Text))
                    {
                        err_message += " 【身份证号】格式错误！\n";
                    }
                    if (err_message != "")
                    {
                        MessageBoxz.ShowError(err_message + "请按照正确的格式输入！");
                        err_message = "";
                        return;
                    }
                    break;
                default:
                    break;
            }
            int leftNote = Ticket_purchase.Instance.leftNote;
            if (leftNote < (add_user_id+1))
            {
                MessageBoxz.ShowError("余票不足！\n您当前共添加了"+ (add_user_id+1) + "位乘车人\n"+"当前余票"+leftNote+"张");
                return;
            }
            var w = new Payment.Payment(sum_user,price);
            w.Show();
            this.Close();
            return;
        }

        private void reselection_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!MessageBoxz.ShowQuestion("确定要重新选择吗？"))
            {
                return;
            }
            string type="";
            switch (type_icon.Text)
            {
                case "t":
                    type = "train";
                    break;
                case "v":
                    type = "car";
                    break;
                default:
                    type = "train";
                    break;
            }
            var w = new Ticket_purchase(type);
            w.Show();
            this.Close();
            return;
        }

    }
}
