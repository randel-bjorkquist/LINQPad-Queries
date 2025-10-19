<Query Kind="Program" />

void Main()
{
	// Creating objects of Author and Work class
	var a = new Author { name = "Ankita"
							 				,rank = 5 };

	var w = new Work { articl_no = 80
							 			,improv_no = 50 };

	// Check 'a' is of Author type or not Using is operator
	Console.WriteLine( "a IS Author : {0}"		,	  a is Author );
	Console.WriteLine( "a IS NOT Work : {0}"	, !(a is Work) 	);
	Console.WriteLine( "w IS Author : {0}"		,		w is Author );

	Console.WriteLine();

	// Check w is of Author type using is operator
	Console.WriteLine( "w IS Work: {0}"				,   w is Work		 );
	Console.WriteLine( "w IS NOT Author: {0}"	, !(w is Author) );
	Console.WriteLine( "a IS NOT Work: {0}"		,		a is Work 	 );

	Console.WriteLine();

	// Check null object Using is operator
	Console.WriteLine("Is a is Author? : {0}", a = null);
}

public class Author
{
	public string name;
	public int rank;

	public void details(string n, int r) {
		name = n;
		rank = r;
	}
}

public class Work
{
	public int articl_no;
	public int improv_no;

	public void totalno(int a, int i) {
		articl_no = a;
		improv_no = i;
	}
}

