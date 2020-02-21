Test Management
===============

Test Management is accomplished using a variety of tools. All testing artifacts, documents, issues, test cases, and results are stored, verified, and updated using the ZenHub Issue Tracking System.

- Developer technical communications including technical presentations, meeting minutes, and communications with the sponsor will be placed into Google Drive.
- During test design, tests will be placed under revision control to ensure logging of change history.
- Development Team members have access to individual test results and issue documentation.



Test Design
+++++++++++

- Team member reviews requirement under User Story and prepares tests which verifies requirement is met.
- Test cases are mapped to User Stories and Requirements as part of requirement tracking.
- Test cases are reviewed by Development Team to ensure the test faithfully validates existing requirement(s).
- Development Team will use prototypes, user stories, use cases, and functional specifications to write step by step test cases.
- QA Team will maintain test and issue tracking information to be shared periodically with Project Manager. Change requests or requirement clarifications can cause test cases to be modified, added, or removed as necessary.
- Change requests must be reviewed and accepted by Development Team.



Executing the Test Plan
+++++++++++++++++++++++

- QA Team performs testing tasks as per test plan.
- Defects are logged using the ZenHub Issue Tracking System. Developer to report the defect is responsible for initial assignment of severity but final determination made by the entire Development Team.
- Product issues related to defects that prevent execution on test plan are reported, logged, and escalated as necessary to the Development Team. e.g. defects causing product features to be unavailable for testing.
- Any defects marked as fixed in a previous test cycle are verified as fixed using test scripts and regression tests.



Risks and Risk Response
+++++++++++++++++++++++

.. table::
    :widths: 20 10 30 30

    +-----------------------+------------+----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    |          Risk         | Likelihood |                                                                                                                       Effect                                                                                                                       |                                                                                                             Response                                                                                                             |
    +=======================+============+====================================================================================================================================================================================================================================================+==================================================================================================================================================================================================================================+
    | Resource availability |     Low    | Unable to receive information we need to render a specific location. Users will no longer be able to use the application that requires information that we don’t currently have for the next 24 hours.                                             | Ensure proper storage of repeatedly used testing resources, and understanding capabilities of API token reuse to minimize new API calls.                                                                                         |
    +-----------------------+------------+----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
    | Unforeseen Delays     |     Low    | Impossible to tell the impact due to not knowing the issue. It could potentially create a time slip and we will not be able to deliver the product that we have agreed to produce for L3Harris, which may impact future projects with the company. | Work will be scheduled the highest priority in the next Sprint. During Christmas break there will be some time to make up for time slips that may occur to get the project back on schedule for development for Certification 3. |
    +-----------------------+------------+----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+