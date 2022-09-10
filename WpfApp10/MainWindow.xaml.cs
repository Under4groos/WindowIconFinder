using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp10.Model;
using WpfApp10.ViewModel;

namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataFiles dataFiles = new DataFiles();


        ThreadTimer _ThreadTimer;
       

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = dataFiles;
            //_ThreadTimer = new ThreadTimer(this);

            //string[] lines_ = File.ReadAllLines(@"C:\Users\Maks\source\repos\FindImage\FindImage\bin\Debug\net6.0\data.txt");
            //int i = 0;
            //_ThreadTimer.TickTime = 5;
            //_ThreadTimer.Tick += () =>
            //{
            //    if(i < 10)// lines_.Length)
            //    {
            //        dataFiles.ListImages.Add(new FileImage()
            //        {
            //            Path = lines_[i],
            //        });
            //        i++;
            //        count_.Content = dataFiles.ListImages.Count;
            //    }
               

            //};
            //_ThreadTimer.initialize();


          
            new Thread(() =>
            {

                _find("C:\\Windows");


            }).Start();
        }
        public void _find(string dir)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    foreach (var item in Directory.GetFiles(dir))
                    {
                        if (Regex.IsMatch(new FileInfo(item).Extension, "(.png)|(.jpeg)|(.gif)|(.bmp)|(.ico)|(.bmp)"))
                        {
                            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                            {
                                dataFiles.ListImages.Add(new FileImage()
                                {
                                    Path = item,
                                });
                                count_.Content = dataFiles.ListImages.Count;
                            }, new object[] { });

                        }
                        
                    }
                    foreach (var item in Directory.GetDirectories(dir))
                    {
                        _find(item);
                    }

                }
            }
            catch { }

        }
        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image_ = (sender as Image);
            ImageSource isource = image_.Source;
         
            Clipboard.SetText(isource.ToString().Substring("file:///".Length));
           
        }
    }
}
