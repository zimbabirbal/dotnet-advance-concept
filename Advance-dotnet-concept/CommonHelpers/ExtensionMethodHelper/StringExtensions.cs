using System.Text.Json;

namespace Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper
{
    public static class StringExtensions
    {
        public static string Dump<T>(this T obj, bool indent = true, bool populateThreadId = true, bool writeConsole = true)
        {
            var serializeString = JsonSerializer.Serialize<T>(obj, indent ? new JsonSerializerOptions { WriteIndented = true } : new JsonSerializerOptions());

            if (populateThreadId)
                serializeString += $" :ThreadId:{Thread.CurrentThread.ManagedThreadId}";
            if (writeConsole)
                Console.WriteLine($"{serializeString}");

            return serializeString;
        }

        public static string Dump<T>(this T obj)
        {
            string tempString = $"ThreadId:{Thread.CurrentThread.ManagedThreadId} " + (obj as string != null ? obj.ToString() : "");
            Console.WriteLine(tempString);
            return tempString;
        }
    }
}
