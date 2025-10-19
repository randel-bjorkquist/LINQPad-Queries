---------------------------------------------------------------------------------------------------
USE Survey
GO

---------------------------------------------------------------------------------------------------
--DECLARE @IDs AS VARCHAR(MAX) = '1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 , 10'
----DECLARE @IDs AS VARCHAR(MAX) = '1 ,2 ,3 ,4 ,5 ,6 ,7 ,8 ,9 ,10'
----DECLARE @IDs AS VARCHAR(MAX) = '1, 2, 3, 4, 5, 6, 7, 8, 9, 10'
----DECLARE @IDs AS VARCHAR(MAX) = '1,2,3,4,5,6,7,8,9,10'
--SELECT IDs.[Value]
--FROM dbo.Csv2BigInt(@IDs, DEFAULT) IDs
--ORDER BY IDs.[Value] DESC
--
--SELECT IDs.[BIGINT] AS "BIGINT"
--FROM dbo.CSV_2_BIGINT(@IDs, DEFAULT) IDs
--ORDER BY IDs.[BIGINT] DESC

---------------------------------------------------------------------------------------------------
DECLARE @IDs AS VARCHAR(MAX) = '1 => 2 => 3 => 4 => 5 => 6 => 7 => 8 => 9 => 10'
--DECLARE @IDs AS VARCHAR(MAX) = '1 =>2 =>3 =>4 =>5 =>6 =>7 =>8 =>9 =>10'
--DECLARE @IDs AS VARCHAR(MAX) = '1=> 2=> 3=> 4=> 5=> 6=> 7=> 8=> 9=> 10'
--DECLARE @IDs AS VARCHAR(MAX) = '1=>2=>3=>4=>5=>6=>7=>8=>9=>10'
SELECT IDs.[Value]
FROM dbo.Csv2BigInt(@IDs, '=>') IDs
ORDER BY IDs.[Value] DESC

SELECT IDs.[BIGINT]
FROM dbo.CSV_2_BIGINT(@IDs, '=>') IDs
ORDER BY IDs.[BIGINT] DESC

---------------------------------------------------------------------------------------------------
--DECLARE @IDs AS VARCHAR(MAX) = '1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10'
----DECLARE @IDs AS VARCHAR(MAX) = '1 |2 |3 |4 |5 |6 |7 |8 |9 |10'
----DECLARE @IDs AS VARCHAR(MAX) = '1| 2| 3| 4| 5| 6| 7| 8| 9| 10'
----DECLARE @IDs AS VARCHAR(MAX) = '1|2|3|4|5|6|7|8|9|10'
--SELECT IDs.Value
--FROM dbo.Csv2Int(@IDs, '|') IDs
--ORDER BY IDs.Value DESC
--
--SELECT IDs.INT
--FROM dbo.CSV_2_INT(@IDs, '|') IDs
--ORDER BY IDs.INT DESC

---------------------------------------------------------------------------------------------------
--DECLARE @names AS VARCHAR(MAX) = 'Fred , Wilma , Barney , Betty , Mr. Slate'
----DECLARE @names AS VARCHAR(MAX) = 'Fred ,Wilma ,Barney ,Betty ,Mr. Slate'
----DECLARE @names AS VARCHAR(MAX) = 'Fred, Wilma, Barney, Betty, Mr. Slate'
----DECLARE @names AS VARCHAR(MAX) = 'Fred,Wilma,Barney,Betty,Mr. Slate'
--SELECT 
--  names.Value         AS "Value"
-- ,LOWER(names.Value)  AS "LowerCase"
-- ,UPPER(names.Value)  AS "UpperCase"
--FROM 
--  dbo.Csv2VarChar(@names, DEFAULT, DEFAULT) names
----  dbo.Csv2VarChar(@names, DEFAULT, 0) names
----  dbo.Csv2VarChar(@names, ',', DEFAULT) names
----  dbo.Csv2VarChar(@names, ',', 1) names
----  dbo.Csv2VarChar(@names, ',', 0) names
--ORDER BY
--  NAMES.Value DESC
--
--SELECT 
--  names.VARCHAR         AS "VARCHAR"
-- ,LOWER(names.VARCHAR)  AS "LowerCase"
-- ,UPPER(names.VARCHAR)  AS "UpperCase"
--FROM 
--  dbo.CSV_2_VARCHAR(@names, DEFAULT, DEFAULT) names
----  dbo.CSV_2_VARCHAR(@names, DEFAULT, 0) names
----  dbo.CSV_2_VARCHAR(@names, ',', DEFAULT) names
----  dbo.CSV_2_VARCHAR(@names, ',', 1) names
----  dbo.CSV_2_VARCHAR(@names, ',', 0) names
--ORDER BY
--  NAMES.VARCHAR DESC

