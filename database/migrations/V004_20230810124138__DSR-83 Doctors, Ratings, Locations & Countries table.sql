/*

Countries Table

*/
CREATE TABLE countries (
    country_id INTEGER NOT NULL COMMENT "ISO 3166 three-digit country number",
    country_name VARCHAR(56) NOT NULL COMMENT "The name of the country",
    iso2_name CHAR(2) NOT NULL COMMENT "ISO 3166 two-letter country codes",
    iso3_name CHAR(3) NOT NULL COMMENT "ISO 3166 three-letter country codes",
    created_at TIMESTAMP NOT NULL DEFAULT now() COMMENT "When was this record created",
    deleted_at TIMESTAMP COMMENT "When was this record deleted",
    is_deleted BOOLEAN NOT NULL DEFAULT 0 COMMENT "Whether this record is deleted or not",

    

    CONSTRAINT PK_country PRIMARY KEY (country_id),
    CONSTRAINT UC_country_iso2 UNIQUE (iso2_name),
    CONSTRAINT UC_country_iso3 UNIQUE (iso3_name),
    CONSTRAINT check_country_deleted_at_integrity CHECK (NOT (deleted_at IS NULL XOR is_deleted = 0 ) ),
    CONSTRAINT check_country_is_deleted_is_boolean CHECK (is_deleted = 0 or is_deleted = 1)

);

-- Create Trigger on is_deleted column so the timestamp is recorded
DELIMITER //
CREATE TRIGGER countries_when_deleted BEFORE UPDATE ON countries
FOR EACH ROW
BEGIN
    IF OLD.is_deleted = 0 AND NEW.is_deleted = 1 THEN
        SET NEW.deleted_at = now();
    ELSEIF OLD.is_deleted = 1 AND NEW.is_deleted = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "A Country Row can't be undeleted";
    END IF;
END//
DELIMITER ;

/*

Location Table

*/
CREATE TABLE locations (
    location_id INTEGER,
    country_id INTEGER NOT NULL COMMENT "ISO 3166 three-digit country number",
    city_name VARCHAR(80) NOT NULL COMMENT "The name of the city/town as a Unicode string (e.g. Goiânia)",
    city_ascii VARCHAR(80) NOT NULL COMMENT "ASCII string of the city/town (e.g. Goiania)",
    latitude DECIMAL(7,4) NOT NULL COMMENT "The latitude of the city/town",
    longitude DECIMAL(7,4) NOT NULL COMMENT "The longitude of the city/town",
    created_at TIMESTAMP NOT NULL DEFAULT now() COMMENT "When was this record created",
    deleted_at TIMESTAMP COMMENT "When was this record deleted",
    is_deleted BOOLEAN NOT NULL DEFAULT 0 COMMENT "Whether this record is deleted or not",
    

    CONSTRAINT PK_location PRIMARY KEY (location_id),
    CONSTRAINT FK_locations_countries_country_id FOREIGN KEY (country_id) 
    REFERENCES countries(country_id) ON DELETE NO ACTION,
    CONSTRAINT check_location_deleted_at_integrity CHECK (NOT (deleted_at IS NULL XOR is_deleted = 0 ) ),
    CONSTRAINT check_location_is_deleted_is_boolean CHECK (is_deleted = 0 or is_deleted = 1)
);

-- Create Trigger on is_deleted column so the timestamp is recorded
DELIMITER //
CREATE TRIGGER locations_when_deleted BEFORE UPDATE ON locations
FOR EACH ROW
BEGIN
    IF OLD.is_deleted = 0 AND NEW.is_deleted = 1 THEN
        SET NEW.deleted_at = now();
    ELSEIF OLD.is_deleted = 1 AND NEW.is_deleted = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "A Location Row can't be undeleted";
    END IF;
END//
DELIMITER ;

/*

Patients Abstract Table

*/
CREATE TABLE patients (
    patient_id INTEGER AUTO_INCREMENT,
    user_id VARCHAR(255) COMMENT "User Account associated with this patient",
    created_at TIMESTAMP NOT NULL DEFAULT now() COMMENT "When was this record created",
    deleted_at TIMESTAMP COMMENT "When was this record deleted",
    is_deleted BOOLEAN NOT NULL DEFAULT 0 COMMENT "Whether this record is deleted or not",

    CONSTRAINT PK_patient PRIMARY KEY (patient_id),
    CONSTRAINT FK_patients_AspNetUsers_user_id FOREIGN KEY (user_id)
    REFERENCES AspNetUsers(Id) ON DELETE NO ACTION,
    CONSTRAINT check_patient_deleted_at_integrity CHECK (NOT (deleted_at IS NULL XOR is_deleted = 0 ) ),
    CONSTRAINT check_patient_is_deleted_is_boolean CHECK (is_deleted = 0 or is_deleted = 1)
);



-- Create Trigger on is_deleted column so the timestamp is recorded
DELIMITER //
CREATE TRIGGER patients_when_deleted BEFORE UPDATE ON patients
FOR EACH ROW
BEGIN
    IF OLD.is_deleted = 0 AND NEW.is_deleted = 1 THEN
        SET NEW.deleted_at = now();
    ELSEIF OLD.is_deleted = 1 AND NEW.is_deleted = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "A Patient Row can't be undeleted";
    END IF;
END//
DELIMITER ;

