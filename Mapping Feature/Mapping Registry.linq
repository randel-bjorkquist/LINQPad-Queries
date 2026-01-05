<Query Kind="Statements" />

// <Query Kind="Statements" />

// Core mapping registry - holds all registered mappings
internal static class MappingRegistry
{
  private static readonly Dictionary<(Type Source, Type Destination), Delegate> Maps = new();

  // Public read-only view for scanning/tools if needed
  public static IReadOnlyDictionary<(Type Source, Type Destination), Delegate> Registrations => Maps;

  public static void Register<TSource, TDestination>(Func<TSource, TDestination> mapper)
  {
    var key   = (typeof(TSource), typeof(TDestination));
    Maps[key] = mapper;
  }

  internal static TDestination Execute<TDestination>(object source)
  {
    if (source == null) 
      throw new ArgumentNullException(nameof(source));

    var sourceType = source.GetType();
    var key = (sourceType, typeof(TDestination));

    if (Maps.TryGetValue(key, out var _delegate))
      return (TDestination)_delegate.DynamicInvoke(source)!;

    throw new InvalidOperationException($"No mapping registered from {sourceType.Name} to {typeof(TDestination).Name}");
  }
}
