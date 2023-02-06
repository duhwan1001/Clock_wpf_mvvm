using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VewModelSample.Model;

namespace VewModelSample.ViewModel
{
    internal class ViewLogViewModel : INotifyPropertyChanged
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

        public ViewLogViewModel()
        {
            clockModel = ClockModel.Instance;
        }

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

    }
}
