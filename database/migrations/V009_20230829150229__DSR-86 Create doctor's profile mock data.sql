/*

	DSR-86: Create doctors profile mock data

	This script adds some mock doctors to the database.

	In order for doctors to be added to the DB, we also need to provide some data:

	- Countries
	- Locations
	
*/

-- Adds the countries to be used (Just BG, PY & US)
INSERT INTO countries (country_id,country_name,iso2_name,iso3_name) VALUES
	 (100,'Bulgaria','BG','BGR'),
	 (600,'Paraguay','PY','PRY'),
	 (840,'United States of America','US','USA');


-- Adds the cities/locations the doctors are going to be attending patients from
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840000373,840,'Rochester','Rochester',43.1680,-77.6162,'New York'),
	 (1840000386,840,'Buffalo','Buffalo',42.9018,-78.8487,'New York'),
	 (1840000417,840,'Albany','Albany',42.6664,-73.7987,'New York'),
	 (1840000434,840,'Worcester','Worcester',42.2705,-71.8079,'Massachusetts'),
	 (1840000455,840,'Boston','Boston',42.3188,-71.0852,'Massachusetts'),
	 (1840000466,840,'Springfield','Springfield',42.1155,-72.5395,'Massachusetts'),
	 (1840000494,840,'Chicago','Chicago',41.8375,-87.6866,'Illinois'),
	 (1840000572,840,'Toledo','Toledo',41.6638,-83.5827,'Ohio'),
	 (1840000596,840,'Cleveland','Cleveland',41.4764,-81.6805,'Ohio'),
	 (1840000673,840,'Philadelphia','Philadelphia',40.0077,-75.1339,'Pennsylvania');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840000791,840,'Akron','Akron',41.0798,-81.5219,'Ohio'),
	 (1840001044,840,'Allentown','Allentown',40.5961,-75.4756,'Pennsylvania'),
	 (1840001254,840,'Pittsburgh','Pittsburgh',40.4397,-79.9763,'Pennsylvania'),
	 (1840001288,840,'Harrisburg','Harrisburg',40.2752,-76.8843,'Pennsylvania'),
	 (1840001592,840,'Baltimore','Baltimore',39.3051,-76.6144,'Maryland'),
	 (1840001651,840,'St. Louis','St. Louis',38.6359,-90.2451,'Missouri'),
	 (1840001686,840,'Wichita','Wichita',37.6895,-97.3443,'Kansas'),
	 (1840001698,840,'Richmond','Richmond',37.5295,-77.4756,'Virginia'),
	 (1840001839,840,'New Orleans','New Orleans',30.0687,-89.9288,'Louisiana'),
	 (1840002915,840,'Madison','Madison',43.0822,-89.3930,'Wisconsin');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840002928,840,'Grand Rapids','Grand Rapids',42.9619,-85.6562,'Michigan'),
	 (1840003046,840,'Milwaukee','Milwaukee',43.0642,-87.9675,'Wisconsin'),
	 (1840003289,840,'Providence','Providence',41.8230,-71.4187,'Rhode Island'),
	 (1840003760,840,'Columbus','Columbus',39.9862,-82.9855,'Ohio'),
	 (1840003814,840,'Cincinnati','Cincinnati',39.1413,-84.5060,'Ohio'),
	 (1840003871,840,'Virginia Beach','Virginia Beach',36.7335,-76.0435,'Virginia'),
	 (1840003971,840,'Detroit','Detroit',42.3834,-83.1024,'Michigan'),
	 (1840004773,840,'Hartford','Hartford',41.7661,-72.6834,'Connecticut'),
	 (1840004836,840,'Bridgeport','Bridgeport',41.1918,-73.1954,'Connecticut'),
	 (1840004850,840,'New Haven','New Haven',41.3113,-72.9246,'Connecticut');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840005293,840,'Hempstead','Hempstead',40.6629,-73.6089,'New York'),
	 (1840006060,840,'Washington','Washington',38.9047,-77.0163,'District of Columbia'),
	 (1840006507,840,'Birmingham','Birmingham',33.5279,-86.7971,'Alabama'),
	 (1840007069,840,'Des Moines','Des Moines',41.5725,-93.6105,'Iowa'),
	 (1840007830,840,'Minneapolis','Minneapolis',44.9635,-93.2678,'Minnesota'),
	 (1840008535,840,'Kansas City','Kansas City',39.1238,-94.5541,'Missouri'),
	 (1840009315,840,'Omaha','Omaha',41.2627,-96.0529,'Nebraska'),
	 (1840013305,840,'Honolulu','Honolulu',21.3294,-157.8460,'Hawaii'),
	 (1840013660,840,'Atlanta','Atlanta',33.7628,-84.4220,'Georgia'),
	 (1840013941,840,'Baton Rouge','Baton Rouge',30.4420,-91.1311,'Louisiana');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840014486,840,'Knoxville','Knoxville',35.9692,-83.9496,'Tennessee'),
	 (1840014497,840,'Raleigh','Raleigh',35.8324,-78.6429,'North Carolina'),
	 (1840014557,840,'Charlotte','Charlotte',35.2083,-80.8303,'North Carolina'),
	 (1840014730,840,'Columbia','Columbia',34.0378,-80.9036,'South Carolina'),
	 (1840015031,840,'Jacksonville','Jacksonville',30.3322,-81.6749,'Florida'),
	 (1840015094,840,'Palm Bay','Palm Bay',27.9631,-80.6593,'Florida'),
	 (1840015099,840,'Orlando','Orlando',28.4773,-81.3370,'Florida'),
	 (1840015119,840,'Port St. Lucie','Port St. Lucie',27.2796,-80.3884,'Florida'),
	 (1840015130,840,'Cape Coral','Cape Coral',26.6443,-81.9957,'Florida'),
	 (1840015149,840,'Miami','Miami',25.7840,-80.2101,'Florida');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840015163,840,'Charleston','Charleston',32.8168,-79.9687,'South Carolina'),
	 (1840015457,840,'Memphis','Memphis',35.1087,-89.9663,'Tennessee'),
	 (1840015509,840,'Little Rock','Little Rock',34.7256,-92.3577,'Arkansas'),
	 (1840015982,840,'Tampa','Tampa',27.9945,-82.4447,'Florida'),
	 (1840015988,840,'Sarasota','Sarasota',27.3387,-82.5432,'Florida'),
	 (1840018789,840,'Denver','Denver',39.7620,-104.8758,'Colorado'),
	 (1840018825,840,'Colorado Springs','Colorado Springs',38.8674,-104.7605,'Colorado'),
	 (1840018905,840,'Concord','Concord',37.9722,-122.0016,'California'),
	 (1840019148,840,'Bakersfield','Bakersfield',35.3529,-119.0359,'California'),
	 (1840019176,840,'Albuquerque','Albuquerque',35.1054,-106.6465,'New Mexico');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840019440,840,'Dallas','Dallas',32.7935,-96.7667,'Texas'),
	 (1840019590,840,'Austin','Austin',30.3005,-97.7522,'Texas'),
	 (1840019941,840,'Portland','Portland',45.5371,-122.6500,'Oregon'),
	 (1840020121,840,'Reno','Reno',39.5497,-119.8483,'Nevada'),
	 (1840020135,840,'Ogden','Ogden',41.2279,-111.9682,'Utah'),
	 (1840020174,840,'Provo','Provo',40.2457,-111.6457,'Utah'),
	 (1840020319,840,'Fresno','Fresno',36.7830,-119.7939,'California'),
	 (1840020364,840,'Las Vegas','Las Vegas',36.2333,-115.2654,'Nevada'),
	 (1840020428,840,'Oklahoma City','Oklahoma City',35.4676,-97.5136,'Oklahoma'),
	 (1840020490,840,'Long Beach','Long Beach',33.7977,-118.1670,'California');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840020491,840,'Los Angeles','Los Angeles',34.1141,-118.4068,'California'),
	 (1840020551,840,'Riverside','Riverside',33.9381,-117.3949,'California'),
	 (1840020553,840,'Murrieta','Murrieta',33.5719,-117.1909,'California'),
	 (1840020566,840,'Mesa','Mesa',33.4015,-111.7178,'Arizona'),
	 (1840020568,840,'Phoenix','Phoenix',33.5722,-112.0892,'Arizona'),
	 (1840020580,840,'Mission Viejo','Mission Viejo',33.6096,-117.6551,'California'),
	 (1840020696,840,'Fort Worth','Fort Worth',32.7817,-97.3474,'Texas'),
	 (1840020925,840,'Houston','Houston',29.7860,-95.3885,'Texas'),
	 (1840021024,840,'McAllen','McAllen',26.2252,-98.2467,'Texas'),
	 (1840021117,840,'Seattle','Seattle',47.6211,-122.3244,'Washington');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840021383,840,'Salt Lake City','Salt Lake City',40.7776,-111.9311,'Utah'),
	 (1840021491,840,'Sacramento','Sacramento',38.5677,-121.4685,'California'),
	 (1840021543,840,'San Francisco','San Francisco',37.7558,-122.4449,'California'),
	 (1840021570,840,'San Jose','San Jose',37.3012,-121.8480,'California'),
	 (1840021672,840,'Tulsa','Tulsa',36.1283,-95.9042,'Oklahoma'),
	 (1840021990,840,'San Diego','San Diego',32.8313,-117.1222,'California'),
	 (1840022101,840,'Tucson','Tucson',32.1541,-110.8787,'Arizona'),
	 (1840022220,840,'San Antonio','San Antonio',29.4632,-98.5238,'Texas'),
	 (1840023252,840,'El Paso','El Paso',31.8476,-106.4300,'Texas'),
	 (1840030084,840,'Indianapolis','Indianapolis',39.7771,-86.1458,'Indiana');
INSERT INTO locations (location_id,country_id,city_name,city_ascii,latitude,longitude,admin_name) VALUES
	 (1840030815,840,'Louisville','Louisville',38.1663,-85.6485,'Kentucky'),
	 (1840033999,840,'Bronx','Bronx',40.8501,-73.8662,'New York'),
	 (1840034000,840,'Manhattan','Manhattan',40.7834,-73.9662,'New York'),
	 (1840034002,840,'Queens','Queens',40.7498,-73.7976,'New York'),
	 (1840034016,840,'New York','New York',40.6943,-73.9249,'New York'),
	 (1840034030,840,'Brooklyn','Brooklyn',40.6501,-73.9496,'New York'),
	 (1840034032,840,'Staten Island','Staten Island',40.5834,-74.1496,'New York'),
	 (1840034249,840,'Dayton','Dayton',39.7805,-84.2003,'Ohio'),
	 (1840036155,840,'Nashville','Nashville',36.1715,-86.7842,'Tennessee'),
	 (1840057464,840,'Brookhaven','Brookhaven',40.8320,-72.9517,'New York');


-- Adds the doctors to be presented to the users
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000000,'dc5e03c5-5ac9-4614-a98f-667e34a4bb7a',1,1840006060,2,'Jonathan Walker, M.D., a cardiologist in Washington, D.C., has been providing patients with specialized care for two years. With a background in research, Dr. Walker is certified in cardiovascular medicine and has a particular interest in treating heart conditions related to diabetes.','- M.D.
- Harvard Medical School
- Massachusetts General Medical Center
- George Washington University
- Georgetown University Medical School

