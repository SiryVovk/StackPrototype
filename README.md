Stack Prototype

Simple hyper-casual stacking game prototype made in Unity.

Description

The player taps the screen to stop a moving block and stack it on top of the previous one.

If the block is not perfectly aligned:

The overlapping part remains

The extra part is cut off and falls down

If there is no overlap, the game ends.

Features

Alternating movement on X and Z axis

Overlap calculation and dynamic block resizing

Falling cut pieces

Object pooling for performance

Basic camera follow

Mobile input support

Technical Notes

The cutting logic is fully math-based (no mesh slicing).
Block size is adjusted using overlap calculations between current and previous block.

Cut fragments are reused through Unity’s Object Pool system to avoid unnecessary allocations.

Project Status

Core mechanic is implemented and playable.
This is an early gameplay prototype focused on logic rather than visuals or polish.

Next Steps

Score system

Difficulty progression

Visual feedback and polish

Sound effects

UI improvements


Gamplay gif

![ToGif](https://github.com/user-attachments/assets/3192f761-3dd8-42b5-92fd-e5768b5c8619)

