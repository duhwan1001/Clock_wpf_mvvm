using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VewModelSample.ViewModel;
using VewModelSample.ViewModel.Command;

namespace VewModelSample.Model
{
    public class ClockModel
    {
        public int timeMode = 0;
        public DateTime standard;

        // ClockView

        public DateTime currentTime;
        public double radius;
        public Point center;
        public int hourHand;
        public int minHand;
        public int secHand;



        // ChangeTime
        public int timeSelectIndex = 0;
        public String timeSelectFormat;
        public String selectedDate;
        public String getHour;
        public String getMin;
        public String getSec;

        // TimeFormatChange
        public int temporaryDateIndex;
        public int temporaryTimeIndex;

        public String temporaryDateFormat;
        public String temporaryTimeFormat;

        public String viewTemporaryTime;
        public String viewTemporaryDate;

        // ClockMain

        public String dateFormat;
        public String timeFormat;
        public String kind = "KST(UTC+9)";

        public String viewCurrentDate;
        public String viewCurrentTime;
        public String viewCurrentKind;

        // StandardChange
        public int standardChangeIndex;
        public String standardChangeFormat;

        public String standardChangeViewFormat = "yyyy'년' M'월' d'일' dddd tt h:mm:ss";
        public String standardChangeView;

        // SelAlarm
        public int alaSequence = 0;
        public class alarmData
        {
            public int alarmSequence { get; set; }
            public String targetTime { get; set; }
        }

        // StopWatch
        public String stopWatch;

        // SelAlarm
        public int stopWatchSeq = 0;
        public class swData
        {
            public int stopWatchSeq { get; set; }
            public String saveTime { get; set; }
        }

        // log
        public int logSequence = 0;
        public class dataGridData
        {
            public int dataGridSequence { get; set; }
            public String dataGridFunction { get; set; }
            public String dataGridAddedTime { get; set; }
            public String dataGridSimpleRecordText { get; set; }
        }

    }
}