Number of years of experience: 2','Jonathan','Walker','images/generated_0.png','61251 Melissa Radial Apt. 746'),
	 (2000000001,'ab8fab90-c9c1-46b8-9e42-969e96a48863',2,1840034030,10,'Dr. Rick Shaffer is a renowned orthopedic surgeon, having completed his medical education at Columbia University. With over 10 years of experience, Dr. Shaffer specializes in shoulder and upper extremity surgeries, focusing on providing his patients with cutting-edge treatment options. Born and raised in Brooklyn, New York, Dr. Shaffer has built his successful career by prioritizing patient care while utilizing the latest advancements in medical technology at his state-of-the-art facility.','- 2005: MD in Columbia University, Columbia University
- 2001: Residency in Columbia University Health Sciences
- Medical Degree from New York University School of Medicine
- 1998: BA in Columbia College, Columbia University

Dr. Rick Shaffer is an experienced orthopedic surgeon, having completed his medical education at Columbia University. Dr. Shaffer specializes in shoulder and upper extremity surgery, focusing on providing his patients with cutting-edge treatment options. Born and raised in Brooklyn, New York, he has built his successful career by prioritizing patient care while utilizing the latest advancements in medical technology at his state-of-the-art facility.','Rick','Shaffer','images/generated_1.png','6727 Daniel Drives'),
	 (2000000002,'398124e3-18d8-4a84-97ef-7ff00f1f864e',3,1840018825,2,'Dr. Bryan Gilbert is a practicing neurologist in Colorado Springs, CO, with a special interest in neuroradiology. He completed his undergraduate studies at Vanderbilt University, before obtaining his medical degree from Vanderbilt University School of Medicine. Dr. Gilbert completed his training in neurology Residency at Vanderbilt University Medical Center, where he received numerous honors and awards during his tenure. He is board certified in neurology through the American Board of Neurology and Neurosurgery, as well as Neuroradiology Certification through the American Radiological Society. Dr. Gilbert is a member of multiple professional organizations, including the American Academy of Neurology and Radiology and the Colorado Neurology Society.','- Vanderbilt University School of Medicine
- Vanderbilt University School of Medicine
- American Board of Neurology and Neurosurgery
- Neuroradiology Certification

Number of years practicing: 1-4
Specialties:
- Neuroradiology
- Neurology
- Radiology
- Diagnostic Radiology','Bryan','Gilbert','images/generated_2.png','49385 Day Vista Suite 343'),
	 (2000000003,'3b68d4f5-d3c2-4110-9596-8a073f5ed1b5',4,1840021024,3,'Dr. Gregory Wilson is an oncologist from McAllen, Texas, with 3 years of experience.','*Baylor College of Medicine, Houston, Texas (2009-2013)

- MD degree from Baylor College of Medicine
- Residency in Oncology at MD Anderson Cancer Center

Baylor College of Medicine, Houston, Texas

Therapeutic Radiology at Baylor College of Medicine

McAllen Cancer Center, McAllen, Texas

- Member - American College of Radiology (ACR)
- Member - American Society of Clinical Oncology (ASCO)
- Member - Texas Society of Clinical Oncology','Gregory','Wilson','images/generated_3.png','6115 Montgomery Fields Suite 314'),
	 (2000000004,'01b3e821-4b38-4ede-be37-963eccc84bb8',5,1840019941,15,'Dr. Michael Miller is a gastroenterologist based in Portland, Oregon. With over 15 years of experience, he specializes in the diagnosis, treatment, and prevention of digestive system disorders. He received his medical degree from the University of California, San Francisco, and completed a gastroenterology fellowship at Columbia University. Dr. Miller is a member of several professional organizations, including the American Gastroenterological Association and the American College of Gastroenterology.','- M.D. / Medical School
--Columbia University
- G.S.A. (Gastroenterology Specialty Achievement)
- American Gastroenterological Association
- American College of Gastroenterology
Dr. Michael Miller''s Practice Location:
--Gastroenterology
Portland Gastroenterology Associates
1111 NE 122nd Avenue, #100, Portland, OR 97220
Phone: 503-255-1111, Fax: 503.255.1136

--Providence Portland Medical Center
--Providence Portland Medical Center-South Building

--Clackamas County Medical Center
--Tualatin Medical Center','Michael','Miller','images/generated_4.png','756 Kevin Orchard Apt. 140'),
	 (2000000005,'4aa0b913-f987-4fe6-abe9-5926091923fb',6,1840015982,4,'Known for her compassionate approach, Rebecca Garcia, DO, FAAD, is a board-certified dermatologist located in Tampa, FL. With over 4 years of experience as a practicing physician, Dr. Garcia specializes in medical, surgical, and cosmetic dermatology. Committed to delivering personalized care, she focuses on the diagnosis and management of a wide range of skin issues. Dr. Garcia received her Doctor of Osteopathic Medicine degree at the University of Osteopathic Medicine and Surgery in Des Moines, IA. To stay up-to-date on the latest advancements in her field, she belongs to several medical organizations, including the American Osteopathic College of Dermatology and the American Academy of Dermatology.','- DO, FAAD (University of Osteopathic Medicine and Surgery)

- MD (University of Medicine and Dentistry of NJ)
- BA, St. John’s University (College of Arts and Sciences)
- BS (Biology, Chemistry, & Math at Saint Peter''s University)

Current Professional Positions:

- Member, American Academy of Dermatology (2012)
- Member, American Osteopathic College of Dermatology (2011)
- Member, American Board of Dermatology, Dermatologic Surgery, and Laser Therapy (2013)


- Dermatology (2010 to current)

- Dermatopathology (2000 to current)

- Mohs Surgery (1988 to current)
- Laser Medicine: Laser Treatments, Laser Hair Removal, and Laser Surgery
- Botox
- Injectables (Botox, Radiesse, Bellafill, Latisse, etc.)
- Cosmetic Laser Medicine (Botox, Laser Hair Removal, Laser Treatment, Laser Therapy, Laser Rejuvenation, Laser Tattoo Removal)
- Skin, Hair, Nail, and Cosmetic Dermatology
- Acne
- Psoriasis
- Eczema
- Rosacea
- Mucormycosis
- Wound Care
- Skin Cancer
- Mohs Surgical Pathology (1996 to current)
- Dermatopathologist (2000 to current)
- Dermatopathologist (2001 to 2008)
- Dermatopathologist
- Dermatopathologist
- Dermatopathologist
- Skin Pathologist
- Skin Pathologist
- Skin Pathologist
- Skin Pathologist
- Skin Pathologist
- Dermatopathologist','Rebecca','Garcia','images/generated_5.png','41890 Dunn Junctions'),
	 (2000000006,'52d24282-4d89-42d1-ad65-36433af67634',7,1840030815,9,'Bobby Miller is a pediatric healthcare professional hailing from Louisville, Kentucky, with over 9 years of experience. He is board-certified in Pediatrics and has obtained multiple certifications to his name, including fluency in Spanish. Throughout his career, Bobby has been dedicated to providing compassionate and quality healthcare to children, focusing on preventive care and treating the whole child - body, mind, and spirit.','- 2005 Master of Science, Pediatric Nurse Practitioner, University of Cincinnati, Cincinnati, Ohio 
- 2003 Bachelor of Science, Psychology, Western Kentucky University, Bowling Green, Kentucky
- 2004 Certified Pediatric Nurse Practitioner, Kentucky Board
- 2005 Master of Science, Pediatric Nurse Practitioner, Kentucky Board
- 2013 - 2020
- 2002-2005
- 2000-2004
- 2008-Present
- 2008-Present
- 2004-Present
- 2003-2004

This doctor’s certifications are:

- ACHPN Certified Pediatric Nurse Practitioner
- 2016 - 2021
- 2016-2021
- 2016 - 2021

This doctor’s board certifications are:

- Pediatrics
- 2005-present
- 2005-present

Specialties:
- Pediatric Primary Care
- Immunizations and Vaccines
- Asthma
- ADD/ADHD
- ADHD
- ADHD & Depression
- Acute care','Bobby','Miller','images/generated_6.png','38411 Fisher Vista'),
	 (2000000007,'ed1f8d42-d645-4e9d-9013-0fbdaa2e7dea',8,1840020925,11,'Dr. Jacob Carter is a board-certified urologist, based in Houston, Texas. With more than a decade of experience, Dr. Carter specializes in treating a wide range of urological and bladder disorders, including urinary tract infections, prostate problems, and kidney stones. As a dedicated professional, Dr. Carter strives to provide his patients with the highest-quality care and personalized attention.','* 1985-1989: BS, Vanderbilt University

* 2000-2002: MD, UT-Health-San Antonio

* 2002: Residency in Urology at the Baylor College of Medicine

Dr. Jacob Carter is a Urologist in Houston, TX.','Jacob','Carter','images/generated_7.png','90464 Amanda Port'),
	 (2000000008,'83fdd470-e027-4d49-ab18-3ab36795938a',9,1840021024,13,'Dr. Brian Dennis is a board-certified ophthalmologist based in McAllen, Texas. He has over thirteen years of experience in the medical field, specializing in ophthalmology, and has completed numerous advanced training programs in the field of ocular disease. Dr. Dennis''s areas of expertise include glaucoma, corneal surgery, and cataracts.','- Doctor of Ophthalmology, University of Texas Health Science Center San Antonio 
- Residency in Medicine & Ophthalmology, University Health System - Augusta, Georgi
- Bachelor of Science in Zoology, Texas A & M University
- Member, American Medical Association
- Member, American Academy of Ophthalmology

Dr. Dennis''s current medical education is a post-residency fellowship in Glaucoma with emphasis in Corneal Disease. Dr. Dennis is a board certified ophthalmologist who graduated with honors in 2000. 

Dr. Dennis completed additional training in Corneal Disease in his specialty at the renowned Bascom Palmer Hospital in Sarasota, Florida. He is a member of the American Medical Association and American Academy of Ophthalmology.
- Glaucoma
- Corneal Disease
- Ophthalmology
- Cataracts
- Glaucoma
- Corneal Disease
- Refractive Laser
- Eye Exam
- Eye Exams
Dr. Dennis is a Board-Certified Ophthalmologist. He received his Doctorate in Ophthalmology from the University of Texas Health Science Center in San Antonio, Texas. He also completed a post-residency Glaucoma Fellowship with a focus on Corneal Disease at Bascom-Palmer Eye Hospital. In addition to his training at Bascom-Palmer, Dr. Dennis also attended the prestigious Cornea and Refractive Laser Specialty Residency at the University of Tennessee Medical Center.
Dr. Dennis is a member of the American Medical Association and American Academy of Ophthalmology.
Dr. Dennis completed additional training in Corneal Disease in his specialty at the renowned Bascom Palmer Eye Hospital in Sarasota, Florida. He is also a member of the prestigious Cornea and Refractive Laser Specialty Residency at the University of Tennessee Medical Center.
Glaucoma, Corneal Disease, Corneal Transplant, Cataract, Refractive Laser, Eye Exam
- University of Texas Health Science Center','Brian','Dennis','images/generated_8.png','4503 Braun Pine Apt. 904'),
	 (2000000009,'fcaafb6c-910f-4d77-a18c-fa9ba0e0e49f',1,1840000572,2,'Dr. Kelly Evans is a cardiologist based in Toledo, Ohio. With over 2 years of experience, Dr. Evans specializes in the diagnosis, treatment, and prevention of heart diseases and other related ailments. He is highly knowledgeable in the field and is dedicated to providing personalized, comprehensive care to his patients.','- Medical Degree: Harvard University

Dr. Kelly Evans has been recognized as one of the best Cardiologists in the country and the top 1 % in the nation in 3 different categories of treatment.','Kelly','Evans','images/generated_9.png','0103 Jamie Point Apt. 976');
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000010,'a734c7ee-431a-4011-8a85-cabe670dd748',2,1840020568,13,'Dr. Carrie Tyler is a well-respected orthopedic surgeon based in Phoenix, Arizona. With more than 13 years of experience, Dr. Tyler specializes in complex joint and bone-related issues. A highly skilled surgeon, she has achieved numerous certifications and awards throughout her illustrious career.','
MSc: [University of Arizona, Arizona]','Carrie','Tyler','images/generated_10.png','32991 Christopher Cove Apt. 143'),
	 (2000000011,'63228b2f-b48c-4a39-a671-56eb1fe0075b',3,1840057464,14,'Richard Hamilton is a board-certified neurologist based in Brookhaven, New York. With over 14 years of experience in the field, Dr. Hamilton specializes in the diagnosis and treatment of a multitude of neurological conditions. He is highly regarded in his community for his compassionate care and thorough approach to patient care.','- Bachelor of Science in Biology, University of California, San Diego
- Doctor of Medicine, University of Medicine and Dentistry of New Jersey - Newark
- Fellowship, Clinical Neurology, New Jersey Medical School - Newark
- Diplomate Board Certification in Neurology
Number of years in practice: 14
Specialties: Neurology
Practice Location: Brooklyn NY, USA','Richard','Hamilton','images/generated_11.png','26728 Miller Expressway Apt. 812'),
	 (2000000012,'b57bd8da-24a3-4ef0-9695-06b3d3814aa9',4,1840013305,11,'Sherri Hernandez, MD, is an oncologist at Hawaii Cancer Center. With over 11 years of experience, Dr. Hernandez specializes in cancer treatment and is based in Honolulu, Hawaii.','- Medical degree (MD), Johns Hopkins University
- Postdoctoral fellowship, Johns Hopkins University, Department of Oncology
- Fellowship, American Medical Association, American Cancer Society, and National Cancer Institutes
- Bachelor of Science (BS), George Washington University


- American Cancer Society
- American Medical Association
- American Osteopathic Physicians
- Hawaii Cancer Center','Sherri','Hernandez','images/generated_12.png','1815 Deborah Harbor'),
	 (2000000013,'972a5d32-24c3-42dd-85c7-18366c7227c0',5,1840020491,7,'Dr. Ryan Lyons is a gastroenterologist practicing in Los Angeles, California. He is board certified in gastroenterology, internal medicine, and nutrition. Dr. Lyons has 7 years of clinical experience in the treatment of various gastroenterological conditions, including but not limited to, peptic ulcer disease, inflammatory bowel disease, and liver diseases. He currently works at St. John''s Health Center and Providence Saint John''s Health Center, where he performs procedures such as colonoscopies and endoscopies.','- MD: 2020-Present
- BS: 2004-2010
- BS: 1999-2004
- BS: 1995-1999

List any clinical affiliations this doctor is affiliated with. 
Dr. Ryan Lyons is not only board certified in gastroenterology, internal medicine, and nutrition but also currently serves as an attending physician of the Providence Saint John''s Medical Center and St. John''s Health Center, where he performs procedures such as colonoscopies and endoscopies. He also holds academic affiliations with UCLA, Cedars-Sinai, and USC, providing expertise and guidance in the treatment of gastroenterological conditions. 
Dr. Ryan Lyons holds academic affiliations with Cedars-Sinai, University of Southern California, and UCLA, providing expertise in the treatment of gastroenterological conditions. Dr. Lyons also holds a medical degree from UCLA School of Medicine, as well as a BS in Biochemistry and a BS in Microbiology & Physiology.','Ryan','Lyons','images/generated_13.png','85650 Parker Lakes'),
	 (2000000014,'7576c293-6e64-4eae-b06c-999bf9049a49',6,1840001254,5,'Dr. Sabrina Everett is a board-certified dermatologist based in Pittsburgh, PA. With over five years of experience in the field, she specializes in general and cosmetic dermatology. She attended the University of Pennsylvania for her medical education, and completed her residency at the renowned Children''s Hospitals and Clinics of Pittsburgh. Dr. Everett is a member of several professional organizations, including the American Academy of Dermatology and the Pennsylvania Dermatological Society. She is committed to providing her patients with personalized, high-quality care.','- Medical degree: University of Pennsylvania

- Residency: Children''s Hospitals and Clinics of Pittsburgh','Sabrina','Everett','images/generated_14.png','09956 Joanna Junctions Suite 403'),
	 (2000000015,'5b56b0a5-e0ad-4726-b242-eff19a96e50b',7,1840020174,10,'Shannon Haynes, a pediatric nurse practitioner from Provo, Utah, with over 10 years of experience in the medical field, is an accomplished healthcare professional dedicated to providing quality care to children.','- 2003 BSN BYU-Idaho
- BSN Brigham Young Univeristy
- MSN BYU-Idaho
- 2013-2016: Family Practice Residency at Brigham Young Univeristy

I would highly recommend him to anyone. 
Shannon Haynes
- Pediatric Nurse Practitioner / 12+ yrs experience
M: 801-420-1239 (call)
E: shannonhaynes.com
- Pediatric Nurse Practitioner / 12+ yrs experience
M: 801-420-1239 (call)
E: shannonhaynes.com
- BSN BYU-Idaho
- BSN Brigham Young Univeristy
- MSN BYU-Idaho

- Family Practice Residency at Brigham Young University (2013 to 2016)
I would recommend Ms. Haynes to any parent.
Shannon Haynes
- 12yrs
- Pediatric Nurse Practitioner
- BYU-Idaho
E: shannonhaynes.com
- 12-yrs experience as a PNP
- BYU-Idaho
- BSN BYU
- MS in Family Practice at BYU
M: 801-420-1239
E: shannaynes.com
- 5+ yrs experience as a PNP
- 7+ years experience in Pediatrics
BSN, BYU
M: 801-420-1239
E: shannonhaynes.com

- Graduate from BYU-Idaho''s Family Practice Program
E: shannonhaynes.com','Shannon','Haynes','images/generated_15.png','55206 Martin Lake'),
	 (2000000016,'ecbe0752-d3dd-407e-8e81-2d1f60f02ae5',8,1840005293,11,'Daniel Hebert, a urologist in Hempstead, New York, is a highly experienced professional with 11 years of experience in the field.','- University of Rochester Medical Center: 1988
- University of Virginia Medical Center: 1989
- University of Virginia School of Medicine: 1989','Daniel','Hebert','images/generated_16.png','4273 Alisha Viaduct'),
	 (2000000017,'31c596da-0f50-4f2d-b4bc-13c3354de979',9,1840015982,5,'Eric Miller is a well-known ophthalmologist from Tampa, FL, with over 5 years of experience in the field. He received his medical degree from the University of Florida College of Optometry and has been an active member of the American Academy of Ophthalmology since 2003. Dr. Miller specializes in comprehensive eye care, including but not limited to corneal surgeries, glaucoma management, and retinal detachments.','- University of Florida College of Optometry, Doctor of Optometry
- University of Florida College of Optometry, Residency in Optometric
- University of Pennsylvania, Residency in Ophthalmology
- Pennsylvania College of Optometry, Residency in Corneal
- Florida College of Optometry, Residency in Glaucoma
- American Academy of Ophthalmology','Eric','Miller','images/generated_17.png','1143 Jennifer Wells Apt. 987'),
	 (2000000018,'45bfaa36-60fe-492a-856b-03be973b9ce9',1,1840021672,11,'Dr. Jim Maynard is a board-certified cardiologist based in Tulsa, Oklahoma, with over 11 years of clinical experience in the treatment of various heart conditions.','- University of Oklahoma College of Physicians & Surgeons
- University of Oklahoma
- Medical College, Oklahoma City, OK

Dr. Jim Maynard, MD is a cardiologist based in Tulsa, Oklahoma. He is in-network with numerous hospitals in the area, including INTEGRIS Baptist Health and Saint Francis Hospital of Tulsa. Dr. Maynard has additional experience in cardiac electrophysiology and electrophysiological mapping. He has received a 5-star rating from ZocDoc. Dr. Jim Maynard''s specialties include electrocardiogram (ECG) and echocardiography. Dr. Jim Maynard is affiliated with INTEGRIS Baptist Health and Saint Francis Hospital of Tulsa. He graduated from Oklahoma State College of Medicine. He speaks Spanish.','Jim','Maynard','images/generated_18.png','86263 Denise Hill'),
	 (2000000019,'0fe905c9-1ed7-48f6-97b7-328c42368413',2,1840013660,3,'Melissa Davenport, MD, is a skilled orthopedist who has been practicing medicine for 3 years. Originally from Atlanta, Georgia, she graduated from Emory University School of Medicine where she obtained her medical degree. Throughout her career, she has received multiple certifications from prestigious organizations like the American Board of Orthopedic Surgery, as well as specialized training in complex orthopedic procedures. Dr. Davenport''s areas of expertise include shoulder and knee injuries, joint replacement surgeries, and general orthopedics.','- MD: Emory University School of Medicine
- Residency in Orthopedics: Emory University School of Medicine

- Certifications and Licensure:

-- American Board of Orthopedic Surgery Certification in Orthopedic Surgery
-- American Board of Orthopedic Surgery Certification in Orthopaedic Surgery of the Hand
-- Fellowship in Adult Reconstruction
-- Fellowship in Orthopaedic Surgery
-- Certification in Shoulder and Knee Surgery
-- Certification in Arthroscopy','Melissa','Davenport','images/generated_19.png','017 Moore Vista Suite 129');
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000020,'45bd95e7-885d-4053-aaff-d6d94695eed2',3,1840034016,4,'Dr Jose Woodard is a board-certified neurologist from New York, boasting 4 years of clinical experience.','* M.D. from Harvard University.
* Neurology Residency at Harvard University, where he also served as clerkship Chief.
* Fellowship in Neuromuscular Disorders from Harvard University, where he was involved in research.
* Medical license and board certification in New York

In the current year: 
* Clinical Neuroscience Residency at the University of Toronto
* Fellowship in Electromyography from the University of Toronto.

In the past years:
* Research Fellowship in Neuromuscular Disorders from Harvard University
* Electromyography Fellowship in Electromyography.

He has also been a volunteer at the New York-based Hospital for Special Surgery, the Mount Sinai Hospital in New York, and the University of Toronto Hospital in Toronto.

Dr Woodard''s interests include the neuromuscular disorders, including muscular dystrophy, peripheral nerve injuries, and neuropathy.','Jose','Woodard','images/generated_20.png','94412 Sarah Mission Apt. 149'),
	 (2000000021,'84980d58-5bfb-475f-b83c-346bc7fa4e9d',4,1840015149,1,'Dr. Brianna Gregory, a board-certified Oncologist, is a graduate of the prestigious Columbia University College of Physicians and Surgeons. With over 1 years of experience, Dr. Gregory is highly trained in diagnosing and treating various types of cancer, utilizing cutting-edge medical techniques and the latest in cancer research. As a member of the American Medical Association and a fellow of the American College of Physicians, Dr. Brianna Gregory is dedicated to patient care and ensuring high-quality treatment in a compassionate, patient-centric manner.','- Columbia University College of Physicians and Surgeons | New York, NY
- Residency: University of Medicine and Dentistry - New Jersey, Newark, NJ
Education:
- Doctor of Medicine, Columbia University College of Physicians and Surgeons, New York, NY
- Residency in Internal Medicine

Professional Society:
- American Medical Association
Professional Affiliation:
- American College of Physicians
- New York Medical College
Professional Licenses:
- Board eligible in Internal Medicine','Brianna','Gregory','images/generated_21.png','70224 Darryl Run Suite 548'),
	 (2000000022,'d3937221-fc16-4ddc-ba7f-28d051e2a14f',5,1840018789,5,'Dr. Lisa Yu is a gastroenterologist in Denver, CO, specializing in treating patients with gastrointestinal issues. She brings her unique experience of more than 5 years and has been well-known in the area for her expertise in diagnosing and treating various gastroenterological conditions. With a compassionate approach and a strong dedication to patient care, Dr. Yu works with her patients to develop an individualized, holistic treatment plan that meets their specific needs.','
• Diplomate of Gastroenterology (2012)• American Board of Internal Medicine (2013)• American Board of Gastroenterology and Colon and Rectal Medicine

Awards:
• Denver Business Journal, Best in Health Care Awards 2015 - Top Gastroenterologist in Denver
• American College of Gastroenterology’s (ACG) Patient Education Award, 2014 - “Best Abstract of the Year”
• American College of Gastroenterology’s (ACG) Young Investigator Award, 2015 - “Best Abstract of the Year”
Professional memberships:
• American College of Gastroenterology (ACG)
• American Gastroenterological Association (AGGA)','Lisa','Yu','images/generated_22.png','597 Nicholas Pass'),
	 (2000000023,'334bb504-9b6b-4989-9da6-5da8e0b17980',6,1840000596,4,'Dr. Anna Deleon is a renowned dermatologist with over 4 years of experience, based out of Cleveland, Ohio. She specializes in a range of medical and cosmetic dermatology treatments. Apart from providing treatment services, Dr. Deleon has also been associated with numerous research efforts focusing on the latest dermatological advancements that can improve patient care. She has received prestigious awards and recognitions for her exceptional contributions to the medical field.','• M.D.
• Board-Certified
• Advanced Clinical Research Training
• Residency in Dermatology
• Fellowship in Dermatopathology
• Member of the American Academy of Dermatology
• Fellowship in Dermatopathologists
• Member in Dermatology
• Member in The Society for Pediatric Dermatology
• Member in The American Society for Dermatopathology



Dr. Deleon''s main areas of expertise are:

• Dermatology
• Skin Care
• Cosmetic Dermatology
• Laser and Aesthetic Dermatology

As a board-certified dermatologist with an exceptional amount of years in this medical field, Dr. Anna Deleon has a wealth of knowledge regarding all aspects of skin care, aesthetic dermatology, and medical dermatology. Dr. Deleon is a highly-respected physician, having been featured prominently in the renowned “Best Doctors of America” and “America’s Top Doctors for Diabetes Care”. She believes in providing personalized treatments for each of her patients and is committed to ensuring that each of her patients receives exceptional treatment and care. Her areas of expertise include cosmetic dermatology, dermatopathology, medical dermatology, and laser and aesthetic dermatology.','Anna','Deleon','images/generated_23.png','636 Welch Pine'),
	 (2000000024,'417f2e36-76d1-4e9f-bc0d-84d3e6b642fe',7,1840003971,13,'Dr. Ryan Hess is a renowned pediatric neurologist with over 13 years of experience. She is originally from Detroit, Michigan, and has completed multiple certifications in her specialized field. Throughout her career, Dr. Hess has worked with a wide range of cases, providing comprehensive care to children with neurological and developmental disorders.','- MD, University of Michigan Medical School
- Residency, University of Michigan-Ann Arbor
- Fellowship: Pediatric Neurology, Children''s Hospital of Philadelphia

Current practice:
* Children''s Healthcare Associates
* Pediatric Neurological Institute','Ryan','Hess','images/generated_24.png','86606 Jessica Fields Apt. 799'),
	 (2000000025,'fc64d644-ad92-49b6-ae8c-442a88efc80a',8,1840014486,14,'Dr. Jeffrey Jones is a highly experienced urologist based in Knoxville, Tennessee. With over 14 years of experience in his field, Dr. Jones is renowned for his expertise in treating conditions affecting the urinary tract. He is certified in Urology by the American Board of Urology and has published numerous research papers on various urology-related topics.','- 2008, University of Tennessee Medical School
- 2010, University of Tennessee School of Medicine and Health Sciences
- 2014: American Board of Urology

- 2016: Urology Residency Program at Vanderbilt University
- 2021, University of Tennessee Health Science Center','Jeffrey','Jones','images/generated_25.png','595 Joshua Port'),
	 (2000000026,'97e6f7eb-3725-4d22-8c6d-15bd5a59d2a5',9,1840000673,14,'Dr. Carmen McClure is a board-certified ophthalmologist based in Philadelphia, PA with over 14 years of experience. She specializes in comprehensive eye care, including medical and surgical treatment of a wide range of conditions, such as corneal abrasions, cataracts, and diabetic retinopathy.','- MD, University of Medicine and Dentistry of New Jersey','Carmen','Mcclure','images/generated_26.png','90419 Cherry Plaza Suite 631'),
	 (2000000027,'27d0a13f-9ef9-4eca-ada0-5b3dcf1710f9',1,1840000494,15,'James Martin is a renowned cardiologist hailing from Chicago, Illinois, United States. With more than two decades of experience in the medical field, Dr. James Martin has become a highly respected authority on heart-related matters. He is a graduate of Northwestern University''s prestigious medical program and has received numerous awards, including a prestigious fellowship from the American College of Cardiology. In addition to his impressive credentials, Dr. James Martin is a passionate advocate for heart health and wellness. He has authored numerous articles, presented at national conferences, developed innovative treatment plans, and has earned a reputation as a dedicated medical professional.','- Doctor of Medicine, Northwestern University (1989)
- Residency in Cardiovascular Disease, Northwestern University (1991)
- Fellow in Cardiovascular Medicine, American College of Cardiology (1993)
- Member, American College of Cardiology
- Member, American College of Physicians
- Member, Society for Cardiovascular Computed Imaging
- Certified in Cardiac Electrophysiology
- Licensed in Illinois and Wisconsin 
The following information should not be used as a substitute for medical advice from your doctor. Always consult with your medical professional before starting or changing treatments. 
What are some of the unique treatment plans Dr. James Martin has developed to help his patients achieve better heart health? 
Dr. James Martin has developed a range of unique treatment plans for his patients to achieve better heart health. Some of these include:

1. Lifestyle Changes: Dr. Martin has a strong belief in a healthy lifestyle. He often advises his patients to eat better foods, exercise regularly, quit smoking, and manage stress to help improve their heart health.

2. Holistic Approach: Dr. Martin takes a holistic approach to patient care, and he often uses a number of tools to assess patients'' overall health, such as a lipid profile, blood pressure, and glucose levels.

3. Exercise Plans: Dr. Martin is a big believer in physical activity, and he often recommends tailored exercise plans for each of his patients.

4. Telemedicine: Dr. Martin has embraced the use of telemedicine to help improve the quality of the care he provides to his patients.

5. Clinical Trials: Dr. Martin has been involved in numerous clinical trials, and he is a strong advocate for new research and treatment options for heart disease.

6. New Treatments: Dr. Martin is always searching for innovative treatments for heart conditions, and he often recommends treatments and procedures that are not yet widely available.','James','Martin','images/generated_27.png','704 Jennifer Extensions Suite 115'),
	 (2000000028,'33063f09-1255-420d-b880-9de7032f9ae8',2,1840020491,10,'Dr. Jim Baldwin is an orthopedic surgeon located in Los Angeles, California. He has been in practice for 10 years, certified in his field, and is affiliated with Cedars-Sinai Medical Center.','- Medical Doctorate (MD)','Jim','Baldwin','images/generated_28.png','04792 Emily Garden'),
	 (2000000029,'2de69f2f-c382-4043-8202-de364ee744f2',3,1840030815,7,'Latasha Edwards, M.D., a neurologist with extensive experience, is from Louisville, Kentucky. With over seven years of experience in the field, Dr. Edwards specializes in treating a variety of neurological conditions.','- 2001: Doctor of Medicine, Medical College of Georgia, Augusta,Georgia
- 2003: Neurology Residency, University of Medicine, Louisville, Kentucky
- 2005: Certificate, Clinical Neurosciences, University of Kentucky - College of Medicine, Lexington, Kentucky;
- 2003: Neurology Residency, University of Medicine, Louisville, Kentucky
- 2018: Fellowship - Clinical Neurophysiology, University of Louisville - College of Medicine, Louisville, Kentucky
- 2020: Certification, Neuroradiology, American Board of Radiology
- 2021: Certification, Neuroradiology, American Board of Radiology
- 2022: Certification, Neuroradiology, American Board of Radiology','Latasha','Edwards','images/generated_29.png','1412 Jennifer Dale Suite 713');
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000030,'7779a508-51f0-406f-be36-e151a489f7c3',4,1840019148,9,'Roger Blair, M.D., is a seasoned oncologist based in Bakersfield, California. With more than 9 years of experience, Dr. Blair has built a reputation for his compassionate care, individualized treatment approach, and dedication to patient education and communication. He is board-certified in Internal Medicine and Hematology/Oncology, and has received certifications in Medical Oncology from the American Board of Internal Medicine.','
M.D.
Molecular Medicine/Medical Microbiology

F.A.C.R.

A.S. in Nursing


B.S. in Biology

C.S. in Biology
Number of years spent in residency: 4

Number of years practicing as a doctor: 9

Number of years in medical school: 3

Current hospital/facility affiliation:

San Joaquin Valley Health System

Bakersfield Memorial Hospital

Sierra Surgical Center

Bakersfield Cancer Center

Bakersfield Memorial Medical Center

Kern Med Center','Roger','Blair','images/generated_30.png','913 David Greens Suite 096'),
	 (2000000031,'12eed070-c102-4af5-9dcc-5732349f10c7',5,1840001698,6,'Daniel Robertson, M.D., a gastroenterologist practicing in Richmond, Virginia with over 6 years of experience, specializes in diagnosing and treating conditions affecting the digestive system.','- Bachelor of Medicine and Surgery (M.B.B.S.) at University College Galway

- Fellowship in Gastroenterology at Virginia Commonwealth University','Daniel','Robertson','images/generated_31.png','22994 Andrew Course Suite 166'),
	 (2000000032,'99e63cd6-3ffb-4809-98a0-a9991f0c4a09',6,1840034032,8,'Dr. Kimberly Reed, a Staten Island, New York-based dermatologist with over 8 years of experience in the medical field, specializes in treating various skin conditions. She obtained her medical degree from Rutgers University Medical School and completed a residency in Dermatology at Mount Sinai Hospital. Dr. Reed is certified in Dermatology and is a member of multiple professional organizations.','
- Medical Degree - Rutgers University Medical School

- Residency - Mount Sinai Hospital

Dr. Kimberly Reed specializes in treating various skin conditions.
- Dermatology

Dr. Kimberly Reed is a member of the following organizations:
- New York Dermatologic Society
- American Academy of Dermatology
- American Medical Association
- American Society of Dermatologic Surgery','Kimberly','Reed','images/generated_32.png','87076 Ronald Rapid Suite 743'),
	 (2000000033,'12029cb9-0f1e-4cb7-b424-3f02f20c4d8a',7,1840015988,5,'Dr. Jonathan Holland is a board-certified pediatric physician located in Sarasota, Florida. With over 5 years of experience in treating children, Dr. Holland specializes in a wide range of pediatric conditions and diseases. He received his medical degree from Vanderbilt University and completed his pediatric residency at Vanderbilt Children''s Hospital in Nashville. In addition to providing compassionate care to his patients, Dr. Holland is also an active member of the American Academy of Pediatrics and the Florida Pediatric Medical Association.','- Vanderbilt University
- Vanderbilt Children''s Hospital


Dr. Jonathan Holland is a board-certified pediatric physician located in Sarasota, Florida. With over 5 years of experience in treating children, Dr. Holland specializes in a wide range of pediatric conditions and diseases. He received his medical degree from Vanderbilt University and completed his pediatric residency at Vanderbilt Children''s Hospital in Nashville. In addition to providing compassionate care to his patients, Dr. Holland is also an active member of the American Academy of Pediatrics and the Florida Pediatric Medical Association.
Dr. Holland is a pediatric physician who specializes in treating patients who have a wide range of health issues and concerns related to childhood development. He is a graduate of Vanderbilt University School of Medicine and Vanderbilt Children’s Hospital and completed a residency in pediatrics. Dr. Holland is a member of the American Academy of Pediatrics, Florida Pediatric Medical Association, and other organizations.','Jonathan','Holland','images/generated_33.png','272 Danielle Ways'),
	 (2000000034,'b6214b56-2026-4f26-b003-0e0777725694',8,1840020580,11,'Dr. James Hill is a seasoned urologist with over 11 years of experience. Born and raised in Mission Viejo, California, he completed his undergraduate studies at Stanford University before attending UCLA''s David Geffen School of Medicine, where he obtained his medical degree. Dr. Hill completed a residency in urology at UCLA''s David Geffen School of Medicine and a fellowship in minimally invasive procedures at the prestigious USC Keck School of Medicine. His expertise lies in minimally invasive surgery, urinary tract reconstruction, and the treatment of prostate, bladder, and kidney-related conditions.','- Medical Degree: MD, David Geffen School of Medicine at UCLA
- Residency: UCLA David Geffen School of Medicine
- Fellowship: USC Keck School of Medicine


Dr. James Hill''s main office location (if different than listed):

- (949) 768-9118','James','Hill','images/generated_34.png','6220 John Hill Suite 325'),
	 (2000000035,'c3f40e81-8e91-456b-b987-53e13361e0d9',9,1840034032,15,'Maurice Smith, an ophthalmologist based in Staten Island, New York, has 15 years of experience. He graduated from Columbia University College of Physicians and Surgeons and completed his residency in ophthalmology at NYU-Belmont Hospital. Dr. Smith is board certified by the American Board of Ophthalmology.','- 4 year Undergraduate degree from University of Pennsylvania
- Medical school degree from Columbia University College of Physicians and Surgeons
- Residency in Ophthalmology at NYU-Belmont Hospital

Number of years of experience: 15 years
Number of years of experience in specific specialty: 15','Maurice','Smith','images/generated_35.png','8746 Sarah Garden Apt. 450'),
	 (2000000036,'ed8bce79-6e76-4fb6-8b5a-aec1445771f2',1,1840003289,3,'Glaoria Hoffman, M.D., is a board-certified cardiologist who has been practicing medicine for 3 years. Dr. Hoffman graduated from Brown University in 1996 and completed her medical training at Brown''s Alpert Medical School, where she also served as a clinical instructor. She is a member of the American Medical Association and the American College of Cardiology, and is licensed to practice medicine in Rhode Island.','- Brown University
- Alpert Medical School

Licensing & Board Certifications
- Rhode Island
- American Medical Association
- American College of Cardiology','Gloria','Hoffman','images/generated_36.png','8101 Christopher Crossing'),
	 (2000000037,'fe3eb703-2844-49bd-9315-57b6b9db4020',2,1840001686,1,'Jessica Avila, a board-certified orthopedic surgeon from Wichita, KS, specializes in treating musculoskeletal injuries. With over a decade of experience, Dr. Avila is highly skilled in various surgical procedures to repair and restore mobility to her patients'' lives.','• Bachelor of Science at the University of Kansas School of Medicine- • Doctoral of Medicine - University of Kansas School of Medicine- • Orthopedic Residency - Wichita Family Medicine Residency - Wichita Family Medicine Residency -

Dr. Jessica Avila has a specialty in Orthopedic Surgery. Her board certifications are:

-Board Certified in Orthopedic Surgery-
• Board Certified in Orthopedic Surgery (Fellowship trained)

Dr. Jessica Avila is affiliated with:

- Wichita Family Medicine
- Wichita Family Medicine Residency
- Wichita Regional Health Center
- Wichita Regional Health','Jessica','Avila','images/generated_37.png','144 Parsons Hill'),
	 (2000000038,'3a3b1897-f150-4f2a-8c68-b6a0b2b9bb30',3,1840008535,6,'Randall Richardson, M.D. is a neurologist based in Kansas City, Missouri with 6 years of experience. Dr. Richardson specializes in the diagnosis and treatment of disorders of the nervous system, including epilepsy, headaches, and peripheral nerve disorders.','- University of Missouri-Kansas City-Kansas City, MO (1997)
- University of Oklahoma-Oklahoma City, OK (1999)
- St. Louis University-St. Louis, MO (2003)

(More about Dr. Richardson) -
- EducationRandall Richardson, M.D. received $3M in damages.
- GenderMales
- LocationsKansas City, MO
- SpecialtyNeurology
- +','Randall','Richardson','images/generated_38.png','242 Susan Village'),
	 (2000000039,'6629b0c1-1887-4322-a217-584dee157777',4,1840021117,7,'Shawn Roberts is a highly skilled oncologist based in Seattle, Washington, with 7 years of experience in the field. His expertise covers a range of cancer treatments, from chemotherapy to radiation therapy. Shawn Roberts''s journey began in 2013 when he completed his residency at the prestigious University of Washington School of Medicine. Since then, he has held various oncological positions at several top hospitals across the Pacific Northwest. Shawn Roberts''s passion has always been in oncology, and he strives to provide personalized care to each of his patients.','• Bachelor of Science degree in Biochemistry from the University of Pennsylvania
• Doctor of Medicine degree from the University of Washington School of Medicine
• Master of Science degree in Medical Science from Case Western University
• Post-Doctoral residency in Hematology and Oncology from the National Cancer Institute

Dr. Shawn Roberts is a highly skilled oncologist with over 7 years of experience in the field. He specializes not only in chemotherapy but also radiation therapy, providing personalized care to each patient.','Shawn','Roberts','images/generated_39.png','9580 Anthony Burg Suite 997');
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000040,'6ba08ef9-0ceb-4a6b-b54c-0009075dabc3',5,1840003814,7,'Nathaniel Miller, MD, is a gastroenterologist from Cincinnati, OH. He is board certified in internal medicine, gastroenterology, and has 7 years of experience.','- MD: University of Cincinnati College of Medicine, USA
- PhD: University of Cincinnati College of Medicine, USA
- Residency: University of Cincinnati College of Medicine, USA

Specialty: Gastroenterology
- Internal Medicine
- Gastroenterology
- Hepatology
Education history:
- 2010 - 2020 Doctorate in Medicine (MD)
- 2010 - 2020 Residency in Internal Medicine and Gastroenterology
- 2004 - 2018 Bachelor of Science
- 2001 - 2004 Bachelor of Science
- 2001 - 2003 Master''s Degree (PharmD)
- 2004 - 2006 Ph.D.

Practice information:
- Member, American Gastroenterological Association
- Member, American College of Gastroenterology
- Member, Cincinnati Gastroenterology Society
- Member, Cincinnata Gastroenterology Society
- Member, Society of American Gastroenterological Associates
- Member, American College of Physicians
- Member, American Medical Association
- Member, American Society of Internal Medicine
- Member, Cincinnati Medical Association
- Member, Greater Cincinnati Gastroenterology & Hepatology Society
Clinical information:
- Gastroenterologist (Gastroenterologist) in the Cincinnati area who treats patients with conditions such as abdominal pain, chronic diarrhea, and abdominal pain.
- Gastroenterology procedures include colonoscopy, endoscopy, and upper GI bleed treatment.
- Dr. Miller has a special interest in colonoscopy, upper endoscopy, and biliary endoscopy.
- His practice is located in Milford, OH and he has been with this practice since 2020.
- Dr. Miller can see adults and children who have gastrointestinal conditions.
- His specialty is Gastroenterology.
- Dr. Miller is affiliated with a number of hospitals, including Milford Regional Medical Center.
- He can accept new patients and patients who wish to schedule appointments.
- His office is in Milford, OH.
Dr. Miller''s practice information:
- Address: Milford Regional Medical Center, 2800 Blue Ridge Rd, Milford, OH, 45150, USA
- Accepting new patients: Yes
- Accepting new patients: Yes
- Address: 2600 Mercy Hospital Medical Centre, Cincinnati, OH, 45211, USA
- Accepting new patients: Yes
- Accepting new patients: Yes
- Accepting new patients: Yes','Nathaniel','Miller','images/generated_40.png','1098 Robert Centers'),
	 (2000000041,'a68d67bf-39db-414f-ba0b-c76d6eca0ba0',6,1840013305,3,'Diane Patterson, a dermatologist in Honolulu, Hawaii, is a highly experienced professional with over 3 years of expertise in the field.','- B.S. in Biology at the University of Hawaii
- M.D. - Hawaii Medical School
- Residency in Dermatology - Honolulu
- Member of The Royal Society of Medicine
- Member of The American College of Physicians 

This doctor has a medical degree in the specialty of Dermatology (MD).
This doctor has completed 4 years of residency in the specialty.
This doctor has 4 years of experience.','Diane','Patterson','images/generated_41.png','074 John Cove'),
	 (2000000042,'e5e1330f-9a93-4809-b442-b94485d38951',7,1840015982,3,'Dr. Ryan Brown is a board-certified pediatric cardiologist based in Tampa, Florida. With over 3 years of experience in the medical field, Dr. Brown specializes in the diagnosis and treatment of heart conditions affecting children. He is a highly skilled and compassionate doctor who believes in providing personalized care to his patients.','• Bachelor of Science - University of Florida
• Doctorate of Medicine - University of Florida, College of Pharmacy
• Fellowship in pediatric cardiology - Children’s Hospital of Tampa
• Board Certified in Pediatrics & Cardiology','Ryan','Brown','images/generated_42.png','013 Phillip Wells Apt. 827'),
	 (2000000043,'d4a636b2-f8c9-4cfb-975a-8fddc6336b2a',8,1840007069,4,'Daniel Burke, MD, is a urologist based in Des Moines, Iowa. With over four years of experience, Dr. Burke specializes in the diagnosis, treatment, and management of various urological conditions affecting both men and women.','- MD: University of Iowa Medical School
- Internship: University of Iowa Hospitals and Clinics, Urology
- Residency: University of Iowa Hospitals and Clinics, Urology
- Fellowship: University of Iowa Hospitals and Clinics, Urology

Credentials:

Board Certifications: Urology

Specializations:
- Urology
- Urinary tract infections
- Kidney stone treatment
- Urinary incontinence
- Ureteroscopic procedures
- Urology cancer diagnosis and treatment
- Urology procedures
- Male infertility
- Erectile dysfunction
- Urinary tract infections
- Ureteroscopy
- Urinary tract obstruction
- Bladder cancer diagnosis and treatment
- Overactive bladder treatment
- Urinary incontinence treatment','Daniel','Burke','images/generated_43.png','465 Cheryl Passage'),
	 (2000000044,'8f60ba07-cf49-4d7e-b338-6a5b61aa7500',9,1840020568,9,'Dr. Rachel Rosales, a prominent ophthalmologist, hails from Phoenix, Arizona. With over a decade of experience in her field, she specializes in comprehensive eye care, performing a range of procedures including cataract surgery, corneal transplants, and diabetic retinopathy management.','• B.A. in Biology, University of Arizona
• M.D., University of Arizona College of Medicine
• Residency, Bascom Palmer Eye Institute
• Fellowship, Bascom Palmer Eye Institute
Dr. Rachel Rosales’s practice is located in Scottsdale, Arizona.','Rachel','Rosales','images/generated_44.png','1299 Jason Corners'),
	 (2000000045,'85a85935-2855-409a-9c94-5316dd46b0d9',1,1840018789,12,'Teresa Keith, MD, a cardiologist in Denver, Colorado, has over 12 years of experience in diagnosing and treating cardiovascular diseases.','- Bachelor of Science, University of Denver, 1989
- Doctor of Medicine, University of Denver, 1991
- Internal Medicine Residency, Denver Presbyterian Hospital, 1992
- Fellowship in cardiology, University of Colorado at Denver, 1995
- American Board Certification in Cardiovascular Medicine','Teresa','Keith','images/generated_45.png','05163 Young Loop Suite 766'),
	 (2000000046,'ea614548-24d3-4e56-ae0c-d905784dabf2',2,1840020568,8,'Cody Young is an orthopedic surgeon in Phoenix, Arizona, with 8 years of experience. He/she specializes in shoulder and knee surgeries and holds several certifications including board certification in orthopedic surgery.','- 4 degrees from Arizona State University
- Medical degree
- 6 years residency training in Orthopedics

Cody Young is an experienced orthopedic surgeon in Phoenix, Arizona. He holds 8 years of experience in the field, as well as 4 degrees from Arizona State University and 6 years of residency training in orthopedics. He has several certifications to his name, ensuring that he is a highly qualified professional.','Cody','Young','images/generated_46.png','333 Mitchell Wall Suite 805'),
	 (2000000047,'7c57a606-2699-4f2a-b404-aa7ace9dc8f4',3,1840001592,4,'Christopher Glenn, M.D., is a skilled neurologist located in Baltimore, Maryland. With 4 years of experience, Dr. Glenn specializes in the diagnosis and treatment of various neurological disorders. He is board certified and highly respected in the medical community.','- 2009 - 2010
- University Medicine
- University Medicine
- 2010 - 2016
- Johns Hopkins Medicine
- Johns Hopkins University School Of Medicine
- 2016 - 2022
- University of Maryland, School of Medicine
- United States Medical Licensing Board

Dr. Glenn is a very compassionate, caring and knowledgeable medical doctor. It was a real privilege to have him as a physician.
Dr. Glenn is an excellent doctor. She is very thorough in her care of her patients, and takes the time to answer any questions you might have. She is a very compassionate, caring and knowledgeable medical doctor. It is a real privilege to have her as a physician.','Christopher','Glenn','images/generated_47.png','72701 Johnson Ports'),
	 (2000000048,'0031e4ab-b7f5-4567-8f5c-729b46f393fc',4,1840034016,7,'Yvette Weeks, M.D., F.A.C.O.S., is an Oncologist at New York-based Memorial Sloan Kettering Cancer Centre, with specialization in Head and Neck Cancer. Dr. Weeks is a graduate of Columbia University College of Physicians & Surgeons, where she earned her Medical Degree. She completed her residency in Otolaryngology-Head and Neck Surgery at Mount Sinai Hospital. Dr. Weeks has been in practice for over 7 years and is highly experienced in the diagnosis, management, and treatment of Head and Neck Cancers.','
M.D. - Columbia College of Physicians & Surgeons
Otolaryngology - Mount Sinai Hospital
- Member American Head and Neck Society
- Member American College of Otolaryngology
- Member American Cancer Society
- Fellowship - Head & Neck Oncology
- Member American Society for Clinical Otolaryngology
- Member American Society for Otolaryngology Head and Neck Surgery
- Clinical Faculty - Columbia Medical School
- Member - American Head and Neck Cancer Foundation
','Yvette','Weeks','images/generated_48.png','61861 Michael Viaduct'),
	 (2000000049,'892533ed-d7d0-4bb7-aa49-0b4b80017fc8',5,1840004836,9,'Dr. Courtney Powell, a gastroenterologist from Bridgeport, Connecticut, has 9 years of experience in the field.','- MD, 2004, Mount Sinai School of Medicine
- MPH (Masters in Public Health), 2008, Johns Hopkins University School of Public Health
- Residency, 2009-2013, Mount Sinai School of Medicine
- Fellowship, 2013-2014, Mount Sinai School of Medicine
- American Board Certified in Gastroenterology

Dr. Courtney Powell''s Practice Locations','Courtney','Powell','images/generated_49.png','843 Adams Skyway');
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000050,'66a8657e-4302-48ac-8ef7-7d4a5a3b07b7',6,1840021543,15,'Dr. Bradley Dougherty is a board-certified dermatologist with over 15 years of experience in the field. Having grown up in San Francisco, California, he received his undergraduate degree from UC Berkeley and attended Stanford Medical School. He has been in private practice for over a decade, focusing on treating a variety of skin conditions.','- 1998 - University of California San Francisco, School of Medicine
- 1992 - Stanford Medical School
- MD (Medicine)
Specialties:
- Dermatology

Dr. Bradley Dougherty is a board-certified dermatologist, specializing in the treatment of a variety of skin conditions. He has been practicing since 1992, receiving his medical degree from Stanford Medical School in 1998. He currently serves as an assistant professor at San Francisco General Hospital, where he is responsible for the training of new dermatology residents.

Dr. Dougherty’s clinical interests include medical, surgical, and cosmetic dermatology, as well as the use of lasers and light-based treatments. He is a member of the American Academy of Dermatology, the American Dermatological Association, and the California Dermatologic Society.

Dr. Dougherty has authored numerous articles and book chapters on a variety of skin conditions, including acne, psoriasis, and rosacea. His book, "The Skin of Color in Health and Medicine, 2nd Edition" was published in 2008. In addition, he is a frequent speaker at national and international conferences, where he is sought out for advice on skin conditions and treatments.

Dr. Dougherty’s current practice specializes in treating acne and other skin conditions, as well as the use of lasers and light-based therapies.

Dr. Dougherty is currently accepting new patients. Please call or schedule your appointment online for more information. [Call or click to schedule an appointment](phone number)

In the media:','Bradley','Dougherty','images/generated_50.png','4402 Felicia Parkway Suite 310'),
	 (2000000051,'e13a5ddd-5ec4-4087-8aec-dc42b9d1d5d2',7,1840034016,6,'James Lloyd is a pediatric healthcare professional with 6 years of experience located in New York. He holds relevant certifications and is dedicated to providing quality care to children.','* 2021, 6 years

Dr. Lloyd has 4 degrees in 2 specialties, 3 of which are listed below:

- Pediatrics
- Medical Pediatrics
- Physician Assistant

Specialities (3)
There are 3 specialties this doctor has in our database.
More About Dr. J Lloyd''s Areas of Expertise
- - Family Medicine
More About Dr. J Lloyd''s Practice
The following information about Dr. Lloyd''s practice is protected by medical licensing laws in the country. It may not be used for commercial purposes.
The following information about Dr. Lloyd''s practice may not be available on this site. Please contact Dr. Lloyd directly to find out.','James','Lloyd','images/generated_51.png','23348 Veronica Plain'),
	 (2000000052,'d752a61a-f601-44ea-a59b-f424b0f2370f',8,1840020428,4,'Dr. Joseph Johnson is an accomplished urologist based in Oklahoma City, Oklahoma, with over four years of experience. He received his medical degree from the University of Oklahoma Health Sciences Center and completed a four-year urology residency program at the Veterans Affairs Medical Center in Oklahoma City. Throughout his career, Dr. Johnson has received numerous awards and recognition, including being named a Castle Connolly ''Top Doctor,'' a Patients'' Choice Award, and a Compassionate Doctor. In addition to clinical practice, he is an active member of multiple medical associations.','-MD: University of Oklahoma Health Sciences Center
-Urology Resident: Veterans Affairs Medical Center in Oklahoma City','Joseph','Johnson','images/generated_52.png','3392 Garcia Camp Apt. 919'),
	 (2000000053,'e760e126-7c5c-408f-a782-782db87f2b3e',9,1840019440,8,'Dr. Allison Martinez is a board-certified ophthalmologist based in Dallas, Texas. She has over 8 years of experience in the field and specializes in comprehensive eye care.','- MD, University of Texas Southwestern School of Medicine
- BS, University of Texas
- Residency, Baylor University Medical Center, 2003-2004
- Fellowship, American Academy of Ophthalmology, 2004-2005
- Board Certified Ophthalmologist, American Board of Ophthalmology
- American Medical Association
- Texas Health Care Physicians

Dr. Martinez''s areas of focus are cosmetic, corneal, cataract, and glaucoma. She performs routine eye exams, as well as vision testing and contact lenses fitting.','Allison','Martinez','images/generated_53.png','7027 Bradley Roads Suite 998'),
	 (2000000054,'43cc6f37-d887-436d-8fa6-4c0da53d356b',1,1840007830,1,'Dr. Brian Davidson is a cardiologist based in Minneapolis, Minnesota. With 1 years of experience, Dr. Davidson specializes in diagnosing and treating various heart conditions.','- MD from Harvard University
- Fellowship in Cardiovascular Medicine at the Mayo Clinic
- Residency in Internal Medicine
- Board Certification in Cardiovascular Disease','Brian','Davidson','images/generated_54.png','425 Robert Fields'),
	 (2000000055,'62f3b0fd-7434-469b-b5c7-f11610039d06',2,1840001254,6,'Andrea White, MD, a board-certified orthopedic surgeon located in Pittsburgh, PA, has been practicing for 6 years. Dr. White received her medical degree at Harvard Medical School and completed a fellowship in sports medicine at the University of Pittsburgh. She specializes in sports medicine and provides treatment for conditions affecting the shoulder, knee, and ankle.','- Harvard Medical School (MD)
- UPMC University of Pittsburgh Medical Center

Dr. Andrea White, MD (Mentioned)

Pittsburgh, PA
(UPMC University of Pittsburgh Medical Center)
2017
Sports Medicine, Orthopedic, Sports Medicine
2019
Fellowship
Dr. Andrea White, MD, has been rated on multiple occasions by her peers. These occasions are not necessarily yearly, but the first year she received a peer review rating of four stars in a year, in addition to 4, 5-star ratings later on in her medical career, including ratings of 4 stars in 2 years and 5 stars in 1 year, so she is considered to be in top 10% of doctors in a specialty, according to her peers. Dr. Andrea White, MD has been found to have a very good patient experience as rated by her patients. This may be her most notable accomplishment. She is one of only 5% of physicians in the nation that are rated by their patients.

There are currently no consumer ratings for this doctor.','Andrea','White','images/generated_55.png','26405 Kelly Bridge Apt. 345'),
	 (2000000056,'66c51787-5efa-420a-8a21-0a529eda1c70',3,1840015149,3,'Matthew Williams, MD, a neurologist located in Miami, FL, has been in practice for 3 years. Dr. Williams completed her residency at University of Florida College of Medicine, where she also completed a fellowship in Neurology. She is certified in Neurology by the American Board of Neurology and Psychiatry.','- MD (University of Florida College of Medicine), 2020
- Neurology Fellowship (University of Florida College of Medicine), 2022




There are 4,000,000+ verified patient evaluations in the Healthgrades database. Doctors on Healthgrades are seen by millions of patients.
Dr. Williams has been recognized as one of America’s Top Doctors (2021-2023).
There are thousands of doctors in the USA (not just the top 100 or 1k) that could be a “fit” based on your insurance.
Dr. Williams’ specialties include Neurology. She is also certified by Neurology, which means that she is board-certified in this area.
There are 2,000+ doctors in the Healthgrades database. Doctors on Healthgrades have been found that practice Neurology in the USA. They are a rare breed, but they will provide you excellent care.
Dr. Williams is a Neurologist who is a board-certified Neurologist in Miami, FL. She is a graduate of University of Florida College of Medicine, College in FL (2020) and has 3 areas of expertise, which include Neurology. There are over 100,000+ verified patient evaluations in Healthgrades database. She is one of the doctors at Miami Neurology, Inc. and has been practicing for 3 years. She has a medical degree in medical and a master in medical school. She is in-network with many insurance providers, including Blue Cross Blue Shield, Anthem, and Blue Cross Blue Shield. She is not accepting new patients. The average rating for doctors in the area is 4 stars, so Dr. Williams is a very good doctor. Patients should call her office at (786) 428-3338 to schedule an appointment. She is currently affiliated with Miami Neurology, Inc. Her specialties include Neurology. She is also certified by Neurology. Her average rating from millions of ratings, reviews & comments is 4 stars.','Matthew','Williams','images/generated_56.png','828 Anderson Freeway'),
	 (2000000057,'c2cc7965-16dc-4320-ae9a-d99554d34858',4,1840013660,5,'Daniel Patterson, an oncologist based in Atlanta, Georgia, is a highly experienced medical professional with over 5 years of expertise in cancer treatment. With a background in both conventional and alternative medicine, Dr. Patterson has a comprehensive understanding of oncology and a passion for helping his patients achieve the best possible outcomes.','- Doctor of Medicine, University of Georgia
- Residency in Internal Medicine, Emory University,
- Fellowship, Hematology & Oncology, Emory University,
- Diplomat, American Board of Internal Medicine','Daniel','Patterson','images/generated_57.png','86217 Benjamin Streets Apt. 270'),
	 (2000000058,'6c6d7765-b1e0-4344-9f08-03ae234592de',5,1840030084,2,'Dr. James Long, MD, is a gastroenterologist based in Indianapolis, Indiana, with over 2 years of experience. He graduated from Indiana University School of Medicine in 2014, and has been practicing gastroenterology ever since.','- Indiana University School of Medicine
- University of California, San Francisco, Fellowship

Dr. Long''s Specialties
- Gastroenterology
Dr. Long''s Specialties (by Specialty)
- Gastroenterology
Education & Training
- Indiana University School of Medicine in 2014
Additional information
- Education Level MD (in years) 2
- Residency/Internship Indiana University School of Medicine in 2021
- Fellowship University of California, San Francisco in 2022','James','Long','images/generated_58.png','045 John Ford Suite 597'),
	 (2000000059,'4c56ccc7-bf33-49b6-b4a8-2f08426899d5',6,1840030084,5,'Samantha Dunn, a resident of Indianapolis, Indiana, is a seasoned dermatologist with 5 years of experience in the medical field.','-Bachelors of Science in Biochemistry from Butler University in Indianapolis, IN
-Medical School Degree from the Indiana School of Medicine in Terre Haute, IN
-Residency in Dermatology from the Department of Dermatology at the Indiana University School of Medicine in Indianapolis, IN
-Board Certification in Pediatrics, Dermatology, and Clinical Dermatology. 

Education/Experience/Training
- Butler University - Butler, IN (B.S. Biochemistry)
- Indiana University School of Medicine - Indianapolis, IN (M.D. Medicine)
- Indiana School of Medicine (Dermatology/Dermatopathology)
- Butler University - Indianapolis, IN (Post-Baccalaureate)
- Indiana Department of Health (Post-Professional Dermatology)
- Children’s Hospital Indianapolis (Inpatient Pediatrics/NICU)
- Children’s Hospital Indianapolis (Pediatric Dermatology)
- Indiana University School of Medicine (Dermatopathology)
- Indiana University School of Medicine (Residency)
- American Academy of Dermatology','Samantha','Dunn','images/generated_59.png','34149 Thomas Parks');
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000060,'7ca7a2af-344d-4d96-858a-cf8f88d56a79',7,1840020568,6,'Amber Ellis, a pediatric nurse practitioner in Phoenix, Arizona, has over 6 years of experience in the medical field. She holds a certification in pediatric emergency medicine and is passionate about providing quality care to children.','
1. BSN - Bachelor of Science in Nursing
2. MSN - Master of Science in Nursing
3. PMNP - Pediatric Nurse Practitioner','Amber','Ellis','images/generated_60.png','13093 Jordan Passage'),
	 (2000000061,'ccf7d5b1-1f69-4972-b022-41c7fa2ffb53',8,1840021990,2,'Dr. Leah Parsons is a urologist practicing in San Diego, California. With more than 2 years of experience, Dr. Leah Parsons specializes in male reproductive system and bladder issues.','- MD - University of California, San Diego School of Medicine
- BS - University of Southern California

Dr. Leah Parsons''s main areas of expertise are in male reproductive system and bladder issues.
- Urinary tract infections
- Urinary incontinence
- Sexual dysfunction
- Bladder issues
- Erectile dysfunction','Leah','Parsons','images/generated_61.png','77478 Avila Port Suite 086'),
	 (2000000062,'35a6826f-8861-4aae-a275-26a45ca8529e',9,1840021117,6,'Christopher Thompson, M.D., is a board-certified ophthalmologist based in Seattle, Washington. With over 6 years of experience, Dr. Thompson specializes in comprehensive eye care for patients of all ages.','- Bachelor of Science: Dartmouth College
- Doctor of Medicine: University of Washington School of Medicine
- Internship: Ophthalmology Residency at the University of Washington


- (Optional) Additional Training: Retina Fellowship','Christopher','Thompson','images/generated_62.png','36854 Joseph Row'),
	 (2000000063,'09c24c34-8d13-4a1d-b654-7f531cc6d91f',1,1840003971,8,'Mary Lewis, M.D., a cardiologist practicing out of Detroit, Michigan, has over 8 years of experience in the medical field.','- B.S. - University of Michigan
- M.D. - George Washington University, Washington DC
- Internship and Residency - University of Michigan, Ann Arbor and Wayne State University School of Medicine, Detroit, MI','Mary','Lewis','images/generated_63.png','520 Cooper Meadow Suite 576'),
	 (2000000064,'a16a0292-5cd5-4b5b-962c-c54c25d86cde',2,1840019590,2,'Dr. Joy Rose, an orthopedic surgeon from Austin, Texas, has been practicing for two years. She is highly experienced in the diagnosis and treatment of various bone and muscle-related conditions including fractures, arthritis, and herniated discs. Dr. Rose obtained her medical degree from the University of Texas at Austin and completed her residency at UT Southwestern Health System. She is board-certified in orthopedics and is affiliated with several hospitals in the community.','- Bachelor of Science Degree: University of Texas

- Medical Degree: University of Texas at Austin

- Orthopedic Fellowship: UT Southwestern Health System

A few words to describe this doctor:
Highly Experienced, Compassionate, Dedicated
Dr. Joy Rose specializes in:
- Orthopedic Surgery: Joint Injuries, Arthritis, Sports Injuries, Herniated Disks
- Fractures: Hip, Knee, Ankle, Wrist
- Orthopedic Trauma Surgery
- Non-Surgical Treatment: Pain Management
- Total Joint Replacement
Dr. Joy Rose has received numerous awards throughout her career. These include the Texas SuperPhysician Award, the Texas SuperPhysician Award by the Texas Medical Association, and the Women’s 1000 Top Women Physicians in Texas Award.','Joy','Rose','images/generated_64.png','659 Caitlin Crossing'),
	 (2000000065,'1ee2c606-7671-48ce-b2da-a797a4f6e100',3,1840013660,14,'Nancy Cardenas, MD, is a board-certified neurologist who has been practicing for 14 years in Atlanta, Georgia. She specializes in treating conditions related to the nervous system and has been recognized for her expertise in her field. Dr. Cardenas obtained her medical degree from the University of Texas Health Science Center at San Antonio and has been in practice since 2005.','* = Dr. Cardenas received her medical degree
* = School of graduate studies

Dr. Nancy Cardenas, MD, has been recognized by her peers and is board certified in neurology. She has been affiliated with a range of hospitals and medical centers, including Gwinnett Medical Center in Lawrenceville, Georgia.

Dr. Cardenas received her medical degree in 2005 from the University of Texas Health Science Center at San Antonio. She has been in practice as a neurologist in the Atlanta area for 14 years. Dr. Cardenas is affiliated with a variety of hospitals, including Emory University Hospital in Atlanta, Georgia (the main facility in Atlanta).','Nancy','Cardenas','images/generated_65.png','1960 Russell Points Suite 157'),
	 (2000000066,'81600c03-b00d-4bf3-957e-7ff4dd3a1dea',4,1840008535,14,'Linda Wallace, M.D., a native of Kansas City, Missouri, is a highly experienced oncologist with over 14 years of expertise. A graduate of the renowned University of Kansas School of Medicine, she obtained specialized training in hematology and oncology at renowned institutions, including the National Institutes of Health and Duke University. Certified by the American Medical Association, Dr. Wallace is a member of numerous professional organizations. In her spare time, she enjoys hiking, playing the guitar, and spending time with her family.','- 1999 - Medical Degree - University of Kansas School of Medicine
- 2004 - Hematology-Oncology Residency - Duke University','Linda','Wallace','images/generated_66.png','51057 Hernandez Garden Suite 488'),
	 (2000000067,'2d114040-5ab0-49c5-92ec-42e492f6d394',5,1840000455,15,'Dr. Robert Davis is a gastroenterologist based out of Boston, Massachusetts. He has been practicing for approximately 15 years. Dr. Robert Davis is highly experienced, specializing in gastrointestinal disorders and procedures.','
- Harvard Medical School 
- University of Massachusetts, Worcester
- Tufts Harvard Medical School
- Tufts University School of Medicine
- Harvard-Affiliated Emergency Medicine Fellowship','Robert','Davis','images/generated_67.png','145 Henderson Circles'),
	 (2000000068,'667fbc89-0559-4a88-b8a4-3ed792885dfe',6,1840002928,2,'Megan Adams, a board-certified dermatologist, is a native of Grand Rapids, Michigan. With over two years of experience, she specializes in medical, surgical, and cosmetic dermatology. She is a graduate of the University of Michigan Medical School and completed her residency at the renowned Ann & Robert H. Lurie Children''s Hospital in Chicago. Her expertise and passion for skin care have led her to develop a reputation for her comprehensive approach and personalized treatment plans.','
Board certified dermatologist
University of Michigan


Graduated from
University of Michigan
','Megan','Adams','images/generated_68.png','45323 Ferguson Fall'),
	 (2000000069,'19c61809-23fe-401f-a91d-adf130b6a035',7,1840020925,1,'Dr. Laura Allison is a pediatric specialist in Houston, Texas, with 1 years of experience. She graduated from Harvard Medical School in 2019 and has been practicing children’s healthcare ever since. Dr. Allison is known for her expertise in diagnosing and managing various childhood illnesses and injuries. She is passionate about providing quality care to her patients and believes in the importance of building strong relationships with them. Outside of work, Dr. Allison enjoys cooking and spending time with her family.','- Bachelor of Science in Biochemistry from Dartmouth College
- MD from Harvard Medical School

Dr. Allison''s areas of special expertise:

- Childhood injuries
- Childhood illnesses

The following is the information on the practice location of Dr. Laura Allison, a pediatrician located in Houston, TX. We provide you with the details regarding Dr. Allison''s practice location, the provider''s organization, and other related information.','Laura','Allison','images/generated_69.png','1559 Mitchell Extensions Apt. 882');
INSERT INTO doctors (doctor_id,user_id,specialty_id,location_id,years_of_experience,doctor_summary,education_summary,doctor_first_name,doctor_last_name,image_url,doctor_address) VALUES
	 (2000000070,'b0cc3d67-a654-4140-a945-fdeadc9f708f',8,1840020580,15,'Ann Graves, MD, a urologist in Mission Viejo, California, has over 15 years of experience in the medical field. She graduated in 2001 from Stanford University School of Medicine and has been specializing in urology ever since. Dr. Graves is board-certified by the American Urological Association and a member of the American Medical Association. During her free time, she enjoys hiking, cooking, and spending time with her family.','- Medical degree from Stanford University School of Medicine
- Residency at the U.S. Air Force Academy
- Fellowship in Female Pelvic Medicine and Reconstructive Surgery from the University of Southern California
- Member of the American Urological Association and the American Medical Association

List of Professional Publications:


- Graves, G.A. (2000). "Clinical and laboratory evaluation of the bladder." In Pincus, M. (ed.), Urodynamics, 3nd edition. New York: Raven Press.


- Graves, G.A. (2002). "Management of urinary incontinence in women." In: Dindayee, P., and Dindayee, K., eds. Female Pelvic Medicine and Reconstructive Surgery. London: Taylor & Francis.','Ann','Graves','images/generated_70.png','879 Andrew Lane Apt. 335'),
	 (2000000071,'388aef0d-714f-42e7-ad00-482393312d52',9,1840020925,14,'Dr. George Villa is an experienced ophthalmologist based in Houston, Texas. With over 14 years of clinical experience, he specializes in comprehensive eye care, including the treatment of cataracts, corneal and retinal diseases, glaucoma, and other eye conditions.','· Bachelors: Baylor College of Medicine (1989)

· Masters: University of Texas Medical School Houston TX (1994)

· Internship: Baylor College of Medicine Houston TX (1994-1995)

· Residency in Ophthalmology: Baylor College of Medicine Houston TX (1990-94)


· Fellowship in Medical and Surgical Ophthalmology: The University of Texas Eye Bank (1996-97)

In this article:
- Dr. George Villa
- Ophthalmologist
- Eye Doctor
- Cornea and Retinal Disorders
- Cataract and Corneal Surgery
- Glaucoma
Dr George Villa | Ophthalmologist | Eye Doctor | Cornea and Retinal Disorders | Cataract and Corneal Surgery | Glaucoma
George Villa is an experienced ophthalmologist based in Houston, Texas. With over 14 years of clinical experience, he specializes in comprehensive eye care, including the treatment of cataracts, corneal and retinal disorders, glaucoma, and other eye conditions.
Dr George Villa is an established ophthalmologist, based in Houston, Texas. With over 14 years of clinical experience, he specializes in comprehensive eye care, including the treatment of cataracts, corneal and retinal disorders, glaucoma, and other eye conditions. Dr Villa has attended Baylor College of Medicine and the University of Texas for his education. As a graduate of the Baylor College of Medicine, Dr. George Villa went on to attend the University of Texas, where he obtained his medical degree.
After graduating, Dr. Villa went on and completed a fellowship in medical and surgical ophthalmology at the University of Texas Eye Bank. With years of clinical experience, Dr. George Villa has been a member of the American Society of Ophthalmology, which is a national medical organization, as well as the American Ophthalmologist Society. He is also a member of the Texas Medical Association and the Texas Academy of Ophthalmology.
Dr. Villa’s expertise in ophthalmology allows him to treat various eye conditions that include glaucoma, corneal and retinal disorders, and cataracts. The treatment that Dr. Villa recommends for cataracts is a procedure called a corneal transplant. This is because this is the only permanent cure for cataracts and it has a very high success rating.
The corneal transplant procedure, which is performed by a specialist in ophthalmology, is a simple and safe surgery. The surgery is performed in the ophthalmologists’ own clinic, so that there is no need for a hospital stay. The surgery usually takes about 30 minutes and can be completed in just a few hours.
The most common corneal transplant in this country is the Descemet’s stripping endothelial keratoplasty. In the Descemet’s stripping endothelial keratoplasty, the Descemet’s membrane is removed from the back of the eye. This is done in conjunction with the corneal transplant. The surgeon removes the membrane that separates the Descemet’s membrane from the cornea. This is done to prevent the development of any complications such as infections.
The success of this corneal transplant procedure is a result of the fact that the procedure has a high success rate. Dr. George Villa has been a member of several medical and surgical ophthalmology organizations, including the American Society of Ophthalmology, the American Ophthalmology Society','George','Villa','images/generated_71.png','57132 Bobby Groves Apt. 209'),
	 (2000000072,'09225dec-34b6-402b-a712-8ec26b974317',1,1840034249,13,'Dr. Lisa Dawson is a highly experienced cardiologist from Dayton, Ohio, with over 13 years of experience in the field. She is knowledgeable in various areas of cardiology, including diagnosis, risk management, and treatment of heart-related conditions. Dawson is board-certified in cardiovascular disease and clinical cardiac electrophysiology, and has received specialized training in echocardiography, magnetic resonance imaging, and catheter-based procedures. Throughout her career, she has held multiple positions in healthcare, including medical student, resident, and attending physician, giving her a well-rounded understanding of the medical field and the needs of her patients.','- B.S. in Biochemistry, University of Dayton
- MD, University of Kentucky
- Fellowship in Cardiology, Wright State University

Write a short bullet point list of the types of medical procedures this doctor performs:

- Echocardiography
- Cardiac catheterization
- Electrophysiological testing
- Transcatheter heart valve replacement
- Coronary angioplasty/stenting
- Atrial fibrillation/proarrhythmias treatment

Dawson, Dr. Lisa A.
(937) 423-5000
Dayton, OH
Dawson, M. A.
(937) 423-5000
Dayton, OH 45410
Dawson, M. A.
(937) 423-5000
Dayton, OH 45410','Lisa','Dawson','images/generated_72.png','8926 Dickerson Mount'),
	 (2000000073,'ff70b072-6b53-4a53-8bd6-ccd611875b80',2,1840000386,2,'Dr. Rachel Johns is an orthopedic surgeon based in Buffalo, New York, with 2 years of experience. She received her medical degree from University at Buffalo, New York, and has been practicing for 2 years since 2012. Dr. Johns specializes in joint replacement procedures, shoulder replacement, hip replacement, and sports medicine.','- University at Buffalo, New York
- Doctor of Medicine, 2007 
- Residency in Orthopedic Surgery, 2012
- Fellowship in Sports Medicine, 2013

Dr. Johns’ Specialties:
- Orthopedics & Sports
- Orthopedic Medicine
- Joint Replacement Procedures
- Shoulder Replacement
- Hip Replacement
- Sports Medicine','Rachel','Johns','images/generated_73.png','52047 Schroeder Glens'),
	 (2000000074,'2bb250b4-11fc-43d4-a63a-05a653a84d67',3,1840013660,11,'Juan Johnson, a neurologist practicing in Atlanta, Georgia, has over 11 years of experience. He is board certified in neurology and has specialized in treating neurological disorders. He attended Emory University School of Medicine for his medical education, completing a residency program in neurology. Dr. Johnson believes in utilizing a patient-centered approach to care, emphasizing the importance of education and communication in understanding and managing neurological conditions.','- Emory University School of Medicine','Juan','Johnson','images/generated_74.png','90867 Steven Ramp'),
	 (2000000075,'248afb54-e8a1-4cfc-b3cb-afd0dd2f067c',4,1840006507,12,'Dr. Sheila Yoder is a board-certified Medical Oncologist based out of Birmingham, Alabama. With more than a decade of clinical experience, Dr. Yoder is highly skilled in managing various cancer treatments, including chemotherapy, radiation therapy, and surgery. Committed to the compassionate care of each patient, she has been named one of the top Oncologists in the region.','- Medical School: Duke University School of Medicine
- Residency: Duke University Medical Center
- Fellowship: Duke University Medical Center
- Board Certification: Oncological Endocrinologist
- Member of: American Medical Association, American College of Physicians, Association of Women''s Health, Endocrine Society
- Former Chairman, Cancer Registry Committee, American Cancer Society; Member, National Cancer Data Registries Advisory Committee
- Member, Society of Clinical Pharmacology','Sheila','Yoder','images/generated_75.png','88470 Perry Ways Suite 942'),
	 (2000000076,'975aa70c-7b95-41ac-8cab-19bc645bdf2d',5,1840020568,5,'Dr. Jimmy McGee is a gastroenterologist based in Phoenix, Arizona with 5 years of experience. He specializes in the diagnosis and treatment of gastrointestinal issues, including acid reflux, abdominal pain, and Crohn''s disease. His educational background includes a Doctor of Medicine degree from the University of Arizona and a residency in gastroenterology at the Mayo Clinic in Scottsdale, Arizona. He is board certified in gastroenterology by the American Board of Internal Medicine.','**Dr. Jimmy McGee is a Doctor of Medicine and a board-certified gastroenterologist.**
**He has over 5 years of experience.**
**He graduated from the University of Arizona and a residency in gastroenterology at the Mayo Clinic in Scottsdale, Arizona.**


List of conditions that Dr. Jimmy McGee has treated:

- Acid reflux
- Abdominal pain
- Crohn''s disease

List of medications Dr. Jimmy McGee has prescribed:

- Anti-reflux medications
- Proton pump inhibitors
- Bismuth subsalicylate
- Vitamin and mineral supplements
- Anti-inflammatory medication
- Antibiotics

Specialty: Gastroenterology
Licensing: 1 year(s) | Last Updated: 2023-03-19Xperienced gastroenterologist with expertise in various medical conditions.
Dr. McGee has over a decade in the medical profession. He is board-certified in gastroenterology and has a doctorate degree from the University of Arizona. He is experienced in treating gastroesophageal reflux, abdominal pain, and Crohn''s disease and has received numerous awards and accolades.','Jimmy','Mcgee','images/generated_76.png','82815 Andrew Ville'),
	 (2000000077,'7f507e87-6801-4053-aa61-b462c4a15c6a',6,1840034016,14,'Derrick West, a renowned dermatologist from New York, NY, has been in the industry for over 14 years. His expertise lies in providing personalized, evidence-based dermatological care to a wide spectrum of patients, from adolescents to the elderly. Throughout his career, Dr. West has obtained multiple certifications and accolades, demonstrating his commitment to excellence in the field.','- Medical Degree: Doctorate in Medicine from Harvard Medical School
- Residency in Dermatology: Harvard Medical School/Massachusetts General Hospital
- Fellowship: Harvard Dermatology/Cedars-Sinai Medical Center','Derrick','West','images/generated_77.png','37949 Larry Street'),
	 (2000000078,'45e12024-c593-4fd2-99f3-3bcdb5a3468c',7,1840020428,12,'Todd Brown is a highly experienced pediatric nurse practitioner with over 12 years of experience in Oklahoma City, Oklahoma. Todd has obtained certifications in advanced wound care and is a member of multiple professional organizations.','* Bachelor of Science in Nursing, University of Oklahoma
* Master of Science in Nursing, University of Central Oklahoma
* Post-Master''s Certificate in Family Health, Oklahoma State University Health Sciences Center','Todd','Brown','images/generated_78.png','427 Clark Ridges'),
	 (2000000079,'2c1eebf0-cf14-422d-9ab8-f65ba1d73177',8,1840003046,3,'Dr. Mary Merritt is a board-certified urologist based in Milwaukee, Wisconsin. With over three years of experience in the field, she specializes in the treatment of urological disorders and conditions. She has received numerous accolades and awards throughout her career, including being a top-50 urologist in the US multiple times. As a dedicated medical professional, she strives to provide the highest quality of care to her patients while instilling a compassionate and understanding approach.','- BS in Health Sciences, University of Milwaukee
- MD, Creighton University School of Medicine
- Urology Residency, University of Wisconsin-Madison

Specialty:
- Female Infertility
- Sexual Medicine
- Prostate Cancer
Credentials
- Medical Degree','Mary','Merritt','images/generated_79.png','174 Joshua Light');
