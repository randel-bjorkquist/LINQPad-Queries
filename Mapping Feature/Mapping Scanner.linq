<Query Kind="Statements" />

#load "Mapping Feature\Interfaces"
#load "Mapping Feature\Mapping Registry"

public static class MappingScanner
{
  public static void ScanAndRegister(params Assembly[] assemblies)
  {
    // Example: look for types implementing IMapFrom<TSource, TDestination>
    var candidates = assemblies.SelectMany(a => a.GetTypes())
                               .Where(t => t.IsClass && !t.IsAbstract)  // <-- removed IsStatic
                               .Where(t => t.GetInterfaces()
                                            .Any(i => i.IsGenericType 
                                                   && i.GetGenericTypeDefinition() == typeof(IMapFrom<,>)))
                               .ToList();

    foreach (var candidate in candidates)
    {
      var mapInterface = candidate.GetInterfaces()
                                  .First(i => i.IsGenericType 
                                           && i.GetGenericTypeDefinition() == typeof(IMapFrom<,>));

      var sourceType      = mapInterface.GenericTypeArguments[0];
      var destinationType = mapInterface.GenericTypeArguments[1];

      var mapMethod = candidate.GetMethod("Map", BindingFlags.Public | BindingFlags.Static) 
                                ?? throw new InvalidOperationException($"Static Map method not found on {candidate.Name}");

      if (mapMethod is null || mapMethod.ReturnType != destinationType)
        continue;

      var delegateType = typeof(Func<,>).MakeGenericType(sourceType, destinationType);
      var mapper       = mapMethod.CreateDelegate(delegateType);

      var registerMethod = typeof(MappingRegistry).GetMethod(nameof(MappingRegistry.Register))
                                                 ?.MakeGenericMethod(sourceType, destinationType);

      registerMethod?.Invoke(null, new object[] { mapper });
    }
  }

  // Or attribute-based, convention-based, etc.
}
