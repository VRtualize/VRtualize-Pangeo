Introduction
======================================

Purpose
------------------------
The purpose of this document is to present a detailed description of Project Pangeo. It will explain the purpose and features of the system, the interfaces of the system, what the system will do, and the constraints under which it must operate. This document is intended for both the stakeholders and the developers of the system.

Scope
------------------------

The SDSMT VRtualize Team is working together with L3Harris on Project Pangeo. Pangeo is a platform aiming to render the real world with 3D technology in a dynamically generated virtual reality environment. This platform will cover two major needs of L3-Harris. The first is a method to quickly and easily survey an area in the real world. The second is a cost effective method to train their computer vision algorithms.

For the first method, VRtualize approximates these savings:

* 60% time saved per 5 square miles
* 196 times more area covered per square degree than distance surveyed in a given work day
* $1540 saved from room and board to support surveying one square degree

According to our source, within a given work day, data is gathered on latitude and longitude,
elevation, geographical features, soil composition, and in some cases, civilian features, for 5
square miles. Pangeo provides latitude and longitude, elevation, and geographical features.

VRtualize also estimated saved costs for training computer vision algorithms below:

* Depending on the aircraft, L3-Harris will save $1,500-$4,000 per flight hour
* The salary of a cargo pilot, which is around $60,000 a year

Audience
------------------------

Pangeo is first and foremost an internal platform for the company L3Harris. However, the aim is to provide an open source proof of concept for a faster and more efficient method of handling and rendering Terrain Data. This proof of concept has many applications; thus, the target audience is also any project utilizing Generated Terrain.

Glossary
------------------------

.. tabularcolumns:: |L|L|

+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Term                                | Definition                                                                                                                                                                                                    |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Application                         | The interface between the Explorer and the Local Database. It handles interpretting Terrain Data for Generated Terrain                                                                                        |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Bing Maps                           | A Cooperating System which the Local Database can retrieve Terrain Data from. Owned by Microsoft and distributed for free use                                                                                 |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Cooperating System                  | Any system external to the Local System necessary to the overall System Environment (e.g. sources of Terrain Data)                                                                                            |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Explorer                            | The user viewing Generated Terrain. Can be a Researcher, Home User, or any other Stakeholder                                                                                                                  |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Familiar VR Movements               | Most common method adopted by software for users to move about and look around in a virtual reality environment using VR Equipment. This teleporting and moving around within the user play area.             |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Generated Terrain                   | Rendered Interactive 3-Dimensional virtual reality imagery from interpreted Terrain Data                                                                                                                      |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Local Database                      | A database instance within the Local System. It handles the caching logic and serving Terrain Data to the Application.                                                                                        |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Local System                        | The necessary system environment installed on the Explorer's machine.                                                                                                                                         |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Software Requirements Specification | A document that completely describes all of the functions of a proposed system and the constraints under which it must operate. For example, this document.                                                   |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Stakeholder                         | Any person with an interest in the project who is not a developer.                                                                                                                                            |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| System Environment                  | The overall environment consisting of the Local System, the Cooperating Systems and the users                                                                                                                 |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Terrain Data                        | Data necessary for Generated Terrain                                                                                                                                                                          |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| USGS Database                       | A Cooperating System which the Local Database can retrieve Terrain Data from. Owned by USGS and distributed for free use                                                                                      |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| VR Equipment                        | Virtual Reality headset and associated accessories necessary to interact with interactive 3-dimensional virtual reality imagery (e.g. HTC Vive Pro headset, light stations, controllers, keyboard, and mouse) |
+-------------------------------------+---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+

Overview
------------------------
This document is comprised of two parts, the high-level description and the requirements specification. 

The high-level description includes the overall system environment; and assumptions, risks, and dependencies VRtualize has considered for this project.

The requirements specification outlines the functional and non-functional requirements. Functional requirements can be broken down into use case requirements, local system requirements, and external interface requirements. Use case requirements are requirements of supported usages between the application and the explorer. local system requirements are requirements of supported usages of the local system. local system requirements are further broken down into the application functions and the local database functions. Last, external interface requirements are the requirements of the cooperating systems necessary for the local database to fetch terrain data.
