[![GitHub license](https://img.shields.io/github/license/Laritello/osu-music)](https://github.com/Laritello/osu-music/blob/main/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/Laritello/tavernkeep)](https://github.com/Laritello/tavernkeep/issues)

# Tavernkeep
Locally hosted web application for managing PF2E campaigns. Build using .NET and Vue (Tailwind + daisyUI).

![UI preview](https://github.com/user-attachments/assets/dbc6d56e-d601-4cbe-b3c7-b399c6782b08)
## Disclaimer
**Tavenkeep is still in development and not ready for use. We strongly recommend against using it in your games.**

## Overview
This project is an advanced character sheet designed for Pathfinder 2E, aimed at enhancing the gameplay experience for both players and Game Masters. While it doesn't encompass the full functionality of the Pathfinder 2E system, it provides a comprehensive set of features that make character management and gameplay more streamlined and intuitive.

## Planned features
* **Character sheet**: somewhere in between paper sheet and Pathbuilder.
   * What it **_does_** or **_will do_**:
      * **Abilities, Skills, etc.**: just select your proficiency, bonus is already calculated.
      * **Conditions**: you flat-footed? App already subtracted 2 from your AC 
      * **Health**: all you need to know is how much damage or heal you receive. Everything else is handled by the app.
      * **Inventory**: track your items and their weight.
      * **Wallet**: you won't need to remember how much is 14 gold plus 13 silver when buying using copper.
   * What it **_doesn't do_** and probably **_won't do_**:
      * **Full implementation of the system**: currently there are no plans to fully implement the Pathfinder 2E system.
      * **Character builder with a roadmap**: the app will have a simplified version, but not level-by-level builder.
      * **Spellcasting**: the app will have an ability to add spells, but it won't account for possible restrictions or rules.
      * **Feats**: you will be able to add feats, but they won't affect any properties of the character.
* **Chat**
  * What it **_does_** or **_will do_**:
      * **Whispers**: if you want to keep some secrets from other party members, you can whisper to other players or GM.
      * **Skill checks**: track history of your dice rolls.
      * **Secret/private skill checks**: more useful for GMs (i.e. lore checks), but also can be useful for players.
      * **Custom rolls**: if you want to roll 100d100 - you can. However, we strongly advise you not to.

* **Encounter System**: A planned feature that will allow GMs and players to track encounters more efficiently, ensuring smoother gameplay without unnecessary interruptions.
  * What it **_will do_**:
      * **Encounter notification**: even if you away from the table, you'll know when the fight starts.
      * **Initiative order**: stop asking who's next.
      * **Statblocks**: only if your GM said you know your opponent.

If you need any of the features we won't implement, then we recommend trying [Pathbuilder 2E](https://pathbuilder2e.com). It's a great application.

## Requirements
* .NET 9
* Node.js
* NPM

## Installation (Currently Windows only)
1. Clone the repository:
    ```
    git clone https://github.com/Laritello/tavernkeep.git
    ```
2. Navigate to the project directory:
    ```
    cd tavernkeep
    ```
3. Run the build script: 
    ```
    scripts\launch.bat
    ```
4. Launch the URL:
    ```
    http://localhost:5173/
    ```
5. Enter credentials:
    ```
    login: admin
    password: admin
    ```
## Acknowledgments
Thanks to the awesome Pathfinder 2E community. We used data from [Archives of Nethys](https://2e.aonprd.com), [Russian PF Wiki](https://pf2e-ru-translation.readthedocs.io/ru/latest/#) and some more. We will fill out full list closer to release.

## License
Distributed under the MIT License. See `LICENSE` for more information.

## Contact
For any questions or suggestions, feel free to reach via GitHub issues.

Happy adventuring! 🎲
