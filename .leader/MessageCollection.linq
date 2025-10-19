<Query Kind="Program" />

void Main()
{
  var messages = new MessageCollection();

  var exception_message = new ExceptionMessage("exception message description");  
  messages.Add(exception_message);
  messages.Dump("exception_message");
  
  var error_message = new ErrorMessage("error message description");
  messages.Add(error_message);
  messages.Dump("error_message");
  
  var information_message = new InformationMessage("information message description");
  messages.Add(information_message);
  messages.Dump("information_message");
  
  var warning_message = new WarningMessage("message description");
  messages.Add(warning_message);
  messages.Dump("waring_message");
  
  var message = new Message { Type = MessageTypes.Error
                             ,Description = "message description" };

  messages.Add(message);
  messages.Dump("message");
}


public class MessageTypes 
{
    public const string Error       = "error";
    public const string Exception   = "exception";

    public const string Information = "information";
    public const string Warning     = "warning";
}

public class Message 
{
    private short   _priority;
    private string  _type;
    private string  _description;

    public string Type {
        get => _type;
        set
        {
            switch( value )
            {
                case MessageTypes.Exception:    _priority = 1;  break;
                case MessageTypes.Error:        _priority = 2;  break;
                case MessageTypes.Information:  _priority = 3;  break;
                case MessageTypes.Warning:      _priority = 4;  break;
                
                default: 
                    throw new ArgumentOutOfRangeException($"'{value}' is not a valid Message Type."); 
            }
            
            _type = value;
        }
    }
    
    public string Description {
        get => _description;
        set => _description = value;
    }

    public short Priority => _priority;

    public override string ToString()
    {
        return string.Format( "{0}: {1}"
                             ,Type.ToUpper().Trim()
                             ,Description.StartsWith($"{Type.ToUpper()}:") 
                                  ? Description.Substring($"{Type.ToUpper()}:".Length).Trim()
                                  : Description.Trim() );
    }
}

public class ErrorMessage : Message 
{
    public ErrorMessage(string message)
    {
        Type = MessageTypes.Error;
        
        //REQUIRED: without this, the 'ERROR: ' label is duplicated when collections are added
        Description = $"ERROR: {(message.StartsWith("ERROR:") ? message[(message.IndexOf(":") + 2)..] : message)}";
    }
}

public class ExceptionMessage : Message 
{
    public ExceptionMessage(string message)
    {
        Type = MessageTypes.Exception;
        
        //REQUIRED: without this, the 'EXCEPTION: ' label is duplicated when collections are added
        Description = $"EXCEPTION: {(message.StartsWith("EXCEPTION:") ? message[(message.IndexOf(":") + 2)..] : message)}";
    }
}

public class InformationMessage : Message 
{
    public InformationMessage(string message)
    {
        Type = MessageTypes.Information;
        
        //REQUIRED: without this, the 'INFORMATION: ' label is duplicated when collections are added
        Description = $"INFORMATION: {(message.StartsWith("INFORMATION:") ? message[(message.IndexOf(":") + 2)..] : message)}";
    }
}

public class WarningMessage : Message 
{
    public WarningMessage(string message)
    {
        Type = MessageTypes.Warning;
        
        //REQUIRED: without this, the 'WARNING: ' label is duplicated when collections are added
        Description = $"WARNING: {(message.StartsWith("WARNING:") ? message[(message.IndexOf(":") + 2)..] : message)}";
    }
}

public class MessageCollection 
{
    private List<Message> _items;

    public MessageCollection()
    {
        _items = new List<Message>();
    }

    public void Add(Message message)
    {
        _items ??= new List<Message>();
        Add(message.Type, message.Description);
    }
    
    public void Add(ExceptionMessage message)
    {
        _items ??= new List<Message>();
        _items.Add(message);
    }
    
    public void Add(ErrorMessage message)
    {
        _items ??= new List<Message>();
        _items.Add(message);
    }
    
    public void Add(InformationMessage message)
    {
        _items ??= new List<Message>();
        _items.Add(message);
    }
    
    public void Add(WarningMessage message)
    {
        _items ??= new List<Message>();
        _items.Add(message);
    }

