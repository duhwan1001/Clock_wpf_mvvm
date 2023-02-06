using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VewModelSample.Model;
using VewModelSample.ViewModel.Command;
using System.Windows;

namespace VewModelSample.ViewModel
{
    internal class StandardChangeViewModel : INotifyPropertyChanged
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
        private ViewModel.ClockViewModel clockViewModel = new ClockViewModel();
        private ViewModel.ViewLogViewModel viewLogViewModel = new ViewLogViewModel();


        public StandardChangeViewModel()
        {
            clockModel = ClockModel.Instance;

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
        }

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
            String now = clockViewModel.Standard.ToString(StandardChangeViewFormat);
            String beforeKind = clockViewModel.Kind;

            if (StandardChangeIndex == 0)
            {
                clockViewModel.TimeMode = 0;
            }
            else if (StandardChangeIndex == 1)
            {
                clockViewModel.TimeMode = 2;
            }
            else if (StandardChangeIndex == 2)
            {
                clockViewModel.TimeMode = 1;
                clockViewModel.Standard = clockViewModel.Standard.Subtract(new TimeSpan(8, 0, 0));
            }

            clockViewModel.Kind = StandardChangeFormat;
            String RecordText = beforeKind + "에서 변경한 기준시 : " + clockViewModel.Kind;

            viewLogViewModel.AddData(function, now, RecordText);

            MessageBox.Show("수정 완료");
        }

    }
}
