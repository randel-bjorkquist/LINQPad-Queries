<Query Kind="Program">
  <Namespace>System.Net</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Security</Namespace>
</Query>

void Main()
{
  //DateTimeOffset? blah = new DateTimeOffset(DateTime.UtcNow, new TimeSpan());
  //DateTimeOffset? now = new DateTimeOffset(DateTime.Now);
  //DateTimeOffset? utc_now = new DateTimeOffset(DateTime.UtcNow);
  //
  //now.Dump("now");
  //utc_now.Dump("utc_now");

  foreach(var car in Cars())
  {
    $"ID: {car.ID}, Color: {car.Color}, Wheels: {car.Wheels}".Dump();
  }
  
  Console.WriteLine();
  
  foreach(var motorcycle in Motorcycles())
  {
    $"ID: {motorcycle.ID}, Color: {motorcycle.Color}, Wheels: {motorcycle.Wheels}".Dump();
  }

  var vehicles = Cars().Concat(Motorcycles());
  Console.WriteLine();
  PrintVehicleDetails(vehicles);
  
  foreach(var vehicle in PrintVehicleDetails(vehicles))
  {
    $"ID: {vehicle.ID}, Color: {vehicle.Color}, Wheels: {vehicle.Wheels}, Type: {vehicle.Type}".Dump();
  }
}

internal IEnumerable<VehicleDTO> PrintVehicleDetails(IEnumerable<Vehicle> vehicles) {
  foreach(var vehicle in vehicles) {
    yield return new VehicleDTO { ID = vehicle.ID
                                 ,Color = vehicle.Color
                                 ,Wheels = vehicle.Wheels
                                 ,Type = vehicle.GetType()
                                                .ToString()
                                                .Split("+")[1] };
  }
}

internal IEnumerable<Vehicle> Cars() {
  IEnumerable<Vehicle> cars = new List<Vehicle> { new Car { ID = 1, Color = "black" }
                                                 ,new Car { ID = 2, Color = "white" }
                                                 ,new Car { ID = 3, Color = "red"   }
                                                 ,new Car { ID = 4, Color = "green" }
                                                 ,new Car { ID = 5, Color = "yellow" }};
                                             
  foreach(var car in cars){
    yield return car;
  }
}

internal IEnumerable<Vehicle> Motorcycles() {
  IEnumerable<Vehicle> motorcycles = new List<Vehicle> { new Motorcycle { ID = 10, Color = "Silver" }
                                                        ,new Motorcycle { ID = 11, Color = "Purple" }
                                                        ,new Motorcycle { ID = 12, Color = "Blue"   }
                                                        ,new Motorcycle { ID = 13, Color = "Grey"   }};

  foreach(var motorcycle in motorcycles){
    yield return motorcycle;
  }
}

internal class VehicleDTO
{
  public int ID       { get; set; }
  public string Color { get; set; }
  public short Wheels { get; set; }
  public string Type  { get; set; }
}

internal abstract class Vehicle
{
  public int ID               { get; set; }
  public string Color         { get; set; }
  public virtual short Wheels { get; private set; }
}

internal class Car : Vehicle
{
  public override short Wheels => 4;
}

internal class Motorcycle : Vehicle
{
  public override short Wheels => 2;
}

