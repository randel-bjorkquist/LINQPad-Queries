<Query Kind="Program" />

void Main()
{
  var exception = new Exception( "Outer Exception"
                                ,new Exception( "Inner Exception"
                                               ,new Exception("Inner Inner Exception")));
  
  GetCompleteMessage(exception).Dump("GetCompleteMessage(exception)");
  
  exception.GetCompleteMessage().Dump("exception.GetCompleteMessage()");
}


public string GetCompleteMessage(Exception ex)
{
  return ex.InnerException == null
                            ? ex.Message
                            : ex.Message
                              + " ==> "
                              + GetCompleteMessage(ex.InnerException);
}


public static class ExceptionExtensions
{
  public static string GetCompleteMessage(this Exception ex)
  {
    return ex.InnerException == null
                              ? ex.Message
                              : ex.Message
                                + " ==> "
                                + ex.InnerException.GetCompleteMessage();
  }
}

