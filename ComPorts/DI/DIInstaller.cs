using Autofac;
using ComPorts.Services;

namespace ComPorts.DI
{
    public class DIInstaller
    {
        public static IContainer Container { get; private set; }

        public static void Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AlgorithmsService>().As<IAlgorithmsService>().SingleInstance();
            builder.RegisterType<ComPortsService>().As<IComPortsService>().SingleInstance();

            Container = builder.Build();
        }
    }
}
