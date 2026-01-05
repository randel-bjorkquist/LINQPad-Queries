<Query Kind="Statements" />

#load "Mapping Feature\Mapping Registry"

// <Query Kind="Statements" />

public static class MappingExtensions
{
  /// <summary>
  /// Maps a source object to the destination type using registered mapping.
  /// Syntax: source.MapTo<DestinationType>()
  /// </summary>
  public static TDestination MapTo<TDestination>(this object source)
    => MappingRegistry.Execute<TDestination>(source);

  /// <summary>
  /// Maps a collection of source objects to a collection of destination objects.
  /// </summary>
  public static IEnumerable<TDestination> MapTo<TDestination>(this IEnumerable<object> sources)
    => sources?.Select(s => s.MapTo<TDestination>()) ?? Enumerable.Empty<TDestination>();

  /// <summary>
  /// Maps a collection of source objects to a list of destination objects.
  /// </summary>
  //public static List<TDestination> MapToList<TDestination>(this IEnumerable<object> sources)
  //  => sources?.Select(s => s.MapTo<TDestination>()).ToList() ?? new List<TDestination>();
}