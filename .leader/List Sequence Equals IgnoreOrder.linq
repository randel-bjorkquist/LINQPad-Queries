<Query Kind="Program" />

void Main()
{
  var list1 = new List<int> { 1, 2, 3, 1 };
  var list2 = new List<int> { 2, 1, 3, 1 };
  var list3 = new List<int> { 2, 2, 3, 2 };
  
  var question_IDs    = new List<int> { 2, 3, 1, 4 };
  var new_answer_IDs  = new List<int> { 2, 4 };
  
  var complete = Enumerable.SequenceEqual(new_answer_IDs.OrderBy(id => id),
                                          question_IDs.OrderBy(id => id));
  
  if(!complete)
  {
    var current_answer_IDs  = new List<int> { 3, 1 };
    var all_answer_IDs = current_answer_IDs.Select(id => id)
                                           .Concat(new_answer_IDs);
                                           
    complete = Enumerable.SequenceEqual(all_answer_IDs.OrderBy(id => id),
                                        question_IDs.OrderBy(id => id));
  }
  
  complete.Dump("complete");
  
  Enumerable.SequenceEqual(list1.OrderBy(o => o),
                           list2.OrderBy(o => o)).Dump();
  Enumerable.SequenceEqual(list1.OrderBy(o => o),
                           list3.OrderBy(o => o)).Dump();
  Enumerable.SequenceEqual(list2.OrderBy(o => o),
                           list3.OrderBy(o => o)).Dump();

  Console.WriteLine();

  list1.SequenceEqualsIgnoreOrder(list2).Dump();  //True
  list1.SequenceEqualsIgnoreOrder(list3).Dump();  //False
  list2.SequenceEqualsIgnoreOrder(list3).Dump();  //False
  
  Console.WriteLine();
  
  list1.SequenceEqual(list2).Dump();              //False 
  list1.SequenceEqual(list3).Dump();              //False 
  list2.SequenceEqual(list3).Dump();              //False 
}

public static class IEnumerableExtensions
{
  //-------------------------------------------------------------------------------------------
  public static bool SequenceEqualsIgnoreOrder<T>(this IEnumerable<T> list1
                                                      ,IEnumerable<T> list2
                                                      ,IEqualityComparer<T> comparer = null)
  {
    if(list1 is ICollection<T> ilist1 && 
       list2 is ICollection<T> ilist2 && 
       ilist1.Count != ilist2.Count)
    {
      return false;
    }

    if(comparer == null)
    {
      comparer = EqualityComparer<T>.Default;
    }

    var itemCounts = new Dictionary<T, int>(comparer);
    
    foreach (T s in list1)
    {
      if (itemCounts.ContainsKey(s))
      {
        itemCounts[s]++;
      }
      else
      {
        itemCounts.Add(s, 1);
      }
    }
    
    foreach (T s in list2)
    {
      if (itemCounts.ContainsKey(s))
      {
        itemCounts[s]--;
      }
      else
      {
        return false;
      }
    }
    
    return itemCounts.Values.All(c => c == 0);
  }
  
  //-------------------------------------------------------------------------------------------
}