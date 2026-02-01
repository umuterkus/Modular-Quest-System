 Modular Quest System (Code Sample)

This repository contains the core logic for an event-driven Quest System designed for Unity. 
This is a code-only showcase demonstrating architecture and logic. It is not a complete Unity project.

==== Architecture ====

- Core Logic: Handles quest generation, validation, and lifecycle (`QuestManager`).
- Data Oriented: Uses ScriptableObjects for scalable data management (`QuestTemplateSO`, `ItemDataSO`).
- Event Driven:*Decoupled communication using a static Event Bus (`GameEvents`), preventing spaghetti code dependencies.
  
- Generation: Procedurally generates quests based on templates.
- UI: The UI system listens to events and updates automatically without direct references to the logic.
