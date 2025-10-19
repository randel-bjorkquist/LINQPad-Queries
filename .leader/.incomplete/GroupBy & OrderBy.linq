<Query Kind="Program" />

void Main()
{

  #region Dictionary<Pet, List<Employee>>()
  
  var pets_employees_1 = new Dictionary<string, List<Employee>>();

  foreach (var employee in Data.Employees)
  {
    foreach(var pet in employee.Pets as IEnumerable<Pet>)
    {
      if(!pets_employees_1.ContainsKey(pet.Name))
      {
        pets_employees_1.Add(pet.Name, new List<Employee> { employee });
        continue;
      }
      
      pets_employees_1[pet.Name].Add(employee);
    }
  }
  
  pets_employees_1.Dump("pets_employees_1", 0);
  
  var pets_employees_2 = new Dictionary<Pet, List<Employee>>();

  foreach (var employee in Data.Employees)
  {
    foreach(var pet in employee.Pets as IEnumerable<Pet>)
    {
      if(!pets_employees_2.ContainsKey(pet))
      {
        pets_employees_2.Add(pet, new List<Employee> { employee });
        continue;
      }
      
      pets_employees_2[pet].Add(employee);
    }
  }
  
  pets_employees_2.Dump("pets_employees_2", 0);
  
  #endregion
  
  #region Query (SQL "like") Syntax --------------------------------------------------------------------------
  "Query (SQL \"like\") Syntax".Dump();
  
  (
    from emp in Data.Employees
    group emp by new { emp.Department, emp.Gender } into empGroup
    orderby empGroup.Key.Department
           ,empGroup.Key.Gender
    select new { Dept      = empGroup.Key.Department
                ,Gender    = empGroup.Key.Gender
                ,Employees = empGroup.OrderBy(o => o.Name) }
  ).Dump("Data.Employees.GroupBy(dept, gender).OrderBy(dept, gender).Select(new {Dept, Gender, Employees})", 0);

  //Same thing, only different ... assigning result to a variable, then Dumping it out.
  //var employees = from emp in Data.Employees
  //                group emp by new { emp.Department, emp.Gender } into empGroup
  //                orderby empGroup.Key.Department,
  //                        empGroup.Key.Gender
  //                select new { Dept      = empGroup.Key.Department
  //                            ,Gender    = empGroup.Key.Gender
  //                            ,Employees = empGroup.OrderBy(o => o.Name) };
  //                            
  //employees.Dump("Data.Employees.GroupBy(dept, gender).OrderBy(dept, gender).Select(new {Dept, Gender, Employees})", 0);
  
  #endregion "Query (SQL "like") Syntax"
  
  #region Extension Method Syntax Style ----------------------------------------------------------------------
  "Extension Method Syntax Style".Dump();
  
  //Data.Employees
  //    .Dump("employees", 1);
  //
  //Data.Employees
  //    .GroupBy(e => e.Department)
  //    .Dump("employees.GroupBy(dept)", 1);
  //
  //Data.Employees
  //    .GroupBy(e => new {e.Department, e.Gender })
  //    .Dump("employees.GroupBy(dept, gender)", 1);
  //
  //Data.Employees
  //    .GroupBy(e => new {e.Department, e.Gender })
  //    .OrderBy(e => e.Key.Department)
  //    .ThenBy(e => e.Key.Gender)
  //    .Dump("employees.GroupBy(dept, gender).OrderBy(dept, gender)", 1);

  Data.Employees
      .GroupBy(employee => new {employee.Department, employee.Gender })
      .OrderBy(group => group.Key.Department)
      .ThenBy(group => group.Key.Gender)
      .Select(group => new { Dept   = group.Key.Department
                            ,Gender = group.Key.Gender
                            ,Employees = group.OrderBy(e => e.Name) })
      .Dump("Data.Employees.GroupBy(dept, gender).OrderBy(dept, gender).Select(new {Dept, Gender, Employees})", 0);

  #endregion "Extension Method Syntax Style"
  
  #region Pets -----------------------------------------------------------------------------------------------
  
  #region Query Syntax -------------------------------------------------------------------
  //OPTION 1: IEnumerable<IEnumerable>, explicitly cast to IEnumerable<Pet>, then OrderBy, LASTLY Method Syntax Distinct
  //var pet_list = (from employee_pets in Data.Employees.Select(e => e.Pets)
  //                from pets in employee_pets as IEnumerable<Pet>
  //                orderby pets.ID
  //                select pets).Distinct();
  //
  //pet_list?.Dump("pet_list", 1);
  
  //OPTION 2: IEnumerable<IEnumerable>, explicitly cast to IEnumerable<Pet>, then Method Syntax Distinct, LASTLY OrderBy
  //var pet_list = (from employee_pets in Data.Employees.Select(e => e.Pets)
  //                from pets in employee_pets as IEnumerable<Pet>
  //                select pets).Distinct()
  //                            .OrderBy(o => o.ID);
  //
  //pet_list?.Dump("pet_list", 1);

  //OPTION 3: IEnumerable<IEnumerable> as IEnumerable<Pet>, then OrderBy, LASTLY Method Syntax Distinct
  //var pet_list = (from employee_pets in Data.Employees.Select(e => e.Pets as IEnumerable<Pet>)
  //                from pets in employee_pets
  //                orderby pets.ID
  //                select pets).Distinct();
  //
  //pet_list?.Dump("pet_list", 1);

  //OPTION 4: IEnumerable<IEnumerable> as IEnumerable<Pet>, then Method Syntax Distinct, LASTLY OrderBy
  //var pet_list = (from employee_pets in Data.Employees.Select(e => e.Pets as IEnumerable<Pet>)
  //                from pets in employee_pets
  //                select pets).Distinct()
  //                            .OrderBy(o => o.ID);
  //
  //pet_list?.Dump("pet_list", 1);
  
  #endregion "Query Syntax"
  
  #region Method Syntax ------------------------------------------------------------------
  
  //var pets = Data.Employees
  //               .Select(e => e.Pets)
  //               .SelectMany(p => p as IEnumerable<Pet>)
  //               .Distinct()
  //               .OrderBy(o => o.ID);
  
  var pets = Data.Employees
                 .SelectMany(e => e.Pets as IEnumerable<Pet>)
                 .Distinct()
                 .OrderBy(o => o.ID)
                 .ToList();
  
  pets.Dump("Pets.Distinct.OrderBy(ID)", 0);
  
  
  
  //var pets_with_owners = Data.Employees
  //                           .Where(e => pets.Select(p => p.ID)
  //                                           //.Contains((e.Pets as IEnumerable<Pet>).Select(p => p.ID)))
  //                                           //.Contains((e.Pets as IEnumerable<Pet>).SelectMany(p => p.ID)
  //                                           .Contains((e.Pets).Cast<IEnumerable<Pet>>()
  //                                                             .SelectMany(p => p)
  //                                                             .Select(p => p.ID))
  //                                           ;
  //
  //pets.Select(p => p.ID)
  //    .Distinct()
  //    .OrderBy(o => o)
  //    .Dump("p.ID", 0);
  //
  //Data.Employees
  //    .SelectMany(e => e.Pets as IEnumerable<Pet>)
  //    .Select(p => p.ID)
  //    .Distinct()
  //    .OrderBy(o => o)
  //    .Dump("Data.Employees.Pets.IDs", 0);
  
  //var employee_groupedby_pets = from emp in Data.Employees
  //                              group emp by new { emp.Pets }
  
  #endregion "Method Syntax"
  
  #endregion
}

