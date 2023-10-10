/*

DSR-180: [Data] Create Database Schema for Appointments

*/

CREATE TABLE appointments (
    appointment_id INTEGER AUTO_INCREMENT,
    doctor_id INTEGER NOT NULL COMMENT "The doctor that is going to attend",
    patient_id INTEGER NOT NULL COMMENT "The patient that is going to be attended",
    scheduled_time TIMESTAMP NOT NULL COMMENT "When this appointment is scheduled for",
    appointment_notes TEXT COMMENT "Here goes any additional notes that the patient want to include into the appointment. i.e. This appointment is for a child",
    created_at TIMESTAMP NOT NULL DEFAULT now() COMMENT "The date and time the appointment was created",
    deleted_at TIMESTAMP COMMENT "When was this record deleted",
    canceled_at TIMESTAMP COMMENT "When was this appointment canceled",
    is_deleted BOOLEAN NOT NULL DEFAULT 0 COMMENT "Whether this record is deleted or not",
    is_canceled BOOLEAN NOT NULL DEFAULT 0 COMMENT "If this appointment has been cancelled",

    CONSTRAINT PK_appointment PRIMARY KEY (appointment_id),

    CONSTRAINT FK_appointments_doctors_doctor_id FOREIGN KEY (doctor_id)
    REFERENCES doctors(doctor_id) ON DELETE NO ACTION,

    CONSTRAINT FK_appointments_patients_patient_id FOREIGN KEY (patient_id)
    REFERENCES patients(patient_id) ON DELETE NO ACTION,

    CONSTRAINT check_appointment_deleted_at_integrity CHECK (NOT (deleted_at IS NULL XOR is_deleted = 0 ) ),
    CONSTRAINT check_appointment_is_deleted_is_boolean CHECK (is_deleted = 0 or is_deleted = 1),
    CONSTRAINT check_appointment_is_canceled_is_boolean CHECK (is_canceled = 0 or is_canceled = 1),

    -- TIMESTAMPS Should be on 1 hour slots
    CONSTRAINT check_appointment_is_time_slot CHECK (EXTRACT(MINUTE FROM scheduled_time) = 0 and EXTRACT(SECOND FROM scheduled_time) = 0)
);

-- Create Trigger on is_deleted column so the timestamp is recorded
DELIMITER //
CREATE TRIGGER appointment_when_deleted BEFORE UPDATE ON appointments
FOR EACH ROW
BEGIN
    IF OLD.is_deleted = 0 AND NEW.is_deleted = 1 THEN
        SET NEW.deleted_at = now();
    ELSEIF OLD.is_deleted = 1 AND NEW.is_deleted = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "An Appointment Row can't be undeleted";
    END IF;
END//
DELIMITER ;

-- Create Trigger on is_canceled column so the timestamp is recorded

DELIMITER //
CREATE TRIGGER appointment_when_canceled BEFORE UPDATE ON appointments
FOR EACH ROW
BEGIN
    IF OLD.is_canceled = 0 AND NEW.is_canceled = 1 THEN
        SET NEW.canceled_at = now();
    ELSEIF OLD.is_canceled = 1 AND NEW.is_canceled = 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "An Appointment Row can't be uncanceled";
    END IF;
END//
DELIMITER ;

-- Create an Index on Dates for faster retrieval of appointments for specific dates

CREATE INDEX appointment_date_index ON appointments (
    scheduled_time
);


-- Grant Role Privileges

GRANT SELECT ON appointments TO 'doc_connect_read';
GRANT INSERT
(
    doctor_id,
    patient_id,
    scheduled_time,
    appointment_notes
),
UPDATE
(
    doctor_id,
    patient_id,
    scheduled_time,
    appointment_notes,
    is_deleted,
    is_canceled
)
ON appointments TO 'doc_connect_write';