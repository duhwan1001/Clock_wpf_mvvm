using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VewModelSample.Model;
using VewModelSample.ViewModel.Command;
using MessageBox = System.Windows.MessageBox;

namespace VewModelSample.ViewModel
{
    public class ChangeTimeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Model.ClockModel clockModel = null;

        private ViewModel.StandardChangeViewModel standardChangeViewModel = new StandardChangeViewModel();
        private ViewModel.ClockViewModel clockViewModel = new ClockViewModel();

        private ViewModel.ViewLogViewModel viewLogViewModel = new ViewLogViewModel();


        public ChangeTimeViewModel()
        {
            clockModel = ClockModel.Instance;
        }



        /// <summary>
        /// 
        /// </summary>

        public int TimeSelectIndex
        {
            get { return clockModel.timeSelectIndex; }
            set
            {
                clockModel.timeSelectIndex = value;

                if (clockModel.timeSelectIndex == 0)
                {
                    TimeSelectFormat = "오전";
                }
                else if (clockModel.timeSelectIndex == 1)
                {
                    TimeSelectFormat = "오후";
                }
                OnPropertyChanged("TimeSelectIndex");
            }
        }

        public String TimeSelectFormat
        {
            get { return clockModel.timeSelectFormat; }
            set { clockModel.timeSelectFormat = value; OnPropertyChanged("TimeSelectFormat"); }
        }

        public String SelectedDate
        {
            get
            {
                return clockModel.selectedDate;
            }
            set
            {
                clockModel.selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public String GetHour
        {
            get
            {
                return clockModel.getHour;
            }
            set
            {
                clockModel.getHour = value;
                OnPropertyChanged("GetHour");
            }
        }

        public String GetMin
        {
            get
            {
                return clockModel.getMin;
            }
            set
            {
                clockModel.getMin = value;
                OnPropertyChanged("GetMin");
            }
        }

        public String GetSec
        {
            get
            {
                return clockModel.getSec;
            }
            set
            {
                clockModel.getSec = value;
                OnPropertyChanged("GetSec");
            }
        }

        // 시간 변경 확인 버튼
        public ICommand ChangeTimeConfirm => new RelayCommand<object>(changeTimeConfirm, null);

        private void changeTimeConfirm(object e)
        {
            String function = "ChangeTime";

            if (GetHour == null || GetMin == null || GetSec == null)
            {
                MessageBox.Show("시간 입력.");
                return;
            }

            int hour = int.Parse(GetHour);
            int min = int.Parse(GetMin);
            int sec = int.Parse(GetSec);

            if (hour > 12 || min > 60 || sec > 60)
            {
                MessageBox.Show("시간 오류.");
                return;
            }

            if (TimeSelectFormat == "오후" && hour != 12)
            {
                hour += 12;
            }
            if (TimeSelectFormat == "오전" && hour == 12)
            {
                hour -= 12;
            }

            clockViewModel.TimeMode = 1;

            string selDate = SelectedDate;
            DateTime SetDateTime = Convert.ToDateTime(selDate);

            int year = SetDateTime.Year;
            int month = SetDateTime.Month;
            int day = SetDateTime.Day;

            DateTime timeToUse = new DateTime(year, month, day, hour, min, sec);

            // log
            String now = clockViewModel.Standard.ToString(standardChangeViewModel.StandardChangeViewFormat);

            clockViewModel.Standard = timeToUse;
            String afterChangeTime = timeToUse.ToString(standardChangeViewModel.StandardChangeViewFormat);

            String RecordText = now + "에서 변경한 시간 : " + afterChangeTime;

            viewLogViewModel.AddData(function, now, RecordText);


            MessageBox.Show("설정 완료");
        }


    }
}
