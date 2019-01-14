using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
namespace TicketSystem.ModuleDemo.PersonData
{
    /// <summary>
    /// Person_InfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class Person_InfoPage : Page
    {
        private int userId = PersonInfoWindow.userId;
        // 籍贯取值范围
        List<String> places = new List<string>(new String[]
        { "黑龙江","吉林","辽宁","河北","河南","山东",
            "江苏","山西","陕西","甘肃","四川","青海","湖南",
            "湖北","江西","安徽","浙江","福建","广东","贵州","云南","海南","台湾"
        });

        //记录隐藏身份信息
        String hideStr = null;

        // 标志输入的有效性
        int[] flags;

        // 加载用户数据
        private void loadData()
        {
            using (var v = new ticketEntities())
            {
                var q = from user in v.user where user.uid == userId select user;

                // 加载当前用户的数据
                var u = q.FirstOrDefault();
                if (u.headPhoto == null)
                    head_image.Source = new BitmapImage(new Uri("../../../Resources/Images/icon.jpg", UriKind.RelativeOrAbsolute));
                else
                    head_image.Source = ByteArrayToBitmapImage(u.headPhoto);
                userName.Text = u.userName;
                real.Text = u.trueName;
                if(u.gender == null)
                {
                    gender.Visibility = Visibility.Visible;
                    gender_btn.Visibility = Visibility.Hidden;
                }
                else
                {
                    gender.Text = u.gender;
                }

                String temp = "";
                if(u.IDnumber != null && u.IDnumber.Length > 5)
                {
                    hideStr = u.IDnumber.Substring(6, 8);
                    temp = u.IDnumber.Remove(6, 8).Insert(6, "********");
                }
                id.Text = temp;

                phone.Text = u.phoneNumber;
                location.Text = u.nativePlace;
                email.Text = u.loginEmail;

                // 查询购票数
                var cou = from c in v.order where c.uid == userId select c;
                count.Text = cou.Count() + " ";
            }
        }

        /// <summary>
        /// 将图片保存为byte[]
        /// </summary>
        /// <param name="imagepath">图片路径</param>
        /// <returns>byte[]</returns>
        public byte[] GetPictureData(string imagepath)
        {
            FileStream file = new FileStream(imagepath, FileMode.Open);
            byte[] by = new byte[file.Length];
            file.Read(by, 0, by.Length);
            file.Close();
            return by;
        }

        /// <summary>
        /// 把byte[]转换成图片
        /// </summary>
        /// <param name="streamByte">byte[]类型</param>
        /// <returns>图片</returns>
        public static BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;
            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }

