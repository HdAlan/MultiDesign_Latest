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
namespace TicketSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ticketEntities context = new ticketEntities();
        public MainWindow()
        {

            InitializeComponent();
            //using(var c = new ticketEntities())
            //{
            var user = from t in context.user select t;
            users.ItemsSource = user.ToList();
            var q = from t in context.admin select t;
            admin.ItemsSource = q.ToList();
            var q2 = from t in context.order select t;
            data2.ItemsSource = q2.ToList();
            var q3 = from t in context.vehicle select t;
            vehicle.ItemsSource = q3.ToList();
            //}
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            if (btn.Name.Equals("BtnSave"))
            {
                //using(var c = new ticketEntities())
                //{
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("保存成功");
                    users.Items.Refresh();
                    admin.Items.Refresh();
                    data2.Items.Refresh();
                    vehicle.Items.Refresh();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //}
            }else if (btn.Name.Equals("BtnAdd"))
            {
                try
                {
                    context.vehicle.Add(vehicle.SelectedItem as vehicle);
                    context.SaveChanges();
                    MessageBox.Show("成功");
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else if (btn.Name.Equals("BtnDelete"))
            {
                try
                {
                    context.vehicle.Remove(vehicle.SelectedItem as vehicle);
                    context.SaveChanges();
                    vehicle.Items.Refresh();
                    MessageBox.Show("删除成功");
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

        }
    }
}
