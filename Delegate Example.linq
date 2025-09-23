<Query Kind="Program" />

void Main()
{
  //use
  Process process = new Process();
  
  //pass a method that matches the delegate signature
  process.Start(msg => Console.WriteLine(msg));
}

//delegate receive string parameter and return void
public delegate void Notify(string message);

public class Process
{
  //here we use the delegate as a parameter type
  public void Start(Notify notify) => notify.Invoke("delegate called");
}