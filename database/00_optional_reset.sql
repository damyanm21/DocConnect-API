-- RESET SCRIPT / DO NOT EXECUTE IN PRODUCTION !!

-- Drops the whole schema
DROP DATABASE IF EXISTS doc_connect_db;

-- Drops the created Roles
DROP ROLE IF EXISTS doc_connect_developer, doc_connect_read, doc_connect_write;

-- Drops the created users
DROP USER IF EXISTS 'dev1'@'%', 'analytics'@'%', 'backend'@'%';