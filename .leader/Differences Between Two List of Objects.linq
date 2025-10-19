<Query Kind="Program" />

void Main()
{
  #region Data Setup
  
  var FredFlintstone  = new { ID = 0, SurveyId = 1, Name = "Fred Flintstone" };
  var WilmaFlintstone = new { ID = 1, SurveyId = 1, Name = "Wilma Flintstone" };
  
  var BarneyRubble    = new { ID = 2, SurveyId = 2, Name = "Barney Rubble" };
  var BettyRubble     = new { ID = 3, SurveyId = 2, Name = "Betty Rubble" };
  
  var participants = new[] { FredFlintstone
                            ,WilmaFlintstone
                            ,BarneyRubble
                            ,BettyRubble };

  var HowRU2Day      = new {ID = 1, SurveyId = 1, Question = "How are you feeling today?"};
  var FavoriteMuppet = new {ID = 2, SurveyId = 1, Question = "Whish is your favorite Muppet?"};

  var ShortAnswer    = new {ID = 3, SurveyId = 3, Question = "Just answer something" };
  var CustomScale    = new {ID = 4, SurveyId = 3, Question = "Chose a number between 1 and 100." };
  var MultipleChoice = new {ID = 5, SurveyId = 3, Question = "Select something." };

  var questions = new[] { HowRU2Day
                         ,FavoriteMuppet
                         ,ShortAnswer
                         ,CustomScale
                         ,MultipleChoice };
  
  #endregion
  
  #region LINQ Statement(s)
  
  questions.Select(q => q.SurveyId)
           .Distinct()  //NOTE: may actually NOT BE NEEDED ... not sure if it adds processing time
           .Except(participants.Select(p => p.SurveyId)
                               .Distinct()) //NOTE: may actually NOT BE NEEDED, same reason as above
           .Dump("questions.Except(participants)", 0);
           
  participants.Select(q => q.SurveyId)
              .Distinct() //NOTE: may actually NOT BE NEEDED ... not sure if it adds processing time
              .Except(questions.Select(p => p.SurveyId)
                               .Distinct()) //NOTE: may actually NOT BE NEEDED, same reason as above
              .Dump("participants.Except(questions)", 0);

  questions.Select(q => q.SurveyId)
           .Distinct()  //NOTE: may actually NOT BE NEEDED ... not sure if it adds processing time
           .Except(participants.Select(p => p.SurveyId)
                               .Distinct()) //NOTE: may actually NOT BE NEEDED, same reason as above
           .Union(participants.Select(q => q.SurveyId)
                              .Distinct()   //NOTE: may actually NOT BE NEEDED, same reason as above
                              .Except(questions.Select(p => p.SurveyId)
                                               .Distinct()))  //NOTE: may actually NOT BE NEEDED, same reason as above
           .Dump("questions.Except.participants UNION participants.Except.questions", 0);
           
  questions.Select(q => q.SurveyId)
           .Distinct()  //NOTE: may actually NOT BE NEEDED ... not sure if it adds processing time
           .Except(participants.Select(p => p.SurveyId)
                               .Distinct()) //NOTE: may actually NOT BE NEEDED, same reason as above
           .Concat(participants.Select(p => p.SurveyId)
                               .Distinct()  //NOTE: may actually NOT BE NEEDED, same reason as above
                               .Except(questions.Select(q => q.SurveyId)
                                                .Distinct())) //NOTE: may actually NOT BE NEEDED, same reason as above
           .Dump("questions.Except.participants CONCAT participants.Except.questions", 0);
           
           
  //HashSet<int> ----------------------
  var question_survey_ids    = questions.Select(q => q.SurveyId)
                                        .ToHashSet()
                                        .Dump("questions.ToHashSet()", 1);
  
  
  var participant_survey_ids = participants.Select(p => p.SurveyId)
                                           .ToHashSet()
                                           .Dump("participants.ToHashSet()", 1);
  
  question_survey_ids.Except(participant_survey_ids)
                     .Dump("questions.ToHashSet()", 1);
           
  #endregion
}

