using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.ModuleDemo.Others
{
    static class calcDistance
    {
        /// <summary>
        /// 根据相对开封的距离和正北方向的角度获取两地距离
        /// </summary>
        /// <param name="startRadius">始发地距开封距离</param>
        /// <param name="startAngle">始发地相对角度</param>
        /// <param name="endRadius">目的地距开封距离</param>
        /// <param name="endAngle">目的地相对角度</param>
        /// <returns></returns>
        public static float getDistance(float startRadius,float startAngle,float endRadius,float endAngle)
        {
            float angles= startAngle - endAngle;
            if (angles < 0)
            {
                angles = 0-angles;
            }
            if (angles > 180)
            {
                angles = 360 - angles;
            }
            float distance = startRadius * startRadius + endRadius * endRadius - 2 * startRadius * endRadius * (float)Math.Cos(angles);
            distance = (float)Math.Sqrt(distance);
            return distance;
        }
    }
}
