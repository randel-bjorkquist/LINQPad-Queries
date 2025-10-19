----------------------------------------------------------------------------------------------------
DECLARE @Category   AS SMALLINT = 4;  -- 'eNPS'
DECLARE @Type       AS SMALLINT = 2;
DECLARE @Deleted    AS BIT      = 0;

DECLARE @Configuration AS VARCHAR(MAX) = '{"minLabel":"0","minValue":"0","maxLabel":"10","maxValue":"10"}';
DECLARE @Description   AS VARCHAR(MAX) = '<p>Slide the bar to your desired number</p>';

DECLARE @Question1  AS VARCHAR(MAX) = 'How likely are you to recommend [Organization] as a place to work?';
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
( @Category, @Type, @Configuration, @Description, @Question1, @Deleted );
