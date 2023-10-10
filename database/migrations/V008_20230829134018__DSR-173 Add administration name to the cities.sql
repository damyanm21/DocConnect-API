-- DSR-173 Add administration name to the cities

/*
Adds the following fields:
- admin_name : This field will show what state/department/district administers this city
*/
ALTER TABLE locations
ADD admin_name VARCHAR(80) NOT NULL
DEFAULT 'admin_name';

/*
Update the writing privileges
*/

GRANT INSERT (
    location_id, 
    country_id, 
    city_name, 
    city_ascii, 
    latitude, 
    longitude,
    admin_name
    ), 
UPDATE (
    location_id, 
    country_id, 
    city_name, 
    city_ascii, 
    latitude, 
    longitude, 
    is_deleted,
    admin_name
    ) ON locations TO 'doc_connect_write';