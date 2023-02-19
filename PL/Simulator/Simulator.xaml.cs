using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace PL.Simulator;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>


public partial class Simulator : Window
{
    private Stopwatch stopwatch = new Stopwatch();
    private bool isTimerRun;
    private Thread timerthread;
    BackgroundWorker worker= new BackgroundWorker();
    public Simulator()
    {
        InitializeComponent();
        stopwatch.Restart();
        isTimerRun = true;
        timerthread = new Thread(runTimer);
        timerthread.Start();
        worker.RunWorkerAsync();

    }
    void setTextinvok(string text)
    {
        if(!CheckAccess())
        {
            Action<string> d = setTextinvok;
            Dispatcher.BeginInvoke(d,new object[] { text });
        }
        else
        {
            this.txtblcTimer.Text = text;   
        }
    }

    void runTimer()
    {
        while (isTimerRun)
        {
            string timertext = stopwatch.Elapsed.ToString();
            timertext = timertext.Substring(0, 8);
            setTextinvok(timertext);
            Thread.Sleep(1000);
        }

    }
    private void btnStopImaging_Click(object sender, RoutedEventArgs e)
    {

    }
}
