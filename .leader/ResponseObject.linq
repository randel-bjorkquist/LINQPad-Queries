<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
  var success_message = "yipee yahoo";
  var failure_message = "boo hoo hoo";

  ResponseBase.DEFAULT_SUCCESS.Dump("ResponseBase.DEFAULT_SUCCESS", 0);
  ResponseBase.DEFAULT_FAILURE.Dump("ResponseBase.DEFAULT_FAILURE", 0);

  ResponseBase.SERVER_ERROR.Dump("ResponseBase.SERVER_ERROR", 0);
  ResponseBase.GENERIC_ERROR.Dump("ResponseBase.GENERIC_ERROR", 0);
  ResponseBase.UNAUTHORIZED.Dump("ResponseBase.UNAUTHORIZED", 0);
  
  ResponseBase.MISSING_OR_INVALID_PARAMETERS.Dump("ResponseBase.MISSING_OR_INVALID_PARAMETERS", 0);
  
  ResponseBase.SUCCESS(success_message          ).Dump("ResponseBase.SUCCESS", 0);
  ResponseBase.SUCCESS(success_message, "012210").Dump("ResponseBase.SUCCESS", 0);

  ResponseBase.FAILURE(failure_message         ).Dump("ResponseBase.FAILURE", 0);
  ResponseBase.FAILURE(failure_message, "98789").Dump("ResponseBase.FAILURE", 0);

  Console.WriteLine();
  
  ResponseObject<Review>.DEFAULT_SUCCESS.Dump("ResponseObject<Review>.DEFAULT_SUCCESS", 0);
  ResponseObject<Review>.DEFAULT_FAILURE.Dump("ResponseObject<Review>.DEFAULT_FAILURE", 0);
  
  ResponseObject<Review>.SERVER_ERROR.Dump("ResponseObject<Review>.SERVER_ERROR", 0);
  ResponseObject<Review>.GENERIC_ERROR.Dump("ResponseObject<Review>.GENERIC_ERROR", 0);
  ResponseObject<Review>.UNAUTHORIZED.Dump("ResponseObject<Review>.UNAUTHORIZED", 0);
  
  ResponseObject<Review>.MISSING_OR_INVALID_PARAMETERS.Dump("ResponseObject<Review>.MISSING_OR_INVALID_PARAMETERS", 0);

  ResponseObject<Review>.SUCCESS(success_message          ).Dump("ResponseObject<Review>.SUCCESS", 0);
  ResponseObject<Review>.SUCCESS(success_message, "012210").Dump("ResponseObject<Review>.SUCCESS", 0);

  ResponseObject<Review>.FAILURE(failure_message         ).Dump("ResponseObject<Review>.FAILURE", 0);
  ResponseObject<Review>.FAILURE(failure_message, "98789").Dump("ResponseObject<Review>.FAILURE", 0);

  var review_cycle = new ReviewCycle() {
                           Id = new ReviewCycleUid("0x00001")
                          ,Name = "blah blah blah"
                          ,Description = "don't know what to put here"
                         };

  ResponseObject<ReviewCycle>.SUCCESS(review_cycle                           ).Dump("ResponseObject<ReviewCycle>.SUCCESS", 0);
  ResponseObject<ReviewCycle>.SUCCESS(review_cycle, success_message          ).Dump("ResponseObject<ReviewCycle>.SUCCESS", 1);
  ResponseObject<ReviewCycle>.SUCCESS(review_cycle, success_message, "012210").Dump("ResponseObject<ReviewCycle>.SUCCESS", 2);
  
  ResponseObject<ReviewCycle>.FAILURE(failure_message         ).Dump("ResponseObject<ReviewCycle>.FAILURE", 0);
  ResponseObject<ReviewCycle>.FAILURE(failure_message, "98789").Dump("ResponseObject<ReviewCycle>.FAILURE", 0);
}

public class ResponseObject<T> : ResponseBase
{
  #region Static Property(s) => hiding the ResponseBase.Property(s)
  
  [JsonIgnoreAttribute]
  new public static ResponseObject<T> DEFAULT_SUCCESS
    = new ResponseObject<T>(ResponseBase.DEFAULT_SUCCESS) { };
    
  [JsonIgnoreAttribute]
  new public static ResponseObject<T> DEFAULT_FAILURE
    = new ResponseObject<T>(ResponseBase.DEFAULT_FAILURE) { };
  
  [JsonIgnoreAttribute]
  new public static ResponseObject<T> SERVER_ERROR  
    = new ResponseObject<T>(ResponseBase.SERVER_ERROR);
  
  [JsonIgnoreAttribute]
  new public static ResponseObject<T> GENERIC_ERROR 
    = new ResponseObject<T>(ResponseBase.GENERIC_ERROR);
  
  [JsonIgnoreAttribute]
  new public static ResponseObject<T> UNAUTHORIZED  
    = new ResponseObject<T>(ResponseBase.UNAUTHORIZED);
  
  [JsonIgnoreAttribute]
  new public static ResponseObject<T> MISSING_OR_INVALID_PARAMETERS 
    = new ResponseObject<T>(ResponseBase.MISSING_OR_INVALID_PARAMETERS);

