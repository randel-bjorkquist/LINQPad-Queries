<Query Kind="Statements">
  <NuGetReference>Microsoft.Extensions.Logging</NuGetReference>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <Namespace>System.Runtime.CompilerServices</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

/// <summary>
/// Centralized logging wrapper for consistent entry/exit/exception logging
/// across sync/async code paths, with optional CancellationToken support.
/// 
/// Default behavior: logs and rethrows exceptions (preserves caller semantics).
/// </summary>
public static class LoggingWrapper
{
  private const string UnknownMethod = "(unknown)";

  #region Sync

  /// <summary>
  /// Wraps an action with logging. Logs entry/exit and exceptions.
  /// </summary>
  public static void Run(ILogger logger, string method, Action operation)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    RunCore(logger, method, operation);
  }

  /// <summary>
  /// Wraps a function with logging. Returns the result.
  /// </summary>
  public static TResult Run<TResult>(ILogger logger, string method, Func<TResult> operation)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    return RunCore(logger, method, operation);
  }

  /// <summary>
  /// Wraps an action with logging. Logs entry/exit and exceptions.
  /// </summary>
  public static void Run(ILogger logger, string method, Action<CancellationToken> operation, CancellationToken token = default)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    RunCore(logger, method, operation, token);
  }

  /// <summary>
  /// Wraps a function with logging. Returns the result.
  /// </summary>
  public static TResult Run<TResult>(ILogger logger, string method, Func<CancellationToken, TResult> operation, CancellationToken token = default)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    return RunCore(logger, method, operation, token);
  }

  // Optional convenience overloads (no stringly-typed method)
  public static void Run(ILogger logger, Action operation, [CallerMemberName] string? method = null)
      => Run(logger, method ?? UnknownMethod, operation);

  public static TResult Run<TResult>(ILogger logger, Func<TResult> operation, [CallerMemberName] string? method = null)
      => Run(logger, method ?? UnknownMethod, operation);

  public static void Run(ILogger logger, Action<CancellationToken> operation, CancellationToken token = default, [CallerMemberName] string? method = null)
      => Run(logger, method ?? UnknownMethod, operation, token);

  public static TResult Run<TResult>(ILogger logger, Func<CancellationToken, TResult> operation, CancellationToken token = default, [CallerMemberName] string? method = null)
      => Run(logger, method ?? UnknownMethod, operation, token);

  #endregion

  #region Async

  /// <summary>
  /// Wraps an async action with logging. Logs entry/exit and exceptions.
  /// </summary>
  public static Task RunAsync(ILogger logger, string method, Func<Task> operation)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    return RunCoreAsync(logger, method, operation);
  }

  /// <summary>
  /// Wraps an async function with logging. Returns the result.
  /// </summary>
  public static Task<TResult> RunAsync<TResult>(ILogger logger, string method, Func<Task<TResult>> operation)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    return RunCoreAsync(logger, method, operation);
  }

  /// <summary>
  /// Wraps an async action with logging. Logs entry/exit and exceptions.
  /// </summary>
  public static Task RunAsync(ILogger logger, string method, Func<CancellationToken, Task> operation, CancellationToken token = default)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    return RunCoreAsync(logger, method, operation, token);
  }

  /// <summary>
  /// Wraps an async function with logging. Returns the result.
  /// </summary>
  public static Task<TResult> RunAsync<TResult>(ILogger logger, string method, Func<CancellationToken, Task<TResult>> operation, CancellationToken token = default)
  {
    if (operation is null) throw new ArgumentNullException(nameof(operation));
    return RunCoreAsync(logger, method, operation, token);
  }

  // Optional convenience overloads (no stringly-typed method)
  public static Task RunAsync(ILogger logger, Func<Task> operation, [CallerMemberName] string? method = null)
      => RunAsync(logger, method ?? UnknownMethod, operation);

  public static Task<TResult> RunAsync<TResult>(ILogger logger, Func<Task<TResult>> operation, [CallerMemberName] string? method = null)
      => RunAsync(logger, method ?? UnknownMethod, operation);

  public static Task RunAsync(ILogger logger, Func<CancellationToken, Task> operation, CancellationToken token = default, [CallerMemberName] string? method = null)
      => RunAsync(logger, method ?? UnknownMethod, operation, token);

  public static Task<TResult> RunAsync<TResult>(ILogger logger, Func<CancellationToken, Task<TResult>> operation, CancellationToken token = default, [CallerMemberName] string? method = null)
      => RunAsync(logger, method ?? UnknownMethod, operation, token);

  #endregion

  #region Core helpers

  private static void RunCore(ILogger logger, string? method, Action operation)
  {
    method ??= UnknownMethod;

    // If caller passes null logger, just execute (preserve semantics).
    if (logger is null)
    {
      operation();
      return;
    }

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      operation();
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  private static TResult RunCore<TResult>(ILogger logger, string? method, Func<TResult> operation)
  {
    method ??= UnknownMethod;

    if (logger is null)
      return operation();

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      var result = operation();
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
      return result;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  private static void RunCore(ILogger logger, string? method, Action<CancellationToken> operation, CancellationToken token)
  {
    method ??= UnknownMethod;

    if (logger is null)
    {
      token.ThrowIfCancellationRequested();
      operation(token);
      return;
    }

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      token.ThrowIfCancellationRequested();
      operation(token);
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
    }
    catch (OperationCanceledException) when (token.IsCancellationRequested)
    {
      logger.LogDebug("Canceled after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  private static TResult RunCore<TResult>(ILogger logger, string? method, Func<CancellationToken, TResult> operation, CancellationToken token)
  {
    method ??= UnknownMethod;

    if (logger is null)
    {
      token.ThrowIfCancellationRequested();
      return operation(token);
    }

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      token.ThrowIfCancellationRequested();
      var result = operation(token);
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
      return result;
    }
    catch (OperationCanceledException) when (token.IsCancellationRequested)
    {
      logger.LogDebug("Canceled after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  private static async Task RunCoreAsync(ILogger logger, string? method, Func<Task> operation)
  {
    method ??= UnknownMethod;

    if (logger is null)
    {
      await operation().ConfigureAwait(false);
      return;
    }

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      await operation().ConfigureAwait(false);
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  private static async Task<TResult> RunCoreAsync<TResult>(ILogger logger, string? method, Func<Task<TResult>> operation)
  {
    method ??= UnknownMethod;

    if (logger is null)
      return await operation().ConfigureAwait(false);

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      var result = await operation().ConfigureAwait(false);
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
      return result;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  private static async Task RunCoreAsync(ILogger logger, string? method, Func<CancellationToken, Task> operation, CancellationToken token)
  {
    method ??= UnknownMethod;

    if (logger is null)
    {
      token.ThrowIfCancellationRequested();
      await operation(token).ConfigureAwait(false);
      return;
    }

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      token.ThrowIfCancellationRequested();
      await operation(token).ConfigureAwait(false);
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
    }
    catch (OperationCanceledException) when (token.IsCancellationRequested)
    {
      logger.LogDebug("Canceled after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  private static async Task<TResult> RunCoreAsync<TResult>(ILogger logger, string? method, Func<CancellationToken, Task<TResult>> operation, CancellationToken token)
  {
    method ??= UnknownMethod;

    if (logger is null)
    {
      token.ThrowIfCancellationRequested();
      return await operation(token).ConfigureAwait(false);
    }

    using var scope = logger.BeginScope("Method: {MethodName}", method);
    logger.LogDebug("Entering");

    var sw = Stopwatch.StartNew();
    try
    {
      token.ThrowIfCancellationRequested();
      var result = await operation(token).ConfigureAwait(false);
      logger.LogDebug("Exiting successfully in {ElapsedMs}ms", sw.ElapsedMilliseconds);
      return result;
    }
    catch (OperationCanceledException) when (token.IsCancellationRequested)
    {
      logger.LogDebug("Canceled after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Error after {ElapsedMs}ms", sw.ElapsedMilliseconds);
      throw;
    }
  }

  #endregion
}