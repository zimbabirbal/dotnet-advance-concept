using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.DependencyResolver.SimpleDependencyResolver
{
    internal class HelloTransientService
    {
        HelloClass _helloClass;
        public HelloTransientService(HelloClass name)
        {
            _helloClass = name;
        }
        public void Print()
        {
            $"This is Hello Transient service and from {_helloClass.Print()}".Dump();
        }
    }
}
