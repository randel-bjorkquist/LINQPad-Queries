<Query Kind="Program" />

void Main()
{
  ReleaseReview(null, null);
  ReleaseReview(null, new ReviewCycleUid("blah"));
  ReleaseReview(new ReviewUid("boo"), null);
  ReleaseReview(new ReviewUid(""), new ReviewCycleUid(""));
  ReleaseReview(new ReviewUid("boo"), new ReviewCycleUid("blah"));  
}

public const string INVALID_ARGUMENTS = "One or more of the arguments are not valid.";

public void ReleaseReview(ReviewUid reviewId, ReviewCycleUid reviewCycleId)
{
  var arguments = new Dictionary<string, object>() { { "reviewId"       ,reviewId }
                                                    ,{ "reviewCylcleId" ,reviewCycleId} };

  //arguments.Select(a => a).Dump();
  //string.Join("", arguments.Select(a => new { a.Key, a.Value})).Dump();
  //  string.Join(", ", arguments.Select(a => $"{a.Key} = '{a.Value}'")).Dump();

  if (reviewId == null || reviewCycleId == null)
  {
    //Console.WriteLine( INVALID_ARGUMENTS
    //                  + $" The supplied values are: {string.Join(", ", arguments.Select(a => $"{a.Key} = '{a.Value}'"))}" );

    Console.WriteLine( INVALID_ARGUMENTS
                      + $" Values: {string.Join(", ", arguments.Select(a => $"{a.Key} = '{a.Value}'"))}.");
    
    //Console.WriteLine( INVALID_ARGUMENTS 
    //                  + $" NULL VALUES: The supplied values are: reviewId ({reviewId})"
    //                  + $" and reviewCycledId ({reviewCycleId})." );
    
    //var reviewIdValue      = (string.IsNullOrWhiteSpace((string)reviewId)      ? "null" : (string)reviewId);
    //var reviewCycleIdValue = (string.IsNullOrWhiteSpace((string)reviewCycleId) ? "null" : (string)reviewCycleId);

    //Console.WriteLine( INVALID_ARGUMENTS 
    //                  + $" The supplied values are: reviewId ({reviewIdValue})"
    //                  + $" and reviewCycledId ({reviewCycleIdValue})." );
    
    return;
  }

  Console.WriteLine();
  Console.WriteLine("Arguments are valid."
                    + $" The supplied values are: reviewId ({reviewId})"
                    + $" and reviewCycledId ({reviewCycleId}).");

}

public class ReviewUid : Uid
{
//  protected ReviewUid(string primitive) : base(primitive)
  public ReviewUid(string primitive) : base(primitive)
  {
  }

  public static explicit operator ReviewUid(string from)
  {
    return new ReviewUid(from);
  }
}

public class ReviewCycleUid : Uid
{
//  protected ReviewCycleUid(string primitive) : base(primitive)
  public ReviewCycleUid(string primitive) : base(primitive)
  {
  }

  public static explicit operator ReviewCycleUid(string from)
  {
    return new ReviewCycleUid(from);
  }
}

public class Uid
{
  protected string _primitive;
  
  protected Uid(string primitive)
  {
    _primitive = primitive;
  }

  public static explicit operator string(Uid from)
  {
    return from._primitive;
  }

  public static explicit operator Uid(string from)
  {
    return new Uid(from);
  }

  public override string ToString() => _primitive;

  public override bool Equals(object obj)
  {
    if (obj is Uid uid)
    {
      return uid._primitive == this._primitive;
    }
    return false;
  }

  public override int GetHashCode()
  {
    return _primitive.GetHashCode();
  }

  public static bool operator ==(Uid b1, Uid b2)
  {
    if (b1 is null)
      return b2 is null;

    return b1.Equals(b2);
  }

  public static bool operator !=(Uid b1, Uid b2)
  {
    return !(b1 == b2);
  }
}




