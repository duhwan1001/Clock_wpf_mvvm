using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using VewModelSample.Model;
using VewModelSample.ViewModel.Command;
using static VewModelSample.Model.ClockModel;
using MessageBox = System.Windows.MessageBox;
using System.IO;
using System.Windows.Media;

namespace VewModelSample.ViewModel
{
    public class ClockViewModel : INotifyPropertyChanged
    {
        // PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        // 싱글톤
        private static ClockViewModel _instance = new ClockViewModel();

        public static ClockViewModel Instance
        {
            get { return _instance; }
        }

        private Model.ClockModel clockModel = null;

        public ClockViewModel()
        {
            clockModel = new Model.ClockModel();

            //CurrentDate = Standard.ToString(DateFormat);
            //CurrentTime = Standard.ToString(TimeFormat);
            //CurrentKind = "KST";

            DateFormat = "yyyy'년' M'월' d'일' dddd";
            TimeFormat = "tt h:mm:ss";

            if (StandardChangeIndex == 0)
            {
                StandardChangeView = DateTime.Now.ToString(StandardChangeViewFormat);
            }
            else if (StandardChangeIndex == 1)
            {
                StandardChangeView = DateTime.UtcNow.ToString(StandardChangeViewFormat);
            }
            else if (StandardChangeIndex == 2)
            {
                StandardChangeView = DateTime.UtcNow.Subtract(new TimeSpan(8, 0, 0)).ToString(StandardChangeViewFormat);
            }

            TemporaryDateFormat = DateFormat;
            TemporaryTimeFormat = TimeFormat;

            Thread RefreshThread = new Thread(Refresh);
            RefreshThread.IsBackground = true;
            RefreshThread.Start();
            RefreshThread.Name = nameof(Refresh);
        }

        // Refresh Thread
        void Refresh(Object obj)
        {
            while (true)
            {
                if (TimeMode == 0)
                {
                    Standard = DateTime.Now;
                }
                else if (TimeMode == 1)
                {
                    Thread.Sleep(1000);
                    Standard = Standard.AddSeconds(1);
                }
                else if (TimeMode == 2)
                {
                    Standard = DateTime.UtcNow;
                }

                SecAngle = Standard.Second * 6;
                MinAngle = Standard.Minute * 6;
                HourAngle = (Standard.Hour * 30) + (Standard.Minute * 0.5);

                // Clock
                ViewCurrentDate = Standard.ToString(DateFormat);
                ViewCurrentTime = Standard.ToString(TimeFormat);
                ViewCurrentKind = Kind;

                ViewTemporaryDate = Standard.ToString(TemporaryDateFormat);
                ViewTemporaryTime = Standard.ToString(TemporaryTimeFormat);
            }
        }

        public double HourAngle
        {
            get { return clockModel.hourAngle; }
            set { clockModel.hourAngle = value; OnPropertyChanged("HourAngle"); }
        }
    
        public int MinAngle
        {
            get { return clockModel.minAngle; }
            set { clockModel.minAngle = value; OnPropertyChanged("MinAngle"); }
        }
    
        public int SecAngle
        {
            get { return clockModel.secAngle; }
            set { clockModel.secAngle = value; OnPropertyChanged("SecAngle"); }
        }

        // 기준
        public DateTime Standard
        {
            get { return clockModel.standard; }
            set { clockModel.standard = value; OnPropertyChanged("SetTime"); }
        }

        public int TimeMode
        {
            get { return clockModel.timeMode; }
            set { clockModel.timeMode = value; OnPropertyChanged("TimeMode"); }
        }

        #region ChangeTimeSection -----------------------------------------------------------------------------------------------

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



        // 시간 변경 확인 버튼
        public String SetHour
        {
            get { return clockModel.setHour; }
            set 
            {
                clockModel.setHour = value;
                if(!(int.Parse(SetHour) > 12))
                {
                    SetHourAngle = int.Parse(SetHour) * 30;
                }
                OnPropertyChanged("SetHour"); }
        }
        public String SetMin
        {
            get { return clockModel.setMin; }
            set 
            { 
                clockModel.setMin = value;
                if (!(int.Parse(SetMin) > 60))
                {
                    SetMinAngle = int.Parse(value) * 6;
                }
                OnPropertyChanged("SetMin"); 
            }
        }
        public String SetSec
        {
            get { return clockModel.setSec; }
            set 
            {
                clockModel.setSec = value;
                if (!(int.Parse(SetSec) > 60))
                {
                    SetSecAngle = int.Parse(value) * 6;
                }
                OnPropertyChanged("SetSec"); 
            }
        }

