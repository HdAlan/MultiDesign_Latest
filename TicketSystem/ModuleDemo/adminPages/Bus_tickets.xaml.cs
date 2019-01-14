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

namespace TicketSystem.ModuleDemo.adminPage
{
    /// <summary>
    /// Bus_tickets.xaml 的交互逻辑
    /// </summary>
    public partial class Bus_tickets : Page
    {
        private ticketEntities context = new ticketEntities(); //加一个全局变量context
        private int type = 0;
        public Bus_tickets()
        {
            InitializeComponent();
            Unloaded += delegate
            {
                context.Dispose();
            };

            var q = from t in context.vehicle where t.type == type select t;

            Busdata.Items.Clear();
            Busdata.ItemsSource = q.ToList();
        }

        private void Userdata_LoadingRow(object sender, DataGridRowEventArgs e)
        {
          //e.Row.Header = e.Row.GetIndex() + 1;
        } //加载序号
          //但有个问题----uid会跟着变


        private void Userdata_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        } //使enter键可以直接编辑下一个单元格
        private void Updata_Click(object sender, RoutedEventArgs e)
        {
            Busdata.Visibility = Visibility.Visible;
            Busdata.IsReadOnly = false;
            Txt.Visibility = Visibility.Visible;
        }




        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Busdata.IsReadOnly = false;
            Busdata.CanUserAddRows = true;
            Save.IsEnabled = false;
            Ok.Visibility = Visibility.Visible;
            Ok.Click += delegate
            {
                var item = Busdata.SelectedItem as vehicle;
                Save.IsEnabled = true;
                if (item != null)
                {
                    context.vehicle.Add(new vehicle { id = item.id, leftNote = item.leftNote, predictTime = item.predictTime, price = item.price, seats = item.seats, startPlace = item.startPlace, endPlace = item.endPlace, startTime = item.startTime, type = type });
                    context.SaveChanges();
                    Busdata.Items.Refresh();
                    ShowResult();
                }
                else if (item == null)
                {
                    MessageBoxz.ShowWarning("内容不能为空！");
                }

            };

        }//增加并点击确定才能保存




        private void Save_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                context.SaveChanges();
                Busdata.Items.Refresh();
                MessageBoxz.ShowInfo("保存成功");
                Busdata.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBoxz.ShowError(ex.Message, "保存失败");
            }

        }

        //private bool GetCellXY(DataGrid dg, ref int rowIndex, ref int columnIndex)
        //{
        //    var _cells = dg.SelectedCells;
        //    if (_cells.Any())
        //    {
        //        rowIndex = dg.Items.IndexOf(_cells.First().Item);
        //        columnIndex = _cells.First().Column.DisplayIndex;
        //        return true;
        //    }
        //    return false;
        //}     
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Txt.Text = "选中某一条记录进行删除";
            var item = Busdata.SelectedItem as vehicle;
            if (item != null)
            {
                try
                {

                    context.vehicle.Remove(item);
                    context.SaveChanges();
                    Busdata.Items.Refresh();
                    ShowResult();
                }
                catch (Exception ex)
                {
                    MessageBoxz.ShowError(ex.ToString(), "出错了");
                }
            }
            else
            {
                MessageBoxz.ShowError("请先选中一行", "提示");
            }


        }

        public void ShowResult()
        {
            using (var c = new ticketEntities())
            {
                var q = from t in c.vehicle
                        where t.type == type
                        select t;

                Busdata.ItemsSource = q.ToList();

                c.SaveChanges();
                Busdata.Items.Refresh();
            }
        }
    }
}
