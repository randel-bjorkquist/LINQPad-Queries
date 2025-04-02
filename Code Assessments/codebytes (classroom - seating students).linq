<Query Kind="Program" />

#region COMMENTED OUT: INSTRUCTIONS
/*

You have to complete the function "SeatingStudents(arr)" which accepts an array of integers stored in the argument 'arr'. The
data in the array, is in the following format: [K, r1, r2, r3, ...], where K represents the number of desks in a classroom 
and the rest of the integers in the array will be in sorted order and will represent the desks that are already occupied. All 
of the desks will be arranged in 2 columns, where desk #1 is at the top left, desk #2 will be at the top right, desk #3 is 
below desk #1, desk #4 is below desk #2, etc. 

Your program should return the number of ways students can be seated next to each other. This means 1 student is on the left 
and 1 student on the right, or 1 student is directly above or below the other student.

For example: if "arr" is [12, 2, 6, 7 11], the classroom should look like this ...
  1      (2)
  3       4
  5      (6)
 (7)      8
  9      10
(11)     12

NOTE: the numbers in parenthesis are occupied and based on the above arrangement, there are a total of 6 ways to seat 2 new 
students next to each other. 

The combinations are: 
  [  1,  3 ]  |             Horizontal: [ 3,  4 ]    
  [  3,  4 ]  |                         [ 9, 10 ]
  [  3,  5 ]  |  Vertical Left-Column:  [ 1, 3  ]
  [  8, 10 ]  |                         [ 3, 5  ]
  [  9, 10 ]  | Vertical Right-Column:  [ 8, 10 ]
  [ 10, 12 ]  |                         [10, 12 ]

So for this input your program should return 6. 

K will range from 2 to 24 and will always be an even number; after K, the number of occupied desks in the array can range 
from 0 to K.

*/
#endregion

void Main()
{
  var test1 = new int[] { 12, 2, 6, 7, 11 };
  var test2 = new int[] { 6 };
  var test3 = new int[] { 6, 4 };
  var test4 = new int[] { 8, 1, 8 };
  var test5 = new int[] { 2 };

  Console.WriteLine($"Test 1: [12, 2, 6, 7, 11] options: {SeatingStudents(test1)}");  // Should return 6
  Console.WriteLine($"Test 2: [6] options: {SeatingStudents(test2)}");                // Should return 7
  Console.WriteLine($"Test 3: [6, 4] options: {SeatingStudents(test3)}");             // Should return 4
  Console.WriteLine($"Test 4: [8, 1, 8] options: {SeatingStudents(test4)}");          // Should return 6
  Console.WriteLine($"Test 5: [2] options: {SeatingStudents(test5)}");                // Should return 1
}

private int SeatingStudents(int[] arr)
{
  int desks = arr[0];     //Number of desks in the classroom
  int rows = desks / 2;   //Number of rows, since 2 columns
  
  bool[,] classroom = new bool[rows + 1, 3];  //building it for 1-based indexing
  
  for(int index = 1; index < arr.Length; index++)
  {
    int desk = arr[index];
    int row  = (desk + 1) / 2;
    int column = (desk % 2 == 0) ? 2: 1;
    
    classroom[row, column] = true;
  }
  
  //NOTE: uncomment to see the 1-base array ...
  //classroom.Dump("classroom");
  
  int count = 0;
  
  for(int row = 1; row <= rows; row++)
  {
    // Check horizontal pairs in the same row (e.g., desks 1-2, 3-4)
    if(!classroom[row, 1] && !classroom[row, 2])
      count++;
  }
  
  for(int row = 1; row < rows; row++)
  {
    // Check vertical pairs in the left column (e.g., desks 1-3, 3-5)
    if(!classroom[row, 1] && !classroom[row + 1, 1])
      count++;
    
    // Check vertical pairs in the right column (e.g., desks 2-4, 4-6)
    if(!classroom[row, 2] && !classroom[row + 1, 2])
      count++;
  }
  
  return count;
}
