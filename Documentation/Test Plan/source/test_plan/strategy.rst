.. _strategy:

Strategy
========

Objectives
++++++++++

Test objectives are meant to verify that the Pangeo application meets design specifications.

Testing will include the execution of automated tests, test scripts, and performance tests.
Criteria for the cessation of testing:

- Production ready  software (as per the Software Requirements Specification (SRS))
- Automated tests and test scripts suitable for reuse as functional and user-acceptance testing.




Assumptions
+++++++++++

**General**

- Environment downtime will adversely impact test schedules.
- Test environment will exactly duplicate the production environment.
- Issue reporting includes complete reproduction details (as per Issue Reporting Template).
- Issues are tracked using the ZenHub Issue Tracking System only.
- Issues reported fixed after Certification N will include regression tests which will be added to the test plan for Certification N+1.


**Key Assumptions**

- Certifications are defined as follows:

+--------------------+--------------------------------------------------------------------------------------+
|   Certification    | Description                                                                          |
+====================+======================================================================================+
| Certification 1    | Image Retrieval                                                                      |
|                    | Retrieval of elevation data and satellite imagery from external sources.             |
+--------------------+--------------------------------------------------------------------------------------+
| Certification 2    | Terrain Rendering                                                                    |
|                    | Generation of a 3D virtual model of terrain.                                         |
+--------------------+--------------------------------------------------------------------------------------+
| Certification 3    | User Interface                                                                       |
|                    | Display and navigation of the user through the application features.                 |
+--------------------+--------------------------------------------------------------------------------------+
| Certification 4    | Image Caching                                                                        |
|                    | Chunk prediction and the management of cached data.                                  |
+--------------------+--------------------------------------------------------------------------------------+
| Certification 5    | User Experience                                                                      |
|                    | User movement interactions with the terrain model.                                   |
+--------------------+--------------------------------------------------------------------------------------+

- A release cannot go into production with any severity 1 (Critical), 2 (High) or 3 (Medium) defects.
- Functional testing requires production-like data.
- Alpha tests will be performed by identified alpha testers.



Testing Scope
+++++++++++++

Unit
----

**Who:** Development Team

**When:** During product development

**Why:** Primarily for the purpose of identifying bugs at the unit level as early as possible

**Scope:** User stories, separate modular functions, scripts

**How:** All major software components will be developed using Unity’s built-in Test Runner which utilizes the NUnit framework or the Python module, unittest


Back-end
--------

**Who:** Development Team

**When:** During database setup

**Why:** To avoid complications like deadlock, data corruption, and data loss

**Scope:** Database

**How:** Tests will be developed as SQL queries


Code Reviews
------------

**Who:** Development Team

**When:** Within 4 days of an issue going into Review/QA pipeline

**Why:** To ensure that the code upholds coding standards

**Scope:** All units of code

**How:** Team members working on the same Certification will be required to participate and approve code reviews with the author of the code. Results in one of three results:

- Pass: An ok to push to merge with next branch
- Revision/Pass: Can be pushed after some changes
- Fail: Too many problems, will require another code review


QA Testing
----------

**Who:** QA Team

**When:** After units of code passes code reviews

**Why:** To ensure units of code pass user acceptance criteria

**Scope:** All units of code

**How:** Using the testing software used to create the unit, back-end, and user acceptance tests


Integration
-----------

**Who:** Development Team

**When:** Combining individual units of code

**Why:** To expose defects in the interfaces and interactions between integrated components or systems

**Scope:** Interaction between the database and the application, simulating the key interactions of a user using the application

**How:** Tests will be developed using the Integration Test Framework, which is part of the Unity Test Tools package


Regression
----------

**Who:** Development Team

**When:** When a defect is discovered

**Why:** To make ensure the reproduction of the failing case and recognize when that failure is fixed

**Scope:** Specific to the defect

**How:** Depending on the type of defect found, the regression test could be a User Acceptance Test or use the same format as a unit test


System and Functional
---------------------

**Who:** QA Team

**When:** Prior to Exploratory Testing

**Why:** Thorough testing of all application functions

**Scope:** All required features of the application as described in the SRS

**How:** Tests are performed using scripts, automated processes, and input decks.


Soak and Performance
--------------------

**Who:** Development Team

**When:** Any new system update

**Why:** To ensure the application does not have any memory leaks and performs to the agreed-upon performance specification

**Scope:** Memory management, algorithms, and loading time

**How:** Unity Profiler and Unity Performance Testing Extension to internally monitor performance and optimizations of key systems.


Stress
------

**Who:** Development Team

**When:** Before product release

**Why:** To determine the acceptable user limitations

**Scope:** Algorithms and loading time

**How:** Unity Profiler and Unity Performance Testing Extension to internally monitor performance and optimizations of key systems


Exploratory and Alpha
---------------------

**Who:** Alpha testers

**When:** After functional tests

**Why:** Primarily to familiarize the alpha testers with the features and behavior of the software to set expectations for new features and identify any hiccups

**Scope:** Production level product

**How:** Testers are encouraged to try the interface without scripts or documentation

**Deliverables:** UAT Test Cases written by Development Team and reviewed and signed off on by Development Team and Project Manager