----------------------------------------------------------------------------------------------------
-- 'Template' => 'Leadr.Pulse' -------------------------------------------------
----------------------------------------------------------------------------------------------------
INSERT INTO leadr."Template"(
    "OrganizationUID"
   ,"Type"
   ,"Title"
   ,"Objective"
   ,"Configuration"
--   ,"IsPulse"
   ,"IsDeleted"
)
VALUES
( NULL
 ,5 -- Curated | Pulse
 ,'Leadr Pulse'
 ,'used to help gauge people''s engagement score.'
 ,'[{\"Category\":\"1\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"4\",\"Count\":\"1\",\"Frequency\":\"4\"},{\"Category\":\"8\",\"Count\":\"1\",\"Frequency\":\"1\"},{\"Category\":\"16\",\"Count\":\"1\",\"Frequency\":\"1\"}]'
-- ,true
 ,false );
