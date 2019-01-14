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

namespace TicketSystem.ModuleDemo.PurchaseModule.Ticket_purchase
{
    /// <summary>
    /// Ticket_purchase.xaml 的交互逻辑
    /// </summary>
    public partial class Ticket_purchase : Windowz
    {
        public int id=-1;
        public DateTime startTime;
        public double predictTime;
        public double price;
        public int seats;
        public int leftNote;
        public static Ticket_purchase Instance;
        public Ticket_purchase(string type)
        {
            InitializeComponent();
            this.Closing += Windowz_Closing;
            Instance = this;
            t_type.Text = type;
            if (type == "train")
            {
                ticket_title.Text = "火车票";
            }
            else if (type == "car")
            {
                ticket_title.Text = "汽车票";
            }
            List<string> cities;
            using (var d = new ticketEntities())
            {
                var q = from t in d.city select t.city1;
                cities = q.ToList();
            }
            start.ItemsSource = cities;
            end.ItemsSource = cities;
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            int type;
            switch (t_type.Text)
            {
                case "train":
                    type = 1;
                    break;
                case "car":
                    type = 0;
                    break;
                default:
                    type = 0;
                    break;
            }
            string start_place = start.Text;
            string end_place = end.Text;
            int input_time = Common.TimeStamp.ConvertDateTimeInt((DateTime)time.SelectedDate);
            if (start_place == "" || end_place == "")
            {
                MessageBoxz.ShowError("请选择出发地或目的地！");
                return;
            }
            using (var d = new ticketEntities())
            {
                var q = (from t in d.vehicle
                        where t.type == type && t.startPlace == start_place && t.endPlace == end_place
                        && t.leftNote > 0 orderby t.startTime ascending
                        select t).ToList();
                //select new
                //{
                //    车次 = t.id,
                //    发车时间 = t.startTime,
                //    预计用时 = t.predictTime,
                //    票价 = t.price,
                //    座位总数 = t.seats,
                //    余票量 = t.leftNote
                //};
                var q2 = from t in q where Common.TimeStamp.ConvertDateTimeInt(t.startTime) > input_time select t;//比较时间
                /*这是linq to sql 和linq to entity的问题， linq to sql不支持格式化， linq to entity才支持。第一句的ToList()
                 * 是将linq to sql 的结果转成了entity,第二句采用的linq to entity语法 */
                datagrid.ItemsSource = q2;
                result_num.Text="共"+q2.Count()+"查询结果";
            }
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagrid.SelectedIndex==-1)
            {
                return;
            }
            vehicle vehicle = (vehicle)datagrid.SelectedItem;
            id = vehicle.id;
            startTime = vehicle.startTime;
            predictTime = (double)vehicle.predictTime;
            price = vehicle.price;
            seats = vehicle.seats;
            leftNote = vehicle.leftNote;

        }
        /// <summary>
        /// 解决点击提交会出现Windowz_Closing的问题,如果为true，则Windowz_Closing需要经过判断才能关闭，如果为false，则不需要判断
        /// </summary>
        private Boolean close_helper = true;
        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if (id == -1)
            {
                MessageBoxz.ShowWarning("当前没有选择车次，不能提交！");
                return;
            }
            if (MessageBoxz.ShowQuestion("确认要购买编号为" + id + "的车票吗"))
            {
                close_helper = false;
                this.Close();
                Boolean type;
                switch (t_type.Text)
                {
                    case "car":
                        type = false;
                        break;
                    case "train":
                        type = true;
                        break;
                    default:
                        type = false;
                        break;
                }
                var w = new Confirm_info(type,price);
                w.ShowDialog();
                return;
            }
            else
            {
                return;
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxz.ShowQuestion("确认取消购买车票吗？填写的表单将不会保存！", "确认取消"))
            {
                close_helper = false;
                this.Close();
            }
            else
            {
                return;
            }
        }

        void Windowz_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (close_helper == false)
            {
                return;
            }
            if (!MessageBoxz.ShowQuestion("确认取消购买车票吗？填写的表单将不会保存！", "确认取消"))
            {
                MessageBoxz.ShowInfo("asdf");
                e.Cancel = true;
            }
        }
    }
}
