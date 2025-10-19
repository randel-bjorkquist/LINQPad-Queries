------------------------------------------------------------------------------------------------------------------------
BEGIN TRANSACTION;

------------------------------------------------------------------------------------------------------------------------
WITH
CTE_LEADR_PULSE_TEMPLATE AS (
  SELECT
    T."OrganizationUID"
   ,T."CreatedByUID"
  FROM leadr."Template" AS T
  WHERE T."Title" = 'Leadr Pulse'
  LIMIT 1
),
CTE_TEMPLATE AS (
  INSERT INTO leadr."Template"(
    "OrganizationUID"
   ,"Type"
   ,"Title"
   ,"Objective"
   ,"Configuration"
   ,"CreatedByUID"
   ,"CreatedOn"
  )
  SELECT
    CTE_LEADR_PULE_TEMPLATE."OrganizationUID"
   ,1 --TemplateTypeEnum.Curated
   ,'(RB-TEST) Post-project Retrospective'
   ,''
   ,''
   ,CTE_LEADR_PULE_TEMPLATE."CreatedByUID"
   ,CURRENT_TIMESTAMP
  FROM CTE_LEADR_PULE_TEMPLATE
  RETURNING (SELECT last_value FROM "Template_ID_seq") AS "TemplateID"
)
INSERT INTO leadr."Question" (
  "TemplateID"
 ,"Category"
 ,"Type"
 ,"Configuration"
 ,"Description"
 ,"Question"
 ,"CreatedByUID"
 ,"CreatedOn"
)
SELECT
  CTE_TEMPLATE."TemplateID"
 ,2   -- QuestionCategoryEnum.Custom
 ,4   -- QuestionTypeEnum.MultipleChoice
 ,'[{"option":"Yes","rank":1},{"option":"No","rank":2}]'
 ,''
 ,'(RB-TEST) Were you clear on how you were expected to contribute to this project?'
 ,CTE_LEADR_PULE_TEMPLATE."CreatedByUID"
 ,CURRENT_TIMESTAMP
FROM CTE_TEMPLATE
    ,CTE_LEADR_PULE_TEMPLATE
UNION
SELECT
  CTE_TEMPLATE."TemplateID"
 ,2   -- QuestionCategoryEnum.Custom
 ,1   -- QuestionTypeEnum.CustomScale
 ,'{"minLabel":"No one seemed invested","minValue":"1","maxLabel":"This was a team effort","maxValue":"10"}'
 ,''
 ,'(RB-TEST) Did everyone contribute their part effectively towards this project?'
 ,CTE_LEADR_PULE_TEMPLATE."CreatedByUID"
 ,CURRENT_TIMESTAMP
FROM CTE_TEMPLATE
    ,CTE_LEADR_PULE_TEMPLATE
UNION
SELECT
  CTE_TEMPLATE."TemplateID"
 ,2   -- QuestionCategoryEnum.Custom
 ,1   -- QuestionTypeEnum.CustomScale
 ,'{"minLabel":"This project was a fail","minValue":"1","maxLabel":"We achieved every goal","maxValue":"10"}'
 ,''
 ,'(RB-TEST) Did we achieve our goals with this project?'
 ,CTE_LEADR_PULE_TEMPLATE."CreatedByUID"
 ,CURRENT_TIMESTAMP
FROM CTE_TEMPLATE
    ,CTE_LEADR_PULE_TEMPLATE
UNION
SELECT
  CTE_TEMPLATE."TemplateID"
 ,2   -- QuestionCategoryEnum.Custom
 ,2   -- QuestionTypeEnum.ShortAnswer
 ,''  -- no need for an empty JSON object
 ,''
 ,'(RB-TEST) Are there any team members you’d specifically like to shout out?'
 ,CTE_LEADR_PULE_TEMPLATE."CreatedByUID"
 ,CURRENT_TIMESTAMP
FROM CTE_TEMPLATE
    ,CTE_LEADR_PULE_TEMPLATE
UNION
SELECT
  CTE_TEMPLATE."TemplateID"
 ,2   -- QuestionCategoryEnum.Custom
 ,2   -- QuestionTypeEnum.ShortAnswer
 ,''  -- no need for an empty JSON object
 ,''
 ,'(RB-TEST) What will you do differently next time?'
 ,CTE_LEADR_PULE_TEMPLATE."CreatedByUID"
 ,CURRENT_TIMESTAMP
FROM CTE_TEMPLATE
    ,CTE_LEADR_PULE_TEMPLATE
UNION
SELECT
  CTE_TEMPLATE."TemplateID"
 ,2   -- QuestionCategoryEnum.Custom
 ,2   -- QuestionTypeEnum.ShortAnswer
 ,''  -- no need for an empty JSON object
 ,''
 ,'(RB-TEST) Any other thoughts you’d like to share?'
 ,CTE_LEADR_PULE_TEMPLATE."CreatedByUID"
 ,CURRENT_TIMESTAMP
FROM CTE_TEMPLATE
    ,CTE_LEADR_PULE_TEMPLATE;

------------------------------------------------------------------------------------------------------------------------
SELECT * FROM leadr."Template" AS T WHERE T."Title"    LIKE '(RB-TEST)%';
SELECT * FROM leadr."Question" AS Q WHERE Q."Question" LIKE '(RB-TEST)%';

------------------------------------------------------------------------------------------------------------------------
ROLLBACK TRANSACTION;
--COMMIT TRANSACTION;
