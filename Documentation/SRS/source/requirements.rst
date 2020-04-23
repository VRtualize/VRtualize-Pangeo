Requirements Specification
======================================

Functional Requirements
------------------------

Use Case Requirements
``````````````````````

Use Case Requirements outlines the functions the application provides to the explorer.

**Use case:**  Interacting With Generated Terrain

The Explorer can move by teleporting and look around the Generated Terrain with familiar VR movements. The Explorer may also move via flying. Familiar VR movements, and requires a VR controller as part of VR equipment. To teleport around the terrain, the explorer will press the thumbstick/trackpad, and point to the area to teleport to.

**Use case:** Jumping

The application provides controls to jump to a specified longitude and latitude provided by the explorer. "jumping" is a mechanism to instantly navigate to another location. The explorer accesses the menu and selects the "Enter Coordinates" option. The explorer may input longitute and latitude and press "Go" to jump to the specified location.

**Use case:**  View Generated Terrain

The explorer can see generated terrain. The application will *create generated terrain* viewable to the explorer through VR equipment.

Local System Requirements
``````````````````````````

This section outlines the functions of the application and the local database

**Application:** Create Generated Terrain

The application renders interactive 3-dimensional virtual reality imagery. Creating generated terrain depends on the location of the explorer. It *requests terrain data* according to the location and uses the terrain data to create generated terrain.

**Application:** Request Terrain Data

The application requests terrain data from the data manager.

**Data Manager:** Prepare Terrain Data

The data manager prepares terrain data necessary to *create generated terrain*

**Data Manager:**  Fetch the Data

The data manager fetches data from a cooperating system.

External Interface Requirements
````````````````````````````````

This section outlines the requirements of the Cooperating Systems necessary for the Local Database to fetch Terrain Data.

The USGS Database does not have any interface requirements necessary for the Local Database to retrieve Terrain Data.

The Application assumes the Explorer will access it via VR Equipment

Non-Functional Requirements
--------------------------------

System Characteristics
````````````````````````````````

**The Explorer**

The explorer is expected to be familiar with VR equipment. Plus, the explorer is knowledgable about valid geological data (longitude and latitude) and terminology.

**The Local System**

The physical machine must install the local system. The machine hosting the local system is expected to have these characteristics:

*   External Interfaces

    *   VR equipment
    *   1GBps or better network connection

*   Minimum System Specifications

    *   Hardware

        *   Quad Core Processor
        *   8GB of RAM
        *   1TB available storage space
        *   NVIDIA GTX 970 / AMD Radeon RX 480
        *   1GBps NIC

    *   Software

        *   Microsoft Windows 10
        *   Unity 2019.2.12f1

The application part of the local system will provide interactions familiar to users who have used virtual reality before, so buttons, menu options, etc. will behave similar to other virtual reality software, and will be placed in locations similar to other virtual reality software. In places where the application receives input from the explorer, the application assumes the input is valid.

The local database part of the local system will have well encapsulated caching logic, and the application should not need to know how the caching works when requesting terrain data. The local database must also handle fetching terrain data from cooperating systems and the application should not need to interact with cooperating systems.

**The Cooperating Systems**

The local database expects at least one cooperating system to be active and connectable.
