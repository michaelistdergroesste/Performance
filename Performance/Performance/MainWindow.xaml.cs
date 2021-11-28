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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Performance
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread newThread;

        ProcessTimer w;

        public MainWindow()
        {
            InitializeComponent();
            w = new ProcessTimer();

            StartThread();
        }
        private void StartThread()
        {
            newThread = new Thread(w.DoWork);
            newThread.Start();
        }
    }
}
