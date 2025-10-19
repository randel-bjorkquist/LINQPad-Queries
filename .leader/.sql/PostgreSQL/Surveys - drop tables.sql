/***********************************************************************************************************************
** VERSION: 1 -> non_snake_case_entity_names
***********************************************************************************************************************/
begin transaction;

drop table leadr."QuestionSeriesQuestion";
drop table leadr."QuestionSeries";

drop table leadr."SurveySeriesSurvey";
drop table leadr."SurveySeries";

-- drop table leadr."Answer";
-- drop table leadr."SurveyParticipant";
-- drop table leadr."SurveyQuestion";

alter table leadr."SurveyQuestion"
  drop constraint if exists "FK_SurveyQuestion_Question";

drop table leadr."Question";
drop table leadr."Template";
-- drop table leadr."Survey";

rollback transaction;
--commit transaction;

/***********************************************************************************************************************
** VERSION: 2 -> with_snake_case_entity_names
***********************************************************************************************************************/
begin transaction;

drop table leadr."QuestionSeries_Question";
drop table leadr."QuestionSeries";

--alter table leadr."Survey"
--     drop constraint if exists "FK_Survey_SurveySeries";

drop table leadr."SurveySeries_Survey";
drop table leadr."SurveySeries";

--drop table leadr."Answer";
--drop table leadr."SurveyParticipant";
--drop table leadr."SurveyQuestion";

alter table leadr."SurveyQuestion"
    drop constraint if exists "FK_SurveyQuestion_Question";

drop table leadr."Question";
drop table leadr."Template";
--drop table leadr."Survey";

rollback transaction;
--commit transaction;

/**********************************************************************************************************************/