        /// <summary>
        /// 主要完成用户输入数据的校验
        /// <param name="modify">修改资料</param>
        /// <param name="confirm">确定修改</param>
        /// </summary>
        public Person_InfoPage()
        {
            InitializeComponent();

            // 加载数据
            loadData();

            //修改头像
            modify_head.Click += delegate
            {
                try
                {
                    Microsoft.Win32.OpenFileDialog d = new Microsoft.Win32.OpenFileDialog
                    {
                        InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location
                    };
                    if (d.ShowDialog() == true)
                    {
                        using (var db = new ticketEntities())
                        {
                            var current_user = (from user in db.user where user.uid == userId select user).FirstOrDefault();
                            current_user.headPhoto = GetPictureData(d.FileName);
                            db.SaveChanges();
                        }
                        var uri = new Uri(d.FileName, UriKind.RelativeOrAbsolute);
                        head_image.Source = new BitmapImage(uri);
                    }
                }
                catch
                {
                    MessageBoxz.ShowError("无效图片设置!!!");
                }
            };


            // 点击修改资料
            modify.Click += delegate
            {
                // 变换按钮
                modify.Visibility = Visibility.Collapsed;
                confirm.Visibility = Visibility.Visible;

                // 变换输入界面
                userName.IsReadOnly = false;
                real.IsReadOnly = false;

                gender.Visibility = Visibility.Collapsed;
                gender_btn.Visibility = Visibility.Visible;
                if ("男".Equals(gender.Text))
                    boy.IsChecked = true;
                else
                    girl.IsChecked = true;

                id.IsReadOnly = false;
                if(hideStr != null)
                {
                    id.Text = id.Text.Remove(6, 8).Insert(6, hideStr);
                    hideStr = null;
                }

                phone.IsReadOnly = false;
                location.IsReadOnly = false;

                userName.BorderBrush = Brushes.LightGray;
                real.BorderBrush = Brushes.LightGray;
                id.BorderBrush = Brushes.LightGray;
                phone.BorderBrush = Brushes.LightGray;
                location.BorderBrush = Brushes.LightGray;

                // 初始化有效标志
                flags = new int[6] { 1, 1, 1, 1, 1, 1 };

                ///
                /// 校验数据
                ///

                // 校验实名
                real.TextChanged += delegate
                {
                    String realName = real.Text;
                    int i;
                    if (realName.Length == 0)
                    {
                        // 未输入跳过
                        real_good.Visibility = Visibility.Collapsed;
                        real_wrong.Visibility = Visibility.Collapsed;
                        flags[0] = 1;
                        return;
                    }

                    for (i = 0; i < realName.Length; i++)
                    {
                        if (!((int)realName[i] > 0x4e00 && (int)realName[i] < 0x9fa5) || realName.Length > 4)
                        {
                            // 不是为汉字 或 长度过长
                            flags[0] = 0;
                            real_good.Visibility = Visibility.Collapsed;
                            real_wrong.Visibility = Visibility.Visible;
                            break;
                        }
                    }
                    if (i == realName.Length)
                    {
                        flags[0] = 1;
                        real_good.Visibility = Visibility.Visible;
                        real_wrong.Visibility = Visibility.Collapsed;
                    }
                };


                // 校验手机号
                phone.TextChanged += delegate
                {
                    String handSet = phone.Text;
                    if (handSet.Length == 0)
                    {
                        phone_good.Visibility = Visibility.Collapsed;
                        phone_wrong.Visibility = Visibility.Collapsed;
                        flags[3] = 1;
                        return;
                    }
                    if (IsHandset(handSet))
                    {
                        // 正确
                        flags[3] = 1;
                        phone_good.Visibility = Visibility.Visible;
                        phone_wrong.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        flags[3] = 0;
                        phone_good.Visibility = Visibility.Collapsed;
                        phone_wrong.Visibility = Visibility.Visible;
                    }

                };
                // 校验籍贯
                location.TextChanged += delegate
                {
                    String locat = location.Text;
                    if (locat.Length == 0)
                    {
                        locat_good.Visibility = Visibility.Collapsed;
                        locat_wrong.Visibility = Visibility.Collapsed;
                        flags[4] = 1;
                        return;
                    }
                    if (places.Contains(locat))
                    {
                        // 正确
                        flags[4] = 1;
                        locat_good.Visibility = Visibility.Visible;
                        locat_wrong.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        flags[4] = 0;
                        locat_good.Visibility = Visibility.Collapsed;
                        locat_wrong.Visibility = Visibility.Visible;
                    }
                };
                // 校验邮箱
                email.TextChanged += delegate
                {
                    String em = email.Text;
                    if (em.Length == 0)
                    {
                        email_good.Visibility = Visibility.Collapsed;
                        email_wrong.Visibility = Visibility.Collapsed;
                        flags[5] = 1;
                        return;
                    }
                    if (isEmail(em))
                    {
                        // 正确
                        flags[5] = 1;
                        email_good.Visibility = Visibility.Visible;
                        email_wrong.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        flags[5] = 0;
                        email_good.Visibility = Visibility.Collapsed;
                        email_wrong.Visibility = Visibility.Visible;
                    }
                };
                // 校验身份证号
                id.TextChanged += delegate
                {
                    String ID = id.Text;
                    if (ID.Length == 0)
                    {
                        id_good.Visibility = Visibility.Collapsed;
                        id_wrong.Visibility = Visibility.Collapsed;
                        flags[2] = 1;
                        return;
                    }
                    if (CheckIDCard18(ID))
                    {
                        // 正确
                        flags[2] = 1;
                        id_good.Visibility = Visibility.Visible;
                        id_wrong.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        flags[2] = 0;
                        id_good.Visibility = Visibility.Collapsed;
                        id_wrong.Visibility = Visibility.Visible;
                    }
                };
                hide();
            };

            // 点击确认修改
            confirm.Click += delegate
            {
                // 存在无效数据则返回
                foreach (var v in flags)
                {
                    if (v == 0)
                    {
                        MessageBoxz.ShowInfo("输入信息有误! 请修正..", "");
                        return;
                    }
                }

                // 向数据库提交数据
                using (var db = new ticketEntities())
                {
                    var q = from user in db.user where user.uid == userId select user;
                    var item = q.FirstOrDefault();
                    if (item != null)
                    {
                        item.userName = userName.Text;
                        item.trueName = real.Text;

                        if (boy.IsChecked == true)
                            gender.Text = "男";
                        else
                            gender.Text = "女";
                        item.gender = gender.Text;

                        item.IDnumber = id.Text;
                        item.phoneNumber = phone.Text;
                        item.nativePlace = location.Text;
                        db.SaveChanges();
                    }
                }
                loadData();

                // 变换按钮
                modify.Visibility = Visibility.Visible;
                confirm.Visibility = Visibility.Collapsed;

                // 设置textbox只读并隐藏边框 
                userName.IsReadOnly = true;
                real.IsReadOnly = true;

                gender_btn.Visibility = Visibility.Collapsed;
                gender.Visibility = Visibility.Visible;
                id.IsReadOnly = true;
                phone.IsReadOnly = true;
                location.IsReadOnly = true;

                real.BorderBrush = Brushes.Transparent;
                id.BorderBrush = Brushes.Transparent;
                phone.BorderBrush = Brushes.Transparent;
                location.BorderBrush = Brushes.Transparent;
                userName.BorderBrush = Brushes.Transparent;
                
                //取消监听
                id.TextChanged -= delegate { };

                // 提交已完成,隐藏标识
                hide();
            };
            
        }

