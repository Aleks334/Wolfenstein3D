# Wolfenstein 3D - a long-term project for programming lessons

## Summary

Two students of technical secondary school are attempting to create a 1992 Wolfenstein 3D clone whose visuals and game mechanics look similarly to the original game.

## What we changed/added since last project deadline (turn of November/December):

* Refactored most of the classes, that was created previously.
* Improved player movement (used FSM)
* Improved weapon system (through inheritance and polymorphism)
* Enhanced items system. We used Scriptable Objects advantages to make it more flexible and easy to maintain.
* Used Scriptable Objects as intermediate point of communication. They hold events which are raised by MonoBehaviours. Other scripts can subscribe to SO events.
* Replaced door moving animation with linear interpolation.
* Player now can use interact with objects by raycast and trigger without asking for certain type of object. We used interface for these two interaction types.
* Added menu system with splash screens, main menu, which holds multiple pages, quotes on exit from original game, episode selection and level difficulty selection (this menu system wasn't refactored though, so it needs lots of improvements in order to make menu more scalable).
* Added more weapons (mini gun, machine gun). These weapons are new type - full auto (others are melee and semi auto). 
* Basically rebuilt enemy system. Moreover we added new enemy types (dog, machine-gun guy). Previously there was only one type (guard). In system used state pattern in order to make code cleaner and more flexible.
