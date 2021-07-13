## Table of contents

- [Morgans Laws 💻](#morgans-laws-)
  - [About the project and our motivation](#about-the-project-and-our-motivation)
- [Required Packages](#required-packages)
- [Scenes](#scenes)
  - [Structure](#structure)
  - [PlayDemo](#playdemo)
      - [Description](#description)
  - [Debug](#debug)
      - [Description](#description-1)
- [Prefabs](#prefabs)
  - [Logic Gates (And / Or / Not)](#logic-gates-and--or--not)
    - [Description](#description-2)
  - [Socket / SocketSingle](#socket--socketsingle)
    - [Description](#description-3)
    - [Properties](#properties)
  - [Male / Female (Connectors)](#male--female-connectors)
    - [Description](#description-4)
  - [Cable](#cable)
    - [Description](#description-5)
    - [Properties](#properties-1)
      - [Cable Component](#cable-component)
      - [Cable IO](#cable-io)
  - [Doors (DoorRight / DoorLeft / DoorBoth)](#doors-doorright--doorleft--doorboth)
    - [Properties](#properties-2)
  - [Power](#power)
    - [Properties](#properties-3)
  - [Player](#player)
  - [Floor](#floor)
  - [InfiniteFloor](#infinitefloor)
  - [StageWindow](#stagewindow)
  - [Table](#table)
  - [Walls (Wall_A / Wall_B)](#walls-wall_a--wall_b)
- [Add new Stages](#add-new-stages)
  - [1. Setting up the Scene](#1-setting-up-the-scene)
  - [2. Setting up the Stage](#2-setting-up-the-stage)
  - [3. Add Sockets and Gates](#3-add-sockets-and-gates)
  - [4. Setting up the Stage Window](#4-setting-up-the-stage-window)
    - [i. Resetting the cubes](#i-resetting-the-cubes)
      - [ii. Reference the New Group to the Reset Button](#ii-reference-the-new-group-to-the-reset-button)
  - [5. Setting up the floor](#5-setting-up-the-floor)
  - [6. Add and Connect new Cables](#6-add-and-connect-new-cables)
  - [7. Add Walls](#7-add-walls)
  - [8. Test your stage](#8-test-your-stage)
- [References](#references)

# Morgans Laws 💻

## About the project and our motivation
Aim of the project is teaching the player boolean algebra. To accomplish this the player has to master several stages in a virtual world. In the stages the player has to decide which logical gates should be used to complete a circuit. A connected circuit will open a door to the next stage. Stages get harder and contain a bigger variety of logical gates as the player passes through the game.

By allowing the player to touch the logical gates we hope to improve the understanding of how these gates work exactly. On and off states of the gates are visualized and should help the player to get an idea how different gates affect a circuit. To enable the player to transfer the newly learned concepts into the real world the logical gates are modeled after the ANSI/IEEE Std 91/91a-1991 standard [[1]](#references). Therefore a player should also be able to read and understand real circuit diagrams after playing Morgans Laws.

# Required Packages

- Vive Input Utility 1.13.2
- Vive Wave XR Plugin 4.1.0
- Vive Wave XR Plugin - Essence 4.1.0
- Vive Wave XR Plugin - Native 4.1.0
- Ultimate 10+ Shaders 1.0.2

We use the Vive Input Utility and Wave XR Plugin to handle VR controller inputs. The Vive Wave XR Essence Plugin is used to show the correct VR controller model in the game and. Buttons pressed will also get highlighted. Ultimate 10+ Shaders are used for graphical effects, like outlines.


# Scenes
## Structure
Each set of stages is built within a scene. Additionally there's the Debug scene to help out developers to try out different elements and settings.

## PlayDemo

<p align="center">
    <img alt="PlayDemo" width="500" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Scenes/PlayDemo.png">
</p>

#### Description
The Play Demo Scene for Morgans Laws contains 4 demo stages showing the various gameplay elements. The player first discovers two controllers explaining the controls. He can already try out grabbing and moving the controllers as he likes. Then he finds on his right side a socket. An And gate lies on the floor indicating he can pick it up and use the gate on the socket. After he is done, the doors open leading to the next stage.

## Debug

<p align="center">
    <img alt="Debug" width="500" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Scenes/Debug.png">
</p>

#### Description
The Debug scene is for the developers and is used to test out single elements or complex circuits during development. Here you can test things out, like new scripts or different sets of component properties.

# Prefabs
## Logic Gates (And / Or / Not)
<p align="center">
    <img alt="And" width="450" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Gates.png">
</p>

### Description
The goal of each stage in this application is to position the logic gates in the right order to open a door. Each logic gate uses its own boolean function in the method `OnCircuitChanged` to determine an output.

## Socket / SocketSingle

<p align="center">
    <img alt="Socket" width="170" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Socket.png">
    <img alt="SocketSingle" width="150" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/SocketSingle.png">
</p>

### Description
The player has to place the logic gates into the ring of the socket to connect them the attached cables. There are two Prefabs of Sockets with either one or two female cable inputs.

### Properties

The `Socket` Prefabs contain two GameObjects. The lower`Spotlight` element on the floor and the floating `Socket` connection ring. When creating stages the `Socket` GameObject contains additional properties in the `Socket` Component  to set.

| Property | Description | Default |
| ------------- | ------------- | ------------- |
| Locked | The logic gate inside the socket can be locked in the socket ring. If enabled, the player isn’t able to remove or replace the current connected logic gate. | false |
| On Material | The material of the socket ring, when the output of the connected logic gate return true | Material_Active |
| Off Material | The material of the socket ring, when the output of the connected logic gate return false | Material_Inactive |

## Male / Female (Connectors)

<p align="center">
<img alt="Female" width="170" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Female.png">
<img alt="Male" width="150" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Male.png">
</p>

### Description
`Male` and `Female` connectors are used for Sockets, Power units and Doors.

| Property | Description | Default |
| ------------- | ------------- | ------------- |
| Value | The boolean value that determines if the connector is enabled | false |

## Cable

<p align="center">
    <img alt="Cable" width="300" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Cable.png">
</p>

### Description
Cables are attached to their male and female connector counterparts of sockets, power units or doors.

### Properties

The `Cable` Prefab contains two GameObjects, representing the start (female) and end (male) point of the cable. The `Female` GameObject contains various properties for the rendered cable in the `Cable Component` and CABLE IO Components.

#### Cable Component

| Property | Description | Default |
| ------------- | ------------- | ------------- |
| End Point | The end point to where the line of the cable will be rendered | Male |
| Cable Material | The default material of the cable | Grey |
| Cable Length | The longer the length of a cable, the more it hangs down between the start and end point | 0.1 |
| Total Segments | The total number of segments determines how many times the cable is split into single elements | 0 |
| Segments Per Unit | The number of segments per unit determines how many times the cable is split into single elements between multiple start and end points. | 6 |
| Cable Width | Determines the thickness of the cable | 0.06 |

#### Cable IO

| Property | Description | Default |
| ------------- | ------------- | ------------- |
| Value | The boolean value that determines if the connector is enabled | false |
| Cable On Material | The material of the cable when the value of the connected male output is true | Material_Laser_Emitter |
| Cable Off Material | The material of the cable when the value of the connected male output is false | Dark Greay |

## Doors (DoorRight / DoorLeft / DoorBoth)

<p align="center">
   <img alt="Door" width="250" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Door.png">
</p>

Doors separate the stages inside a scene. There are three with an input connector to the left, right or both sides of the door. The goal of each stage is to open the doors by setting the value of the inputs to true.

### Properties

| Property | Description | Default |
| ------------- | ------------- | ------------- |
| Door Plane | The plane to move down- and upwards depending on the value of the doors inputs | door_plane |
| Door Speed | The speed of the moving door plane | 1.5 |
| Display | The display on top of the door to show if the door is open or closed | door_display |
| Display On Material | The material for the display, when the value of the doors inputs are true | NextStage |
| Display Off Material | The material for the display, when the value of the doors inputs are false | NextStageX |
| Display Speed X | The horizontal speed of the moving display material | 0.27 |
| Display Speed Y | The vertical speed of the moving display material | 0 |

## Power

<p align="center">
   <img alt="Power" width="200" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Power.png">
</p>

Power units are used as basic static on and off switches for the cables connected to it.

### Properties

| Property | Description | Default |
| ------------- | ------------- | ------------- |
| Value | The boolean value of the male output connector of the power unit | false |

## Player

<p align="center">
   <img alt="Player" width="200" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Player.png">
</p>

The Player Prefab contains the ViveCameraRig and VivePointer Prefabs of the ViveInputUitlity Plugin. For player collision a Collider is attached to its ViveCameraRig.

## Floor

<p align="center">
   <img alt="Floor" width="400" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Floor.png">
</p>

The area of the stage, where the player is able to teleport on. It is surrounded by blue outlines to signal the player that the floor is teleportable. Objects like tables placed on the floor are also surrounded by blue outlines. To generate this effect we used the Ultimate 10+ Shaders Plugin.

## InfiniteFloor

<p align="center">
   <img alt="InfiniteFloor" width="400" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/InfiniteFloor.png">
</p>

The area, where the player isn’t able to teleport on.

## StageWindow

<p align="center">
   <img alt="StageWindow" width="320" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/StageWindow.png">
</p>

The menu inside a stage. The player can reset the positions of Gates he moved or quit the application. The StageWindow will be hidden and shown depending on the stage the player is currently in.

## Table

<p align="center">
   <img alt="Table" width="180" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Table.png">
</p>

A basic cube serving as a shelf space for GameObjects like Gates, that the player can drag.

## Walls (Wall_A / Wall_B)

<p align="center">
   <img alt="Walls" width="320" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Prefabs/Walls.png">
</p>

The restrictions of a stage. They contain an outer and inner element, each with its own material. This way, the glowing, transparency and color of each wall can be modified.

# Add new Stages

## 1. Setting up the Scene

<p align="center">
    <img alt="PlayDemoFull" width="700" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/PlayDemoFull.png">
</p>

Each scene contains a set of interconnected stages. The lighting, world floor (infinite floor), the player and the interaction mode manager are placed on the root. 

## 2. Setting up the Stage

<p align="center">
    <img alt="StageControl" width="500" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/StageControl.png">
</p>

Each stage have to contain at least the following elements:

* a floor
* a door
* at least one power unit
* at least one cable connected to the power unit
* a stage window (Menu)

After you added all these elements to your new stage, proceed to configure its important elements.

## 3. Add Sockets and Gates

<p align="center">
    <img alt="SocketsGatesPower" width="500" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/SocketsGatesPower.png">
</p>

Place your gates, sockets and power unit on your stage. Preferably put a `Gate` on a `Table`, so the player can easily grab the gate.

## 4. Setting up the Stage Window

After adding the stage window deactivate the canvas component to make it invisible, until the player enters your new stage. If your stage is the first stage of the scene, keep it activated.

### i. Resetting the cubes

<p align="center">
    <img alt="GatesControl" width="150" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/GatesControl.png">
</p>
<p align="center">
    <img alt="ResetScript" width="400" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/ResetScript.png">
</p>

To make it possible for the player to reset the logic gates in the stage, you have to group the gates in a new empty GameObject. Then add the Reset script to the new GameObject.

#### ii. Reference the New Group to the Reset Button

<p align="center">
    <img alt="ResetButton" width="150" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/ResetButton.png">
</p>
<p align="center">
    <img alt="ResetClick" width="400" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/ResetClick.png">
</p>

Select the reset button in the stage window and add the method `ResetStage.ResetPositions` as the `On Click ()` method.

## 5. Setting up the floor

<p align="center">
    <img alt="FloorProperties" width="490" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/FloorProperties.png">
</p>

Select the floor and select your stage window as `Window` and your door as `Door`. Make sure you select the canvas of your stage window and the door element, that contains the Door script as component.

## 6. Add and Connect new Cables

<p align="center">
    <img alt="CablePlacement" width="320" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/CablePlacement.png">
</p>

Place a new `Cable` into your stage for each connection you want to add. Each Sable has a `Female` (start point / input) and `Male` (end point / output) connector. Place these connectors around the fitting connector of your power units, sockets or door.

## 7. Add Walls

<p align="center">
    <img alt="WallsPlacement" width="500" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/Developers%20Guide/WallsPlacement.png">
</p>

Now You can add walls or other objects you want to add to your stage.

## 8. Test your stage

Your stage is now ready to get tested by you. If you want to know more about the Prefabs and their properties see the [Prefabs page](https://github.com/VRLAB-HSKL/AVR21-3/wiki/Prefabs). For best practices have a look at the [PlayDemo](https://github.com/VRLAB-HSKL/AVR21-3/wiki/Scenes#playdemo) scene in the assets/scene directory.


# References
[1]	„IEEE/ANSI 91/91a-1991 - IEEE Standard Graphic Symbols for Logic Functions (Including and incorporating IEEE Std 91a-1991, Supplement to IEEE Standard Graphic 
Symbols for Logic Functions)“. https://standards.ieee.org/standard/91-91a-1991.html (zugegriffen Juli 12, 2021). 