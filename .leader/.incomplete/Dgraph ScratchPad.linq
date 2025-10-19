<Query Kind="Expression" />




// Google Search: dgraph GraphQL LINQPad
//  https://www.bing.com/search?q=dgraph+GraphQL+LINQPad+&form=ANSPH1&refig=f195a61dcde046d9a6f36c7baff70908&pc=U531

// Google Search: dgraph DQL LINQPad
//  https://www.bing.com/search?q=dgraph+dql+linqpad&cvid=588d8e3ffcb74af6809ec2fc5b9d3dcb&aqs=edge.0.0j69i64.1383j0j1&pglt=2083&FORM=ANNTA1&PC=DCTS

// Working with GraphQL Data in LINQPad
//  https://www.cdata.com/kb/tech/graphql-ado-linqpad.rst



private async Task<List<string>> BuildUserIdListForMeeting(List<string> attendeeEmails, string orgId)
{
    var emailList = BuildListOfEmailsForDgraphQuery(attendeeEmails);
    var varmap    = new Dictionary<string, string>();
    
    var queryText = @"{
        users(func: type(LOGIN_ACCOUNT)) @filter(eq(email, $emailAddresses) AND eq(loginAccountType, [""auth0"", ""temp""])) @normalize @cascade {
            connectedTo {
                userId: uid
                has @filter(type(ORGANIZATION_USER)) {
                    belongsTo @filter(type(ORGANIZATION) AND uid($orgId)) {
                        orgId: uid
                    }
                }
            }
        }
    }".Replace("$emailAddresses", emailList)
      .Replace("$orgId", orgId);
      
    OrgAndUserMap queryResult = await _dgraph.Query<OrgAndUserMap>(queryText, varmap);

    return queryResult?.Users
                      ?.Select(u => u?.UserId)
                      ?.ToHashSet()
                      ?.ToList() ?? new List<string>();
}
        