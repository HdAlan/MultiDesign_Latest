using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicketSystem.ModuleDemo.PurchaseModule.Common
{
    class Verificate
    {
        /// <summary>
        /// 18位身份证号码验证
        /// <param name="idNumber">用户输入身份证</param>
        /// </summary>
        public static bool CheckIDCard18(string idNumber)
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
        public static bool IsHandset(string str_handset)

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
        public static bool isEmail(string Em)
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
