# Wolfenstein 3D - a long-term project for programming lessons

##READ THIS!
After cloning the repo and opening it in Unity, you may see the "Untitled" scene loaded. If this happens, you should select the first scene from the Build Settings (go to Scenes/Menu Scenes/ in Assets and open the scene named "SplashScreens").

## Summary

Two students of technical secondary school are attempting to recreate a 1992 Wolfenstein 3D clone whose visuals and game mechanics look similarly to the original game. We code it in Unity Engine.

## What we changed/added since last project deadline (turn of November/December):

* Refactored most of the classes, that was created previously.
* Improved player movement (used FSM). Furthermore we added new movement way - strafing. Player can change defualt behaviour of rotating when hold left/right arrow. As long as he presses ALT key he can move left/right using left/right arrows.
* Improved weapon system (through inheritance and polymorphism)
* Enhanced items system. We used Scriptable Objects advantages to make it more flexible and easy to maintain.
* Used Scriptable Objects as intermediate point of communication. They hold events which are raised by MonoBehaviours. Other scripts can subscribe to SO events.
* Replaced door moving animation with linear interpolation.
* Player now can use interact with objects by raycast and trigger without asking for certain type of object. We used interface for these two interaction types.
* Added menu system with splash screens, main menu, which holds multiple pages, quotes on exit from original game, episode selection and level difficulty selection (this menu system wasn't refactored though, so it needs lots of improvements in order to make menu more scalable).
* Added more weapons (mini gun, machine gun). These weapons are new type - full auto (others are melee and semi auto). 
* Basically rebuilt enemy system. Moreover we added new enemy types (dog, machine-gun guy). Previously there was only one type (guard). In system used state pattern in order to make code cleaner and more flexible.

## What we can do during next project:

* Make gun system even more flexible. All weapons of type that exists (class was made previously) could be made by adding new SO that would have weapon data, sprites, sounds etc. Player weapons could be stored in other inventory SO in list. Player would change sprite of current weapon instead of setting active one of gameobjects that he have (in player object there are all weapon game objects).
* Improve UI system. It could rely on events instead of statics. It would make code more modular and decoupled. Then parts of the system could be test individually.
* Add sound effects nad music
* Refactor menu system
* Make a possibility to get parameters of certain type enemy from SO, which could hold all needed data for one type of enemy. This would make gameplay settings adjustments easier to test.
