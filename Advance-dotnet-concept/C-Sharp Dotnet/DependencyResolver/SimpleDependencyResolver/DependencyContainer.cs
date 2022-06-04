namespace Advance_dotnet_concept.C_Sharp_Dotnet.DependencyResolver.SimpleDependencyResolver
{
    internal class DependencyContainer
    {
        private List<Dependency> _dependencies;

        public List<Dependency> Dependencies { get => _dependencies; set => _dependencies = value; }
        public DependencyContainer()
        {
            _dependencies = new List<Dependency>();
        }

        public void AddSingleton<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), DependencyType.Singleton));
        }

        public void AddTransient<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), DependencyType.Transient));
        }

        public Dependency? GetDependency(Type type)
        {
            return _dependencies?.First(x => x.Type.Name == type.Name);
        }
    }
}