// You can define other methods, fields, classes and namespaces here

public class Employee
{
  public Employee()
  {
    //Pets = new List<PetType>();
    Pets = Enumerable.Empty<PetType>();
  }
  
  public int ID             { get; set; }
  
  public string FirstName   { get; set; }
  public string LastName    { get; set; }
  
  public string Gender      { get; set; }
  
  public string Department  { get; set; }
  public int Salary         { get; set; }
  
  public IEnumerable Pets   { get; set; }

  public string Name
  {
    get { return $"{this.FirstName} {this.LastName}"; }
  }
}

public class Pet {
  public int ID       { get; set; }
  public string Name  { get; set; }
  public PetType Type { get; set; }
}

[Flags]
public enum PetType : short {
   Cat  = 0
  ,Dog  = 1
  ,Fish = 2
  ,Bird = 4
}

public static class Data 
{
  public static IEnumerable<Employee> Employees {
    get {
      var employees = new List<Employee> { new Employee { ID = 1  ,FirstName = "Mark"    ,LastName = "" ,Gender = "Male"   ,Department = "IT" ,Salary = 45000 }
                                          ,new Employee { ID = 2  ,FirstName = "Steve"   ,LastName = "" ,Gender = "Male"   ,Department = "HR" ,Salary = 55000 } 
                                          ,new Employee { ID = 3  ,FirstName = "Ben"     ,LastName = "" ,Gender = "Male"   ,Department = "IT" ,Salary = 65000 }
                                          ,new Employee { ID = 4  ,FirstName = "Philip"  ,LastName = "" ,Gender = "Male"   ,Department = "IT" ,Salary = 55000 }
                                          ,new Employee { ID = 5  ,FirstName = "Mary"    ,LastName = "" ,Gender = "Female" ,Department = "HR" ,Salary = 48000 }
                                          ,new Employee { ID = 6  ,FirstName = "Valarie" ,LastName = "" ,Gender = "Female" ,Department = "HR" ,Salary = 70000 }
                                          ,new Employee { ID = 7  ,FirstName = "John"    ,LastName = "" ,Gender = "Male"   ,Department = "IT" ,Salary = 64000 }
                                          ,new Employee { ID = 8  ,FirstName = "Pam"     ,LastName = "" ,Gender = "Female" ,Department = "IT" ,Salary = 65400 }
                                          ,new Employee { ID = 9  ,FirstName = "Stacey"  ,LastName = "" ,Gender = "Female" ,Department = "HR" ,Salary = 84000 }
                                          ,new Employee { ID = 10 ,FirstName = "Andy"    ,LastName = "" ,Gender = "Male"   ,Department = "IT" ,Salary = 36000 }};
      FillPets(employees);
      return employees; 
    }
  }

