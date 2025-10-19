/******************************************************************************/
/** SCHEMA CHANGE *************************************************************/
/******************************************************************************/

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
-- leadr."TemplateQuestion" ----------------------------------------------------
create table leadr."TemplateQuestion"
(
    "ID"            integer generated always as identity
        constraint "PK_TemplateQuestion"
            primary key,
    "TemplateID"    integer       not null
        constraint "FK_TemplateQuestion_Template"
            references leadr."Template",
    "QuestionID"    integer       not null
        constraint "FK_TemplateQuestion_Question"
            references leadr."Question",
    "Rank"          smallint      not null,
    "IsDeleted"     bool          not null,
    constraint "CK_TemplateID_QuestionID"
        unique ("TemplateID", "QuestionID"),
    constraint "CK_TemplateID_Rank"
        unique ("TemplateID", "Rank")
);

alter table leadr."TemplateQuestion"
owner to postgres;

create index "IX_TemplateQuestion_TemplateID"
    on leadr."TemplateQuestion" using hash ("TemplateID");

create index "IX_TemplateQuestion_QuestionID"
    on leadr."TemplateQuestion" using hash ("QuestionID");

create index "IX_TemplateQuestion_IsDeleted"
    on leadr."TemplateQuestion" using hash ("IsDeleted");
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
