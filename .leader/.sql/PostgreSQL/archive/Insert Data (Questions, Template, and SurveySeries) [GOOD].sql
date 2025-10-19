/******************************************************************************/
/** INSERT DATA ***************************************************************/
/******************************************************************************/

-- 'Template' => 'Leadr.Pulse' -------------------------------------------------
INSERT INTO leadr."Template"(
    "OrganizationUID",
    "Type",
    "Title",
    "Objective",
    "Configuration",
    "IsDeleted"
)
VALUES
( NULL
 ,5 -- Curated | Pulse
 ,'Leadr Pulse'
 ,'used to help gauge people''s engagement score.'
 ,'[{\"Category\":\"1\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"},{\"Category\":\"8\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"16\",\"Count\":\"1\",\"Frequency\":\"1\"}]'
 ,false );
--------------------------------------------------------------------------------

-- 'Clarity' -------------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I understand what is expected of me in my role.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am motivated by our organization''''s mission.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'Our team goals align with the primary goals of our organization.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'The work I do every day directly contributes to the success of our team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'The information I need to do my job is always made available to me.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,1
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I have access to the learning I need to grow in my role.'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------

-- 'eNPS' ----------------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,4
 ,2
 ,'{"minLabel":"","minValue":"0","maxLabel":"","maxValue":"10"}'
 ,'<p>Slide the bar to your desired number</p>'
 ,'How likely are you to recommend {OrganizationName} as a place to work?'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------

-- 'Maximization' --------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am regularly recognized and appreciated for the work I do.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'My individual strengths are being best utilized.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am strongly valued for the unique strengths I bring to the team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'There are clear opportunities for my growth and development here.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I have regular conversations with my leader about my development.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,8
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'In the last 6 months, I have met with my leader to discuss my progress.'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------

-- Rapport ---------------------------------------------------------------------
INSERT INTO leadr."Question"(
    "TemplateID",
    "Category",
    "Type",
    "Configuration",
    "Description",
    "Question",
    "IsDeleted"
)
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am comfortable giving honest feedback to my team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I have confidence and trust in my leader.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am encouraged to come up with new and better ways of doing things.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I enjoy collaborating with my team.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am comfortable asking for help when I need it.'
 ,false
FROM leadr."Template" AS T
UNION
SELECT
  max(T."ID")
 ,16
 ,2
 ,'[{"option":"Strongly Disagree","rank":1},{"option":"Disagree","rank":2},{"option":"Neutral","rank":3},{"option":"Agree","rank":4},{"option":"Strongly Agree","rank":5}]'
 ,'<p>Select one of the options</p>'
 ,'I am a valued member of my team.'
 ,false
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------

-- SurveySeries ----------------------------------------------------------------
INSERT INTO leadr."SurveySeries"(
    "OrganizationUID",
    "CreatedByUID",
    "TemplateID",
    "Configuration",
    "BeginDate"
)
SELECT
    '0x1c45f',
    '0x674891',
    T."ID",
    T."Configuration",
    current_timestamp
FROM leadr."Template" AS T;
--------------------------------------------------------------------------------
