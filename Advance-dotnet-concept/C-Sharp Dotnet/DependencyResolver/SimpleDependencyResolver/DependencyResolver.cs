namespace Advance_dotnet_concept.C_Sharp_Dotnet.DependencyResolver.SimpleDependencyResolver
{
    internal class DependencyResolver
    {
        private DependencyContainer _dependencyContainer;

        DependencyContainer DependencyContainer { get => _dependencyContainer; set => _dependencyContainer = value; }
        public DependencyResolver(DependencyContainer dependencyContainer)
        {
            _dependencyContainer = dependencyContainer;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        private object GetService(Type type)
        {
            var dependency = _dependencyContainer.GetDependency(type);
            var constructor = dependency.Type.GetConstructors()?.Single();
            if (constructor != null)
            {
                var parameters = constructor.GetParameters().ToArray();
                if (parameters.Length > 0)
                {
                    var paramsImplementations = new Object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        paramsImplementations[i] = GetService(parameters[i].ParameterType);
                    }

                    return CreateImplementation(dependency, t => Activator.CreateInstance(t, paramsImplementations));
                }

                return CreateImplementation(dependency, t => Activator.CreateInstance(t));
            }
            return null;
        }

        private object CreateImplementation(Dependency dependency, Func<Type, object> factory)
        {
            if (dependency.IsImplemented)
                return dependency.Implementation;

            var implementation = factory(dependency.Type);

            if(dependency.DependencyType == DependencyType.Singleton)
            {
                dependency.AddImplementation(implementation);
            }

            return implementation;
        }
    }
}
