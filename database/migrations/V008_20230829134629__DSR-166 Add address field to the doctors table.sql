
-- DSR-166 Add missing field to Doctor (Address: pshysical direction of the doctors building)

ALTER TABLE doctors
ADD doctor_address VARCHAR(100) NOT NULL
DEFAULT '791 Crist Parks';


-- Update the writing privileges

GRANT 
INSERT (
    user_id, 
    specialty_id, 
    location_id, 
    years_of_experience, 
    doctor_summary, 
    education_summary, 
    doctor_first_name, 
    doctor_last_name, 
    image_url,
    doctor_address
    ), 
UPDATE (
    user_id, 
    specialty_id, 
    location_id, 
    years_of_experience, 
    doctor_summary, 
    education_summary, 
    is_deleted, 
    doctor_first_name, 
    doctor_last_name, 
    image_url,
    doctor_address
    )
ON doctors TO 'doc_connect_write';
