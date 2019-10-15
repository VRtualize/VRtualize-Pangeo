Introduction
======================================

Purpose
------------------------
The purpose of this document is to present a detailed description of Project Pangeo. It will explain the purpose and features of the system, the interfaces of the system, what the system will do, and the constraints under which it must operate. This document is intended for both the stakeholders and the developers of the system.

Scope
------------------------
The SDSMT VRtualize Team is working together with L3Harris on Project Pangeo. Pangeo is a research project aiming to render the real world with 3D technology in a dynamically generated virtual reality environment. There are three main goals that contribute to this overall project: imagery retrieval, image caching, and virtual rendering.

Ideally Pangeo will be generalized to pull data from multiple sources of Cooperative Systems, and create Generated Terrain for multiple versions of VR Equipment. However, for the current scope, VRtualize aims to build Pangeo around one Cooperative System, the USGS Database, and one version of VR Equipment, the HTC Vive Pro and accompanying accessories. Controls will also be developed towards the HTC Vive Pro and accompanying accessories, though the generalized version is that the Application may create Generated Terrain for interaction with other equipment such as the Twisted Reality 2-Dimensional Treadmill, and in cases without VR Equipment, keyboard and mouse controls, though this will be bundled as part of VR Equipment, since the main objective is to develop software for VR Equipment. The generalized version is a near future goal though, so unless otherwise specified, the document will reference the generalized versions of the Cooperative System and the VR Equipment.

Audience
------------------------
Pangeo is designed to be an internal research tool for the company L3Harris. Despite that, the aim of the project is to be usable by anyone with access to virtual reality equipment and an interest in touring geographical points of interest from the comfort of their home.

Glossary
------------------------
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Term                                | Definition                                                                                                                                                  |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Application                         | The interface between the Explorer and the Local Database. It handles interpretting Terrain Data for Generated Terrain                                      |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Cooperating System                  | Any system external to the Local System necessary to the overall System Environment                                                                         |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Explorer                            | The user viewing Generated Terrain. Can be a Researcher, Home User, or any other Stakeholder                                                                |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Familiar VR Movements               | Most common method adopted by software for users to move about and look around in a virtual reality environment using VR Equipment                          |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Generated Terrain                   | Rendered Interactive 3-Dimensional Virtual Reality Imagery from interpreted Terrain Data                                                                    |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Local Database                      | A database instance within the Local System. It handles the caching logic and serving Terrain Data to the Application.                                      |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Local System                        | The system environment necessary installed on the Explorer's machine.                                                                                       |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Software Requirements Specification | A document that completely describes all of the functions of a proposed system and the constraints under which it must operate. For example, this document. |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Stakeholder                         | Any person with an interest in the project who is not a developer.                                                                                          |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| System Environment                  | The overall environment consisting of the Local System, the Cooperating Systems and the users                                                               |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| Terrain Data                        | Data necessary for Generated Terrain                                                                                                                        |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| USGS Database                       | The database the Local System retrieves Terrain Data from. Owned by USGS and distributed for free use                                                       |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+
| VR Equipment                        | Virtual Reality headset and associated accessories necessary to interact with Interactive 3-Dimensional Virtual Reality Imagery                             |
+-------------------------------------+-------------------------------------------------------------------------------------------------------------------------------------------------------------+

Overview
------------------------
Chapter two provides a high-level description, including the overall System Environment; and assumptions, risks, and dependencies VRtualize has considered for this project.

Chapter three is the Requirements Specification, which outlines the functional and non-functional requirements. Functional requirements can be broken down into use cases supported by the project; the Local System functional requirements which are further broken down into Application functions and Local Database functions; and External Interface Requirements which are the Requirements of the Cooperating Systems necessary for the Local Database to fetch Terrain Data.
