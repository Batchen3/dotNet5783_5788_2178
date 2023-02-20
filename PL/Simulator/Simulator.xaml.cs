using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using NameSpaceSimulator;
namespace PL.Simulator;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>


public partial class Simulator : Window
{
   // private Stopwatch stopwatch = new Stopwatch();
  //  private bool isTimerRun;
   // private Thread timerthread;
    BackgroundWorker worker= new BackgroundWorker();
    public BO.Order order { get; set; }
    public int index { get; set; }
    public Simulator()
    {
        InitializeComponent();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.RunWorkerAsync();
        //stopwatch.Restart();
        //isTimerRun = true;
        //timerthread = new Thread(runTimer);
        //timerthread.Start();
        //worker.RunWorkerAsync();

    }
    //void setTextinvok(string text)
    //{
    //    if(!CheckAccess())
    //    {
    //        Action<string> d = setTextinvok;
    //        Dispatcher.BeginInvoke(d,new object[] { text });
    //    }
    //    else
    //    {
    //        this.txtblcTimer.Text = text;   
    //    }
    //}

    //void runTimer()
    //{
    //    while (isTimerRun)
    //    {
    //        string timertext = stopwatch.Elapsed.ToString();
    //        timertext = timertext.Substring(0, 8);
    //        setTextinvok(timertext);
    //        Thread.Sleep(1000);
    //    }

    //}


    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        ClassSimulator.startProcessEvent += ProcessStart;
        ClassSimulator.updateProcessEvent += ProcessUpdate;
        ClassSimulator.run();
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        

    }

    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        lblProcess.Content = $"The process start for:{order.ID} the process will finish at:{index} second";
        //txtOrderId.Text = order.ID.ToString();
        //txtCustomerName.Text = order.CustomerName?.ToString();
        //txtCustomerEmail.Text = order.CustomerEmail?.ToString();
    }
    private void btnStopImaging_Click(object sender, RoutedEventArgs e)
    {
        ClassSimulator.stop();
    }

    private void ProcessUpdate(BO.EStatus prev,BO.EStatus next) 
    {
        lblProcess2.Content = $"The result for this order:Previous status-{prev} and now:{next}";

    }
    private void ProcessStart(BO.Order order, int index)
    {
        this.order = order;
        this.index = index;
        worker.ReportProgress(index,order);
    }  
}