  private static void FillPets(IEnumerable<Employee> employees) {
    var pets = new List<Pet> { new Pet { ID = 1 ,Name = "Blue"        ,Type = PetType.Dog  }
                              ,new Pet { ID = 2 ,Name = "Lucy"        ,Type = PetType.Dog  }
                              ,new Pet { ID = 3 ,Name = "Bearegard"   ,Type = PetType.Dog  }
                              ,new Pet { ID = 4 ,Name = "Oliver"      ,Type = PetType.Dog  } 
                              ,new Pet { ID = 5 ,Name = "Spike"       ,Type = PetType.Cat  }
                              ,new Pet { ID = 6 ,Name = "Peter"       ,Type = PetType.Bird }
                              ,new Pet { ID = 7  ,Name = "Nemo"       ,Type = PetType.Fish }
                              ,new Pet { ID = 8  ,Name = "Dora"       ,Type = PetType.Fish }
                              ,new Pet { ID = 9  ,Name = "Gill"       ,Type = PetType.Fish }
                              ,new Pet { ID = 10 ,Name = "Marlin"     ,Type = PetType.Fish }
                              ,new Pet { ID = 11 ,Name = "Darla"      ,Type = PetType.Cat  }
                              ,new Pet { ID = 12 ,Name = "Bruce"      ,Type = PetType.Dog  }
                              ,new Pet { ID = 13 ,Name = "Astro"      ,Type = PetType.Dog  }
                              ,new Pet { ID = 14 ,Name = "Calimero"   ,Type = PetType.Bird }
                              ,new Pet { ID = 15 ,Name = "Daffy Duck" ,Type = PetType.Bird } };
    
    foreach(var employee in employees){
      switch(employee.ID){
        default:
          break;

        case 1: //Employee.Mark 
          employee.Pets = pets.Where(p => (new [] { 1, 2, 3, 4}).Contains(p.ID));
          break;

        case 2: //Employee.Steve 
          employee.Pets = pets.Where(p => (new [] { 5, 6, 7, 8}).Contains(p.ID));
          break;                                  
                                                  
        case 3: //Employee.Ben                    
          employee.Pets = pets.Where(p => (new [] { 9, 10 }).Contains(p.ID));
          break;                                  
                                                  
        case 4: //Employee.Philip                 
          employee.Pets = pets.Where(p => (new [] { 11, 12, 13 }).Contains(p.ID));
          break;                                  
                                                  
        case 5: //Employee.Mary                   
          employee.Pets = pets.Where(p => (new [] { 1, 14}).Contains(p.ID));
          break;                                  
                                                  
        case 6: //Employee.Valarie                
          employee.Pets = pets.Where(p => (new [] { 2, 15}).Contains(p.ID));
          break;                                  
                                                  
        case 7: //Employee.John                   
          employee.Pets = pets.Where(p => (new [] { 10, 12 }).Contains(p.ID));
          break;                                  
                                                  
        case 8: //Employee.Pam                    
          employee.Pets = pets.Where(p => (new [] { 5, 10 }).Contains(p.ID));
          break;                                  
                                                  
        case 9: //Employee.Stacey                 
          employee.Pets = pets.Where(p => (new [] { 1, 2, 3, 4 }).Contains(p.ID));
          break;                                  
                                                  
        case 10: //Employee.Andy                  
          employee.Pets = pets.Where(p => (new [] { 8, 9, 15 }).Contains(p.ID));
          break;                                  
      }
    }
  }
}