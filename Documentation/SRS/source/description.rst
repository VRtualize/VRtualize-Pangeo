High Level Description
=======================

System Environment
-----------------------

.. figure:: _static/images/Local_System.png
   :width: 50%

   Diagram depicting application component interactions

The overall system has one active user (the Explorer), one Local System, and Cooperating Systems. The Explorer interacts with the Local System via VR Equipment. The solid line shows a undirected connection where the Explorer may interact with the Application and send it control signals, and the Application may update and provide Generated Terrain to the Explorer. The Local System is comprised of two major parts, the Application, and the Local Database. The diagram shows these two parts separately to illustrate data flow, but the two parts are not separate, as the one tightly coupled piece, signified by the dotted line. The Local System retrieves Terrain Data from a Cooperating System, and stores it within the Local Database for caching logic. This is one directional from the Cooperating System to the Local Database and is reflected on the diagram. The Application requests Terrain Data from the Local Database through an abstraction layer to loosely couple the Terrain Data to the Application. The Application will use the Terrain Data to create Generated Terrain for the Explorer.

Assumptions, Risks, and Dependencies
-------------------------------------

Each individual requirement will describe its own assumptions and dependencies if it has any, in the next chapter. The biggest risk would be the motion sickness the Explorer may encounter typical with software using VR Equipment. VRtualize seeks to mitigate this with Application characteristics described in section 3.2.
