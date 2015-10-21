using CalcTime.Entity;
using CalcTime.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalcTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LinkEvents();
        }

        #region custom method

        private void InitializeComboBox()
        {
            DeserializeXml dx = new DeserializeXml();
            Timeline timeline = dx.DeserializeObject();
            List<Format.JsonData> formats = dx.DeserializeJson();

            var timelines = timeline.UTCs.ToList();

            cbxTimeline.ItemsSource = timelines;
            cbxTimeline.DisplayMemberPath = "Name";
            cbxTimeline.SelectedValuePath = "Value";

            cbxFormat.ItemsSource = formats;
            cbxFormat.DisplayMemberPath = "Name";
            cbxFormat.SelectedValuePath = "Value";

            // initialize properties
            cbxTimeline.SelectedValue = "+00:00";
            cbxFormat.SelectedValue = "w64l";

        }

        private void LinkEvents()
        {
            // button events
            btnClear.Click += new RoutedEventHandler(btnClear_Click);
            btnClose.Click += new RoutedEventHandler(btnClose_Click);
            btnDecode.Click += new RoutedEventHandler(btnDecode_Click);

            btnCopy.Click += new RoutedEventHandler(btnCopy_Click);
            btnPaste.Click += new RoutedEventHandler(btnPaste_Click);

            // combobox events
            cbxFormat.SelectionChanged += new SelectionChangedEventHandler(cbxFormat_SelectionChanged);
            cbxTimeline.SelectionChanged += new SelectionChangedEventHandler(cbxTimeline_SelectionChanged);

            // checkbox events
            ckbWindowsTop.Click += new RoutedEventHandler(ckbWindowsTop_Click);
        }

        private TimeValue CalculateTime()
        {

            TimeValue timeValue = new TimeValue
            {
                ReturnTime = DateTime.Now,
                TimeFormat = cbxFormat.SelectedValue.ToString(),
                TimelineValue = cbxTimeline.SelectedValue.ToString(),
                ConvertingTime = txtValue.Text,
                IsInvalid = false
            };

            timeValue = ConvertBytesTime.CallConvertMethod(timeValue);

            return timeValue;
        }

        #endregion custom method

        #region Events

        // button events
        // clear button
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDateTime.Text = string.Empty;
            txtValue.Text = string.Empty;
        }

        // close button
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // decode button
        private void btnDecode_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValue.Text))
            {
                var timeValue = CalculateTime();
                if (timeValue.IsInvalid)
                {
                    MessageBox.Show("Input Value is Invalid, Please select the right Format or verify your input value!!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                CultureInfo ci = CultureInfo.InvariantCulture;
                txtDateTime.Text = timeValue.ReturnTime.ToString("ddd, dd MMM yyyy HH:mm:ss.FFFFFFF", ci);
            }
        }

        // copy button
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtDateTime.Text))
                return;
            else
            {
                txtDateTime.SelectAll();
                txtDateTime.Copy();
            }
        }

        // paste button
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            txtValue.Clear();
            txtValue.Paste();
        }

        // combobox events
        // format combobox
        private void cbxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFormat = ((ComboBox)sender).SelectedItem;
            txbExample.Text = ((Format.JsonData)selectedFormat).Example;
        }

        // timeline combox
        private void cbxTimeline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        // checkbox events
        // Window on top
        private void ckbWindowsTop_Click(Object sender, RoutedEventArgs e)
        {
            if (ckbWindowsTop.IsChecked == true)
                this.Topmost = true;
            else
                this.Topmost = false;
        }

        #endregion Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComboBox();           
        }
    }
}
