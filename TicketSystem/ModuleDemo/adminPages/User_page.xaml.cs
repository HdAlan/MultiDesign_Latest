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



namespace TicketSystem.ModuleDemo.adminPages
{
    /// <summary>
    /// User_page.xaml 的交互逻辑
    /// </summary>
    public partial class User_page : Page
    {
        
       
        private ticketEntities context = new ticketEntities(); //加一个全局变量context
      

        public User_page()
        {
          
            InitializeComponent();
            
            //using (var c = new ticketEntities())
            //{
            //    var q = from t in c.user
            //            select t;
            //    Userdata.Items.Clear();
            //    Userdata.ItemsSource = q.ToList();

            //    c.SaveChanges();
            //}
            Unloaded += delegate
            {
                context.Dispose();
            };

            var q = from t in context.user select t;
                    //select new
                    //{
                    //    用户ID=t.uid,
                    //    用户名=t.userName,
                    //    邮箱=t.loginEmail,
                    //    密码=t.loginPWD,
                    //    身份证号=t.IDnumber,

                    //};
                    

         Userdata.Items.Clear();
         Userdata.ItemsSource = q.ToList();
        }
        //private void Userdata_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    e.Row.Header = e.Row.GetIndex() + 1;
        //} 


        private void Userdata_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            
            if (e.Key == Key.Enter)
            {
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                e.Handled = true;
            }
        } 
        private void Updata_Click(object sender, RoutedEventArgs e)
        {
            Txt.Visibility = Visibility.Visible;
            Userdata.IsReadOnly = false;
            Txt.Text = "双击记录进行修改";         
        }




        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Userdata.IsReadOnly = false;
            Userdata.CanUserAddRows = true;
            Save.IsEnabled = false;
            Ok.Visibility = Visibility.Visible;
            Ok.Click += delegate
            {
                var item = Userdata.SelectedItem as user;
                Save.IsEnabled = true;
                if (item != null)
                {
                    try
                    {
                        context.user.Add(item);
                        context.SaveChanges();
                    }catch(Exception ex)
                    {
                        MessageBoxz.ShowError(ex.Message,"添加失败");
                    }
                    
                    Userdata.Items.Refresh();
                    ShowResult();
                }
                else if (item == null)
                {
                    Userdata.IsReadOnly = true;
                    Userdata.CanUserAddRows = false;
                }

            };

        }




        private void Save_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                
                context.SaveChanges();
                Userdata.Items.Refresh();
                MessageBoxz.ShowInfo("保存成功");
                Userdata.IsReadOnly = true;
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
           
            var item = Userdata.SelectedItem as user;
            if(item != null)
            {
                try
                {

                    context.user.Remove(item);
                    context.SaveChanges();
                    Userdata.Items.Refresh();
                    ShowResult();
                }
                catch (Exception ex)
                {
                    MessageBoxz.ShowError(ex.Message, "出错了");
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
                var q = from t in c.user
                        select t;

                Userdata.ItemsSource = q.ToList();
                try
                {
                    c.SaveChanges();
                }catch(Exception ex)
                {
                    MessageBoxz.ShowError(ex.Message, "错误");
                }
                
                Userdata.Items.Refresh();
            }
        }
    }

}
