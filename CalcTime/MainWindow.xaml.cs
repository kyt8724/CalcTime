using CalcTime.Entity;
using CalcTime.Utility;
using System;
using System.Collections.Generic;
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
        private string timeOperator { get; set; }
        private string timelineValue { get; set; }
        private string timeFormat { get; set; }

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
            cbxTimeline.SelectedValue = timelineValue = "+00:00";
            cbxFormat.SelectedValue = timeFormat = "w64l";
            timeOperator = "+";
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
            
        }

        private void CalculateTime()
        {
            switch (timeFormat)
            {
                case "":
                    ;
                    break;
                default:
                    ;
                    break;
            }
        }

        #endregion custom method

        #region Events

        // button events
        // clear button
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDateTime.Text = string.Empty;
            txtValue.Text = string.Empty;
            cbxTimeline.SelectedValue = "00:00";
            cbxFormat.SelectedValue = "w64l";
        }

        // close button
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // decode button
        private void btnDecode_Click(object sender, RoutedEventArgs e)
        {

        }

        // copy button
        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtDateTime.Text))
                ;
            else
                txtDateTime.Copy();
        }

        // paste button
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            txtValue.Paste();
        }

        // combobox events
        // format combobox
        private void cbxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxFormat.SelectedValue != null)
                timeFormat = cbxFormat.SelectedValue.ToString();
        }

        // timeline combox
        private void cbxTimeline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timeOperator = string.Empty;
            timelineValue = string.Empty;
            if (cbxTimeline.SelectedValue != null)
            {
                timeOperator = cbxTimeline.SelectedValue.ToString().Substring(0, 1);
                timelineValue = cbxTimeline.SelectedValue.ToString().Substring(1);
            }
        }

        #endregion Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComboBox();           
        }
    }
}
