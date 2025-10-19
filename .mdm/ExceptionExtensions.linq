<Query Kind="Program" />

void Main()
{
  var exception = new Exception("blah blah blah");
  
  exception.Dump("exception");
  
//  exception.GetFullMessage().Dump("exception.GetFullMessage()");
}

//public static class ExceptionExtensions
//{
//  public static string GetFullMessage(this Exception ex)
//  {
//    return ex.InnerException == null
//          ? ex.Message
//          : ex.Message 
//            + " ==> " 
//            + ex.InnerException.GetFullMessage();
//  }
//}

//public string FormatExceptions(this Exception ex, string message = null)
//{  
//throw new NotImplementedException();
//
//    var msg = string.IsNullOrWhiteSpace(message)
//            ? ex.Message
//            : $"{message} -- Exception Description: {ExceptionExtensions.GetFullMessage(ex.InnerException)}";
//    
//    return msg;
//}