        public Double SetHourAngle
        {
            get { return clockModel.setHourAngle; }
            set { clockModel.setHourAngle = value; OnPropertyChanged("SetHourAngle"); }
        }
        public int SetMinAngle
        {
            get { return clockModel.setMinAngle; }
            set { clockModel.setMinAngle = value; OnPropertyChanged("SetMinAngle"); }
        }
        public int SetSecAngle
        {
            get { return clockModel.setSecAngle; }
            set { clockModel.setSecAngle = value; OnPropertyChanged("SetSecAngle"); }
        }

        //SecAngle = Standard.Second* 6;
        //MinAngle = Standard.Minute* 6;
        //HourAngle = (Standard.Hour* 30) + (Standard.Minute* 0.5);

        public void UpdateHour(object sender, RoutedEventArgs e)
        {
            TextBox textbox = ((TextBox)sender);
            SetHourAngle = (int.Parse(SetHour) * 30) + (int.Parse(SetMin) * 0.5);
        }        
        public void UpdateMin(object sender, RoutedEventArgs e)
        {
            TextBox textbox = ((TextBox)sender);
            SetMinAngle = int.Parse(SetMin) * 6;
        }        
        public void UpdateSec(object sender, RoutedEventArgs e)
        {
            TextBox textbox = ((TextBox)sender);
            SetSecAngle = int.Parse(SetSec) * 6;
        }

        public ICommand ChangeTimeConfirm => new RelayCommand<object>(changeTimeConfirm, null);

