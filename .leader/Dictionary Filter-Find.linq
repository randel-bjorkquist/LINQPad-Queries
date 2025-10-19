<Query Kind="Program" />

void Main()
{
  //Data.AllRelationships.Dump("AllRelationships", 0);
  
  //Data.AllRelationships
  //    .GroupBy(r => r.ManagerId)
  //    .ToDictionary(grp => grp.Key
  //                        , grp => grp.Select(g => g.OrgUserId))
  //    .Select(d => d.Value.Select(v => v.ToString()))
  //    .Dump("AllRelationships.GroupBy", 0);
      
  var all_relationships = Data.AllRelationships.GroupBy(r => r.ManagerId)
                                               .ToDictionary( grp => grp.Key
                                                             ,grp => grp.Select(g => g.OrgUserId));
  all_relationships.Dump("all_relationships Dictionary", 0);
      
  var new_relationships = Data.NewRelationships
                              .GroupBy(r => r.ManagerId)
                              .ToDictionary( grp => grp.Key
                                            ,grp => grp.Select(v => v.OrgUserId) );
  new_relationships.Dump("new_relationships Dictionary", 0);
  
  var relationships2email = all_relationships.Where(k => new_relationships.Select(n => n.Key).Contains(k.Key));
  relationships2email.Dump("relationships2email", 0);
  
  //var email_list = all_relationships.Where(k => new_relationships.Select(n => n.Key).Contains(k.Key));
  //email_list.Dump("email_list",0);
  
  //var all_reviewees = all_relationships.Select(a => a.Value);
  //
  //all_reviewees.Dump("all_reviewees", 0);
  //
  //var new_reviewees = new_relationships//.Where(n => n.Key == first.Key)
  //                                     .Select(n => n.Value)
  //                                     //.Contains(n => all_reviewees.)
  //                                     ;
  //
  //new_reviewees.Dump("new_reviewees", 0);
  //
  //all_reviewees.Where(a => new_reviewees.Select(n => n.ToString())
  //                                      .Contains(a.ToString()))
  //             .Dump("all_reviewees.Where(reviewee => new_reviewees.Contains(reviewee))", 0);
  
  #region  !!! DO NOT DELETE !!!
  
  //var email_list = all_relationships.Where(k => new_relationships.Select(n => n.Key).Contains(k.Key))
  //                                  .ToDictionary( r => r.Key
  //                                                ,r => Enumerable.Empty<OrganizationUserUid>()
  //                                                
  //                                                //,r => r.Value.Where(v => new_relationships.Where(m => m.Key == r.Key)
  //                                                //                                          .Select(o => o.Value)
  //                                                //                                          .Contains(r.Value.Select(va => va)))
  //                                                             
  //                                                //,r => new_relationships.Where(m => m.Key == r.Key)
  //                                                //                       .Select(o => o.Value)
  //                                                //                       //.Contains(r.Value.Select(v => v.ToString()))
  //                                                //                       //.Where(k => k.ToString() == r.Value.ToString())
  //                                                //
  //                                                
  //                                                //,r => new_relationships.Where(n => n.Key == r.Key)
  //                                                //                       .SelectMany(n => n.Value.Select(v => v))
  //                                                                       
  //                                                );
  //email_list.Dump("email_list", 0);
  
  //var email_list = new List<(OrganizationUserUid Reviewer, IEnumerable<OrganizationUserUid> Reviewees)>();
  var email_list = new Dictionary<OrganizationUserUid, List<OrganizationUserUid>>();
  
  //all_relationships = Enumerable.Empty<(OrganizationUserUid, List<OrganizationUserUid>)>();
  //all_relationships = Enumerable.Empty<OrganizationUserUid, IEnumerable<OrganizationUserUid>>().ToDictionary(d => d
  //                                                                                                          ,d => d);
  
  foreach(var relationship in all_relationships)
  {
    var orgUserIds = new_relationships.Where(n => n.Key == relationship.Key)
                                      .SelectMany(n => n.Value);

    var rrs2email = relationship.Value.Where(v => orgUserIds.Contains(v)).ToList();

    if(rrs2email.Any())
    {
      //email_list.Add((relationship.Key, newemail));
      email_list.Add(relationship.Key, rrs2email);
    }
  }

  email_list.Dump("email_list", 2);


  //foreach (var relationship2email in relationships2email)
  //{
  //  var filter = new_relationships.Where(n => n.Key == relationship2email.Key)
  //                                  .SelectMany(n => n.Value);
  //  var newemail = relationship2email.Value.Where(v => filter.Contains(v));
  //
  //  newemail.Dump("newemail", 1);
  //
  //  //relationship2email.Value = newemail;
  //}


  #endregion
}

//SelectList SelectList = new SelectList((IEnumerable)mylist, "ID", "Name", selectedValue);
//SelectList SelectList = new SelectList((IEnumerable)dict, "Key", "Value", selectedValue);

public class Relationship 
{
  public OrganizationUserUid OrgUserId { get; set; }
  public OrganizationUserUid ManagerId { get; set; }
}

public class OrganizationUserUid : Uid 
{
  public OrganizationUserUid(string primitive) : base(primitive)
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

public class Uid 
{
  #pragma warning disable CA1051 // Do not declare visible instance fields
  protected string _primitive;
  #pragma warning restore CA1051 // Do not declare visible instance fields

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

public static class Data 
{
  public static IEnumerable<Relationship> AllRelationships
  {
    get => new List<Relationship>() { 
                new Relationship { ManagerId = new OrganizationUserUid("0x674891")  ,OrgUserId = new OrganizationUserUid("0xf937149") }
               ,new Relationship { ManagerId = new OrganizationUserUid("0xf937149") ,OrgUserId = new OrganizationUserUid("0xf951ebe") }
               ,new Relationship { ManagerId = new OrganizationUserUid("0xf937149") ,OrgUserId = new OrganizationUserUid("0xf951ec8") }
               ,new Relationship { ManagerId = new OrganizationUserUid("0xf951ebe") ,OrgUserId = new OrganizationUserUid("0xf94839c") }
               ,new Relationship { ManagerId = new OrganizationUserUid("0xf951ebe") ,OrgUserId = new OrganizationUserUid("0xf951eaf") }
              };
  }

  public static IEnumerable<Relationship> NewRelationships
  {
    get => new List<Relationship>() {
                new Relationship { ManagerId = new OrganizationUserUid("0xf937149") ,OrgUserId = new OrganizationUserUid("0xf951ec8") }
               ,new Relationship { ManagerId = new OrganizationUserUid("0xf951ebe") ,OrgUserId = new OrganizationUserUid("0xf94839c") }
               ,new Relationship { ManagerId = new OrganizationUserUid("0xf951ebe") ,OrgUserId = new OrganizationUserUid("0xf951eaf") }
              };
  }
}