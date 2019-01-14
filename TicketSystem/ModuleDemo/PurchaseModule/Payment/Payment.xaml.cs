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
using System.Windows.Shapes;
using Wpfz;

namespace TicketSystem.ModuleDemo.PurchaseModule.Payment
{
    /// <summary>
    /// Payment.xaml 的交互逻辑
    /// </summary>
    public partial class Payment : Windowz
    {
        private string verificate = "";
        private double price;
        private int sum_user;
        public Payment(int userNum,double t_price)
        {
            InitializeComponent();
            price = t_price;
            sum_user = userNum;
            //MessageBoxz.ShowWarning("请扫码，并将扫码得到的数字填写到输入框中以完成付款操作！");
            QrcodeImg();
        }

        public void QrcodeImg()
        {
            Random r = new Random();
            verificate = string.Format("{0:D6}", r.Next(0, 999999));
            string str = "您需要支付 " + price*sum_user + " 元\n请您在相应的输入框中输入下列验证码以完成付款\n验证码：" + verificate;
            var qr = new QRcode.QRcode();
            qrcode.Source = qr.createQRCode(str, 300, 300);
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            int id = Ticket_purchase.Ticket_purchase.Instance.id;
            DateTime startTime = Ticket_purchase.Ticket_purchase.Instance.startTime;
            double predictTime = Ticket_purchase.Ticket_purchase.Instance.predictTime;
            double price = Ticket_purchase.Ticket_purchase.Instance.price;
            int seats = Ticket_purchase.Ticket_purchase.Instance.seats;
            int leftNote = Ticket_purchase.Ticket_purchase.Instance.leftNote;
            int vehicle_type;//0为汽车 1为火车
            switch (Ticket_purchase.Ticket_purchase.Instance.t_type.Text)
            {
                case "car":
                    vehicle_type = 0;
                break;
                case "train":
                    vehicle_type = 1;
                    break;
                default:
                    vehicle_type = 0;
                    break;
            }
            //int userId = 1;
            int userId;
            string userLoginEmail;
            //同步用户，为与登录时的用户一致，使用邮箱检索，确定userId
            userLoginEmail = Login.Login.instrance.accountNumber.Text.ToString();
            using (var c = new ticketEntities())
            {
                var q = from t in c.user where t.loginEmail == userLoginEmail select t;
                userId = q.FirstOrDefault().uid;
            }

            if (input_ver.Password == "")
            {
                MessageBoxz.ShowError("请输入付款验证码！");
                return;
            }
            if (input_ver.Password == verificate)
            {
                using (var d=new ticketEntities())
                {
                    vehicle v = d.vehicle.Find(id);
                    v.leftNote = v.leftNote - sum_user;

                    List<order> ord = new List<order>{};
                    List<Append_User> append = new List<Append_User> { };
                    string trueName;
                    string phoneNumber;
                    string IDnumber;
                    for (int i = 0; i < sum_user; i++)
                    {
                        ord.Add(new order { type= vehicle_type, uid=userId, vid=id});
                        switch (i)
                        {
                            case 0:
                                trueName = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_name_0.Text;
                                phoneNumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_phone_0.Text;
                                IDnumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_id_0.Text;
                                break;
                            case 1:
                                trueName = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_name_1.Text;
                                phoneNumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_phone_1.Text;
                                IDnumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_id_1.Text;
                                break;
                            case 2:
                                trueName = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_name_2.Text;
                                phoneNumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_phone_2.Text;
                                IDnumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_id_2.Text;
                                break;
                            case 3:
                                trueName = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_name_3.Text;
                                phoneNumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_phone_3.Text;
                                IDnumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_id_3.Text;
                                break;
                            case 4:
                                trueName = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_name_4.Text;
                                phoneNumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_phone_4.Text;
                                IDnumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_id_4.Text;
                                break;
                            case 5:
                                trueName = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_name_5.Text;
                                phoneNumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_phone_5.Text;
                                IDnumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_id_5.Text;
                                break;
                            default:
                                trueName = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_name_0.Text;
                                phoneNumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_phone_0.Text;
                                IDnumber = PurchaseModule.Ticket_purchase.Confirm_info.Instance.user_id_0.Text;
                                break;
                        }
                        append.Add(new Append_User { fromuid=userId, trueName=trueName, phoneNumber=phoneNumber,IDnumber=IDnumber });
                    }
                    try
                    {
                        d.order.AddRange(ord);
                        //Append_User表用于存储多余的订单信息，此项目暂时用不上
                        //d.Append_User.AddRange(append);
                        d.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBoxz.ShowError("数据库出错！");
                    }
                }
                MessageBoxz.ShowInfo("付款成功");
                this.Close();
            }
            else
            {
                MessageBoxz.ShowError("付款失败，请重新尝试");
                QrcodeImg();
            }
        }
    }
}
