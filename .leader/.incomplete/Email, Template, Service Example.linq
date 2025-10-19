<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
  //Email<EmailTemplateBase, EmailService>();
  //Email<NewReviewerEmailTemplate, EmailService>();
  //Email<OldReviewerEmailTemplate, EmailService>();
  
//  Email<NewReviewerEmailTemplate, EmailService.EmailReviewees()>();
//  Email<OldReviewerEmailTemplate, EmailService.EmailReviewers()>();
}

public void Email<T, M>()
{
  DumpTypeName<T>();
  
  switch(typeof(T).Name)
  {
    case "EmailTemplateBase":
      break;
      
    case "NewReviewerEmailTemplate":
      break;
      
    case "OldReviewerEmailTemplate":
      break;
  }
}

void DumpTypeName<T>() => typeof(T).Name.Dump();

public class EmailTemplateBase {
  public string Name { get; } = "EmailTemplateBase";
}

public class NewReviewerEmailTemplate: EmailTemplateBase
{
  new public string Name { get; } = "NewReviewerEmailTemplate";
}

public class OldReviewerEmailTemplate : EmailTemplateBase
{
  new public string Name { get; } = "OldReviewerEmailTemplate";
}

public static class EmailService
{
  private static async Task SendEmail()
  {
    await Task.Run(() => { 
            throw new NotImplementedException(); 
          });
  }
  
  public static async Task EmailReviewees()
  {
    await SendEmail();
  }

  public static async Task EmailReviewers()
  {
    await SendEmail();
  }  
}