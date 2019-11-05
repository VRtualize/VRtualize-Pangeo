Repository Management
=====================

The repository is owned by Michael Theesen and is stored in the GitHub server under the name VRtualize-Pangeo. All members of the team have Developer permissions.


Branch Structure
++++++++++++++++

.. todo::
    Add branch structure diagram. **Chezka is currently working on this**

Feature Branches
----------------

Each issue with have a feature branch for the purpose of developement specifically for that issue named using the following standard:

*developer-name*/*issue#-name-of-issue*

Unit tests will be developed, run, and passed in this branch before a code review can be scheduled.

Code reviews will be done within 4 days of an issue entrance into the QA/Review swimlane of issue tracker.


Integration Branch
------------------

`Cost of Entry`: Passes unit tests and code review.

The relationships between units of code is tested within this branch.


Testing Branch
--------------

`Cost of Entry`: Passes integration tests.

The application as a whole is tested within this branch.


Master
------

`Cost of Entry`: Passes all tests.

Product-ready code is found in this branch.
