-- Database: users_db
DROP DATABASE IF EXISTS users_db;

CREATE DATABASE users_db
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;