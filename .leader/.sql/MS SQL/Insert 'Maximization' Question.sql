----------------------------------------------------------------------------------------------------
DECLARE @Category   AS SMALLINT = 8;  -- 'Maximization'
DECLARE @Type       AS SMALLINT = 2;
DECLARE @Deleted    AS BIT      = 0;

DECLARE @Configuration AS VARCHAR(MAX) = '[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]';
DECLARE @Description   AS VARCHAR(MAX) = '<p>Select one of the options</p>';

DECLARE @Question1  AS VARCHAR(MAX) = 'I am regularly recognized and appreciated for the work I do.';
DECLARE @Question2  AS VARCHAR(MAX) = 'My individual strengths are being best utilized.';
DECLARE @Question3  AS VARCHAR(MAX) = 'I am strongly valued for the unique strengths I bring to the team.';
DECLARE @Question4  AS VARCHAR(MAX) = 'There are clear opportunities for my growth and development here.';
DECLARE @Question5  AS VARCHAR(MAX) = 'I have regular conversations with my leader about my development.';
DECLARE @Question6  AS VARCHAR(MAX) = 'In the last 6 months, I have met with my leader to discuss my progress.';
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
