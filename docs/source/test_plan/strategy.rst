Strategy
========

Objectives
++++++++++

Test objectives are meant to verify that the Pangeo application meets design specifications.

Testing will include the execution of automated tests, test scripts, and performance tests.  Issues will be discussed with the team during the weekly stand-ups and prioritized as High, Medium, or Low severity.  High and Medium severity defects must be retested as per the acceptance criteria.  Low severity defects will be deferred to a subsequent sprint as discussed.
Criteria for the cessation of testing:

- Production ready  software (as per the Software Requirements Specification (SRS))

- Automated tests and test scripts suitable for reuse as functional and user-acceptance testing.

Assumptions
+++++++++++

*General*

- Evironment downtime will adversely impact test schedules.

- Test environment will exactly duplicate the production environment.

- Issue reporting includes complete reproduction details (as per Issue Reporting Template).

- Issues are tracked using the ZenHub Issue Tracking System only.

- The team will review, verify, and sign-off on all test cases prior to the start of testing.

- Issues reported fixed after Certification N will include regression tests which will be added to the test plan for Certification N+1.


*Key Assumptions*

- Certifications are defined as follows:

+--------------------+--------------------------------------------------------------------------------------+
|   Certification    | Description                                                                          |
+====================+======================================================================================+
| Certification 1a   | Pulling data from a file and rendering an object that resembles the data.            |
+--------------------+--------------------------------------------------------------------------------------+
| Certification 1b   | Pulling data from a database instead of a file system.                               |
+--------------------+--------------------------------------------------------------------------------------+
| Certification 2a   | Pulling from a cache when necessary and pulling from the database ahead of the time  |
+--------------------+--------------------------------------------------------------------------------------+
| Certification 2b   | why                                                                                  |
+--------------------+--------------------------------------------------------------------------------------+

- A release cannot go into production with any severity 1 (Critical), 2 (High) or 3 (Medium) defects.

- Functional testing requires production-like data.

- Alpha tests will be performed by identified alpha testers.



Test Principles
+++++++++++++++

- Quality and business objectives are the focus of this testing.

- Testing cycles will be controlled by agreed on start and end criteria.

- Testing is divided into two test cycles (Certifications).

- Certification environment must exactly duplicate the production environment to support meaningful test results.



Testing Scope
+++++++++++++

Unit
----

Who: Development Team, the expectation is that the team implement best practices in terms of Test Driven Design (TDD) during initial development.

When: During product development.

Why: Primarily for the purpose of identifying bugs at the unit level as early as possible.

Scope: User stories, separate modular functions, scripts.

How: All major software components will be developed using Unity’s built-in Test Runner which utilizes the NUnit framework or the Python module, unittest.


Back-end
--------

Who: Development Team

When: During database setup.

Why: To avoid complications like deadlock, data corruption, and data loss.

Scope: Validating schema, database tables, columns, keys and indexing, stored procedures, triggers, database server validations, validating data duplication.

How: Tests will be developed as SQL queries.


Integration
-----------

Who: Development Team

When: Combining individual units.

Why: To expose defects in the interfaces and interactions between integrated components or systems.

Scope: Interaction between the database and the application, simulating the key interactions of a user using the application.

How: Tests will be developed using the Integration Test Framework, which is part of the Unity Test Tools package.


System and Functional
---------------------

Who: QA

When: Prior to Exploratory Testing

Why: Thorough testing of all application functions.

Scope: [Note: Functional test plans are detailed lists of all the features and capabilities expected from the application.  The Test Plan should be a fairly high-level document so the details and scope of the functional tests will typically appear in a spreadsheet, appendix, or other content management document].

How: Tests are performed using agreed on scripts, automated processes, and input decks.


*Acceptance Criteria:*

    1. Functional spec and use case documents available before Test Design phase.

    2. Test environment available, configured, verified, and ready to use.

    3. Unit test results compiled and shared with the Development Team to avoid duplication of effort.

    4. Test cases reviewed, validated, and signed off on by the Development Team.


Exploratory and Alpha
---------------------
Who: Alpha testers

When: After Certification 2.

Why: Primarily to familiarize the alpha testers with the features and behavior of the software to set expectations for new features and identify any hiccups.

Scope: Production level product.

How: Testers are encouraged to try the interface without scripts or documentation.


QA Test
-------


Soak and Performance
--------------------

Who: Development Team

When: Any new system update.

Why: To ensure the application does not have any memory leaks and performs to the agreed-upon performance specification.

Scope: Memory management, algorithms, and loading time.

How: Unity Profiler and Unity Performance Testing Extension to internally monitor performance and optimizations of key systems. 


Stress
------

Who: Development Team

When: Before product release.

Why: To determine the acceptable user limitations.

Scope: Algorithms and loading time.

How: Unity Profiler and Unity Performance Testing Extension to internally monitor performance and optimizations of key systems.


Alpha Testing
-------------

Who: Identified alpha testers

When:  Before release.

Why:  Software is tested outside a controlled test environment by users who are unfamiliar with the product or features. These tests validate the requirements gathering and test design phases of the production cycle.

How:  Business users/customers will frequently attempt to use the software in unanticipated ways. This can lead to new requirement generation (user expects functionality that was not developed), modification of the user interface (user follows a use path that causes failures or other unwanted behavior in the software), or clarification to user guides, help screens, on screen guidance.

Deliverables: UAT Test Cases written by Development Team and reviewed and signed off on by Development Team and Project Manager.