using System;
using System.Threading;
using Quartz;
using Topshelf.Logging;

namespace TopshelfSchedulerUsingAutofac
{
    public class DemoJob : IJob
    {
       
        static readonly LogWriter _log = HostLogger.Get<DemoJob>();

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                _log.Info("Scheduler Process started.");
                 Thread.Sleep(2000);
                _log.Info("Scheduler completed.");
            }
            catch (Exception ex)
            {
                _log.Error("Error in Scrum Tool Data Loading Process",ex);
            }
        }
    }
}
