<Query Kind="Program" />

void Main()
{
  var value = 5;
  Display(value);

  var values = new List<int> {1, 2, 3, 4, 5, 6};
  Display(values);
}

private void Display(int value)
{
  Display(new []{ value });
}

private void Display(IEnumerable<int> values)
{
  var output = new StringBuilder();

  foreach(var value in values)
  {
    output.Append(value + ", ");
  }
  
  #region Remove the last comma & space
  
  //OPTION 1:
  output.Remove(output.Length - 2, 2)
        .ToString()
        .Dump(values.Count() == 1
                              ? "value"
                              : "values");
    
  //OPTION 2:                             
  //output.ToString()
  //      .Trim(new []{' ', ','})
  //      .Dump(values.Count() == 1
  //                            ? "value"
  //                            : "values");
  
  #endregion
}
