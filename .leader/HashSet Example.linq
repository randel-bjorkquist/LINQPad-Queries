<Query Kind="Program" />

void Main()
{
  HashSet<int> evenNumbers = new HashSet<int>();
  HashSet<int> oddNumbers  = new HashSet<int>();
  
  for(int i = 0; i < 5; i++)
  {
    // Populate numbers with just even numbers.
    evenNumbers.Add(i * 2);

    // Populate oddNumbers with just odd numbers.
    oddNumbers.Add((i * 2) + 1);
  }

  Console.WriteLine($"evenNumbers contains {evenNumbers.Count} elements: {{ {string.Join(", ", evenNumbers)} }}");
  Console.WriteLine($" oddNumbers contains {oddNumbers.Count} elements: {{ {string.Join(", ", oddNumbers)} }}");

  // Create a new HashSet populated with even numbers.
  HashSet<int> numbers = new HashSet<int>(evenNumbers);
  
  Console.WriteLine();
  Console.WriteLine("numbers UnionWith oddNumbers...");
  numbers.UnionWith(oddNumbers);

  Console.WriteLine();
  Console.WriteLine("numbers Except oddNumbers...");
  numbers.Except(oddNumbers);

  Console.WriteLine();
  Console.WriteLine($"numbers contains {numbers.Count} elements: {{ {string.Join(", ", numbers)} }}");

  for (int i = 0; i < 5; i++)
  {
    // Populate numbers with just even numbers.
    evenNumbers.Add(i * 2);

    // Populate oddNumbers with just odd numbers.
    oddNumbers.Add((i * 2) + 1);
  }

  numbers.UnionWith(evenNumbers);
  numbers.UnionWith(oddNumbers);

  Console.WriteLine();
  Console.WriteLine($"numbers contains {numbers.Count} elements: {{ {string.Join(", ", numbers)} }}");

  /* This example produces output similar to the following:
  * evenNumbers contains 5 elements: { 0 2 4 6 8 }
  * oddNumbers contains 5 elements: { 1 3 5 7 9 }
  * numbers UnionWith oddNumbers...
  * numbers contains 10 elements: { 0 2 4 6 8 1 3 5 7 9 }
  */
}
