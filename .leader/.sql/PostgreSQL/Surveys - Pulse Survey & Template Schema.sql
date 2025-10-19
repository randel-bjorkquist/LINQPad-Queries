USE [Survey];
GO

/****** Object:  Table [dbo].[Question]    Script Date: 4/19/2022 10:39:56 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[Question]
(
    [ID]            [INT]           IDENTITY(1, 1)  NOT NULL
   ,[Category]      [SMALLINT]                      NOT NULL
   ,[Type]          [SMALLINT]                      NOT NULL
   ,[Configuration] [NVARCHAR](MAX)                 NOT NULL
   ,[Description]   [NVARCHAR](MAX)                 NOT NULL
   ,[Question]      [NVARCHAR](MAX)                 NOT NULL
   ,[IsDeleted]     [BIT]                           NOT NULL

   ,CONSTRAINT [PK_Question]
        PRIMARY KEY CLUSTERED ([ID] ASC)
        WITH ( PAD_INDEX                   = OFF
              ,STATISTICS_NORECOMPUTE      = OFF
              ,IGNORE_DUP_KEY              = OFF
              ,ALLOW_ROW_LOCKS             = ON
              ,ALLOW_PAGE_LOCKS            = ON
              ,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

/****** Object:  Table [dbo].[QuestionSeries]    Script Date: 4/19/2022 10:39:56 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[QuestionSeries]
(
    [ID]                [INT]       IDENTITY(1, 1)  NOT NULL
   ,[SurveySeriesID]    [INT]                       NOT NULL
   ,[QuestionCategory]  [SMALLINT]                  NOT NULL
   ,[Status]            [SMALLINT]                  NOT NULL

   ,CONSTRAINT [PK_QuestionSeries]
        PRIMARY KEY CLUSTERED ([ID] ASC)
        WITH ( PAD_INDEX                   = OFF
              ,STATISTICS_NORECOMPUTE      = OFF
              ,IGNORE_DUP_KEY              = OFF
              ,ALLOW_ROW_LOCKS             = ON
              ,ALLOW_PAGE_LOCKS            = ON
              ,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON [PRIMARY]
) ON [PRIMARY];
GO

/****** Object:  Table [dbo].[QuestionSeriesQuestion]    Script Date: 4/19/2022 10:39:56 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[QuestionSeriesQuestion]
(
    [ID]                [INT] IDENTITY(1, 1)  NOT NULL
   ,[QuestionSeriesID]  [INT]                 NOT NULL
   ,[QuestionID]        [INT]                 NOT NULL
   ,[CreatedOn]         [DATETIME]            NOT NULL

   ,CONSTRAINT [PK_QuestionSeriesQuestion]
        PRIMARY KEY CLUSTERED ([ID] ASC)
        WITH ( PAD_INDEX = OFF
              ,STATISTICS_NORECOMPUTE      = OFF
              ,IGNORE_DUP_KEY              = OFF
              ,ALLOW_ROW_LOCKS             = ON
              ,ALLOW_PAGE_LOCKS            = ON
              ,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON [PRIMARY]
) ON [PRIMARY];
GO

/****** Object:  Table [dbo].[SurveySeries]    Script Date: 4/19/2022 10:39:56 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[SurveySeries]
(
    [ID]              [INT]         IDENTITY(1, 1)  NOT NULL
   ,[OrganizationUID] [VARCHAR](25)                 NOT NULL
   ,[TemplateID]      [INT]                         NOT NULL
   ,[BeginDate]       [DATE]                        NOT NULL
   ,[EndDate]         [DATE]                            NULL
   ,CONSTRAINT [PK_SurveySeries]
        PRIMARY KEY CLUSTERED ([ID] ASC)
        WITH ( PAD_INDEX                   = OFF
              ,STATISTICS_NORECOMPUTE      = OFF
              ,IGNORE_DUP_KEY              = OFF
              ,ALLOW_ROW_LOCKS             = ON
              ,ALLOW_PAGE_LOCKS            = ON
              ,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON [PRIMARY]
) ON [PRIMARY];
GO

/****** Object:  Table [dbo].[SurveySeriesSurvey]    Script Date: 4/19/2022 10:39:56 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[SurveySeriesSurvey]
(
    [ID]              [INT] IDENTITY(1, 1)  NOT NULL
   ,[SurveySeriesID]  [INT]                 NOT NULL
   ,[SurveyID]        [INT]                 NOT NULL
   ,[CreatedOn]       [DATETIME]            NOT NULL

   ,CONSTRAINT [PK_SurveySeriesSurvey]
        PRIMARY KEY CLUSTERED ([ID] ASC)
        WITH ( PAD_INDEX                   = OFF
              ,STATISTICS_NORECOMPUTE      = OFF
              ,IGNORE_DUP_KEY              = OFF
              ,ALLOW_ROW_LOCKS             = ON
              ,ALLOW_PAGE_LOCKS            = ON
              ,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON [PRIMARY]
) ON [PRIMARY];
GO

/****** Object:  Table [dbo].[Template]    Script Date: 4/19/2022 10:39:56 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[Template]
(
    [ID]              [INT]         IDENTITY(1, 1)  NOT NULL
   ,[OrganizationUID] [VARCHAR](10)                     NULL
   ,[Type]            [SMALLINT]                    NOT NULL
   ,[Title]           [VARCHAR](50)                 NOT NULL
   ,[Objective]       [VARCHAR](500)                NOT NULL
   ,[Configuration]   [VARCHAR](MAX)                NOT NULL

   ,CONSTRAINT [PK_Template]
        PRIMARY KEY CLUSTERED ([ID] ASC)
        WITH ( PAD_INDEX                   = OFF
              ,STATISTICS_NORECOMPUTE      = OFF
              ,IGNORE_DUP_KEY              = OFF
              ,ALLOW_ROW_LOCKS             = ON
              ,ALLOW_PAGE_LOCKS            = ON
              ,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

/****** Object:  Table [dbo].[TemplateQuestion]    Script Date: 4/19/2022 10:39:56 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

CREATE TABLE [dbo].[TemplateQuestion]
(
    [ID]              [INT]       IDENTITY(1, 1)  NOT NULL
   ,[TemplateID]      [INT]                       NOT NULL
   ,[QuestionID]      [INT]                       NOT NULL
   ,[Rank]            [SMALLINT]                  NOT NULL

   ,CONSTRAINT [PK_TemplateQuestion]
        PRIMARY KEY CLUSTERED ([ID] ASC)
        WITH ( PAD_INDEX                   = OFF
              ,STATISTICS_NORECOMPUTE      = OFF
              ,IGNORE_DUP_KEY              = OFF
              ,ALLOW_ROW_LOCKS             = ON
              ,ALLOW_PAGE_LOCKS            = ON
              ,OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF ) ON [PRIMARY]
) ON [PRIMARY];
GO

ALTER TABLE [dbo].[QuestionSeries] WITH CHECK
ADD CONSTRAINT [FK_QuestionSeries_QuestionCategory]
    FOREIGN KEY ([QuestionCategory])
    REFERENCES [dbo].[QuestionCategory] ([ID]);
GO

ALTER TABLE [dbo].[QuestionSeries] CHECK CONSTRAINT [FK_QuestionSeries_QuestionCategory];
GO

ALTER TABLE [dbo].[QuestionSeries] WITH CHECK
ADD CONSTRAINT [FK_QuestionSeries_SurveySeries]
    FOREIGN KEY ([SurveySeriesID])
    REFERENCES [dbo].[SurveySeries] ([ID]);
GO

ALTER TABLE [dbo].[QuestionSeries] CHECK CONSTRAINT [FK_QuestionSeries_SurveySeries];
GO

ALTER TABLE [dbo].[QuestionSeriesQuestion] WITH CHECK
ADD CONSTRAINT [FK_QuestionSeriesQuestion_Question]
    FOREIGN KEY ([QuestionID])
    REFERENCES [dbo].[Question] ([ID]);
GO

ALTER TABLE [dbo].[QuestionSeriesQuestion] CHECK CONSTRAINT [FK_QuestionSeriesQuestion_Question];
GO

ALTER TABLE [dbo].[QuestionSeriesQuestion] WITH CHECK
ADD CONSTRAINT [FK_QuestionSeriesQuestion_QuestionSeries]
    FOREIGN KEY ([QuestionSeriesID])
    REFERENCES [dbo].[QuestionSeries] ([ID]);
GO

ALTER TABLE [dbo].[QuestionSeriesQuestion] CHECK CONSTRAINT [FK_QuestionSeriesQuestion_QuestionSeries];
GO

ALTER TABLE [dbo].[SurveySeries] WITH CHECK
ADD CONSTRAINT [FK_SurveySeries_Template]
    FOREIGN KEY ([TemplateID])
    REFERENCES [dbo].[Template] ([ID]);
GO

ALTER TABLE [dbo].[SurveySeries] CHECK CONSTRAINT [FK_SurveySeries_Template];
GO

ALTER TABLE [dbo].[SurveySeriesSurvey] WITH CHECK
ADD CONSTRAINT [FK_SurveySeriesSurvey_Survey]
    FOREIGN KEY ([SurveyID])
    REFERENCES [dbo].[Survey] ([ID]);
GO

ALTER TABLE [dbo].[SurveySeriesSurvey] CHECK CONSTRAINT [FK_SurveySeriesSurvey_Survey];
GO

ALTER TABLE [dbo].[SurveySeriesSurvey] WITH CHECK
ADD CONSTRAINT [FK_SurveySeriesSurvey_SurveySeries]
    FOREIGN KEY ([SurveySeriesID])
    REFERENCES [dbo].[SurveySeries] ([ID]);
GO

ALTER TABLE [dbo].[SurveySeriesSurvey] CHECK CONSTRAINT [FK_SurveySeriesSurvey_SurveySeries];
GO

ALTER TABLE [dbo].[TemplateQuestion] WITH CHECK
ADD CONSTRAINT [FK_TemplateQuestion_Question]
    FOREIGN KEY ([QuestionID])
    REFERENCES [dbo].[Question] ([ID]);
GO

ALTER TABLE [dbo].[TemplateQuestion] CHECK CONSTRAINT [FK_TemplateQuestion_Question];
GO

ALTER TABLE [dbo].[TemplateQuestion] WITH CHECK
ADD CONSTRAINT [FK_TemplateQuestion_Template]
    FOREIGN KEY ([TemplateID])
    REFERENCES [dbo].[Template] ([ID]);
GO

ALTER TABLE [dbo].[TemplateQuestion] CHECK CONSTRAINT [FK_TemplateQuestion_Template];
GO


