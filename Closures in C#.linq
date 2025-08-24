<Query Kind="Program" />

#region LinkedIn Post Source & Description ...

//LinkedIn Post by Dmitry Hanziuk
//https://www.linkedin.com/feed/update/urn:li:activity:7316150764798709762/?updateEntityUrn=urn%3Ali%3Afs_updateV2%3A%28urn%3Ali%3Aactivity%3A7316150764798709762%2CFEED_DETAIL%2CEMPTY%2CDEFAULT%2Cfalse%29
//
//C# Closures - Powerfule, Subtle, and Often Misunderstood ...
//  Have you ever used a lambda or a delegate in C# and noticed that it "remembers" variables from 
//  its outer scope — even after that scope is gone? That’s a closure — one of the most elegant and
//  tricky features in modern C#.
//
//What is a Closure?
//  A closure is a function (usually a lambda or anonymous method) that captures variables from its 
//  enclosing scope. In other words, the function closes over the variables it sees around it — and 
//  keeps them alive.
//
//Real-world trap:
//  Closures are very common in async code, event handlers, LINQ, and loops. They can introduce 
//  subtle bugs that are hard to spot, especially when values mutate.
//
//Pro Tips:
//  Closures are great for deferred execution.
//  - Be mindful when capturing variables in loops or async code.
//  - Consider capturing a copy of the value when needed.
//  - Use them to create elegant factories or pipelines.

#endregion 

void Main()
{
  //NOTE: common variable ...
  List<Func<int>> actions = new();

  //-------------------------------------------------------------------------
  //NOTE: the lambda captures the variable i, not its value. After the loop,
  //      i == 3 and all functions reference it,
  //
  //OUTPUT: 3
  //        3
  //        3
  //-------------------------------------------------------------------------  
  for(int i = 0; i < 3; i++)
  {
    actions.Add (() => i);
  }
  
  //-------------------------------------------------------------------------
  //NOTE: we store 'i' in a new variable 'current'. Each lambda captures
  //      its own value
  //
  //OUTPUT: 0
  //        1
  //        2
  //-------------------------------------------------------------------------
  for(int i = 0; i < 3; i++)
  {
    int current = i;  //captures and creates copy
    actions.Add (() => current);
  }

  //-------------------------------------------------------------------------
  //NOTE: simply used to create output; demonstrating the difference ...
  //-------------------------------------------------------------------------
  foreach(var action in actions)
  {
    Console.WriteLine(action());
  }
}