/*

Doctors Table

*/
CREATE TABLE doctors (
    doctor_id INTEGER AUTO_INCREMENT,
    user_id varchar(255) NOT NULL COMMENT "User Account associated with this doctor",
    specialty_id INTEGER NOT NULL COMMENT "The specialty of this doctor",
    location_id INTEGER NOT NULL COMMENT "The location of this doctor",
    years_of_experience INTEGER COMMENT "Years of experience in numbers",
    doctor_summary TEXT COMMENT "Includes info as: past employment, certifications, etc",
    education_summary TEXT COMMENT "Contains info regarding all the education that doctor has",
    created_at TIMESTAMP NOT NULL DEFAULT now() COMMENT "When was this record created",
    deleted_at TIMESTAMP COMMENT "When was this record deleted",
    is_deleted BOOLEAN NOT NULL DEFAULT 0 COMMENT "Whether this record is deleted or not",


    CONSTRAINT PK_doctor PRIMARY KEY (doctor_id),

    CONSTRAINT FK_doctors_AspNetUsers_user_id FOREIGN KEY (user_id) 
    REFERENCES AspNetUsers(Id) ON DELETE NO ACTION,

    CONSTRAINT FK_doctors_specialties_specialty_id FOREIGN KEY (specialty_id) 
    REFERENCES specialties(specialty_id) ON DELETE NO ACTION,

    CONSTRAINT FK_doctors_location FOREIGN KEY (location_id)
    REFERENCES locations(location_id) ON DELETE NO ACTION,

    CONSTRAINT check_doctor_deleted_at_integrity CHECK (NOT (deleted_at IS NULL XOR is_deleted = 0 ) ),
    CONSTRAINT check_doctor_is_deleted_is_boolean CHECK (is_deleted = 0 or is_deleted = 1)
);

-- Create Trigger on is_deleted column so the timestamp is recorded
DELIMITER //
CREATE TRIGGER doctors_when_deleted BEFORE UPDATE ON doctors
FOR EACH ROW
BEGIN
    IF OLD.is_deleted = 0 AND NEW.is_deleted = 1 THEN
        SET NEW.deleted_at = now();
    ELSEIF OLD.is_deleted = 1 AND NEW.is_deleted = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "A Doctor Row can't be undeleted";
    END IF;
END//
DELIMITER ;


/*

Ratings table

*/
CREATE TABLE ratings (
    rating_id INTEGER AUTO_INCREMENT,
    doctor_id INTEGER NOT NULL COMMENT "The doctor that is being rated",
    patient_id INTEGER NOT NULL COMMENT "The patient that is rating",
    created_at TIMESTAMP NOT NULL DEFAULT now() COMMENT "The date and time the rating was posted",
    rating INTEGER NOT NULL COMMENT "The rating value measured by the 1-5 scale",
    comments VARCHAR(500) NOT NULL COMMENT "The comment that the patient left for this doctor, between 3-500 characters",
    deleted_at TIMESTAMP COMMENT "When was this record deleted",
    is_deleted BOOLEAN NOT NULL DEFAULT 0 COMMENT "Whether this record is deleted or not",


    CONSTRAINT PK_rating PRIMARY KEY (rating_id),

    CONSTRAINT FK_ratings_doctors_doctor_id FOREIGN KEY (doctor_id)
    REFERENCES doctors(doctor_id) ON DELETE NO ACTION,

    CONSTRAINT FK_ratings_patients_patient_id FOREIGN KEY (patient_id)
    REFERENCES patients(patient_id) ON DELETE NO ACTION,

    CONSTRAINT CHK_rating_in_range CHECK (rating BETWEEN 1 AND 5),
    CONSTRAINT CHK_length_of_rating_comment CHECK (LENGTH(comments) BETWEEN 3 AND 500),

    CONSTRAINT check_rating_deleted_at_integrity CHECK (NOT (deleted_at IS NULL XOR is_deleted = 0 ) ),
    CONSTRAINT check_rating_is_deleted_is_boolean CHECK (is_deleted = 0 or is_deleted = 1)
);

-- Create Trigger on is_deleted column so the timestamp is recorded
DELIMITER //
CREATE TRIGGER rating_when_deleted BEFORE UPDATE ON ratings
FOR EACH ROW
BEGIN
    IF OLD.is_deleted = 0 AND NEW.is_deleted = 1 THEN
        SET NEW.deleted_at = now();
    ELSEIF OLD.is_deleted = 1 AND NEW.is_deleted = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "A Rating Row can't be undeleted";
    END IF;
END//
DELIMITER ;


/*
PRIVILEGES
*/

-- Read Privileges
GRANT SELECT ON countries TO 'doc_connect_read';
GRANT SELECT ON locations TO 'doc_connect_read';
GRANT SELECT ON patients TO 'doc_connect_read';
GRANT SELECT ON doctors TO 'doc_connect_read';
GRANT SELECT ON ratings TO 'doc_connect_read';

-- Write Privileges
GRANT INSERT (country_id, country_name, iso2_name, iso3_name), UPDATE (country_id, country_name, iso2_name, iso3_name, is_deleted) ON countries TO 'doc_connect_write'; -- Notice it can't delete
GRANT INSERT (location_id, country_id, city_name, city_ascii, latitude, longitude), UPDATE (location_id, country_id, city_name, city_ascii, latitude, longitude, is_deleted) ON locations TO 'doc_connect_write';
GRANT INSERT (user_id), UPDATE (user_id, is_deleted) ON patients TO 'doc_connect_write';
GRANT INSERT (user_id, specialty_id, location_id, years_of_experience, doctor_summary, education_summary), 
UPDATE (user_id, specialty_id, location_id, years_of_experience, doctor_summary, education_summary, is_deleted) ON doctors TO 'doc_connect_write';
GRANT INSERT (doctor_id, patient_id, rating, comments), UPDATE (doctor_id, patient_id, rating, comments, is_deleted) ON ratings TO 'doc_connect_write';