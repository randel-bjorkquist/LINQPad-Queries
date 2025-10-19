<Query Kind="Program" />

//Source Article: Can We Create Method Inside Method In C#?
//URL: https://www.c-sharpcorner.com/blogs/can-we-create-method-inside-method-in-c-sharp2

#region Example 1: Multiple Local Functions
//  Below example provides information has to ow we can create multiple local functions. Here 
//  the Main is the method and inside that we have created Sum, Sub, Mul local functions.
/* *

void Main()
{
  Sum(10 ,20).Dump("SUM");
  Sub(10 ,20).Dump("SUB");
  Mul(10 ,20).Dump("MUL");
  Sum( 5 ,15).Dump("SUM");
  
  int Sum(int a, int b)
  {
    return a + b;
  }
  
  int Sub(int a, int b)
  {
    return a - b;
  }
  
  int Mul(int a, int b)
  {
    return a * b;
  }
}

/* */
#endregion

#region Example 2 - Generic Local Functions
//  The below example provides information on how we can create generic location functions. In 
//  this code snippet, we have local functions with generics.
/* */

void Main()
{
  Display<int>(Sum(10, 20));
  Display<string>("Local Functions Demo");

  void Display<T>(T value)
  {
    value.Dump("value");
  }  
}

int Sum(int a, int b)
{
  return a + b;
}

/* */
#endregion