        /// <summary>
        /// 隐藏提示标识
        /// </summary>
        public void hide()
        {
            real_good.Visibility = Visibility.Collapsed;
            real_wrong.Visibility = Visibility.Collapsed;

            id_good.Visibility = Visibility.Collapsed;
            id_wrong.Visibility = Visibility.Collapsed;

            phone_good.Visibility = Visibility.Collapsed;
            phone_wrong.Visibility = Visibility.Collapsed;

            email_good.Visibility = Visibility.Collapsed;
            email_wrong.Visibility = Visibility.Collapsed;

            locat_good.Visibility = Visibility.Collapsed;
            locat_wrong.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 18位身份证号码验证
        /// <param name="idNumber">用户输入身份证</param>
        /// </summary>
        public bool CheckIDCard18(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                long n = 0;
                if (long.TryParse(idNumber.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
                {
                    return false;//数字验证  
                }
                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
                if (address.IndexOf(idNumber.Remove(2)) == -1)
                {
                    return false;//省份验证  
                }
                string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                DateTime time = new DateTime();
                if (DateTime.TryParse(birth, out time) == false)
                {
                    return false;//生日验证  
                }
                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
                char[] Ai = idNumber.Remove(17).ToCharArray();
                int sum = 0;
                for (int i = 0; i < 17; i++)
                {
                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
                }
                int y = -1;
                Math.DivRem(sum, 11, out y);
                Console.WriteLine("Y的理论值: " + y);
                if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
                {
                    return false;//校验码验证  
                }
                return true;//符合GB11643-1999标准  
            }
            return false;
        }

        /// <summary>
        /// 手机号验证
        /// <param name="str_handset">用户输入手机号</param>
        /// </summary>
        public bool IsHandset(string str_handset)

        {
            // 电信手机号正则
            string dianxin = @"^1[3578][01379]\d{8}$";
            Regex dReg = new Regex(dianxin);
            // 联通手机号正则
            string liantong = @"^1[34578][01256]\d{8}$";
            Regex tReg = new Regex(liantong);
            // 移动手机号正则
            string yidong = @"^(134[012345678]\d{7}|1[34578][0123456789]\d{8})$";
            Regex yReg = new Regex(yidong);

            if (str_handset.Length == 11)
            {
                ////public static bool IsMatch(string input, string pattern);bool类型
                //bool isphone = Regex.IsMatch(str_handset, path);

                //return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5]+\d{9}");
                return dReg.IsMatch(str_handset) || tReg.IsMatch(str_handset) || yReg.IsMatch(str_handset);
            }
            return false;
        }

        /// <summary>
        /// 邮箱验证
        /// </summary>
        /// <param name="Em">用户输入邮箱</param>
        /// <returns></returns>
        public bool isEmail(string Em)
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
