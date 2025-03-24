# Student
## Enrol
Enrolling in a subject works and leads to the subject appearing in the enrolled subjects list.
## Drop
Same as above for dropping. However, currently, the student is not removed from the "Enrolled Students" list in the teacher's view.

# Teacher
## Create
Creating a subject works and leads to the subject appearing for the teacher's "My Subjects" list and students' "Available Subjects" list.
## Delete
Same as above for deleting.

# System
## Login
Logging in works correctly and displays the appropriate view based on type. Currently, the log-in validation is bound to the saved role / type from the JSON and not to the radio buttons; these are there for registration selection.
## Data Persistence
Seems to be working fine. When closing, logging out, logging back in, restarting the app, the information is peristent with the last time it was opened and the JSON reflects that.