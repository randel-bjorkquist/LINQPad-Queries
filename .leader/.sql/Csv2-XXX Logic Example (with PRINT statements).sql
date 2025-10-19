---------------------------------------------------------------------------------------------------
USE Survey
GO

---------------------------------------------------------------------------------------------------
SET NOCOUNT ON
GO

---------------------------------------------------------------------------------------------------
PRINT '------------------------------------------------------------------------------'

---------------------------------------------------------------------------------------------------
--DECLARE @Array      AS VARCHAR(MAX) = '1 => 2 => 3 => 4 => 5 => 6 => 7 => 8 => 9 => 10'
--DECLARE @Separator  AS VARCHAR(9)   = '=>'
-------------------------------------------------
DECLARE @Array      AS VARCHAR(MAX) = 'Fred , Wilma , Barney , Betty , Mr. Slate'
DECLARE @Separator  AS VARCHAR(9)   = ','
---------------------------------------------------------------------------------------------------

DECLARE @Trim       AS BIT = 0
DECLARE @Table      AS TABLE (
--    [Value] INT NOT NULL
--    [Value] BIGINT NOT NULL
    [Value] VARCHAR(MAX) NOT NULL
)

---------------------------------------------------------------------------------------------------
PRINT '@Array            : ''' + COALESCE(CAST(@Array     AS VARCHAR(MAX)), 'NULL') + ''''
PRINT '@Separator        : ''' + COALESCE(CAST(@Separator AS VARCHAR(MAX)), 'NULL') + ''''
PRINT '@Trim             : ''' + COALESCE(CAST(@Trim      AS VARCHAR(MAX)), 'NULL') + ''''

---------------------------------------------------------------------------------------------------
DECLARE @SeparatorPosition AS INT;
DECLARE @SeparatorPrefix   AS VARCHAR(MAX) = '%' + @Separator + '%';
DECLARE @ArrayValue        AS VARCHAR(MAX);

PRINT '@SeparatorPosition: ''' + COALESCE(CAST(@SeparatorPosition AS VARCHAR(MAX)), 'NULL') + ''''
PRINT '@SeparatorPrefix  : ''' + COALESCE(CAST(@SeparatorPrefix   AS VARCHAR(MAX)), 'NULL') + ''''
PRINT '@ArrayValue       : ''' + COALESCE(CAST(@ArrayValue        AS VARCHAR(MAX)), 'NULL') + ''''
  
  SET @Array = @Array + @Separator;

PRINT '@Array            : ''' + COALESCE(CAST(@Array AS VARCHAR(MAX)), 'NULL') + ''' <= Appended @Separator !!!'
PRINT '------------------------------------------------------------------------------'

WHILE PATINDEX(@SeparatorPrefix, @Array) <> 0
  BEGIN
    PRINT ''
    
    SELECT @SeparatorPosition = PATINDEX(@SeparatorPrefix, @Array);
    PRINT '@SeparatorPosition: ''' + COALESCE(CAST(@SeparatorPosition AS VARCHAR(MAX)), 'NULL') + ''''
    
    SELECT @ArrayValue        = LEFT(@Array, @SeparatorPosition - 1);
    PRINT '@ArrayValue       : ''' + COALESCE(CAST(@ArrayValue AS VARCHAR(MAX)), 'NULL') + ''''
    
    --NOTE: The DEFAULT value is to TRIM spaces around the @Separator; only 
    --      supplying the value 0 (zero) will NOT TRIM.
    IF(@Trim <> 0)
      BEGIN
        SET @ArrayValue = LTRIM(RTRIM(@ArrayValue))
        PRINT '@ArrayValue       : ''' + COALESCE(CAST(@ArrayValue AS VARCHAR(MAX)), 'NULL') + ''' <= TRIMMED !!!'
      END
    
    INSERT @Table
    VALUES(
      --CAST(@ArrayValue AS INT)
      --CAST(@ArrayValue AS BIGINT)
      CAST(@ArrayValue AS VARCHAR(MAX))
    );

    DECLARE @COUNT AS INT
    SELECT @COUNT = COUNT(*) FROM @Table;
    
    PRINT '@Table Count      : ''' + COALESCE(CAST(@COUNT AS VARCHAR(MAX)), 'NULL') + ''''
    
    --SELECT @Array = STUFF(@Array, 1, @SeparatorPosition + LEN(@Separator) - 1, '');
    SELECT @Array = STUFF(@Array, 1, @SeparatorPosition + LEN(@Separator) - 1, NULL);
    
    PRINT '@Array            : ''' + COALESCE(CAST(@Array AS VARCHAR(MAX)), 'NULL') + ''''
  END;

---------------------------------------------------------------------------------------------------
--SELECT * FROM @BIGINT_Table AS tbl
--ORDER BY tbl.[Value] DESC
