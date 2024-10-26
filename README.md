# Wolfenstein 3D Clone

A long-term Unity project created in 2023 for a programming subject in technical secondary school. This game is clone of _Wolfenstein 3D_ released in 1992 by Id Software.

---

## 📥 Getting Started

### Requirements

-   **Unity Version:** at least 2021.3.12f1
-   **Resolution:** For the best experience, set the **Game Window** resolution to **Full HD (1920x1080)** instead of **Free Aspect** when running the game in the editor.

### Initial Setup

1. Clone the repository and open it in Unity.
2. If you see the "Untitled" scene loaded by default, navigate to `Assets/Scenes` and open the **Initialization** scene manually.

---

## 🎮 Game Features

### Core Mechanics

-   **Player Movement:**

    -   **Finite State Machine** for smoother, more flexible player control.
    -   **Strafing Movement**: Holding the ALT key allows strafing with the left/right arrow keys, rather than rotation.

-   **Item System:**

    -   Leveraged **Scriptable Objects** for a flexible, maintainable item system.
    -   Different types of items: **Ammo**, **Health**, **PowerUp**, **Weapon**.

-   **Communication with Scriptable Objects:**

    -   Centralized communication using **Scriptable Objects** to manage events raised by `MonoBehaviour`'s. Other scripts can subscribe to these events, which is especially used for **SFX** and **music** handling.

-   **Universal Interaction:**
    -   Interactions with objects via **Raycast** and **Trigger** are interface-driven, making them flexible and extensible without requiring object-type checks.

### User Interface

-   **Menu System:**
    -   Features splash screens, settings for adjusting resolution and sound volume, main menu with multiple pages, quotes on exit (from the original game), episode selection, and level difficulty selection (currently difficulty doesn't affect enemies behaviour).

### Combat System

-   **Weapons:**

    -   Four weapons are available: **Pistol**, **Knife**, **Minigun**, and **Machine Gun**.
    -   Weapons vary by type, supporting **full-auto**, **melee**, and **semi-auto** mechanics.
    -   New weapons can be added easily by creating new **Scriptable Objects** with weapon data, sprites, sounds, etc.

-   **Enemy AI:**
    -   Different types: **Guard Dog**, **SS Guard**, **Guard**) and the level boss.
    -   A **state pattern** was applied to improve code cleanliness and flexibility in AI behavior.

---

## 🤝‍ Contributors

-   **[@Szen400](https://github.com/Szen400)**: Developed enemy mechanics and HUD.
-   **[@Aleks334](https://github.com/Aleks334)**: Player movement, interaction, weapon system, game manager, audio and menu system.

---
