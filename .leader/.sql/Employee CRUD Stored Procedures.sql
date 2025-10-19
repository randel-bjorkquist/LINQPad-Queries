--CREATE
CREATE PROCEDURE Employee_Add
  @Name  AS VARCHAR(50)
 ,@Email AS VARCHAR(50)
 ,@DOB   AS DATE
AS

INSERT INTO Employee (
  [Name]
 ,[Email]
 ,[DOB]
)
VALUES(
  @Name
 ,@Email
 ,@DOB
)

GO

--READ
CREATE PROCEDURE Employee_GetAll
AS

SELECT
  E.[ID]    AS "ID"
 ,E.[Name]  AS "NAME"
 ,E.[Email] AS "EMAIL"
 ,E.[DOB]   AS "DOB"
FROM
	Employee AS E
ORDER BY
	E.[ID]

GO

CREATE PROCEDURE Employee_GetByID
  @ID AS INT
AS

SELECT
  E.[ID]    AS "ID"
 ,E.[Name]  AS "NAME"
 ,E.[Email] AS "EMAIL"
 ,E.[DOB]   AS "DOB"
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
  E.[ID]    AS "ID"
 ,E.[Name]  AS "NAME"
 ,E.[Email] AS "EMAIL"
 ,E.[DOB]   AS "DOB"
FROM
	Employee AS E
  INNER JOIN ToCSV(@IDs) AS IDs
    ON E.ID = IDs.Value
GO

--UPDATE
CREATE PROCEDURE Employee_Update
  @ID    AS INT
 ,@Name  AS VARCHAR(50)
 ,@Email AS VARCHAR(50)
 ,@DOB   AS DATE
AS

UPDATE Employee
  SET	[Name]  = @Name
     ,[Email] = @Email
     ,[DOB]   = @DOB
WHERE
  [ID] = @ID

GO

--DELETE
CREATE PROCEDURE Employe_Delete
  @ID    AS INT
AS

DELETE Employee
WHERE ID = @ID

GO