# Project Template
This project contains a base achitecture for a Unity game, with a tool for creating game submodules that automatically sets up new assembly definitions and their dependencies.

The project is split into multiple assemblies, with the goal of isolating MonoBehaviours from the core game logic. This allows more strict management of game state, and all operations that modify the state should be encapsulated in commands, with services for more complex operations.

# Assemblies
Each assembly has a core role in the project, and has a counter part in the submodules.

## Core
Shared dependencies for all other asssemblies, contains some common data types and type extensions.

## Data
Contains scripts for managing ScriptableObjects and prefabs. All scripts for managing data assets should be here.

## Game
Core game logic for managing the model. Contains all Commands and Service.

## Model
Contains all game state. Should not have any state changing logic.

## View
Should contain almost all MonoBehaviours. This assembly only has access to ViewModel and Game so it should read from the view model, and issue commands if it needs to change state.

## ViewModel
Contains only interfaces implemented by Model classes, with getters only. Used by View to update game state.
