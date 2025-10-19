----------------------------------------------------------------------------------------------------
--Seed the initial values for the Employee table--
----------------------------------------------------------------------------------------------------
--SELECT * FROM Employee AS E
--EXECUTE Employee_GetAll

--DECLARE @NOW AS DATETIME = GETDATE()

--ORIGINAL ADD STORED PROCEDURE ------------------------------------------------
--EXEC Employee_Add @Name  = 'Leadr Coach'
--                 ,@Email = 'Leadr.Coach@leadr.com'
--                 ,@DOB   = @NOW
--
--EXEC Employee_Add @Name  = 'Fred Flintstone'
--                 ,@Email = 'Fred.Flintstone@bedrock.bc'
--                 ,@DOB   = @NOW
--
--EXEC Employee_Add @Name  = 'Barney Rubble'
--                 ,@Email = 'Barney.Rubble@bedrock.bc'
--                 ,@DOB   = @NOW

--OLD ADD STORED PROCEDURE -----------------------------------------------------
--EXEC Employee_Add @Name  = 'Wilma Flintstone'
--                 ,@Email = 'Wilma.Flintstone@bedrock.bc'
--                 ,@DOB   = @NOW
--
--EXEC Employee_Add @Name  = 'Betty Rubble'
--                 ,@Email = 'Betty.Rubble@bedrock.bc'
--                 ,@DOB   = @NOW
--
--NEW ADD STORED PROCEDURE -----------------------------------------------------
--EXEC Employee_Add @FirstName  = 'Pebbles'
--                 ,@LastName   = 'Flintstone'
--                 ,@Email      = 'Pebbles.Flintstone@bedrock.bc'
--                 ,@DOB        = @NOW
--
--EXEC Employee_Add @FirstName  = 'Bambam'
--                 ,@LastName   = 'Rubble'
--                 ,@Email      = 'Bambam.Rubble@bedrock.bc'
--                 ,@DOB        = @NOW

--EXECUTE Employee_GetAll

----------------------------------------------------------------------------------------------------
--Steps to refactor
-- 1. Add the new columns: 'FirstName' and 'LastName' - set them each to allow NULLs
-- 2. Update the new columns, using the value found within the old column 'Name'
-- 3. Update table definition: 'Name' - allow NULLs, 'FirstName' - DO NOT allow NULLs, and 'LastName' - DO NOT allow NULLs 
-- 4. Update the old column, set 'Name' to NULL
-- 5. Update any associated stored procedure(s)
-- 6. Delete the old column 'Name'

--UPDATE TEST SCRIPT
--SELECT
--  [Name]
-- ,FirstName = SUBSTRING([Name], 0, CHARINDEX(' ', [Name], 0))
-- ,LastName  = SUBSTRING([Name], CHARINDEX(' ', [Name], 0) + 1, LEN([Name]))
--FROM Employee

--UPDATE SCRIPT (fills new columns)
--UPDATE Employee
--  SET FirstName = SUBSTRING([Name], 0, CHARINDEX(' ', [Name], 0))
--     ,LastName  = SUBSTRING([Name], CHARINDEX(' ', [Name], 0) + 1, LEN([Name]))
--WHERE
--  ID = ID

--UPDATE SCRIPT (clear old column)
--UPDATE Employee
--  SET Name = NULL
--WHERE
--  ID = ID

--SELECT * FROM Employee AS E

----------------------------------------------------------------------------------------------------
--EXECUTE Employee_GetByID @ID = 1
--EXECUTE Employee_GetByID @ID = 2
--EXECUTE Employee_GetByID @ID = 3
--
--DECLARE @NOW AS DATETIME = GETDATE()
--
--EXEC Employee_Update @ID    = 1
--                    ,@Name  = 'Leadr Coach'
--                    ,@Email = 'Leadr.Coach@leadr.com'
--                    ,@DOB   = @NOW
--
--EXEC Employee_Update @ID    = 2
--                    ,@Name  = 'Fred Flintstone'
--                    ,@Email = 'Fred.Flintstone@bedrock.bc'
--                    ,@DOB   = @NOW
--
--EXEC Employee_Update @ID    = 3
--                    ,@Name  = 'Barney Rubble'
--                    ,@Email = 'Barney.Rubble@bedrock.bc'
--                    ,@DOB   = @NOW
--
--EXECUTE Employee_GetByID @ID = 1
--EXECUTE Employee_GetByID @ID = 2
--EXECUTE Employee_GetByID @ID = 3

----------------------------------------------------------------------------------------------------
EXECUTE Employee_GetAll
DECLARE @NOW        AS DATETIME = GETDATE()
DECLARE @new_month  AS DATETIME = DATEADD(MONTH, 2, @NOW)
DECLARE @new_year   AS DATETIME = DATEADD(YEAR, 2, @NOW)

--OLD ADD STORED PROCEDURE -----------------------------------------------------
EXEC Employee_Update @ID    = 4
                    ,@Name  = 'Wilma Flintstone'
                    ,@Email = 'Wilma.Flintstone@bedrock.bc'
                    ,@DOB   = @new_month

EXEC Employee_Update @ID    = 5
                    ,@Name  = 'Betty Rubble'
                    ,@Email = 'Betty.Rubble@bedrock.bc'
                    ,@DOB   = @new_month

--NEW ADD STORED PROCEDURE -----------------------------------------------------
EXEC Employee_Update @ID    = 6
                    ,@FirstName  = 'Pebbles'
                    ,@LastName   = 'Flintstone'
                    ,@Email      = 'Pebbles.Flintstone@bedrock.bc'
                    ,@DOB        = @new_year

EXEC Employee_Update @ID    = 7
                    ,@FirstName  = 'Bambam'
                    ,@LastName   = 'Rubble'
                    ,@Email      = 'Bambam.Rubble@bedrock.bc'
                    ,@DOB        = @new_year

EXECUTE Employee_GetAll
