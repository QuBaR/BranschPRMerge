using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace BranschPRMerge
{
    internal class Program // Entry point of the application
    {
        static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;
            var studentTypes = assembly
                .GetTypes()
                .Where(t => t.IsClass && t.Namespace == "BranschPRMerge.Students");

            var comparer = StringComparer.Create(new CultureInfo("sv-SE"), ignoreCase: true);

            var students = studentTypes
                .Select(type =>
                {
                    var method = type.GetMethod(
                        name: "SayHello",
                        bindingAttr: BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance,
                        binder: null,
                        types: Type.EmptyTypes,
                        modifiers: null);

                    if (method == null || method.ReturnType != typeof(string))
                        return null;

                    // Prefer static Name property for sorting/display
                    var nameProp = type.GetProperty("Name", BindingFlags.Public | BindingFlags.Static);
                    var displayName = nameProp?.GetValue(null) as string;
                    displayName = string.IsNullOrWhiteSpace(displayName) ? type.Name : displayName;

                    return new
                    {
                        Type = type,
                        Method = method,
                        RequiresInstance = !method.IsStatic,
                        DisplayName = displayName!
                    };
                })
                .Where(x => x != null)!
                .OrderBy(x => x!.DisplayName, comparer)
                .ToList();

            int index = 1;
            foreach (var s in students)
            {
                object? instance = null;
                if (s.RequiresInstance)
                {
                    var ctor = s.Type.GetConstructor(Type.EmptyTypes);
                    if (ctor == null)
                        continue;
                    instance = Activator.CreateInstance(s.Type);
                }

                var result = s.Method.Invoke(instance, null) as string;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    Console.WriteLine($"{index}. {result}");
                    index++;
                }
            }
        }
    }
}
