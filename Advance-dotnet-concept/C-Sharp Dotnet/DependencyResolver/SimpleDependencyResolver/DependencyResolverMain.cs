namespace Advance_dotnet_concept.C_Sharp_Dotnet.DependencyResolver.SimpleDependencyResolver
{
    internal class DependencyResolverMain
    {
        public static void Main()
        {
            var container = new DependencyContainer();
            var resolver = new DependencyResolver(container);

            RegisterDependency<HelloTransientService>(container, DependencyType.Transient);
            RegisterDependency<HelloSingletonService>(container, DependencyType.Singleton);
            RegisterDependency<HelloClass>(container, DependencyType.Transient);

            var service1 = ResolveDependency<HelloSingletonService>(resolver);
            service1.Print();

            var service2 = ResolveDependency<HelloSingletonService>(resolver);
            service2.Print();

            var service3 = ResolveDependency<HelloTransientService>(resolver);//return new object
            service3.Print();

            var service4 = ResolveDependency<HelloSingletonService>(resolver);//return existing object
            service4.Print();

            Console.ReadKey();
        }

        public static void RegisterDependency<T>(DependencyContainer dependencyContainer, DependencyType dependencyType)
        {
            switch (dependencyType)
            {
                case DependencyType.Singleton:
                    dependencyContainer.AddSingleton<T>();
                    break;
                case DependencyType.Transient:
                    dependencyContainer.AddTransient<T>();
                    break;
                default:
                    break;
            }
        }

        public static T ResolveDependency<T>(DependencyResolver resolver)
        {
            return resolver.GetService<T>();
        }
    }
    internal enum DependencyType
    {
        Singleton,
        Transient,
    }

}
