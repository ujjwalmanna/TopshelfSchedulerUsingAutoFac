using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.Quartz;
using Quartz;
using Topshelf;
using Topshelf.Autofac;
using Topshelf.Quartz;

namespace TopshelfSchedulerUsingAutofac
{
    public class Program
    {
        static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<QuartzAutofacFactoryModule>();
            containerBuilder.RegisterModule(new QuartzAutofacJobsModule(typeof(SchedulerServiceModule).Assembly));
            var container = containerBuilder.Build();
            return container;
        }
        static void Main(string[] args)
        {
            HostFactory.Run(x => //1
            {
                var container =BuildContainer();
                x.UseAutofacContainer(container);
                x.UseLog4Net("log4net.config");
                x.ScheduleQuartzJobAsService(q =>
                {
                    q.WithJob(() => JobBuilder.Create<DemoJob>().Build());
                    q.AddTrigger(() => TriggerBuilder.Create().WithSimpleSchedule(b => b.WithInterval(new TimeSpan(0, 0, 0, 5)).RepeatForever()).Build());
                });

                x.StartAutomatically();

                x.RunAsLocalSystem();
                x.SetServiceName("DemoSchedulerService");
                x.SetDisplayName("Demo Service");
                x.SetDescription("Scrum Tool ETL Processor");
            });
        }
    }
}
