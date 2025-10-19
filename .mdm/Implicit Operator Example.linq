<Query Kind="Program" />

void Main()
{
  Dog dog = new Dog {Name = "Spot" };
  dog.Dump();
  
//  Animal pig = dog;
//  Animal pig = (Animal) new Dog {Name = "Spot" };
  Animal pig = (Animal) new Dog {Name = "Spot" };
  pig.Dump();
}

// You can define other methods, fields, classes and namespaces here
public class Dog
{
  public string Name { get; set; }
}

public class Animal
{
  public string Name { get; set; }

//  public static explicit operator Animal(Dog dog)
//  {
//    return new Animal { Name = dog.Name };
//  }

  public static implicit operator Animal(Dog dog)
  {
    return new Animal { Name = dog.Name };
  }
}