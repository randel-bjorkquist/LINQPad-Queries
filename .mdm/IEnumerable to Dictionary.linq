<Query Kind="Program" />

void Main()
{
  var people = Data.People
                   .ToDictionary( p => p.ID
                                 ,p => p );
                                 
  people.Dump("people", 1);

//  var null_people = Data.Null_People
//                         .ToDictionary( np => np.ID
//                                       ,np => np ) ;
  
  var null_people = (Data.Null_People ?? new List<Person>())
                         .ToDictionary( np => np.ID
                                       ,np => np ) ;

  null_people.Dump("null_people", 1);
}

public static class Data
{
  //public static List<Person> Null_People = null;
  public static IEnumerable<Person> Null_People = null;

  public static IEnumerable<Person> People
  {
    get {
      yield return new Person { ID = 1 ,FirstName = "Fred"  ,LastName = "Flintstone" };
      yield return new Person { ID = 2, FirstName = "Barny" ,LastName = "Rubble"     };
      //,new Person { ID = 3 ,FirstName = "Fred"  ,LastName = "Flintstone" }  
      //,new Person { ID = 4 ,FirstName = "Fred"  ,LastName = "Flintstone" }
    }
  }

  //public static IEnumerable<Person> People
  //  => new List<Person> { new Person { ID = 1 ,FirstName = "Fred"  ,LastName = "Flintstone" }  
  //                       ,new Person { ID = 2 ,FirstName = "Barny" ,LastName = "Rubble"     }  
  //                       //,new Person { ID = 3 ,FirstName = "Fred"  ,LastName = "Flintstone" }  
  //                       //,new Person { ID = 4 ,FirstName = "Fred"  ,LastName = "Flintstone" }
  //                       };
}

public class Person
{
  public int ID           { get; set; }
  public string FirstName { get; set; }
  public string LastName  { get; set; }
}