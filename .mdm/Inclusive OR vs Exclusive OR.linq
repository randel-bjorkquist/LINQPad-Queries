<Query Kind="Program" />

/// <summary>
/// resource url: 
/// 	https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators#logical-exclusive-or-operator-
/// </summary>

void Main()
{
	//INCLUSIVE OR => either or, or both/all can be true
	( true  | true  ).Dump("Inclusive OR (true ^ true) == true");
	( true  | false ).Dump("Inclusive OR (true ^ false) == true");
	( false | true	).Dump("Inclusive OR (false ^ true) == true");
	( false | false ).Dump("Inclusive OR (false ^ false) == false");
	
	//EXCLUSIVE OR => either or can be true, NOT BOTH/ALL
	( true  ^ true  ).Dump("Exclusive OR (true ^ true) == false");
	( true  ^ false ).Dump("Exclusive OR (true ^ false) == true");
	( false ^ true	).Dump("Exclusive OR (false ^ true) == true");
	( false ^ false ).Dump("Exclusive OR (false ^ false) == false");
}
