<Query Kind="Program" />

// dofactory
// resource:  https://dofactory.com/net/strategy-design-pattern
void Main()
{
  SortedList student_records = new SortedList();
  
  student_records.Add("Samual");
  student_records.Add("Jimmy");
  student_records.Add("Sandra");
  student_records.Add("Vivek");
  student_records.Add("Anna");
  
  student_records.SetSortStrategy(new QuickSort());
  student_records.Sort();

  student_records.SetSortStrategy(new ShellSort());
  student_records.Sort();

//NOTE: currently not implemented; so if uncommented, exception will be thrown
//  student_records.SetSortStrategy(new MergeSort());
//  student_records.Sort();
}

/// <summary>The 'Strategy' abstract class</summary>
public abstract class SortStrategy
{
  public abstract void Sort(List<string> list);
}

/// <summary>A 'ConcreateStrategy' class</summary>
public class QuickSort : SortStrategy
{
  public override void Sort(List<string> list)
  {
    "QuickSorted list ".Dump();
    list.Sort();  //default is QuickSort
  }
}

/// <summary>A 'ConcreateStrategy' class</summary>
public class ShellSort : SortStrategy
{
  //public override void Sort(List<string> list)
  //{
  //  list.ShellSort();
  //  "ShellSorted list ".Dump();
  //}

  public override void Sort(List<string> items)
  {
    "ShellSorted list ".Dump();

    //throw new NotImplementedException("The ShellSort is not implemented yet.");
    var array = items.ToArray();
    
    int n   = array.Length;
    int gap = n / 2;
    string tmp;
    int i   = 0;
    int j   = 0;
    
    while(gap > 0)
    {
      for(i = gap; i < n; i++)
      {
        tmp = array[i];
        j = i;
        
        while(j >= gap && string.Compare(array[j - gap], tmp) > 0)
        {
          array[j] = array[j - gap];
          j = j - gap;
        }
        
        array[j] = tmp;
      }
      
      gap = gap / 2;
    }    
  }
}

/// <summary>A 'ConcreateStrategy' class</summary>
public class MergeSort : SortStrategy
{
  public override void Sort(List<string> list)
  {
    "MergeSorted list ".Dump();
    list.MergeSort();
  }
  
  public static int[] Sort(int[] array)
  {
    int[] left;
    int[] right;
    int[] result = new int[array.Length];
    
    //As this is a recursive algorithm, we need to have a base case to 
    //avoid an infinite recursion and therfore a stackoverflow
    if (array.Length <= 1)
      return array;
    
    // The exact midpoint of our array  
    int midPoint = array.Length / 2;
    
    //Will represent our 'left' array
    left = new int[midPoint];

    //if array has an even number of elements, the left and right array will have the same number of 
    //elements
    if (array.Length % 2 == 0)
      right = new int[midPoint];
    //if array has an odd number of elements, the right array will have one more element than left
    else
      right = new int[midPoint + 1];
    
    //populate left array
    for (int i = 0; i < midPoint; i++)
      left[i] = array[i];
    
    //populate right array   
    int x = 0;
    
    //We start our index from the midpoint, as we have already populated the left array from 0 to midpont
    for (int i = midPoint; i < array.Length; i++)
    {
      right[x] = array[i];
      x++;
    }
    
    //Recursively sort the left array
    left = Sort(left);
    
    //Recursively sort the right array
    right = Sort(right);
    
    //Merge our two sorted arrays
    result = merge(left, right);
    
    return result;
  }

  //This method will be responsible for combining our two sorted arrays into one giant array
  public static int[] merge(int[] left, int[] right)
  {
    int resultLength = right.Length + left.Length;
    int[] result     = new int[resultLength];    
    
    int indexLeft   = 0;
    int indexRight  = 0;
    int indexResult = 0;
    
    //while either array still has an element
    while (indexLeft < left.Length || indexRight < right.Length)
    {
      //if both arrays have elements  
      if (indexLeft < left.Length && indexRight < right.Length)
      {
        //If item on left array is less than item on right array, add that item to the result array 
        if (left[indexLeft] <= right[indexRight])
        {
          result[indexResult] = left[indexLeft];
          indexLeft++;
          indexResult++;
        }
        // else the item in the right array wll be added to the results array
        else
        {
          result[indexResult] = right[indexRight];
          indexRight++;
          indexResult++;
        }
      }
      //if only the left array still has elements, add all its items to the results array
      else if (indexLeft < left.Length)
      {
        result[indexResult] = left[indexLeft];
        indexLeft++;
        indexResult++;
      }
      //if only the right array still has elements, add all its items to the results array
      else if (indexRight < right.Length)
      {
        result[indexResult] = right[indexRight];
        indexRight++;
        indexResult++;
      }
    }
    
    return result;
  }
}

