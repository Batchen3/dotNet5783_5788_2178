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

    public Simulator()
    {
        InitializeComponent();
        worker.DoWork += Worker_DoWork;
        //worker.ProgressChanged += Worker_ProgressChanged;
        //worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        //worker.WorkerReportsProgress = true;
        worker.RunWorkerAsync();
        stopwatch.Restart();
        isTimerRun = true;
        timerthread = new Thread(runTimer);
        timerthread.Start();
        //worker.RunWorkerAsync();
    }
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
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
        ClassSimulator.run();
    }

    //private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    //{


    //}

    //private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    //{
    //   //this.Dispatcher.Invoke(()=>) lblProcess.Content = $"The process start for:{order.ID} the process will finish at:{index} second";
    //   // txtOrderId.Text = order.ID.ToString();
    //   // txtCustomerName.Text = order.CustomerName?.ToString();
    //   // txtCustomerEmail.Text = order.CustomerEmail?.ToString();
    //}
    private void btnStopImaging_Click(object sender, RoutedEventArgs e)
    {
        ClassSimulator.stop();

    }

    private void ProcessUpdate(BO.EStatus prev, BO.EStatus next)
    {
        this.Dispatcher.Invoke(() =>
        {
            lblProcess2.Content = $"The result for this order:\n" +
            $"Previous status: " + $" {prev} \n" +
            $"now:  {next}  ";
        });
    }
    private void ProcessStart(BO.Order order, int index)
    {
        this.Dispatcher.Invoke(() =>
        {
            lblProcess.Content = $"The process start for:{order.ID} the process will finish at: {index / 1000}.{index % 1000} seconds";
            txtOrderId.Text = order.ID.ToString();
            txtCustomerName.Text = order.CustomerName?.ToString();
            txtCustomerEmail.Text = order.CustomerEmail?.ToString();
        });
    }
    private void ProcessStop()
    {
        this.Dispatcher.Invoke(() =>
        {
            isTimerRun = false;
            Thread.Sleep(1500);
            lblProcess2.Content = "";
            txtOrderId.Text = "";
            txtCustomerName.Text = "";
            txtCustomerEmail.Text = "";
            lblProcess.Content = "";
            MessageBox.Show("The process stop!");
            this.Close();          
        });
    }
}
