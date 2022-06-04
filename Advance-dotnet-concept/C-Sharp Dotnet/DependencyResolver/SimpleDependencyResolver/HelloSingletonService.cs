using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.DependencyResolver.SimpleDependencyResolver
{
    internal class HelloSingletonService
    {
        public void Print()
        {
            $"This is Hello Singleton Service.".Dump();
        }
    }
}