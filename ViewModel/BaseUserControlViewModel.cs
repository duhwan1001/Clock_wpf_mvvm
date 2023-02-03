using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using VewModelSample.View;
using VewModelSample.ViewModel.Command;

namespace VewModelSample.ViewModel
{
    class BaseUserControlViewModel : INotifyPropertyChanged
    {
        // 모델 사용할 때
        //private Model.BaseUserControlModel model = null;

        //public BaseUserControlViewModel()
        //{
        //    model = new Model.BaseUserControlModel();
        //}

        //public Model.BaseUserControlModel Model
        //{
        //    get { return model; }
        //    set { model = value; OnPropertyChanged("Model");
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public ICommand ChangeTimeCommand => new RelayCommand<object>(ViewChangeTime, null);
        private void ViewChangeTime(object e) // 날짜 및 시간 변경
        {
            ChangeTime changeTime = new ChangeTime();
            changeTime.Show();
        }

        public ICommand StandardChangeCommand => new RelayCommand<object>(ViewStandardChange, null);
        private void ViewStandardChange(object e) // 표준 시간대 변경
        {
            StandardChange standardChange = new StandardChange();
            standardChange.Show();
        }

        public ICommand CTFCommand => new RelayCommand<object>(ViewCTF, null);
        private void ViewCTF(object e) // 날짜 및 시간 형식 변경
        {
            TimeFormatChange timeFormatChange = new TimeFormatChange();
            timeFormatChange.Show();
        }

        public ICommand SetAlarmCommand => new RelayCommand<object>(ViewSetAlarm, null);
        private void ViewSetAlarm(object e) // 알람 설정
        {
            SetAlarm setAlarm = new SetAlarm();
            setAlarm.Show();
        }

        public ICommand StopwatchCommand => new RelayCommand<object>(ViewStopwatch, null);
        private void ViewStopwatch(object e) // 스톱 워치
        {
            StopwatchFunc stopwatchFunc = new StopwatchFunc();
            stopwatchFunc.Show();
        }

        public ICommand LogCommand => new RelayCommand<object>(ViewLog, null);
        private void ViewLog(object e) // datagrid
        {
            ViewLog viewLog= new ViewLog();
            viewLog.Show();
        }

    }
}
