/******************************************************************************/
/** SCHEMA CHANGE *****************************************************/
/******************************************************************************/

-- leadr."Template" ------------------------------------------------------------
create table leadr."Template"
(
    "ID"              integer generated always as identity
        constraint "PK_Template"
            primary key,
    "OrganizationUID" varchar(25),
    "Type"            smallint      not null,
    "Title"           varchar(4000) not null,
    "Objective"       varchar(4000) not null,
    "Configuration"   varchar(4000) not null,
    "IsDeleted"       bool          not null
);

alter table leadr."Template"
    owner to postgres;

create index "IX_Template_OrganizationUID"
    on leadr."Template" using hash ("OrganizationUID" varchar_ops);

create index "IX_Template_Type"
    on leadr."Template" using hash ("Type");

create index "IX_Template_IsDeleted"
    on leadr."Template" using hash ("IsDeleted");
--------------------------------------------------------------------------------

-- leadr."Question" ------------------------------------------------------------
create table leadr."Question"
(
    "ID"            integer generated always as identity
        constraint "PK_Question"
            primary key,
    "TemplateID"    int           not null
        constraint "FK_Question_Template"
            references leadr."Template",
    "Category"      smallint      not null,
    "Type"          smallint      not null,
    "Configuration" varchar(4000) not null,
    "Description"   varchar(4000) not null,
    "Question"      varchar(4000) not null,
    "IsDeleted"     bool          not null
);

alter table leadr."Question"
    owner to postgres;

create index "IX_Question_Category"
    on leadr."Question" using hash ("Category");

create index "IX_Question_Type"
    on leadr."Question" using hash ("Type");

create index "IX_Question_IsDeleted"
    on leadr."Question" using hash ("IsDeleted");
--------------------------------------------------------------------------------

/*******************************************************************************
-- leadr."Survey" --------------------------------------------------------------
create table leadr."Survey"
(
    "ID"              integer generated always as identity
        constraint "PK_Survey"
            primary key,
    "OrganizationUID" varchar(25)   not null,
    "Title"           varchar(250)  not null,
    "Objective"       varchar(2500) not null,
    "CompleteBy"      date,
    "CompletedOn"     timestamp,
    "CreatedByUID"    varchar(25)   not null,
    "CreatedOn"       timestamp     not null,
    "ModifiedByUID"   varchar(25),
    "ModifiedOn"      timestamp,
    "IsAnonymous"     boolean       not null,
    "IsDeleted"       boolean       not null,
    "Status"          smallint      not null
);

alter table leadr."Survey"
    owner to postgres;

create index "IX_Survey_IsDeleted"
    on leadr."Survey" using hash ("IsDeleted");

create index "IX_Survey_OrganizationUID"
    on leadr."Survey" using hash ("OrganizationUID" varchar_ops);

create index "IX_Survey_CreatedByUID"
    on leadr."Survey" using hash ("CreatedByUID" varchar_ops);
--------------------------------------------------------------------------------
*******************************************************************************/

-- leadr."SurveySeries" --------------------------------------------------------
create table leadr."SurveySeries"
(
    "ID"              integer generated always as identity
        constraint "PK_SurveySeries"
            primary key,
    "OrganizationUID" varchar(25)   not null,
    "CreatedByUID"    varchar(25)   not null,
    "TemplateID"      integer       not null
        constraint "FK_SurveySeries_Template"
            references leadr."Template",
    "Configuration"   varchar(4000) not null,
    "BeginDate"       date          not null,
    "EndDate"         date
);

alter table leadr."SurveySeries"
    owner to postgres;

create index "IX_SurveySeries_OrganizationUID"
    on leadr."SurveySeries" using hash ("OrganizationUID" varchar_ops);
--------------------------------------------------------------------------------

