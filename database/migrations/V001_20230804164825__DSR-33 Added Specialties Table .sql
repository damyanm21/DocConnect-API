-- Specialties Table Schema creation (Sprint 1)

-- Create Specialties tables
CREATE TABLE IF NOT EXISTS specialties (
    -- Fields
    specialty_id INT AUTO_INCREMENT PRIMARY KEY, -- Primary Key, Identifier
    image_url VARCHAR(2048) NOT NULL, -- The url for the image to be displayed on the Home Page
    specialty_name VARCHAR(60) NOT NULL, -- The name of the specialty

    -- SOFT DELETE Extra Fields
    created_at TIMESTAMP NOT NULL DEFAULT now(),
    deleted_at TIMESTAMP,
    is_deleted BOOLEAN NOT NULL DEFAULT 0,

    CONSTRAINT check_deleted_at_integrity CHECK (NOT (deleted_at IS NULL XOR is_deleted = 0 ) ),
    CONSTRAINT check_is_deleted_is_boolean CHECK (is_deleted = 0 or is_deleted = 1)
);

-- Create Trigger on is_deleted column so the timestamp is recorded
DELIMITER //
CREATE TRIGGER specialties_when_deleted BEFORE UPDATE ON specialties
FOR EACH ROW
BEGIN
    IF OLD.is_deleted = 0 AND NEW.is_deleted = 1 THEN
        SET NEW.deleted_at = now();
    ELSEIF OLD.is_deleted = 1 AND NEW.is_deleted = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "A Specialty Row can't be undeleted";
    END IF;
END//
DELIMITER ;

-- Add default value to image_url
ALTER TABLE specialties ALTER image_url SET DEFAULT 'https://img.freepik.com/free-vector/doctor-background-design_1270-62.jpg?w=360';

-- Create a Specialist View For Backend
CREATE OR REPLACE VIEW specialties_view AS (
    SELECT specialty_id, image_url, specialty_name 
    FROM specialties
    WHERE is_deleted = 0
);