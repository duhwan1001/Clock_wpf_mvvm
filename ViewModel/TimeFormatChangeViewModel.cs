using System;
using System.ComponentModel;
using System.Windows.Input;
using VewModelSample.Model;
using VewModelSample.ViewModel.Command;
using System.Windows;

namespace VewModelSample.ViewModel
{
    internal class TimeFormatChangeViewModel : INotifyPropertyChanged
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

        private Model.ClockModel clockModel = ClockModel.Instance;
        private ViewModel.ClockViewModel clockViewModel = new ClockViewModel();
        private ViewModel.StandardChangeViewModel standardChangeViewModel = new StandardChangeViewModel();
        private ViewModel.ViewLogViewModel viewLogViewModel = new ViewLogViewModel();

        public TimeFormatChangeViewModel()
        {
            //clockModel = 

            TemporaryDateFormat = clockViewModel.DateFormat;
            TemporaryTimeFormat = clockViewModel.TimeFormat;
        }


        /// <summary>
        /// /
        /// </summary>

        public int TemporaryDateIndex
        {
            get { return clockModel.temporaryDateIndex; }
            set
            {
                clockModel.temporaryDateIndex = value;

                if (clockModel.temporaryDateIndex == 0)
                {
                    TemporaryDateFormat = "yyyy'년' M'월' d'일' dddd";
                }
                else if (clockModel.temporaryDateIndex == 1)
                {
                    TemporaryDateFormat = "yyyy'년' M'월' d'일'";
                }
                else if (clockModel.temporaryDateIndex == 2)
                {
                    TemporaryDateFormat = "yy'년' M'월' d'일' dddd";
                }
                else if (clockModel.temporaryDateIndex == 3)
                {
                    TemporaryDateFormat = "yy'년' M'월' d'일'";
                }

                OnPropertyChanged("TemporaryDateIndex");
            }
        }

        public int TemporaryTimeIndex
        {
            get { return clockModel.temporaryTimeIndex; }
            set
            {
                clockModel.temporaryTimeIndex = value;

                if (clockModel.temporaryTimeIndex == 0)
                {
                    TemporaryTimeFormat = "tt h:mm:ss";
                }
                else if (clockModel.temporaryTimeIndex == 1)
                {
                    TemporaryTimeFormat = "tt hh:mm:ss";
                }
                else if (clockModel.temporaryTimeIndex == 2)
                {
                    TemporaryTimeFormat = "H:mm:ss";
                }
                else if (clockModel.temporaryTimeIndex == 3)
                {
                    TemporaryTimeFormat = "HH:mm:ss";
                }

                OnPropertyChanged("TemporaryTimeIndex");
            }
        }

        public String TemporaryDateFormat
        {
            get
            {
                return clockModel.temporaryDateFormat;
            }
            set
            {
                clockModel.temporaryDateFormat = value;
                OnPropertyChanged("TemporaryDateText");
            }
        }

        public String ViewTemporaryTime
        {
            get
            {
                return clockModel.viewTemporaryTime;
            }
            set
            {
                clockModel.viewTemporaryTime = value;
                OnPropertyChanged("ViewTemporaryTime");
            }
        }

        public String ViewTemporaryDate
        {
            get
            {
                return clockModel.viewTemporaryDate;
            }
            set
            {
                clockModel.viewTemporaryDate = value;
                OnPropertyChanged("ViewTemporaryDate");
            }
        }

        public String TemporaryTimeFormat
        {
            get
            {
                return clockModel.temporaryTimeFormat;
            }
            set
            {
                clockModel.temporaryTimeFormat = value;
                OnPropertyChanged("TemporaryTimeText");
            }
        }

        // 타임 포맷 변경 확인 버튼
        public ICommand TimeFormatChangeConfirm => new RelayCommand<object>(timeFormatChangeConfirm, null);

        private void timeFormatChangeConfirm(object e)
        {
            String beforeChangeDate = clockViewModel.DateFormat;
            String beforeChangeTime = clockViewModel.TimeFormat;

            clockViewModel.DateFormat = TemporaryDateFormat;
            clockViewModel.TimeFormat = TemporaryTimeFormat;


            String function = "Format Change";
            String now = clockViewModel.Standard.ToString(standardChangeViewModel.StandardChangeViewFormat);

            String RecordText = "날짜 포맷 변경 : " + beforeChangeDate + " => " + clockViewModel.DateFormat + "\n" + "타임 포맷 변경 : " + beforeChangeTime + " => " + clockViewModel.TimeFormat;

            viewLogViewModel.AddData(function, now, RecordText);

            MessageBox.Show("수정 완료");
        }

    }
}
