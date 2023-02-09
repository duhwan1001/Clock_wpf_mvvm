using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VewModelSample.ViewModel;
using VewModelSample.ViewModel.Command;

namespace VewModelSample.Model
{
    public class ClockModel
    {
        public int timeMode = 0;
        public DateTime standard;

        public string backgroundFilepath = "images/Background/whiteAndGrayGradient.jpg";

        // ClockView

        public DateTime currentTime;
        public double radius;
        public Point center;
        public int hourHand;
        public int minHand;
        public int secHand;

        public double hourX;
        public double hourY;
        public double minX;
        public double minY;
        public double secX;
        public double secY;

        public double hourAngle;
        public int minAngle;
        public int secAngle;

        // ClockFrame
        public class clockFrame
        {
            public double fX1;
            public double fY1;
            public double fX2;
            public double fY2;
            public SolidColorBrush fStrokeColor;
            public int fStrokeThickness;
            public Thickness fThickness;
        }
        // ChangeTime
        public int timeSelectIndex = 0;
        public String timeSelectFormat;
        public String selectedDate;

        public String setHour;
        public String setMin;
        public String setSec;

        public String getHour;
        public String getMin;
        public String getSec;

        public Double setHourAngle;
        public int setMinAngle;
        public int setSecAngle;

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
        public int alarmThreadSeq = 0;
        public class alarmData
        {
            public int alarmSequence { get; set; }
            public String targetTime { get; set; }
        }

        // StopWatch
        public String stopWatch = "00:00:00:00";

        public String swLeftText = "기록";
        public String swRightText = "시작";

        public Boolean swLeftButtonTF = false;

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
