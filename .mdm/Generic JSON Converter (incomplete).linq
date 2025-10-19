<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>static UserQuery</Namespace>
</Query>

void Main()
{
//  string json_string = $"{{"
//                     + $"  \"type\": \"{new Person().GetType()}\","
//                     + $"  \"data\":{{"
//                     + $"    \"FirstName\": \"Fred\","
//                     + $"    \"LastName\": \"Flintstone\","
//                     + $"    \"DOB\": \"2000-07-04\","
//                     + $"    \"Active\": true"
//                     + $"  }}"
//                     + $"}}";
//  json_string.Dump();
//  
//  var serialized_object = JsonConvert.SerializeObject(json_string);
//  serialized_object.Dump();
  
//  var fred_flintstone = new Entity { Type = new Person().GetType().ToString()
//  var fred_flintstone = new Entity<Person> { Type = new Person().GetType().ToString()
  var fred_flintstone = new Entity<Person> { Data = new Person { ID = 10
                                                                ,FirstName = "Fred"
                                                                ,LastName  = "Flintstone"
                                                                ,DOB = new DateOnly(1963, 10, 4)
                                                                ,Active = true }};
  
  var serialized_object = JsonConvert.SerializeObject(fred_flintstone);
  serialized_object.Dump();
  
  var type = JsonConvert.DeserializeObject<Type>(serialized_object).type;
  type.Dump();
  
  var t = System.Type.GetType(type);  
  t.Dump();

  //var typeString = typeof(type).AssemblyQualifiedName;
  //var type = "CluedIn.Crawling.MattangPrototype.Core.Models.AModel, CluedIn.Crawling.MattangPrototype.Core, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null";
  //var tm = typeof(JsonUtility)
  //        .GetMethods()
  //        .Where(x => x.Name == "Deserialize")
  //        .FirstOrDefault(x => x.IsGenericMethod);
  //
  //var genericMethod = tm.MakeGenericMethod(t.MakeArrayType());
  //var deserializedObject = genericMethod.Invoke(this, new object[] { command.HttpPostData });

  //var data = JsonConvert.DeserializeObject<Entity<t>>(serialized_object).Data;
  var data = JsonConvert.DeserializeObject<Entity<Person>>(serialized_object).Data;
  data.Dump();
  
//  var obj = Type.GetType(string_type);
////  var obj = Type.GetType(string_type)
////                .GetMethod("")
////                .MakeGenericMethod()
////                .Invoke();
//  
//  
//  
//  
//  obj.Dump();
}

public class Entity<T>
{
  private string _type;
  private T _data;
  
  public string Type => _type;
  public T Data
  {
    get => _data;
    set { 
      _data = value; 
      _type = value.GetType().ToString(); 
    }
  }
}

public class Type
{
  public string type  { get; set; }  
}

public class Person
{
  public int ID           { get; set; }
  
  public string FirstName { get; set; }
  public string LastName  { get; set; }
  
  public DateOnly DOB     { get; set; }
  public bool Active      { get; set; }
}