    public void Add(string type, string message)
    {
        _items ??= new List<Message>();

        if(string.Equals(type ,MessageTypes.Error       ,StringComparison.CurrentCultureIgnoreCase) ||
           string.Equals(type ,MessageTypes.Exception   ,StringComparison.CurrentCultureIgnoreCase) ||
           string.Equals(type ,MessageTypes.Information ,StringComparison.CurrentCultureIgnoreCase) ||
           string.Equals(type ,MessageTypes.Warning     ,StringComparison.CurrentCultureIgnoreCase))
        {
            switch( type )
            {
                case MessageTypes.Exception:    _items.Add(new ExceptionMessage(message));    break;
                case MessageTypes.Error:        _items.Add(new ErrorMessage(message));        break;
                case MessageTypes.Information:  _items.Add(new InformationMessage(message));  break;
                case MessageTypes.Warning:      _items.Add(new WarningMessage(message));      break;
            }
        }
        else
        {
            throw new ArgumentOutOfRangeException($"'{type}' is not a valid Message Type.");
        }
    }

    public void AddError(string message)
    {
        _items ??= new List<Message>();
        _items.Add(new ErrorMessage(message));
    }

    public void AddError(Exception ex, string message = null)
    {
        string msg = FormatException(ex, message);

        AddError(msg);
    }

    public void AddException(string message)
    {
        _items ??= new List<Message>();
        _items.Add(new ExceptionMessage(message));
    }

    public void AddException(Exception ex, string message = null)
    {
        string msg = FormatException(ex, message);

        AddException(msg);
    }

    public void AddInformation(string message)
    {
        _items ??= new List<Message>();
        _items.Add(new InformationMessage(message));
    }

    public void AddInformation(Exception ex, string message = null)
    {
        string msg = FormatException(ex, message);

        AddInformation(msg);
    }

    public void AddWarning(string message)
    {
        _items ??= new List<Message>();
        _items.Add(new WarningMessage(message));
    }

    public void AddWarning(Exception ex, string message = null)
    {
        string msg = FormatException(ex, message);

        AddWarning(msg);
    }

    private string FormatException(Exception ex, string message = null)
    {
        string msg = string.IsNullOrEmpty(message)
                   ? ex.Message
                   : $"{message} -- Exception Description: \"{ex.Message}\"";

        return msg;
    }

    public void AddRange(IEnumerable<Message> messages)
    {
        if(messages.HasItems())
        {
            _items ??= new List<Message>();
            _items.AddRange(messages.ToList());
        }
    }

    public void AddRange(MessageCollection messages)
    {
        if(messages != null)
        {
            _items ??= new List<Message>();

            foreach(Message item in messages.Items)
            {
                switch(item.Type)
                {
                    case MessageTypes.Exception:     AddException(item.Description);     break;
                    case MessageTypes.Error:         AddError(item.Description);         break;
                    case MessageTypes.Information:   AddInformation(item.Description);   break;
                    case MessageTypes.Warning:       AddWarning(item.Description);       break;
                }
            }
        }
    }

    public bool Remove(Message message)
    {
        return _items.Remove(message);
    }

    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    public void Clear()
    {
        _items?.Clear();
    }

    public void Clear(IEnumerable<string> messages)
    {
        _items = _items?.Where(i => !messages.Any(m => i.Description.Contains(m)))
                        .ToList();
    }

    public void ClearErrors()
    {
        string[] filters = { MessageTypes.Error };
        ClearByMessageTypes(filters);
    }

    public void ClearExceptions()
    {
        string[] filters = { MessageTypes.Exception };
        ClearByMessageTypes(filters);
    }

    public void ClearInformations()
    {
        string[] filters = { MessageTypes.Information };
        ClearByMessageTypes(filters);
    }

    public void ClearWarnings()
    {
        string[] filters = { MessageTypes.Warning };
        ClearByMessageTypes(filters);
    }

    private void ClearByMessageTypes(string[] types)
    {
        _items = _items?.Where(i => !types.Contains(i.Type))
                        .ToList();
    }

