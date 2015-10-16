using CalcTime.Entity;
using CalcTime.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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

            cbxTimeline.SelectedValue = "00:00";
            cbxFormat.SelectedValue = "w64l";
        }

        #endregion custom method

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComboBox();           
        }
    }
}
