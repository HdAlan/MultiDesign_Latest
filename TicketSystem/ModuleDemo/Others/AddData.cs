using Wpfz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicketSystem.ModuleDemo.Others
{
    /// <summary>
    /// 每过1分钟都添加数据到vehicle
    /// </summary>
    class AddData
    {
        /// <summary>
        /// 添加数据时间间隔
        /// </summary>
        private const int time_Interval = 10;

        /// <summary>
        /// 城市名
        /// </summary>
        private List<city> cityList;

        /// <summary>
        /// 数据库中城市总数
        /// </summary>
        private int citySum = 0;

        /// <summary>
        /// 始发地
        /// </summary>
        private string startP;

        /// <summary>
        /// 目的地
        /// </summary>
        private string endP;

        /// <summary>
        /// 汽车每公里单价
        /// </summary>
        private float car_price;

        /// <summary>
        /// 汽车每公里用时(单位：分钟)
        /// </summary>
        private float car_time;

        /// <summary>
        /// 火车每公里单价
        /// </summary>
        private float train_price;

        /// <summary>
        /// 火车每公里用时(单位：分钟)
        /// </summary>
        private float train_time;

        /// <summary>
        /// 汽车座位池
        /// </summary>
        private int[] car_seat = { 54, 38 };

        /// <summary>
        /// 火车座位池
        /// </summary>
        private int[] train_seat = { 116, 118 };
        public AddData()
        {
            getCityInfo();
            getPriceInfo();
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(time_Interval)
            };
            timer.Tick += delegate
            {
                using (var d = new ticketEntities())
                {
                    List<vehicle> vehicles = new List<vehicle> { };
                    Random ran = new Random();
                    int sp, ep;
                    DateTime rantime;
                    float distance;
                    double v_price;
                    double pre_time;
                    int v_seat;
                    for (int i = 0; i < 4; i++)
                    {
                        sp = ran.Next(0, citySum - 1);
                        ep = ran.Next(0, citySum - 1);
                        rantime = RandomDate.GetRandomTime(DateTime.Now, DateTime.Parse("2019-06-30"));
                        while (sp == ep)
                        {
                            ep = ran.Next(0, citySum - 1);
                        }
                        startP = cityList[sp].city1;
                        endP = cityList[ep].city1;
                        distance = calcDistance.getDistance((float)cityList[sp].radius, (float)cityList[sp].angle, (float)cityList[ep].radius, (float)cityList[ep].angle);
                        switch (i / 2)//0为汽车  1为火车
                        {
                            case 0:
                                v_price = distance * car_price;
                                pre_time = distance * car_time;
                                v_seat = car_seat[ran.Next(0, car_seat.Length)];
                                break;
                            case 1:
                                v_price = distance * train_price;
                                pre_time = distance * train_time;
                                v_seat = train_seat[ran.Next(0, train_seat.Length)];
                                break;
                            default:
                                v_price = distance * car_price;
                                pre_time = distance * car_time;
                                v_seat = car_seat[ran.Next(0, car_seat.Length)];
                                break;
                        }
                        v_price = Math.Round(v_price, 2);
                        pre_time = Math.Round(pre_time, 2);
                        vehicles.Add(new vehicle { startPlace = startP, endPlace = endP, startTime = rantime, type = i / 2, price = v_price, predictTime = pre_time, seats = v_seat, leftNote = ran.Next(0, v_seat) });
                    }
                    d.vehicle.AddRange(vehicles);
                    try
                    {
                        d.SaveChanges();
                    }
                    catch (Exception)
                    {
                        MessageBoxz.ShowError("数据库错误！");
                    }
                }
            };
            timer.Start();
        }

        /// <summary>
        /// 获取关于城市的东西
        /// </summary>
        public void getCityInfo()
        {
            using (var d = new ticketEntities())
            {
                var q = from t in d.city select t;
                cityList = q.ToList();
            }
            citySum = cityList.Count();
        }

        /// <summary>
        /// 获取车辆价格与时间信息
        /// </summary>
        public void getPriceInfo()
        {
            List<price_info> price_Infos;
            using (var d = new ticketEntities())
            {
                var q = from t in d.price_info select t;
                price_Infos = q.ToList();
            }
            car_price = (float)price_Infos[0].car_price;
            car_time = (float)price_Infos[0].car_time;
            train_price = (float)price_Infos[0].train_price;
            train_time = (float)price_Infos[0].train_time;
        }
    }
}
