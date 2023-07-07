# Unity Controller Debugger

A very simple software used to debug key mappings.

The use case is for arcade game platforms where you don't want to run the unity editor but want to figure out which key maps to what.


## How to use

Requires you to install Text Mesh Pro when prompted.

Simply build with the SampleScene as the only scene for the target platform.

The screen will display which `KeyCode`s are pressed.


## Shortcomings

- Handles axis input from 8 joysticks only up to 12 axes (hard-ish coded).