    public IEnumerable<Message> Items {
        get => _items.OrderBy(i => i.Priority);
        set => _items = new List<Message>(value);
    }

    public IEnumerable<Message> Errors => _items.Where(i => i.Type == MessageTypes.Error
                                                         || i.Type == MessageTypes.Exception);

    public IEnumerable<Message> Informations => _items.Where(i => i.Type == MessageTypes.Information);

    public IEnumerable<Message> Warnings => _items.Where(i => i.Type == MessageTypes.Warning);

    public bool IsNullOrEmpty => _items.IsNullOrEmpty();

    public bool HasItems => _items.HasItems();

    public int ErrorCount => _items.Count(i => i.Type == MessageTypes.Error
                                            || i.Type == MessageTypes.Exception);

    public int NonErrorCount => _items.Count(i => i.Type == MessageTypes.Information
                                               || i.Type == MessageTypes.Warning);

    public bool HasErrors => ErrorCount > 0;

    public bool NoErrors => ErrorCount == 0;

    public override string ToString()
    {
        //return this.ToString(", ");
        return Items.OrderBy(item => item.Priority)
                    .Select(item => item.ToString())
                    .ToCSV(", ");
    }

    public string ToString(string delimiter)
    {
        return Items.OrderBy(item => item.Priority)
                    .Select(item => item.ToString())
                    .ToCSV(delimiter);
    }
}

[Flags]
public enum ToCSVOptions 
{
    None              = 0
   ,Distinct          = 1
   ,OrderBy           = 2
   ,OrderByDescending = 4
}

public static class EnumerableExtensions 
{
    #region ToCSV Method(s)
        
    public static string ToCSV<T>(this IEnumerable<T> source)
    {
        return source.ToCSV<T>(ToCSVOptions.None);
    }
        
    public static string ToCSV<T>(this IEnumerable<T> source, ToCSVOptions options)
    {
        return source.ToCSV<T>(",", options);
    }
        
    public static string ToCSV<T>(this IEnumerable<T> source, string separator)
    {
        return source.ToCSV<T>(separator, ToCSVOptions.None);
    }
        
    public static string ToCSV<T>(this IEnumerable<T> source, string separator, ToCSVOptions options)
    {
        foreach (Enum flag in options.GetFlags())
        {
            source = source.ToCSV<T>(flag);
        }

        return source == null
                       ? null
                       : string.Join(separator, source);
    }
        
    private static IEnumerable<T> ToCSV<T>(this IEnumerable<T> source, Enum options)
    {
        switch (options)
        {
            case ToCSVOptions.Distinct:
                return source?.Distinct();

            case ToCSVOptions.OrderBy:
                return source?.OrderBy(o => o);

            case ToCSVOptions.OrderByDescending:
                return source?.OrderByDescending(o => o);

            case ToCSVOptions.None:
            default:
                return source;
        }
    }
        
    #endregion
        
    //This can be used in place of source.Any() and is similar to string.IsNullOrEmpty()
    public static bool HasItems<T>(this IEnumerable<T> source)
    {
        return source?.Any() ?? false;
    }
        
    public static bool IsNullOrEmpty<T>( this IEnumerable<T> source )
    {
        if( source == null )
        {
          return true;
        }
        
        using( IEnumerator<T> e = source.GetEnumerator() )
        {
            if( !e.MoveNext() )
            {
              return true;
            }
        }
        
        return false;
    }
        
    public static bool IsNullOrEmpty<T>( this IEnumerable<T> source, Func<T, bool> predicate )
    {
        if( predicate == null )
        {
            throw new ArgumentNullException( "predicate" );
        }
        
        foreach( T element in source )
        {
            if( predicate( element ))
            {
                return true;
            }
        }
        
        return false;
    }

    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
    {
        return source ?? Enumerable.Empty<T>();
    }
}

public static class EnumExtensions 
{
    public static IEnumerable<Enum> GetFlags(this Enum e)
    {
      return Enum.GetValues( e.GetType() )
                 .Cast<Enum>()
                 .Where( v => !Equals((int)(object)v, 0) 
                           && e.HasFlag(v) );
    }
}
