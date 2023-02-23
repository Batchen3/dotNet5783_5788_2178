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
using BO;
using DalApi;
using NameSpaceSimulator;
namespace PL.Simulator;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>


public partial class Simulator : Window
{
    private Stopwatch stopwatch = new Stopwatch();
    private bool isTimerRun;
    private Thread timerthread;
    BackgroundWorker worker = new BackgroundWorker();
    public BO.Order MyOrder { get; set; }
    public int Index { get; set; }
    public DateTime Time { get; set; }
    private bool _close = true;
    public Simulator()
    {
        InitializeComponent();
        worker.DoWork += Worker_DoWork;

        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerAsync();
        stopwatch.Restart();
        isTimerRun = true;
        timerthread = new Thread(runTimer);
        timerthread.Start();
    }
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = _close;
    }
    void setTextinvok(string text)
    {
        if (!CheckAccess())
        {
            Action<string> d = setTextinvok;
            Dispatcher.BeginInvoke(d, new object[] { text });
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


    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        ClassSimulator.startProcessEvent += ProcessStart;
        ClassSimulator.updateProcessEvent += ProcessUpdate;
        ClassSimulator.stopEvent += ProcessStop;
        try
        {
        ClassSimulator.run();
        }
        catch (DalException ex)
        {
            MessageBox.Show(ex.InnerException?.Message);
        }
       
        while (!((BackgroundWorker)sender).CancellationPending)
        {
            if (!Dispatcher.Thread.IsAlive) { e.Cancel = true; break; }
            if (MyOrder == null) continue;
            if (Index == 0) continue;
            ((BackgroundWorker)sender).ReportProgress(Index);
            Thread.Sleep(1000);
        }
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        isTimerRun = false;
        Thread.Sleep(1500);
        txtOrderId.Text = "";
        txtCustomerName.Text = "";
        txtCustomerEmail.Text = "";
        MessageBox.Show("The process stop!");
        _close = false;
        this.Close();
        ClassSimulator.startProcessEvent -= ProcessStart;
        ClassSimulator.updateProcessEvent -= ProcessUpdate;
        ClassSimulator.stopEvent -= ProcessStop;
    }

    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        TimeSpan timeOfIndex = new TimeSpan(0, 0, 0, 0,Index);
        lblProcess.Content = $"The process start at {Time.ToLongTimeString()} for order number: {MyOrder?.ID} and finish in:{Time.Add(timeOfIndex).ToLongTimeString()} \nit's will take { Index / 1000}.{Index % 1000} seconds";
        txtOrderId.Text = MyOrder?.ID.ToString();
        txtCustomerName.Text = MyOrder?.CustomerName?.ToString();
        txtCustomerEmail.Text = MyOrder?.CustomerEmail?.ToString();
    }
    private void btnStopImaging_Click(object sender, RoutedEventArgs e)
    {
        ClassSimulator.stop();
    }

    private void ProcessUpdate(BO.EStatus prev, BO.EStatus next)
    {
      this.Dispatcher.Invoke(() =>
       {
            lblProcess2.Content = $"The result for order id {MyOrder.ID}:\n" +
            $"Previous status: " + $" {prev} \n" +
            $"now:  {next}  ";
       });
    }
    private void ProcessStart(BO.Order order, int index)
    {
        MyOrder=order;
        Index=index;
        Time = DateTime.Now;
    }
    private void ProcessStop()
    {
        if (worker.WorkerSupportsCancellation == true)
            worker.CancelAsync();
    }

  
}