---------------------------------------------------------------------------------------------------
--DECLARE @names AS VARCHAR(MAX) = 'Fred => Wilma => Barney => Betty => Mr. Slate'
----DECLARE @names AS VARCHAR(MAX) = 'Fred =>Wilma =>Barney =>Betty =>Mr. Slate'
----DECLARE @names AS VARCHAR(MAX) = 'Fred=> Wilma=> Barney=> Betty=> Mr. Slate'
----DECLARE @names AS VARCHAR(MAX) = 'Fred=>Wilma=>Barney=>Betty=>Mr. Slate'
--SELECT 
--  names.Value         AS "Value"
-- ,LOWER(names.Value)  AS "LowerCase"
-- ,UPPER(names.Value)  AS "UpperCase"
--FROM 
----  dbo.Csv2VarChar(@names, DEFAULT, DEFAULT) names
----  dbo.Csv2VarChar(@names, DEFAULT, 0) names
----  dbo.Csv2VarChar(@names, ',', DEFAULT) names
--  dbo.Csv2VarChar(@names, '=>', 1) names
----  dbo.Csv2VarChar(@names, '=>', 0) names
--ORDER BY
--  NAMES.Value DESC
--
--SELECT 
--  names.VARCHAR         AS "VARCHAR"
-- ,LOWER(names.VARCHAR)  AS "LowerCase"
-- ,UPPER(names.VARCHAR)  AS "UpperCase"
--FROM 
----  dbo.CSV_2_VARCHAR(@names, DEFAULT, DEFAULT) names
----  dbo.CSV_2_VARCHAR(@names, DEFAULT, 0) names
----  dbo.CSV_2_VARCHAR(@names, '=>', DEFAULT) names
--  dbo.CSV_2_VARCHAR(@names, '=>', 1) names
----  dbo.CSV_2_VARCHAR(@names, '=>', 0) names
--ORDER BY
--  NAMES.VARCHAR DESC

---------------------------------------------------------------------------------------------------
--DECLARE @IDs AS VARCHAR(MAX) = '1,2,3,4,5,6,7,8,9,10'
--SELECT * 
--FROM 
--  dbo.Survey AS S
--  
--  INNER JOIN dbo.Csv2BigInt(@IDs, DEFAULT) IDs
--    ON S.ID = IDs.value

---------------------------------------------------------------------------------------------------
--DECLARE @IDs AS VARCHAR(MAX) = '1|2|3|4|5|6|7|8|9|10'
--SELECT * 
--FROM 
--  dbo.Survey AS S
--  
--  INNER JOIN dbo.Csv2Int(@IDs, '|') IDs
--    ON S.ID = IDs.value

---------------------------------------------------------------------------------------------------
--DECLARE @IDs AS VARCHAR(MAX) = '1|2|3|4|5|6|7|8|9|10'
--SELECT * 
--FROM 
--  dbo.Survey AS S
--
--  INNER JOIN dbo.Csv2VarChar(@IDs, '|', DEFAULT) IDs
--    ON S.ID = IDs.Value

---------------------------------------------------------------------------------------------------
--DECLARE @IDs AS VARCHAR(MAX) = '1|2|3|4|5|6|7|8|9|10'
--SELECT * 
--FROM 
--  dbo.Survey AS S
--  
--  INNER JOIN dbo.CSV_2_VARCHAR(@IDs, '|', 0) IDs
--    ON S.ID = IDs.VARCHAR

