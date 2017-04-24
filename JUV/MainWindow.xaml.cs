using System.Windows;
using JUnitViewer;
using Microsoft.Win32;

namespace JUV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            JUnitReader blah = new JUnitReader();
            bool something = blah.ParseJUnitFile("C:\\Users\\lobvi02\\Downloads\\junitresults (1).xml");
            txtEditor.Text = something.ToString();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    JUnitReader blah = new JUnitReader();
            //    bool something = blah.parseJUnitFile(openFileDialog.FileName);
            //    txtEditor.Text = something.ToString();
            //}
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }
}
