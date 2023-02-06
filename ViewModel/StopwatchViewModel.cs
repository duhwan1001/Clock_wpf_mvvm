using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VewModelSample.Model;
using VewModelSample.ViewModel.Command;

namespace VewModelSample.ViewModel
{
    internal class StopwatchViewModel : INotifyPropertyChanged
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

        public StopwatchViewModel()
        {
            clockModel = ClockModel.Instance;
        }

        public String Stopwatch
        {
            get { return clockModel.stopWatch; }
            set
            {
                clockModel.stopWatch = value;
                OnPropertyChanged("StopWatch");
            }
        }

        public ICommand StartStopWatch => new RelayCommand<object>(setStopWatch, null);
        private void setStopWatch(object e)
        {
            //String function = "SetAlarm";

            //AddData(function, now, RecordText);
            //AddAlarm(targetTime);

            //Thread alarmThread = new Thread(waitingAlarm);
            //alarmThread.IsBackground = true;
            //alarmThread.Name = (AlarmSequence - 1).ToString();

            //string[] arr = new string[2];

            //alarmThread.Start(arr);


            //MessageBox.Show("설정 완료");
        }

        void startStopWatch(Object obj)
        {
            //Stopwatch sw = new Stopwatch();

            //sw.Start();

            //sw.Stop

            //while (true)
            //{
            //    Stopwatch = 
            //}
        }

    }
}
