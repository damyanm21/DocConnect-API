-- Create the Schema
CREATE DATABASE doc_connect_db;

-- Create our database roles
CREATE ROLE 'doc_connect_developer', 'doc_connect_read', 'doc_connect_write';

-- Create Admin, Read & Write Roles
GRANT ALL ON doc_connect_db.* TO 'doc_connect_developer' WITH GRANT OPTION;

-- Create Users for the sprint database
CREATE USER 'dev1'@'%' IDENTIFIED BY 'dev1pass'; -- Change to Whatever you want
CREATE USER 'analytics'@'%' IDENTIFIED BY 'analytics_pass'; -- Change to Whatever you want
CREATE USER 'backend'@'%' IDENTIFIED BY 'backend_pass'; -- Change to Whatever you want


-- GRANT ROLES to users;
GRANT 'doc_connect_developer' TO 'dev1'@'%';
GRANT 'doc_connect_read' TO 'analytics'@'%';
GRANT 'doc_connect_read', 'doc_connect_write' TO 'backend'@'%';

-- SET DEFAULT ROLES for each user 
-- https://dev.mysql.com/doc/refman/8.0/en/set-default-role.html
SET DEFAULT ROLE 'doc_connect_developer' TO 'dev1'@'%';
SET DEFAULT ROLE 'doc_connect_read' TO 'analytics'@'%';
SET DEFAULT ROLE 'doc_connect_read', 'doc_connect_write' TO 'backend'@'%';

SET global log_bin_trust_function_creators = 1; -- Not needed on Azure