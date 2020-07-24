using Autofac;

namespace CRM.Core
{
    public static class CrmAppContext
    {
        public static IContainer Container { get; private set; } = (new ContainerBuilder()).Build();
        public static ContainerBuilder ContainerBuilder { get; private set; } = (new ContainerBuilder());
        public static void BuildContainer(ContainerBuilder containerBuilder)
        {
            Container = containerBuilder.Build();
        }
        public static T GetService<T>() => Container.Resolve<T>();
    }
}