        private void changeTimeConfirm(object e)
        {
            if (SetHour == null || SetMin == null || SetSec == null)
            {
                MessageBox.Show("시간을 입력하세요", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int hour = int.Parse(SetHour);
            int min = int.Parse(SetMin);
            int sec = int.Parse(SetSec);

            if (hour > 12 || min > 60 || sec > 60)
            {
                MessageBox.Show("정확한 시간을 입력하세요", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
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

            TimeMode = 1;

            string selDate = SelectedDate;
            DateTime SetDateTime = Convert.ToDateTime(selDate);

            int year = SetDateTime.Year;
            int month = SetDateTime.Month;
            int day = SetDateTime.Day;

            DateTime timeToUse = new DateTime(year, month, day, hour, min, sec);

            // log
            String now = Standard.ToString(StandardChangeViewFormat);

            Standard = timeToUse;
            String afterChangeTime = timeToUse.ToString(StandardChangeViewFormat);

            String RecordText = now + "에서 변경한 시간 : " + afterChangeTime;

            AddData("ChangeTime", now, RecordText);


            MessageBox.Show("시간 설정 완료", "메세지", MessageBoxButton.OK, MessageBoxImage.Information);

            SetSecAngle = 0;
            SetMinAngle = 0;
            SetHourAngle = 0;

            


        }


        #endregion

        #region TimeFormatChangeSection -----------------------------------------------------------------------------------------------

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
            String beforeChangeDate = DateFormat;
            String beforeChangeTime = TimeFormat;

            DateFormat = TemporaryDateFormat;
            TimeFormat = TemporaryTimeFormat;


            String function = "Format Change";
            String now = Standard.ToString(StandardChangeViewFormat);

            String RecordText = "날짜 포맷 변경 : " + beforeChangeDate + " => " + DateFormat + "\n" + "타임 포맷 변경 : " + beforeChangeTime + " => " + TimeFormat;

            AddData(function, now, RecordText);

            MessageBox.Show("수정 완료");
        }

        #endregion

        #region ClockMain -----------------------------------------------------------------------------------------------

        public String DateFormat
        {
            get { return clockModel.dateFormat; }
            set
            {
                clockModel.dateFormat = value;
                OnPropertyChanged("SetDateFormat");
            }
        }

        public String TimeFormat
        {
            get { return clockModel.timeFormat; }
            set
            {
                clockModel.timeFormat = value;
                OnPropertyChanged("SetTimeFormat");
            }
        }

        public String Kind
        {
            get { return clockModel.kind; }
            set { clockModel.kind = value; OnPropertyChanged("Kind"); }
        }

        public String ViewCurrentDate
        {
            get
            {
                return clockModel.viewCurrentDate;
            }
            set
            {
                clockModel.viewCurrentDate = value;
                OnPropertyChanged("ViewCurrentDate");
            }
        }

        public String ViewCurrentTime
        {
            get
            {
                return clockModel.viewCurrentTime;
            }
            set
            {
                clockModel.viewCurrentTime = value;
                OnPropertyChanged("ViewCurrentTime");
            }
        }

        public String ViewCurrentKind
        {
            get
            {
                return clockModel.viewCurrentKind;
            }
            set
            {
                clockModel.viewCurrentKind = value;
                OnPropertyChanged("ViewCurrentKind");
            }
        }

        #endregion

        #region StandardChange -----------------------------------------------------------------------------------------------

        public String StandardChangeFormat
        {
            get { return clockModel.standardChangeFormat; }
            set { clockModel.standardChangeFormat = value; OnPropertyChanged("StandardChangeFormat"); }
        }

        public int StandardChangeIndex
        {
            get { return clockModel.standardChangeIndex; }
            set
            {
                clockModel.standardChangeIndex = value;

                if (clockModel.standardChangeIndex == 0)
                {
                    StandardChangeFormat = "KST(UTC+9)";
                    StandardChangeView = DateTime.Now.ToString(StandardChangeViewFormat);
                }
                else if (clockModel.standardChangeIndex == 1)
                {
                    StandardChangeFormat = "UTC(UTC+0)";
                    StandardChangeView = DateTime.UtcNow.ToString(StandardChangeViewFormat);
                }
                else if (clockModel.standardChangeIndex == 2)
                {
                    StandardChangeFormat = "PST(UTC-8)";
                    StandardChangeView = DateTime.UtcNow.Subtract(new TimeSpan(8, 0, 0)).ToString(StandardChangeViewFormat);
                }

                OnPropertyChanged("StandardChangeIndex");
            }
        }

        public String StandardChangeViewFormat
        {
            get { return clockModel.standardChangeViewFormat; }
            set { clockModel.standardChangeViewFormat = value; OnPropertyChanged("StandardChangeViewFormat"); }
        }

        public String StandardChangeView
        {
            get { return clockModel.standardChangeView; }
            set { clockModel.standardChangeView = value; OnPropertyChanged("StandardChangeView"); }
        }

        // 표준시 변경 확인 버튼
        public ICommand StandardChangeConfirm => new RelayCommand<object>(standardChangeConfirm, null);

        private void standardChangeConfirm(object e)
        {
            String function = "Standard Change";
            String now = Standard.ToString(StandardChangeViewFormat);
            String beforeKind = Kind;

            if (StandardChangeIndex == 0)
            {
                TimeMode = 0;
            }
            else if (StandardChangeIndex == 1)
            {
                TimeMode = 2;
            }
            else if (StandardChangeIndex == 2)
            {
                TimeMode = 1;
                Standard = Standard.Subtract(new TimeSpan(8, 0, 0));
            }

            Kind = StandardChangeFormat;
            String RecordText = beforeKind + "에서 변경한 기준시 : " + Kind;

            AddData(function, now, RecordText);

            MessageBox.Show("수정 완료");
        }

        #endregion

        #region viewLog -----------------------------------------------------------------------------------------------

        ObservableCollection<ClockModel.dataGridData> _logDatas = null;
        public ObservableCollection<ClockModel.dataGridData> logDatas
        {
            get
            {
                if (_logDatas == null)
                {
                    _logDatas = new ObservableCollection<ClockModel.dataGridData>();
                }
                return _logDatas;
            }
            set
            {
                _logDatas = value;
                OnPropertyChanged("logDatas");
            }
        }


        public int LogSequence
        {
            get { return clockModel.logSequence += 1; }
            set { clockModel.logSequence = value; OnPropertyChanged("Sequence"); }
        }


        public void AddData(String function, String AddedTime, String RecordText)
        {
            ClockModel.dataGridData dataGrid = new ClockModel.dataGridData();
            dataGrid.dataGridSequence = LogSequence;
            dataGrid.dataGridFunction = function;
            dataGrid.dataGridAddedTime = AddedTime;
            dataGrid.dataGridSimpleRecordText = RecordText;

            logDatas.Add(dataGrid);
        }



        #endregion

        #region SetAlarm --------------------------------------------------------------------------------------
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

            string selDate = SelectedDate;
            DateTime SetDateTime = Convert.ToDateTime(selDate);

            int year = SetDateTime.Year;
            int month = SetDateTime.Month;
            int day = SetDateTime.Day;

            DateTime timeToUse = new DateTime(year, month, day, hour, min, sec);

            // log
            String now = Standard.ToString(StandardChangeViewFormat);

            Standard = timeToUse;
            String targetTime = timeToUse.ToString(StandardChangeViewFormat);

            String RecordText = "등록한 알람 : " + targetTime;

            AddData(function, now, RecordText);
            AddAlarm(targetTime);

            Thread alarmThread = new Thread(waitingAlarm);
            alarmThread.IsBackground = true;
            alarmThread.Name = (AlarmSequence - 1).ToString();

            string[] arr = new string[2];

            arr[0] = alarmThread.Name;
            arr[1] = timeToUse.ToString(StandardChangeViewFormat);

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

                if (Standard.ToString(StandardChangeViewFormat).Equals(datas[1]))
                {
                    MessageBox.Show(datas[1] + "로 설정된 " + datas[0] + "번 알람", "알람");
                    AddData("AlarmRinging", Standard.ToString(StandardChangeViewFormat), datas[1] + "로 설정된 " + datas[0] + "번 알람");
                    break;
                }
            }
        }

        #endregion

        #region StopWatch ------------------------------------------------------------------------------------------------미완성
        public String StopWatch
        {
            get { return clockModel.stopWatch; }
            set
            {
                clockModel.stopWatch = value;
                OnPropertyChanged("StopWatch");
            }
        }

        ObservableCollection<ClockModel.swData> _swDatas = null;
        public ObservableCollection<ClockModel.swData> swDatas
        {
            get
            {
                if (_swDatas == null)
                {
                    _swDatas = new ObservableCollection<ClockModel.swData>();
                }
                return _swDatas;
            }
            set
            {
                _swDatas = value;
                OnPropertyChanged("AlarmDatas");
            }
        }
        public int StopWatchSeq
        {
            get { return clockModel.stopWatchSeq += 1; }
            set { clockModel.stopWatchSeq = value; OnPropertyChanged("StopWatchSeq"); }
        }

        public void AddSwRecord()
        {
            ClockModel.swData swData = new ClockModel.swData();
            swData.stopWatchSeq = StopWatchSeq;
            swData.saveTime = StopWatch;

            swDatas.Add(swData);
        }

        public ICommand StartStopWatch => new RelayCommand<object>(setStopWatch, null);

        int ThreadCount = 0;
        private void setStopWatch(object e)
        {
            Thread SWThread = new Thread(startStopWatch);
            SWThread.IsBackground = true;
            SWThread.Name = nameof(SWThread);
            SWThread.Start();

            AddData("Stopwatch", Standard.ToString(StandardChangeViewFormat), "스톱워치 시작");
        }

        public ICommand RecordStopwatch => new RelayCommand<object>(recordStopwatch, null);
        private void recordStopwatch(object e)
        {
            AddSwRecord();
        }


        int stopwatchFlag = 0;
        public void startStopWatch(Object obj)
        {
            Stopwatch sw = new Stopwatch();


            if(stopwatchFlag == 0)
            {
                sw.Start();
                stopwatchFlag = 1;
            }
            else if(stopwatchFlag == 1)
            {
                sw.Stop();
                stopwatchFlag = 0;
            }
            

            //sw.Stop();

            while (true)
            {
                StopWatch = sw.Elapsed.ToString("hh\\:mm\\:ss\\.ff");
            }
        }
        #endregion

        #region Buttons -----------------------------------------------------------------------------------------------

        public String BackgroundFilepath
        {
            get { return clockModel.backgroundFilepath; }
            set { clockModel.backgroundFilepath= value; OnPropertyChanged("BackgroundFilepath"); }
        }

        public ICommand ChangeBackground => new RelayCommand<object>(changeBackground, null);
        private void changeBackground(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG files(*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|PNG files (*.png)|*.png|All files (*.*)|*.*";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName).ToLower() == ".jpg" || Path.GetExtension(openFileDialog.FileName) == ".jpeg" || Path.GetExtension(openFileDialog.FileName) == ".png")
                {
                    BackgroundFilepath = openFileDialog.FileName;
                    AddData("BackgroundChange", Standard.ToString(StandardChangeViewFormat), "배경 변경 -> " + openFileDialog.FileName);
                    MessageBox.Show("배경을 성공적으로 변경하였습니다..", "성공", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("지원하는 파일 형식이 아닙니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        #endregion
    }
}
