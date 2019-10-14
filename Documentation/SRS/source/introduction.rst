Introduction
======================================

Purpose
------------------------
The purpose of this document is to present a detailed description of the Pangeo. It will explain the purpose and features of the system, the interfaces of the system, what the system will do, and the constraints under which it must operate. This document is intended for both the stakeholders and the developers of the system.

Scope
------------------------
The SDSMT VRtualize Team is working together with L3Harris on project Pangeo. Pangeo is a research project aiming to render the real world with 3D technology in a dynamically generated virtual reality environment. There are three main goals that contribute to this overall project: imagery retrieval, image caching, and virtual rendering.

Audience
------------------------
Pangeo is designed to be an internal research tool for the company L3Harris. Despite that, the aim of the project is to be usable by anyone with access to virtual reality equipment and an interest in touring geographical points of interest from the comfort of their home.

Glossary
------------------------
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Term                                | Definition                                                                                                                                                    |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Active Article                      | The document that is tracked by the system; it is a narrative that is planned to be posted to the public website.                                             |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Author                              | Person submitting an article to be reviewed. In case of multiple authors, this term refers to the principal author, with whom all communication is made.      |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Database                            | Collection of all the information monitored by this system.                                                                                                   |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Editor                              | Person who receives articles, sends articles for review, and makes final judgments for publications.                                                          |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Field                               | A cell within a form.                                                                                                                                         |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Historical Society Database         | The existing membership database (also HS database).                                                                                                          |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Member                              | A member of the Historical Society listed in the HS database.                                                                                                 |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Reader                              | Anyone visiting the site to read articles.                                                                                                                    |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Review                              | A written recommendation about the appropriateness of an article for publication; may include suggestions for improvement.                                    |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Reviewer                            | A person that examines an article and has the ability to recommend approval of the article for publication or to request that changes be made in the article. |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Software Requirements Specification | A document that completely describes all of the functions of a proposed system and the constraints under which it must operate. For example, this document.   |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Stakeholder                         | Any person with an interest in the project who is not a developer.                                                                                            |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+
| User                                | Reviewer or Author.                                                                                                                                           |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------+


References
------------------------
IEEE. *IEEE Std 830-1998 IEEE Recommended Practice for Software Requirements Specifications.* IEEE Computer Society, 1998.

Overview
------------------------
The next chapter, the Overall Description section, of this document gives an overview of the functionality of the product. It describes the informal requirements and is used to establish a context for the technical requirements specification in the next chapter.

The third chapter, Requirements Specification section, of this document is written primarily for the developers and describes in technical terms the details of the functionality of the product.

Both sections of the document describe the same software product in its entirety, but are intended for different audiences and thus use different language.
