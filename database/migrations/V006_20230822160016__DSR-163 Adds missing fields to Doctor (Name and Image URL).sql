-- DSR-163 Adds missing fields to Doctor (Name and Image URL)

/*
Adds the following fields:
- doctor_first_name : The first name of the doctor that is going to be shown on the search page
- doctor_last_name : The last name of the doctor that is going to be shown on the search page
- image_url:  The photo of the doctor that is going to appear on the profile bio
*/
ALTER TABLE doctors
ADD doctor_first_name VARCHAR(80) NOT NULL
DEFAULT 'doctor_first_name';

ALTER TABLE doctors
ADD doctor_last_name VARCHAR(80) NOT NULL
DEFAULT 'doctor_last_name';

ALTER TABLE doctors
ADD image_url VARCHAR(2048) NOT NULL
DEFAULT 'images/generated_0.png';


/*
TODO: Replace existing doctors names with their user names
*/



/*
Update the writing privileges
*/

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
    image_url
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
    image_url
    )
ON doctors TO 'doc_connect_write';
