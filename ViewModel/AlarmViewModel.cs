using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using VewModelSample.Model;
using static VewModelSample.Model.ClockModel;
using VewModelSample.ViewModel.Command;
using System.Windows;

namespace VewModelSample.ViewModel
{
    internal class AlarmViewModel : INotifyPropertyChanged
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

        private ViewModel.ChangeTimeViewModel changeTimeViewModel = new ChangeTimeViewModel();
        private ViewModel.StandardChangeViewModel standardChangeViewModel = new StandardChangeViewModel();
        private ViewModel.ClockViewModel clockViewModel = new ClockViewModel();
        private ViewModel.ViewLogViewModel viewLogViewModel = new ViewLogViewModel();
        
        public AlarmViewModel()
        {
            clockModel = ClockModel.Instance;
        }

        ObservableCollection<ClockModel.alarmData> _alarmDatas = null;
        public ObservableCollection<ClockModel.alarmData> alarmDatas
        {
            get
            {
                if (_alarmDatas == null)
                {
                    _alarmDatas = new ObservableCollection<ClockModel.alarmData>();
                }
                return _alarmDatas;
            }
            set
            {
                _alarmDatas = value;
                OnPropertyChanged("AlarmDatas");
            }
        }


        public int AlarmSequence
        {
            get { return clockModel.alaSequence += 1; }
            set { clockModel.alaSequence = value; OnPropertyChanged("Sequence"); }
        }


        public void AddAlarm(String targetTime)
        {
            ClockModel.alarmData alarmGrid = new ClockModel.alarmData();
            alarmGrid.alarmSequence = AlarmSequence;
            alarmGrid.targetTime = targetTime;

            alarmDatas.Add(alarmGrid);
        }

        // 알람 추가 버튼
        public ICommand AddAlarmConfirm => new RelayCommand<object>(addAlarmConfirm, null);
        private void addAlarmConfirm(object e)
        {
            String function = "SetAlarm";

            if (changeTimeViewModel.GetHour == null || changeTimeViewModel.GetMin == null || changeTimeViewModel.GetSec == null)
            {
                MessageBox.Show("시간 입력.");
                return;
            }

            int hour = int.Parse(changeTimeViewModel.GetHour);
            int min = int.Parse(changeTimeViewModel.GetMin);
            int sec = int.Parse(changeTimeViewModel.GetSec);

            if (hour > 12 || min > 60 || sec > 60)
            {
                MessageBox.Show("시간 오류.");
                return;
            }

            if (changeTimeViewModel.TimeSelectFormat == "오후" && hour != 12)
            {
                hour += 12;
            }
            if (changeTimeViewModel.TimeSelectFormat == "오전" && hour == 12)
            {
                hour -= 12;
            }

            string selDate = changeTimeViewModel.SelectedDate;
            DateTime SetDateTime = Convert.ToDateTime(selDate);

            int year = SetDateTime.Year;
            int month = SetDateTime.Month;
            int day = SetDateTime.Day;

            DateTime timeToUse = new DateTime(year, month, day, hour, min, sec);

            // log
            String now = clockViewModel.Standard.ToString(standardChangeViewModel.StandardChangeViewFormat);

            clockViewModel.Standard = timeToUse;
            String targetTime = timeToUse.ToString(standardChangeViewModel.StandardChangeViewFormat);

            String RecordText = "등록한 알람 : " + targetTime;

            viewLogViewModel.AddData(function, now, RecordText);
            AddAlarm(targetTime);

            Thread alarmThread = new Thread(waitingAlarm);
            alarmThread.IsBackground = true;
            alarmThread.Name = (AlarmSequence - 1).ToString();

            string[] arr = new string[2];

            arr[0] = alarmThread.Name;
            arr[1] = timeToUse.ToString(standardChangeViewModel.StandardChangeViewFormat);

            alarmThread.Start(arr);


            MessageBox.Show("설정 완료");
        }

        public ICommand RemoveCommand => new RelayCommand<object>(RemoveRow, null);
        private void RemoveRow(object parameter)
        {
            int index = alarmDatas.IndexOf(parameter as alarmData);
            if (index > -1 && index < alarmDatas.Count)
            {
                alarmDatas.RemoveAt(index);
            }
        }

        void waitingAlarm(Object obj)
        {
            while (true)
            {
                string[] datas = (string[])obj;

                if (clockViewModel.Standard.ToString(standardChangeViewModel.StandardChangeViewFormat).Equals(datas[1]))
                {
                    MessageBox.Show(datas[1] + "로 설정된 " + datas[0] + "번 알람", "알람");
                    viewLogViewModel.AddData("AlarmRinging", clockViewModel.Standard.ToString(standardChangeViewModel.StandardChangeViewFormat), datas[1] + "로 설정된 " + datas[0] + "번 알람");
                    break;
                }
            }
        }

    }
}
