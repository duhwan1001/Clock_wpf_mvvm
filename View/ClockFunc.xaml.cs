using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VewModelSample.ViewModel;

namespace VewModelSample.View
{
    /// <summary>
    /// Clock.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ClockFunc : UserControl
    {
        public ClockFunc()
        {
            InitializeComponent();
            this.DataContext = ClockViewModel.Instance;

            ClockSettng();

            DispatcherTimer timer = new DispatcherTimer(); // DispatcherTimer에는 Thread가 내포되어 있다.
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500); // 1초마다 갱신함
            timer.Tick += new EventHandler(dispatcherTimer_Tick); // 
            timer.Start();
        }

        private DateTime currentTime;
        private double radius;
        private Point Center;
        private int hourHand;
        private int minHand;
        private int secHand;

        // 이미 짜여져있는 XAML의 요소들의 값을 통해 시계를 그리는데 필요한 값들을 저장함
        private void ClockSettng()
        {
            Center = new Point(Face.Width / 2, Face.Height / 2); // Face : Elipse(타원) 속성의 Name
            radius = Face.Width / 2; // 반지름

            hourHand = (int)(radius * 0.6); // 반지름 * 0.6 : 시 분 초의 길이
            minHand = (int)(radius * 0.8);
            secHand = (int)(radius * 0.9);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Grid1.Children.Clear(); // 화면 갱신을 위한 클리어

            currentTime = Convert.ToDateTime(SetTime.Text); // 현재 시간 불러옴 // 이거 수정 어떻게할건지 고민 해야함

            DrawClockFace();

            // 60F : 단정밀도 부동 소수점
            double radHr = (currentTime.Hour % 12 + currentTime.Minute / 60F) * 30 * Math.PI / 180;
            double radMin = (currentTime.Minute) * 6 * Math.PI / 180;
            double radSec = (currentTime.Second) * 6 * Math.PI / 180;

            // ↑ 현재 시간을 통해 계산 후 좌표값 얻어서 DrawHands에 파라미터로 넣음

            DrawHands(radHr, radMin, radSec);
        }

        // 라인 그리는거 메서드로 만듬 파라미터로 x1 y1 시작점, x2 y2 끝점을 받고 색깔, 두께, 위치를 잡기 위한 margin을 받는다
        private void DrawLine(double x1, double y1, double x2, double y2, SolidColorBrush color, int thickness, Thickness margin)
        {
            Line line = new Line(); // 새로운 라인 객체 생성
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;

            line.Stroke = color;
            line.StrokeThickness = thickness;
            line.Margin = margin;
            Grid1.Children.Add(line);
        }

        private void DrawHands(double radHr, double radMin, double radSec)
        {
            // hour
            DrawLine(hourHand * Math.Sin(radHr), // x값 시작점
                    -hourHand * Math.Cos(radHr), // y값 시작점
                    0, // x값 끝점
                    0, // y값 끝점
                    Brushes.Green, // 색
                    10, // 두께
                    new Thickness(Center.X, Center.Y, 0, 0)); // margin값. 근데 왜 Thickness로 객체 생성해서 주는거지

            // minute
            DrawLine(minHand * Math.Sin(radMin),
                    -minHand * Math.Cos(radMin),
                    0,
                    0,
                    Brushes.Blue,
                    8,
                    new Thickness(Center.X, Center.Y, 0, 0));

            // second
            DrawLine(secHand * Math.Sin(radSec),
                    -secHand * Math.Cos(radSec),
                    0,
                    0,
                    Brushes.Red,
                    3,
                    new Thickness(Center.X, Center.Y, 0, 0));

            // ↑ 바늘 좌표 계산해서 DrawLine 메서드에 대입하여 바늘 실시간 업데이트. 삼각함수 사용하는건데 솔직히 잘 모르겠음

            // center : 중앙에 찍는 시계 코어?
            Ellipse center = new Ellipse(); // 타원 객체 새로 생성
            center.Margin = new Thickness(140, 140, 0, 0); // 두께 지정 left top right bottom
            center.Stroke = Brushes.Brown; // 윤곽선
            center.StrokeThickness = 2; // 윤곽선 두께
            center.Fill = Brushes.Chocolate; // 내부 색깔 채우기
            center.Width = 20; // 넓이
            center.Height = 20; // 높이
            Grid1.Children.Add(center); // Grid1 : XAML -> <Canvas Name="Grid1"> 의 자식요소로 추가

        }

        private void DrawClockFace()
        {
            // 시계판 바깥 원
            Ellipse outer = new Ellipse(); // 바깥에 쓰일 타원 객체 생성
            outer.Stroke = Brushes.Khaki; // 윤곽선 색깔
            outer.StrokeThickness = 3; // 윤곽선 두께
            outer.Width = 300; // 넓이
            outer.Height = 300; // 높이
            outer.Fill = Brushes.White; // 색 채우기
            Grid1.Children.Add(outer); // Grid1 : XAML -> <Canvas Name="Grid1"> 의 자식요소로 추가

            // 눈금
            int L;
            int strokeThickness; // 윤곽선 두께
            SolidColorBrush strokeColor;

            // 시계는 원이니까 360도, 6도씩 
            for (int deg = 0; deg < 360; deg += 6)
            {
                double rad = deg * Math.PI / 180;
                if (deg % 30 == 0)
                {
                    L = (int)(radius * 0.9); // DrawLine의 X1, Y1 시작 좌표값을 찾기 위한 계산
                    strokeThickness = 5; // 윤곽선 두께
                    strokeColor = Brushes.Orange; // 오렌지 색 : 시
                }
                else
                {
                    L = (int)(radius * 0.95);
                    strokeThickness = 3;
                    strokeColor = Brushes.YellowGreen; // YellowGreen : 분 
                }

                DrawLine(L * Math.Sin(rad),
                        -L * Math.Cos(rad),
                        (radius - 2) * Math.Sin(rad),
                        -(radius - 2) * Math.Cos(rad),
                        strokeColor,
                        strokeThickness,
                        new Thickness(Center.X, Center.Y, 0, 0)); // left top right bottom
            }
        }
    }
}
