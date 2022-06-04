namespace Advance_dotnet_concept.C_Sharp_Dotnet.DependencyResolver.SimpleDependencyResolver
{
    internal class Dependency
    {
        private Type _type;
        private DependencyType _dependencyType;
        private object _implementation;
        private bool _isImplemented;

        public Type Type { get => _type; set => _type = value; }
        public object Implementation { get => _implementation; set => _implementation = value; }
        public bool IsImplemented { get => _isImplemented; set => _isImplemented = value; }
        public DependencyType DependencyType { get => _dependencyType; set => _dependencyType = value; }
        public Dependency(Type type, DependencyType dependencyLifeTime)
        {
            _type = type;
            _dependencyType = dependencyLifeTime;
        }

        public void AddImplementation(object obj)
        {
            _implementation = obj;
            _isImplemented = true;
        }
    }
}
