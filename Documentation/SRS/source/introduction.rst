Introduction
======================================

Purpose
------------------------
The purpose of this document is to present a detailed description of Project Pangeo. It will explain the purpose and features of the system, the interfaces of the system, what the system will do, and the constraints under which it must operate. This document is intended for both the stakeholders and the developers of the system.

Scope
------------------------
The SDSMT VRtualize Team is working together with L3Harris on Project Pangeo. Pangeo is a research project aiming to render the real world with 3D technology in a dynamically generated virtual reality environment. There are three main goals that contribute to this overall project: imagery retrieval, image caching, and virtual rendering.

Pangeo uses a Local Database to take advantage of table lookups for Terrain Data, and adding an extra layer for predictive buffering of more Terrain Data over the network. This enables the quick access of Terrain Data for new location requests, but does not require the supported locations to be immediately available to the application, which reduces space necessary to store Terrain Data and reduces labor for maintaining and updating that Terrain Data. The Local Database will also serve as the gateway between the Application and multiple Cooperating Systems, sources of Terrain Data. The Local Database will intelligently choose a Cooperating System at Terrain Data request to ensure data availability. This reduces reliability on the availability of a single Cooperating System. Pangeo also utilizes a caching algorithm between the Local Database and the Application to have Terrain Data immediately relevant to the Explorer available. The caching algorithm provides the smooth transition between sections of Generated Terrain to minimize time and increase efficiency of rendering different Terrain Data segments into Generated Terrain as the Explorer moves between Generated Terrain.

Audience
------------------------
Pangeo is first and foremost an internal research project for the company L3Harris. However, the aim is to provide an open source proof of concept for a faster and more efficient method of handling and rendering Terrain Data. This proof of concept has many applications; thus, the target audience is also any project utilizing Generated Terrain.

Glossary
------------------------
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
This document is comprised of two parts. First, the High-Level Description, followed by the Requirements Specification.

The High-Level Description includes the overall System Environment; and assumptions, risks, and dependencies VRtualize has considered for this project.

The Requirements Specification outlines the functional and non-functional requirements. Functional requirements can be broken down into use case requirements, Local System requirements, and external interface requirements. Use case requirements are requirements of supported usages between the Application and the Explorer. Local System requirements are requirements of supported usages of the Local System. Local System requirements are further broken down into the Application functions and the Local Database functions. Last, External interface requirements are the requirements of the Cooperating Systems necessary for the Local Database to fetch Terrain Data.
