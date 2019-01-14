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

namespace TicketSystem.ModuleDemo.PersonData
{
    /// <summary>
    /// Win_Change.xaml 的交互逻辑
    /// </summary>
    public partial class Win_Change : Windowz
    {
        private int userId = PersonInfoWindow.userId;

        public string Type;
        public string Src;
        public string Des;
        public int Vid;

        /// <summary>
        /// 接受数据
        /// </summary>
        /// <param name="Type">类型</param>
        /// <param name="Src">起始地</param>
        /// <param name="Des">目的地</param>
        /// <param name="datetime">用户选定的改签日期</param>
        public Win_Change(string Type, string Src, string Des, int Vid)
        {
            InitializeComponent();

            
            // 设置背景图片
            ImageBrush b = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("../../Resources/Images/bg.jpg", UriKind.Relative)),
                Stretch = Stretch.Fill
            };
            this.Background = b;

            this.Type = Type;
            this.Src = Src;
            this.Des = Des;

            // 初始化控件内容
            src.Text = Src;
            des.Text = Des;
            type.Text = Type;
            

            // 每次选定日期
            datepicker.SelectedDateChanged += (s, e) =>
            {
                // 查询数据库显示
                //DateTime dateTime = (DateTime)datepicker.SelectedDate;
                string aType = Type;
                string aSrc = Src;
                string aDes = Des;

                DateTime boundTime = (DateTime)datepicker.SelectedDate;
                boundTime = boundTime.AddHours(24);
                using (var db = new ticketEntities())
                {
                    // 显示可改签的车辆信息
                    var temp = from o in db.order select o.vid;
                    var result = from r in db.vehicle
                                 //条件: 订单中有, 起始地与目的地相对应, 用户选定日期, 座位数大于0
                                 where r.type.ToString() == aType && r.startPlace.Equals(aSrc) && r.endPlace.Equals(aDes) && r.seats != 0 && r.startTime >= datepicker.SelectedDate && r.startTime < boundTime /*&& TimeoutException == time 日期相对应*/
                                 && !temp.Contains(r.id) && r.seats > 0
                                 select new Meta
                                 {
                                     Vid = r.id,
                                     Type = r.type.ToString(),
                                     Src = r.startPlace,
                                     Des = r.endPlace,
                                     Price = r.price,
                                     StartTime = r.startTime,
                                     PredictTime = (double)r.predictTime,
                                     Seats = r.seats,
                                     LeftNote = r.leftNote
                                 };
                    listview.ItemsSource = result.ToList();
                }
                listview.SelectedIndex = 0;
            };

            // 确定改签
            change.Click += (s, e) =>
            {
                Meta meta = listview.SelectedItem as Meta;
                if(meta != null)
                {
                    if (MessageBoxz.ShowQuestion("确定要改签吗?", "提示"))
                    {
                        // 更新数据库
                        var selectItem = listview.SelectedItem as Meta;
                        using(var db = new ticketEntities())
                        {
                            // 删除原订单
                            var item = from v in db.order
                                       where v.vid == Vid
                                       select v;
                            if(item.Count() > 0)
                                db.order.Remove(item.FirstOrDefault());

                            // 添加新订单
                            db.order.Add(new order { vid=selectItem.Vid, type=int.Parse(selectItem.Type), uid=userId});

                            // 票数减一
                            var xx = from x in db.vehicle
                                     where x.id == selectItem.Vid
                                     select x;
                            xx.FirstOrDefault().seats -= 1;

                            db.SaveChanges();
                        }
                            // 退出chuangkou
                            this.Close();
                    }
                }
            };
        }
    }
}
