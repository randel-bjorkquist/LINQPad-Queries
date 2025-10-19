<Query Kind="Program" />

//resource => https://stackoverflow.com/questions/39792207/storing-the-content-of-a-2-dimension-c-sharp-array-into-a-csv-file
//top answer by => 'Dmitry Bychenko'
void Main()
{
  StreamWriter file = new StreamWriter("C:/mylocation/data.csv");
  //my2darray  is my 2d array created.
  //  int[][] jagged my2darray = new int [,];
  //int[][] jagged my2darray = new int[,] {{0, 1, 2},
  int [][] my2darray = new int[2][]; // { {0, 1, 2, 1},
                                     //  {0, 2, 4, 6} };
  
  for(int i = 0; i < my2darray.GetLength(1); i++)
  {
    for(int j = 0; j < my2darray.GetLength(0); j++)
    {
      file.Write(my2darray[i][j]);
      file.Write("\t");
    }
    
    file.Write("\n"); // go to next line
  }
}


private static IEnumerable<String> ToCsv<T>(T[,] data, string separator = ",")
{
  for (int i = 0; i < data.GetLength(0); ++i)
  {
    // simplest, we don't expect ',' and '"' in the items
    yield return string.Join(separator, Enumerable.Range(0, data.GetLength(1))
                       .Select(j => data[i, j]));
  }
}