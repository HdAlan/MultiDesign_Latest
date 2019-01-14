using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.ModuleDemo.PersonData
{
    /// <summary>
    /// 这是一个用于绑定数据库行数据的数据类
    /// </summary>
    class Meta:INotifyPropertyChanged
    {
        private int _Vid;
        public int Vid
        {
            get => _Vid;
            set
            {
                _Vid = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Vid"));
                }
            }
        }

        private string _Type;
        public string Type
        {
            get => _Type;
            set
            {
                _Type = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Type"));
                }
            }
        }

        private string _Src;
        public string Src {
            get => _Src;
            set
            {
                _Src = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Src"));
                }
            }
        }

        private string _Des;
        public string Des {
            get => _Des;
            set
            {
                _Des = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Des"));
                }
            }
        }

        private DateTime _StartTime;
        public DateTime StartTime {
            get => _StartTime;
            set
            {
                _StartTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StartTime"));
                }
            }
        }
        
        private double _PredictTime;
        public double PredictTime
        {
            get => _PredictTime;
            set
            {
                _PredictTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PredictTime"));
                }
            }
        }

        private int _LeftNote;
        public int LeftNote {
            get => _LeftNote;
            set
            {
                _LeftNote = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LeftNote"));
                }
            }
        }

        private int _Seats;
        public int Seats {
            get => _Seats;
            set
            {
                _Seats = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Seats"));
                }
            }
        }

        private double _Price;
        public double Price
        {
            get => _Price;
            set
            {
                _Price = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Price"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
