USE Survey

--CREATE
ALTER PROCEDURE Employee_Add
  @Name       AS VARCHAR(205) = NULL -- DEPRICATE BY '2022-06-30'
 ,@FirstName  AS VARCHAR(100) = NULL
 ,@LastName   AS VARCHAR(150) = NULL
 ,@Email      AS VARCHAR(250)
 ,@DOB        AS DATE
AS

-- DEPRICATE BY '2022-06-30' - do this at the same time as removing the @Name parameter
IF @Name IS NOT NULL AND (@FirstName IS NOT NULL OR @LastName IS NOT NULL)
  THROW 60000, 'Invalid Arguments: if a value is supplied to ''Name'', where it IS NOT NULL, then both ''FirstName'' or ''LastName'' must be NULL.', 1

DECLARE @length AS INT
DECLARE @index  AS INT

IF @Name IS NOT NULL
  BEGIN
    SET @length = LEN(@Name)
    SET @index  = CHARINDEX(' ', @Name, 0)

    SET @FirstName = SUBSTRING(@Name, 0, @index)
    SET @LastName  = SUBSTRING(@Name, @index + 1, @length - @index)
  END

INSERT INTO Employee (
  [FirstName]
 ,[LastName]
 ,[Email]
 ,[DOB]
)
VALUES(
  @FirstName
 ,@LastName
 ,@Email
 ,@DOB
)
GO

--READ
ALTER PROCEDURE Employee_GetAll
AS

SELECT
  E.[ID]        AS "ID"
 ,E.[FirstName] 
    + ' ' + 
  E.[LastName]  AS "Name" -- DEPRICATE BY '2022-06-30'
 ,E.[FirstName] AS "FirstName"
 ,E.[LastName]  AS "LastName"
 ,E.[Email]     AS "Email"
 ,E.[DOB]       AS "DOB"
FROM
	Employee AS E
ORDER BY
	E.[ID]

GO

ALTER PROCEDURE Employee_GetByID
  @ID AS INT = -1
AS

SELECT
  E.[ID]        AS "ID"
 ,E.[FirstName] 
    + ' ' + 
  E.[LastName]  AS "Name" -- DEPRICATE BY '2022-06-30'
 ,E.[FirstName] AS "FirstName"
 ,E.[LastName]  AS "LastName"
 ,E.[Email]     AS "Email"
 ,E.[DOB]       AS "DOB"
FROM
	Employee AS E
WHERE
	E.[ID] = @ID

GO

--Uses TVF (Table Value Function) to allow more than one ID to be passed in
CREATE PROCEDURE Employee_GetByIDs
  @IDs AS VARCHAR(MAX) = ''
AS

SELECT
  E.[ID]        AS "ID"
 ,E.[FirstName] 
    + ' ' + 
  E.[LastName]  AS "Name" -- DEPRICATE BY '2022-06-30'
 ,E.[FirstName] AS "FirstName"
 ,E.[Email]     AS "Email"
 ,E.[DOB]       AS "DOB"
FROM
	Employee AS E
  INNER JOIN ToCSV(@IDs) AS IDs
    ON E.ID = IDs.Value
GO

--UPDATE
ALTER PROCEDURE Employee_Update
  @ID         AS INT         = -1
 ,@Name       AS VARCHAR(250) = NULL -- DEPRICATE BY '2022-06-30'
 ,@FirstName  AS VARCHAR(100) = NULL
 ,@LastName   AS VARCHAR(150) = NULL
 ,@Email      AS VARCHAR(250) = NULL
 ,@DOB        AS DATE
AS

-- DEPRICATE BY '2022-06-30' - do this at the same time as removing the @Name parameter
IF @Name IS NOT NULL AND (@FirstName IS NOT NULL OR @LastName IS NOT NULL)
  THROW 60000, 'Invalid Arguments: if a value is supplied to ''Name'', where it IS NOT NULL, then both ''FirstName'' or ''LastName'' must be NULL.', 1

DECLARE @length AS INT
DECLARE @index  AS INT

IF @Name IS NOT NULL
  BEGIN
    SET @length     = LEN(@Name)
    SET @index      = CHARINDEX(' ', @Name, 0)

    SET @FirstName = SUBSTRING(@Name, 0, @index)
    SET @LastName  = SUBSTRING(@Name, @index + 1, @length - @index)
  END

UPDATE Employee
  SET [FirstName] = @FirstName
     ,[LastName]  = @LastName
     ,[Email]     = @Email
     ,[DOB]       = @DOB
WHERE
  [ID] = @ID

GO

--DELETE
CREATE PROCEDURE Employe_Delete
  @ID AS INT = -1
AS

DELETE Employee
WHERE ID = @ID

GO