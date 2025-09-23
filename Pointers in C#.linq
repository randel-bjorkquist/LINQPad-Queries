<Query Kind="Program" />

void Main()
{
  int myLuckyNumber;
  
  unsafe
  {
    int* ptr = &myLuckyNumber;
    
    *ptr = 21;

    Console.WriteLine($"Get value: {*ptr}");
    Console.WriteLine($"Get value with ToString: {ptr -> ToString()}");
    Console.WriteLine($"Get address: {(long)ptr}");
  }

  Console.WriteLine($"After unsafe block: {myLuckyNumber}");
}

