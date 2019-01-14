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
using System.IO;
namespace TicketSystem.ModuleDemo.PersonData
{
    /// <summary>
    /// person_CutPrice.xaml 的交互逻辑
    /// </summary>
    public partial class person_CutPrice : Page
    {
        string path = @"discount.txt";
        public person_CutPrice()
        {
            InitializeComponent();
            cut_info.Text = "车费抵用券" + File.ReadAllText(path, Encoding.Default) + "元";
        }

        private void Btn_AngBird_Click(object sender, RoutedEventArgs e)
        {
            GameWin game = new GameWin();
            game.ShowDialog();
            cut_info.Text = "车费抵用券" + File.ReadAllText(path, Encoding.Default) + "元";
        }
    }
}
