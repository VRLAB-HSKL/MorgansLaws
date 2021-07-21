# User Manual 📗

## Table of contents
- [About Morgans Laws](#about-morgans-laws)
- [Controls](#controls)
- [Completing a circuit](#completing-a-circuit)
- [References](#references)

## About Morgans Laws

Aim of the project is teaching the player boolean algebra. To accomplish this the player has to master several stages in a virtual world. In the stages the player has to decide which logical gates should be used to complete a circuit. A connected circuit will open a door to the next stage. Stages get harder and contain a bigger variety of logical gates as the player passes through the game.

## Controls

<p align="center">
    <img alt="Controls" width="600" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/User%20Guide/Controls.png">
</p>

Moving around in the game's world is accomplished by teleporting and logical gates can be dragged to insert them into the sockets. The controls are based on the raycast pointers of the Vive Input Utility [[1]](#references). It works completely context sensitive. By looking at the teleportable floor (surrounded by blue outlines) and pressing the trigger button the player is teleported to the targeted position. By pointing at a logical gate and pressing the trigger it can be dragged. Pressing the pad will pull the dragged gate towards the player. Swiping up or down on the pad can also be used to push or pull a logical gate.

## Completing a circuit

Your goal is to complete circuits to open a door. Each stage contains one or multiple incomplete circuits you have to finish. A circuit consists at least of the following components:

<p align="center">
    <img alt="Circuit" width="700" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/User%20Guide/Circuit.png">
</p>

To complete a circuit, you have to pick up the logic gates of a stage and put them in the right order inside the sockets. The sockets connect your logic gate with its attached cables. To put a gate into the socket just drag it to the center of the socket ring and release it there. 

<p align="center">
    <img alt="Sockets" width="400" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/User%20Guide/Sockets.png">
</p>

If the boolean operation of your gate is fulfilled the cable on the front side of the socket glows in a white. The stage is completed, when the input of the door is powered and thus the door opens to the next stage.

<p align="center">
    <img alt="CompleteCircuit" width="400" src="https://raw.githubusercontent.com/VRLAB-HSKL/AVR21-3/main/documentation/User%20Guide/CompleteCircuit.png">
</p>

## References
[1]	„IEEE/ANSI 91/91a-1991 - IEEE Standard Graphic Symbols for Logic Functions (Including and incorporating IEEE Std 91a-1991, Supplement to IEEE Standard Graphic 
Symbols for Logic Functions)“. https://standards.ieee.org/standard/91-91a-1991.html (zugegriffen Juli 12, 2021). 