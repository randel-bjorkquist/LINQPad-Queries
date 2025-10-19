----------------------------------------------------------------------------------------------------
DECLARE @Category   AS SMALLINT = 16; -- Rapport
DECLARE @Type       AS SMALLINT = 2;
DECLARE @Deleted    AS BIT      = 0;

DECLARE @Configuration AS VARCHAR(MAX) = '[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]';
DECLARE @Description   AS VARCHAR(MAX) = '<p>Select one of the options</p>';

DECLARE @Question1  AS VARCHAR(MAX) = 'I am comfortable giving honest feedback to my team.';
DECLARE @Question2  AS VARCHAR(MAX) = 'I have confidence and trust in my leader.';
DECLARE @Question3  AS VARCHAR(MAX) = 'I am encouraged to come up with new and better ways of doing things.';
DECLARE @Question4  AS VARCHAR(MAX) = 'I enjoy collaborating with my team.';
DECLARE @Question5  AS VARCHAR(MAX) = 'I am comfortable asking for help when I need it.';
DECLARE @Question6  AS VARCHAR(MAX) = 'I am a valued member of my team.';
----------------------------------------------------------------------------------------------------
INSERT INTO dbo.Question(
    [Category],
    [Type],
    [Configuration],
    [Description],
    Question,
    IsDeleted
)
VALUES
( @Category, @Type, @Configuration, @Description, @Question1, @Deleted ),
( @Category, @Type, @Configuration, @Description, @Question2, @Deleted ),
( @Category, @Type, @Configuration, @Description, @Question3, @Deleted ),
( @Category, @Type, @Configuration, @Description, @Question4, @Deleted ),
( @Category, @Type, @Configuration, @Description, @Question5, @Deleted ),
( @Category, @Type, @Configuration, @Description, @Question6, @Deleted );