-- leadr."SurveySeriesSurvey" --------------------------------------------------
create table leadr."SurveySeriesSurvey"
(
    "ID"            integer generated always as identity
        constraint "PK_SurveySeriesSurvey"
            primary key,
    "SurveySeriesID"  integer       not null
        constraint "FK_SurveySeriesSurvey_SurveySeries"
          references leadr."SurveySeries",
    "SurveyID"        integer       not null
        constraint "FK_SurveySeriesSurvey_Survey"
            references leadr."Survey",
    "CreatedOn"       timestamp     not null,
     constraint "CK_SurveySeriesID_SurveyID"
         unique ("SurveySeriesID", "SurveyID")
);

alter table leadr."SurveySeriesSurvey"
    owner to postgres;
--------------------------------------------------------------------------------

-- leadr."QuestionSeries" ------------------------------------------------------
create table leadr."QuestionSeries"
(
    "ID"                integer generated always as identity
        constraint "PK_QuestionSeries"
            primary key,
    "SurveySeriesID"    integer       not null
        constraint "FK_QuestionSeries_SurveySeries"
            references leadr."SurveySeries",
    "QuestionCategory"  smallint      not null,
    "BeginDate"         timestamp     not null,
    "EndDate"           timestamp
);

alter table leadr."QuestionSeries"
    owner to postgres;

create index "IX_QuestionSeries_QuestionCategory"
    on leadr."QuestionSeries" using hash ("QuestionCategory");
--------------------------------------------------------------------------------

-- leadr."QuestionSeriesQuestion" ----------------------------------------------
create table leadr."QuestionSeriesQuestion"
(
    "ID"                integer generated always as identity
        constraint "PK_QuestionSeriesQuestion"
            primary key,
    "QuestionSeriesID"  integer       not null
        constraint "FK_QuestionSeriesQuestion_QuestionSeries"
            references leadr."QuestionSeries",
    "QuestionID"        integer       not null
        constraint "FK_QuestionSeriesQuestion_Question"
            references leadr."Question",
    "CreatedOn"         timestamp     not null,
    constraint "CK_QuestionSeriesID_QuestionID"
        unique ("QuestionSeriesID", "QuestionID")
);

alter table leadr."QuestionSeriesQuestion"
    owner to postgres;
--------------------------------------------------------------------------------

/******************************************************************************/
/** INSERT Question Data ******************************************************/
/******************************************************************************/

-- 'Template' => 'Leadr.Pulse' -------------------------------------------------
INSERT INTO leadr."Template"(
    "OrganizationUID",
    "Type",
    "Title",
    "Objective",
    "Configuration",
    "IsDeleted"
)
VALUES
( NULL
 ,5 -- Curated | Pulse
 ,'Leadr Pulse'
 ,'used to help gauge people''s engagement score.'
 ,'[{\"Category\":\"1\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"},{\"Category\":\"8\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"16\",\"Count\":\"1\",\"Frequency\":\"1\"}]'
 ,false );
--------------------------------------------------------------------------------

-- 'Clarity' -------------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I understand what is expected of me in my role.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am motivated by our organization''''s mission.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'Our team goals align with the primary goals of our organization.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'The work I do every day directly contributes to the success of our team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'The information I need to do my job is always made available to me.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I have access to the learning I need to grow in my role.'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------

-- 'eNPS' ----------------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,4
 ,2
 ,'{"minLabel":"","minValue":"0","maxLabel":"","maxValue":"10"}'
 ,'<p>Slide the bar to your desired number</p>'
 ,'How likely are you to recommend {OrganizationName} as a place to work?'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------

-- 'Maximization' --------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am regularly recognized and appreciated for the work I do.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'My individual strengths are being best utilized.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am strongly valued for the unique strengths I bring to the team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'There are clear opportunities for my growth and development here.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I have regular conversations with my leader about my development.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'In the last 6 months, I have met with my leader to discuss my progress.'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------

-- Rapport ---------------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am comfortable giving honest feedback to my team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I have confidence and trust in my leader.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am encouraged to come up with new and better ways of doing things.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I enjoy collaborating with my team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am comfortable asking for help when I need it.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am a valued member of my team.'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------



/******************************************************************************/
/******************************************************************************/
/******************************************************************************/
