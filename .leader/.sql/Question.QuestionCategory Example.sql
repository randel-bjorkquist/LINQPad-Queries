----------------------------------------------------------------------------------------------------
SELECT
  QC.*
 ,' | ' AS " + "
 ,QCQ.*
 ,' | ' AS " + "
  ,Q.*
FROM dbo.QuestionCategory AS QC
  INNER JOIN dbo.QuestionCategoryQuestion AS QCQ
    INNER JOIN dbo.Question AS Q
      ON Q.ID = QCQ.QuestionID
    ON QCQ.QuestionCategoryID = QC.ID

----------------------------------------------------------------------------------------------------
-- Question.Category Values (FlagAttribute enum)
--  0: Unknown
--  1: Clarity
--  2: Custom
--  3: Clarity | Custom
--  4: eNPS
--  5: Clarity | eNPS
--  6: Custom  | eNPS
--  7: Clarity | Custom | eNPS
--  8: Maximization
--  9: Clarity | Maximization
-- 10: Custom  | Maximization
-- 11: Clarity | Custom | Maximization
-- 12: eNPS    | Maximization
-- 13: Clarity | eNPS | Maximization
-- 14: Custom  | eNPS | Maximization
-- 15: Clarity | Custom | eNPS | Maximization
-- 16: Rapport
-- 17: Clarity | Rapport
-- 18: Custom  | Rapport
-- 19: Clarity | Custom | Rapport
-- continues (for Rapport) from 20 up to 31
-- 31: General | Clarity | eNPS | Maximization | Rapport
----------------------------------------------------------------------------------------------------
--DECLARE @Category AS TINYINT =  0 -- Unknown
--DECLARE @Category AS TINYINT =  1 -- Clarity
--DECLARE @Category AS TINYINT =  2 -- Custom
--DECLARE @Category AS TINYINT =  4 -- eNPS
--DECLARE @Category AS TINYINT =  8 -- Maximization
--DECLARE @Category AS TINYINT = 16 -- Rapport
--
--SELECT * FROM dbo.Question AS Q
--WHERE Q.Category = @Category
