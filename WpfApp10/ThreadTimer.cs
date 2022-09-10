using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

public class ThreadTimer
{
    public Thread thread;
    Window win = null;
    public Action Tick;
    public Dictionary<string, Action> Ticks = new Dictionary<string, Action>();

    public Action<bool> ChangedStatus;
    public int TickTime { get; set; } = 1;
    private bool status = false;
    public bool Status
    {
        get
        {
            return status;
        }
        set
        {
            status = value;

            if (ChangedStatus != null)
                ChangedStatus(Status);
        }
    }

    public void Setev()
    {
        win.MouseEnter += (o, e) =>
        {
            Status = false;
        };
        win.MouseLeave += (o, e) =>
        {
            Status = true;
        };
    }

    public ThreadTimer(Window wind)
    {
        win = wind;
       
        wind.Closed += (o, e) =>
        {
            
            if (win != null)
                thread.Abort();
        };
        wind.Closing += (o, e) =>
        {
             
            if (win != null)
                thread.Abort();
        };
        thread = new Thread(() =>
        {
            while (true)
            {
                if (Status)
                {
                    Thread.Sleep(500);
                    continue;
                }
                    
                win.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                {
                    if(Tick != null)
                        Tick();
                    foreach (var item in Ticks)
                    {
                        if(item.Value != null)
                            item.Value();
                    }
                }, new object[] { });
                Thread.Sleep(TickTime);
            }
            
        });

    }
    public void initialize()
    {
        if (win != null)
            thread.Start();
    }

}