  #endregion
  
  #region Static Method(s)

  public static ResponseObject<T> SUCCESS(T data) 
    => new ResponseObject<T>(ResponseBase.DEFAULT_SUCCESS) { Data = data };

  public static ResponseObject<T> SUCCESS(T data, string message, string code = null)
    => new ResponseObject<T>(ResponseBase.SUCCESS(message, code)) { Data = data };
  
  #region hiding the ResponseBase.Method(s)
  
  new public static ResponseObject<T> SUCCESS(string message, string code = null)
    => new ResponseObject<T>(ResponseBase.SUCCESS(message, code)) { };

  new public static ResponseObject<T> FAILURE(string message, string code = null)
    => new ResponseObject<T>(ResponseBase.FAILURE(message, code)) { };
  
  #endregion
  
  #region COMMENTED OUT: FAILUREs w/data
  //
  //public static ResponseObject<T> FAILURE(T data)
  //  => new ResponseObject<T>(ResponseBase.DEFAULT_FAILURE) { Data = data };
  //
  //public static ResponseObject<T> FAILURE(T data, string message)
  //  => new ResponseObject<T>(ResponseBase.FAILURE(message)) { Data = data };
  //
  //public static ResponseObject<T> FAILURE(T data, string message, string code)
  //  => new ResponseObject<T>(ResponseBase.FAILURE(message, code)) { Data = data };
  //
  #endregion
  
  #endregion

  [JsonProperty(PropertyName = "data")]
  public T Data { get; set; }

  public ResponseObject() { }

  private ResponseObject(ResponseBase @base)
  : base(@base.Success, @base.Message, @base.ErrorCode) { }

  private ResponseObject(bool success, string message, T data, string errorCode = null)
  : base(success, message, errorCode)
  {
    Data = data;
  }  
}

public class ResponseBase
{
  public const string ERROR_SERVER_ERROR        = "A server error occurred";
  public const string ERROR_MISSING_PARAMETERS  = "One or more parameters are missing or invalid";
  public const string ERROR_UNAUTHORIZED        = "Unauthorized";

  [JsonIgnoreAttribute]
  public static readonly ResponseBase DEFAULT_SUCCESS = SUCCESS(string.Empty);
  
  [JsonIgnoreAttribute]
  public static readonly ResponseBase DEFAULT_FAILURE = FAILURE(string.Empty);

  [JsonIgnoreAttribute]
  public static readonly ResponseBase GENERIC_ERROR = FAILURE(ERROR_SERVER_ERROR);
  
  [JsonIgnoreAttribute]
  public static readonly ResponseBase SERVER_ERROR  = FAILURE(ERROR_SERVER_ERROR, "SERVER_ERROR");
  
  [JsonIgnoreAttribute]
  public static readonly ResponseBase UNAUTHORIZED  = FAILURE(ERROR_UNAUTHORIZED);
  
  [JsonIgnoreAttribute]
  public static readonly ResponseBase MISSING_OR_INVALID_PARAMETERS = FAILURE(ERROR_MISSING_PARAMETERS);

  public static ResponseBase SUCCESS(string message, string code = null) 
    => new ResponseBase(true, message, code ?? string.Empty);

  public static ResponseBase FAILURE(string message, string code = null) 
    => new ResponseBase(false, message, code ?? string.Empty);

  /// <summary>
  /// Boolean flag indicating if the call was ultimately successful or not.
  /// </summary>
  [JsonProperty(PropertyName = "success")]
  public bool Success { get; set; }

  /// <summary>
  /// This message string ins intended to be a user friendly string indicating the result of 
  /// the request. It is optional, and can be used for either success or failure messages.
  /// </summary>
  [JsonProperty(PropertyName = "message")]
  public string Message { get; set; }

  /// <summary>
  /// This is an optional error code that can be passed forward
  /// to a user to expedite investigation of user issues.
  /// </summary>
  [JsonProperty(PropertyName = "errorCode")]
  public string ErrorCode { get; set; }

  protected ResponseBase() { }

  protected ResponseBase(bool success, string message, string errorCode = null)
  {
    Success = success;
    Message = message;
    ErrorCode = errorCode;
  }
}

public class Review
{
  public ReviewUid Uid { get; set; }

  public List<ReviewTopic> Topics { get; set; }

  public OrganizationUserUid ManagerUid { get; set; }

  public OrganizationUserUid DirectReportUid { get; set; }

  public OrganizationUserUid ApproverUid { get; set; } // HR person, etc.

  public ReviewStatusEnum ManagerStatus { get; set; }

  public ReviewStatusEnum DirectReportStatus { get; set; }

  public string ManagerComment { get; set; }

  public string DirectReportComment { get; set; }

}

public class ReviewTopic
{
  public ReviewTopicUid Id { get; set; }

  public string TopicText { get; set; }

  public TopicTypeEnum Type { get; set; }

  public TopicConfig TopicConfig { get; set; }

