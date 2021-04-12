# System Overview

## Input System

+ **WASD** (or arrow keys) to move
+ **Space** to jump
  + If the player is holding something, he is not able to jump.
+ **K** (or left mouse button) to attack with his weapon
+ **E** to interact with scene objects



## Dungeon System

The background will be built via Tilemap. Designers can place 2d object directly on top of it.



## Battle System

Only the basic settings. Scene objects have HP, ATK and DEF.

It may have some effects after an object being destroyed.

#### Scene Objects

Wooden Box

+ HP: 1
+ DEF: 0

Stone

+ HP: 2
+ DEF: 0

Metal Box

+ HP: 1
+ DEF: 1

Water element

+ Recharge the Branch

## Spell System

The branch + fire element = Torch

+ ATK: 1
+ Effects: Ignite, Endurance -1
+ Spell: Fire Attack (ATK=3, go back to branch after use)
+ Last for 10s

The branch + ice element = Ice stuff (seems very hard)

+ ATK: 2
+ Effects: Endurance +1, Speed -30%



## Dialogue System

There is a simple dialogue box for displaying text.



## Collection System

undetermined.