public class SortedList
{
  private List<string> _list = new List<string>();
  private SortStrategy _strategy;
  
  public void SetSortStrategy(SortStrategy strategy)
  {
    _strategy = strategy;
  }
  
  public void Add(string name)
  {
    _list.Add(name);
  }
  
  public void Sort()
  {
    _strategy.Sort(_list);
    
    //Iterate over list and display results
    
    foreach(var item in _list)
    {
      item.Dump();
    }
    
    "".Dump();
  }
}

public static class EnumerableExtensions
{
  //source: AlphaCodingSkills
  //url: https://alphacodingskills.com/cs/pages/cs-program-for-shell-sort.php
  public static void ShellSort(this IEnumerable<string> items)
  {
    //throw new NotImplementedException("The ShellSort is not implemented yet.");
    var array = items.ToArray();
    
    int n   = array.Length;
    int gap = n / 2;
    string tmp;
    int i   = 0;
    int j   = 0;
    
    while(gap > 0)
    {
      for(i = gap; i < n; i++)
      {
        tmp = array[i];
        j = i;
        
        while(j >= gap && string.Compare(array[j - gap], tmp) > 0)
        {
          array[j] = array[j - gap];
          j = j - gap;
        }
        
        array[j] = tmp;
      }
      
      gap = gap / 2;
    }
  }

  //resource: C# Corner
  //https://www.c-sharpcorner.com/blogs/a-simple-merge-sort-implementation-c-sharp
  public static void MergeSort(this IEnumerable<string> items)
  {
    throw new NotImplementedException("The MergeSort is not implemented yet.");
  }

  
  public static int[] MergeSort(this int[] array)
  {
    int[] left;
    int[] right;
    int[] result = new int[array.Length];
    
    //As this is a recursive algorithm, we need to have a base case to 
    //avoid an infinite recursion and therfore a stackoverflow
    if (array.Length <= 1)
      return array;
    
    // The exact midpoint of our array  
    int midPoint = array.Length / 2;
    
    //Will represent our 'left' array
    left = new int[midPoint];

    //if array has an even number of elements, the left and right array will have the same number of 
    //elements
    if (array.Length % 2 == 0)
      right = new int[midPoint];
    //if array has an odd number of elements, the right array will have one more element than left
    else
      right = new int[midPoint + 1];
    
    //populate left array
    for (int i = 0; i < midPoint; i++)
      left[i] = array[i];
    
    //populate right array   
    int x = 0;
    
    //We start our index from the midpoint, as we have already populated the left array from 0 to midpont
    for (int i = midPoint; i < array.Length; i++)
    {
      right[x] = array[i];
      x++;
    }
    
    //Recursively sort the left array
    left = MergeSort(left);
    
    //Recursively sort the right array
    right = MergeSort(right);
    
    //Merge our two sorted arrays
    result = MergeSort(left, right);
    
    return result;
  }

  //This method will be responsible for combining our two sorted arrays into one giant array
  public static int[] MergeSort(int[] left, int[] right)
  {
    int resultLength = right.Length + left.Length;
    int[] result     = new int[resultLength];    
    
    int indexLeft   = 0;
    int indexRight  = 0;
    int indexResult = 0;
    
    //while either array still has an element
    while (indexLeft < left.Length || indexRight < right.Length)
    {
      //if both arrays have elements  
      if (indexLeft < left.Length && indexRight < right.Length)
      {
        //If item on left array is less than item on right array, add that item to the result array 
        if (left[indexLeft] <= right[indexRight])
        {
          result[indexResult] = left[indexLeft];
          indexLeft++;
          indexResult++;
        }
        // else the item in the right array wll be added to the results array
        else
        {
          result[indexResult] = right[indexRight];
          indexRight++;
          indexResult++;
        }
      }
      //if only the left array still has elements, add all its items to the results array
      else if (indexLeft < left.Length)
      {
        result[indexResult] = left[indexLeft];
        indexLeft++;
        indexResult++;
      }
      //if only the right array still has elements, add all its items to the results array
      else if (indexRight < right.Length)
      {
        result[indexResult] = right[indexRight];
        indexRight++;
        indexResult++;
      }
    }
    
    return result;
  }  
}



