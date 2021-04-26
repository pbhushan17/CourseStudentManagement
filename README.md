# CourseStudentManagement
C# MVC Application for managing student and courses.

Task Completed in this Applications are :

Task 1  
We would like the candidate to build a student and course management and registration system using MVC. The system should allow users to add, update and delete students and course. The system should also display a paged list of students in the system and be searchable. Also, the system should allow user to register course for each student. 
The data should be read from a database and the students should have the following characteristics: First Name,  Surname, Gender, DOB, Address 1, Address 2 and Address 3. 
The courses should have the following characteristics: Course Code, Course Name, Teacher Name, Start Date and  End Date  
  
Please note: One student can choose max 5 different courses and there are limited spaces in  each course. One student cannot register same course twice, also, one course cannot have  same student twice. 
Task 2  
Create the below SQL reports for above project. Those reports must be built with SQL Views or SQL Stored  Procedures. 
1. Show registered courses for each student 
2. Show student list for each courses 
3. Show how many students didn’t register max course 
4. Show how many courses still have the space

Task Done are :
   .NET MVC Task

To Build student Course Management system and registration using MVC

Model – View -Controller pattern is used in the system, where model deals with entity and in turn with database, as we have used database first approach, so first created the Database models and then integrated it into application.

MVC architecture is used as it is loosely coupled layers, the entity or model is not directly related with entity. yes, the same properties are mapped in view. However, if we need to change anything in Database and reflect in model, then we just need to update the edmx file which will then update all the reference changes and will not have to work much in resolving mismatch issues. 
And Controller is the place where all the validation and handling of the logic is done and changes are saved to the database layer.

Then with the help of model, view was created and binded the properties to it.

Controller has all the action methods such as add, edit, delete.

Validation of maximum 5 course per person is handled in controller on create and edit method.

Validation of per student one course and per course one student is handled in the database by creating the unique key with combination of course_id , student_id.

Similarly, validation of course available seats are gone then warning message is shown to the user that no more seats are available for course.

Exception handling is also done for action methods.

Datetime format is handled with display format to show only date part of the date in yyyy-mm-dd format. Also, placeholder is given on date to understand the format while entering the date in correct format. Or else invalid date format exception would be shown.
