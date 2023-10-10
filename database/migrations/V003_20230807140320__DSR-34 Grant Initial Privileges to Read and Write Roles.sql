-- 'doc_connect_read' should be able to read Specialties table, ASP.NET Identity tables and Schema Version Table
GRANT SELECT ON specialties TO 'doc_connect_read';
GRANT SELECT ON AspNetRoleClaims TO 'doc_connect_read';
GRANT SELECT ON AspNetRoles TO 'doc_connect_read';
GRANT SELECT ON AspNetUserClaims TO 'doc_connect_read';
GRANT SELECT ON AspNetUserLogins TO 'doc_connect_read';
GRANT SELECT ON AspNetUserRoles TO 'doc_connect_read';
GRANT SELECT ON AspNetUsers TO 'doc_connect_read';
GRANT SELECT ON AspNetUserTokens TO 'doc_connect_read';
GRANT SELECT ON specialties_view TO 'doc_connect_read';
GRANT SELECT ON flyway_schema_history TO 'doc_connect_read';

-- 'doc_connect_write' should be able to write into ASP.NET Tables and Specialties table

-- ASP.NET Identity
GRANT INSERT, UPDATE ON AspNetRoleClaims TO 'doc_connect_write'; -- Notice it can't delete
GRANT INSERT, UPDATE ON AspNetRoles TO 'doc_connect_write';
GRANT INSERT, UPDATE ON AspNetUserClaims TO 'doc_connect_write';
GRANT INSERT, UPDATE ON AspNetUserLogins TO 'doc_connect_write';
GRANT INSERT, UPDATE ON AspNetUserRoles TO 'doc_connect_write';
GRANT INSERT, UPDATE ON AspNetUsers TO 'doc_connect_write';
GRANT INSERT, UPDATE ON AspNetUserTokens TO 'doc_connect_write';

-- Specialties Table
GRANT INSERT (image_url, specialty_name), UPDATE (image_url, specialty_name, is_deleted) ON specialties TO 'doc_connect_write';

-- FlyWay Schema Table -- Only the DBA and the ROOT user can write in this table
