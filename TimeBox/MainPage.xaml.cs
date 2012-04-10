using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;

namespace TimeBox
{
    public partial class MainPage : PhoneApplicationPage
    {
        private TimeSpan timeInterval;
        private DateTime startTime;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            timeInterval = TimeSpan.Zero;
        }

        private void TimeInverval_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox timeIntervalBox = (TextBox)sender;
            System.Diagnostics.Debug.WriteLine("Interval Changed");
            int newTimeInterval = int.MinValue;
            try
            {
                newTimeInterval = int.Parse(timeIntervalBox.Text);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + exception.Message);
                timeIntervalBox.Text = "";
            }

            if (newTimeInterval != int.MinValue)
            {
                timeInterval = new TimeSpan(0, newTimeInterval, 0);
                System.Diagnostics.Debug.WriteLine("Changed Time Interval: " + timeInterval.TotalSeconds.ToString());
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Starting at " + DateTime.Now.Ticks);
            startTime = DateTime.Now;

            Reminder reminder = new Reminder(Guid.NewGuid().ToString());
            reminder.BeginTime = startTime.AddMinutes(TimeSlider.Value);
            reminder.RecurrenceType = RecurrenceInterval.None;
            reminder.Content = "Time's up for task " + TaskNameBox.Text;

            ScheduledActionService.Add(reminder);

            MessageBox.Show("Start task " + TaskNameBox.Text);

            TaskNameBox.Text = "";
        }

        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int newInterval = (int)e.NewValue;
            timeInterval = new TimeSpan(0, newInterval, 0);
            ((Slider)sender).Value = newInterval;

            if (minutesBlock != null)
            {
                minutesBlock.Text = newInterval.ToString();
            }
        }
    }
}