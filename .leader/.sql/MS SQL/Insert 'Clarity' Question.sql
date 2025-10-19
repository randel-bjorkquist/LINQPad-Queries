----------------------------------------------------------------------------------------------------
DECLARE @Category   AS SMALLINT = 1; -- Clarity
DECLARE @Type       AS SMALLINT = 2;
DECLARE @Deleted    AS BIT      = 0;

DECLARE @Configuration AS VARCHAR(MAX) = '[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]';
DECLARE @Description   AS VARCHAR(MAX) = '<p>Select one of the options</p>';

DECLARE @Question1  AS VARCHAR(MAX) = 'I understand what is expected of me in my role.';
DECLARE @Question2  AS VARCHAR(MAX) = 'I am motivated by our organization''''s mission.';
DECLARE @Question3  AS VARCHAR(MAX) = 'Our team goals align with the primary goals of our organization.';
DECLARE @Question4  AS VARCHAR(MAX) = 'The work I do every day directly contributes to the success of our team.';
DECLARE @Question5  AS VARCHAR(MAX) = 'The information I need to do my job is always made available to me.';
DECLARE @Question6  AS VARCHAR(MAX) = 'I have access to the learning I need to grow in my role.';
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


