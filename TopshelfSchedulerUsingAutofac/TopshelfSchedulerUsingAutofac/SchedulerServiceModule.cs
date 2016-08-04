using Autofac;
using Autofac.Extras.Quartz;
using Quartz.Spi;

namespace TopshelfSchedulerUsingAutofac
{
    public class SchedulerServiceModule:Module
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.RegisterType<AutofacJobFactory>().As<IJobFactory>();
            builder.RegisterType<DemoJob>().AsSelf();
            builder.RegisterAssemblyTypes(ThisAssembly).AsSelf();
            base.Load(builder);
        }
    }
}
