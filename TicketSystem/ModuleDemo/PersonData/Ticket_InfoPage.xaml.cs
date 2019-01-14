
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

namespace TicketSystem.ModuleDemo.PersonData
{
    /// <summary>
    /// Ticket_InfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class Ticket_InfoPage : Page
    {
        private int userId = PersonInfoWindow.userId;
        /// <summary>
        /// <param name="listview">ListView控件,用来展示数据</param>
        /// <param name="change">改签按钮</param>
        /// <param name="unsub">退订按钮</param>
        /// <param name="Win_Change">改签窗口</param>
        /// </summary>
        public Ticket_InfoPage()
        {
            InitializeComponent();
            listview.SelectedIndex = 0;


            // 查询订单车辆信息并显示
            ShowAll();


            // 改签事件
            change.Click += (s, e) =>
            {
                // 获取选中行对应实例
               PersonData.Meta meta = listview.SelectedItem as PersonData.Meta;

                if (meta != null)
                {
                    // 跳转到改签窗口并传递数据
                    PersonData.Win_Change win_change = new PersonData.Win_Change(meta.Type, meta.Src, meta.Des, meta.Vid);
                    win_change.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    win_change.Height = 320;
                    win_change.Width = 580;
                    win_change.Title = "改签窗口";
                    win_change.ResizeMode = ResizeMode.NoResize;
                    win_change.ShowDialog();
                    // 刷新列表
                    ShowAll();
                }
                else if(listview.Items.Count == 0)
                {
                    MessageBoxz.ShowInfo("抱歉,暂无符合车次!");
                }
                else
                {
                    MessageBoxz.ShowInfo("请先选您要改签的车次!");
                }
            };

            // 退订事件
            unsub.Click += Unsub_Click;
        }

        private void Unsub_Click(object sender, RoutedEventArgs e)
        {
            // 获取选中行对应实例
            //Meta meta = listview.SelectedItem as Meta;

            var i = listview.SelectedIndex;
            if (i >= 0)
            {
                
                if (MessageBoxz.ShowQuestion("确定要退订吗?", "提示"))
                {
                    // 移除选中行
                    //list.RemoveAt(i);
                    //listview.Items.Refresh();


                    // 更改数据库数据
                    using(var db = new ticketEntities())
                    {
                        // 获得点击行的vid
                        var selectItem = listview.SelectedItem as PersonData.Meta;
                        var vid = selectItem.Vid;
                        // 操作数据库
                        var item = db.order.Where(M => M.vid == vid).FirstOrDefault();
                        db.order.Remove(item);
                        db.SaveChanges();
                        ShowAll();
                        listview.Items.Refresh();
                    }
                }
            }   
            else if (listview.Items.Count == 0)
            {
                MessageBoxz.ShowInfo("抱歉,暂无符合车次!");
            }
            else
            {
                MessageBoxz.ShowInfo("请先选您要退订的车次!");
            }
        }


        /// <summary>
        /// 查询并显示订单信息
        /// </summary>
        private void ShowAll()
        {
            using (var db = new ticketEntities())
            {
                var result1 = from u in db.user
                              join o in db.order on u.uid equals o.uid where o.uid == userId
                              select o.vid;
                var result2 = from v in db.vehicle
                              where result1.Contains(v.id)
                              select new PersonData.Meta
                              {
                                  Vid = v.id,
                                  Type = v.type.ToString(),
                                  Src = v.startPlace,
                                  Des = v.endPlace,
                                  Price = v.price,
                                  StartTime = v.startTime,
                                  PredictTime = (double)v.predictTime,
                                  Seats = v.seats,
                                  LeftNote = v.leftNote
                              };
                listview.ItemsSource = result2.ToList();
            }
        }
    }
}
