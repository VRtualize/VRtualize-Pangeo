Test Management
===============

Test Management is accomplished using a variety of tools. All testing artifacts, documents, issues, test cases, and results are stored, verified, and updated using the ZenHub Issue Tracking System.

- Developer technical communications including technical presentations, meeting minutes, and communications with the sponsor will be placed into Google Drive..
- Prior to Sprint 2, Dev Team, Product Team, and QA Team have Read/Write access to test scripts, expected results, and automated tests.  After start of Sprint 2, test scripts, expected results, and automated tests can only be modified by Development Team with sign off by Project Manager.
- During test design, tests will be placed under revision control to ensure logging of change history.
- Development Team members have access to individual test results and issue documentation.

.. todo::
    The second bullet it untrue, should we edit it to change when it happens, or get rid of it completely. Currently we're doing a write the expectations as we go kind of deal.



Test Design
+++++++++++

- Team member reviews requirement under test and prepares a test which verifies requirement is met.
- Test cases are mapped to User Stories and Requirements as part of the requirement tracking.
- Test cases are reviewed by Development Team and Project Manager to ensure the test faithfully validates existing requirement(s).
- As required, tests and test cases are reworked, Project Manager signs off, and test is entered into test database.
- Development Team will use prototype, user stories, use cases, and functional specifications to write step by step test cases.
- QA Team to maintain test and issue tracking information to be shared periodically with Project Manager. Change requests or requirement clarifications can cause test cases to be modified, added, or removed as necessary.
- Change requests must be reviewed and accepted by Development Team and Project Manager.



Executing the Test Plan
+++++++++++++++++++++++

- QA Team performs testing tasks as per test plan. Defects are logged using the ZenHub Issue Tracking System. Developer to report the issue is responsible for initial assignment of severity but final determination made by the entire Development Team.
- Product issues related to defects that prevent execution on test plan are reported, logged, and escalated as necessary to the Development Team. e.g. defects causing product features to be unavailable for testing.
- Any defects marked as fixed in a previous test cycle are verified as fixed using test scripts and regression tests.