  public List<ReviewAnswerWithReflection> Answers { get; set; }
  /**
   * Supports any config for any topic type from now until the future
   * this makes it so that we don't have to update the model for every type and configuration
   * */
  public Dictionary<string, object> TopicTypeConfig { get; set; }
  public TopicTargetEnum? TopicTarget { get; set; }
  public float? Rank { get; set; }
}

public class ReviewUid : Uid
{
  protected ReviewUid(string primitive) : base(primitive)
  {
  }

  public static explicit operator ReviewUid(string from)
  {
    return new ReviewUid(from);
  }
}

public class OrganizationUserUid : Uid
{
  protected OrganizationUserUid(string primitive) : base(primitive)
  {
  }

  public static explicit operator string(OrganizationUserUid from)
  {
    return from._primitive;
  }

  public static explicit operator OrganizationUserUid(string from)
  {
    return new OrganizationUserUid(from);
  }
}

public class ReviewTopicUid : Uid
{
  protected ReviewTopicUid(string primitive) : base(primitive)
  {
  }

  public static explicit operator string(ReviewTopicUid from)
  {
    return from._primitive;
  }

  public static explicit operator ReviewTopicUid(string from)
  {
    return new ReviewTopicUid(from);
  }
}

public class ReviewCycleUid : Uid
{
  //protected ReviewCycleUid(string primitive) : base(primitive)
  public ReviewCycleUid(string primitive) : base(primitive)
  {
  }

  public static explicit operator ReviewCycleUid(string from)
  {
    return new ReviewCycleUid(from);
  }
}

public class ReviewAnswerUid : Uid
{
  protected ReviewAnswerUid(string primitive) : base(primitive)
  {
  }

  public static explicit operator string(ReviewAnswerUid from)
  {
    return from._primitive;
  }

  public static explicit operator ReviewAnswerUid(string from)
  {
    return new ReviewAnswerUid(from);
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

public class TopicConfig
{
  public PointTypeEnum PointType { get; set; }

  public PointScaleEnum PointScale { get; set; }
}

public enum ReviewStatusEnum
{
    Unknown          = 0, //Not started
    ActiveInProgress = 1,
    Complete         = 2,
    Closed           = 3,
    Released         = 4
}

public enum TopicTypeEnum
{
  Unknown         = 0,
  Freeform        = 1,
  Linear          = 2,
  MultipleChoice  = 3
}

public enum PointTypeEnum
{
  Unknown         = 0,
  Unused          = 1,
  DisagreeOrAgree = 2,
  WorstOrBest     = 3
}

public enum PointScaleEnum
{
  Unknown         = 0,
  Unused          = 1,
  FivePointScale  = 2,
  TenPointScale   = 3
}

public enum TopicTargetEnum
{
  Both          = 0,
  Manager       = 1,
  DirectReport  = 2,
}

public enum ReflectionTypeEnum
{
  Unknown         = 0,
  Goal            = 1,
  Learning        = 2,
  Feedback        = 3,
  BookmarkedTopic = 4
}

public class ReviewAnswerWithReflection : ReviewAnswer
{
  public GenericReflection Reflection { get; set; }

  public string TopicTypeConfig { get; set; }
}

public class ReviewAnswer
{
  public ReviewAnswerUid Uid { get; set; }

  public ReviewTopicUid TopicId { get; set; }

  public string TopicText { get; set; }

  public TopicTypeEnum TopicType { get; set; }

  public TopicConfig TopicConfig { get; set; }

  public OrganizationUserUid Answerer { get; set; }

  public DateTime MostRecentAnswerTime { get; set; }

  // Response could be either of these: (helps keep dGraph simple)
  public string FreeFormAnswer { get; set; }

  public int LinearAnswer { get; set; }
}

public interface IReflection
{
  public string Id { get; set; }
  public string Text { get; set; }
  public ReflectionTypeEnum ReflectionType { get; }
  public string JsonMetadata { get; set; }
}

public class GenericReflection : IReflection
{
  public string Id { get; set; }
  public string Text { get; set; }
  public ReflectionTypeEnum ReflectionType { get; set; }
  public string JsonMetadata { get; set; }
}

public enum ReviewCycleChangeStatusEnum
{
  Unknown = 0,
  Draft = 1,
  Active = 2, // "published"
  Complete = 3,
  Closed = 4
}

public class ReviewCycleBase
{
  public ReviewCycleUid Id { get; set; }

  public string Name { get; set; }

  public string Description { get; set; }

  public DateTime AssesmentPeriodStart { get; set; }

  public DateTime AssesmentPeriodEnd { get; set; }

  public DateTime ReviewDueBy { get; set; }

  public ReviewCycleChangeStatusEnum Status { get; set; }
}

public class Thumbnail
{
  public OrganizationUserUid Id { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public string ImageUrl { get; set; }
}

public class ReviewCycle : ReviewCycleBase
{
  public List<ReviewTopic> Topics { get; set; }

  public List<Thumbnail> Participants { get; set; }

  public int TotalParticipants { get; set; }

  public List<Review> Reviews { get; set; }
}