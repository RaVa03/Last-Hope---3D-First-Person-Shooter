
  <h1 align="center"> Last Hope - A 3D First Person Shooter</h1>

  <br>

  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; This was the project for my bachelor's degree exam. For the development of the game I mainly used the C# programming language, Visual Studio and the Unity game engine.

  ## The game
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The player is represented as a survior in a Zombie Apocalypse and has to reach a safe place.
  He can move around and interact with the game universe: he can pick up items for more life points, collect ammunition, manipulate the gun, fight zombies, run around.  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;![image](https://github.com/RaVa03/Last-Hope-3D-First-Person-Shooter/assets/133386404/df9b05a1-a9db-4fdd-a3ca-a7613ad2e9b7)
  <p align="center"><sup> Screenshot of the player view when entering the first level </sup></p>

  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A simple Heads-Up Display (HUD) is fixed on the bottom of the screen and is always visible to the player. He can see his health points, track the ammo and a radar in the bottom-right corner of the screen helps him navigate the map more efficiently.

  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; On this radar the main character is represented in the center as a green arrow, the enemies as red dots, instruction object as a map, and auxiliary items are represented with yellow or green dots. All the dots are relative to the position of the player.

  ## Levels
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; The game is constructed on three Unity scenes: two game levels and start menu. In Level 1, there are 3 groups of zombies that are spawned when the player enters their action area,
  and then, based to the distance to the player, they have 4 states: following the player, attacking him, roaming aimlessly or being in idle mode.
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;![image](https://github.com/RaVa03/Last-Hope-3D-First-Person-Shooter/assets/133386404/2d1c5851-4561-48cb-a740-5a473151e554)
  <p align="center"><sup> Standard enemies </sup></p>
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; In Level 2, the difficulty is increased. The player has to face 3 boss zombies that are stronger than regular ones. These enemies have more health points, and can deal a lot more damage, thus forcing the player to play smart in order to defeat them.
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ![image](https://github.com/RaVa03/Last-Hope-3D-First-Person-Shooter/assets/133386404/7f919f98-8169-4944-9978-4bf5cdc128d5)
  <p align="center"><sup> Boss enemies from the second level </sup></p>
  
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; There are a few checkpoints for which the state of the game is saved automatically, and the player can continue from last checkpoint or start a new game.
  Beside the starting menu, which is a scene in itself, extra menus are also available: Pause menu, Start Menu, Instructions Menu, Victory menu or Game Over menu.

  ## Other screenshots
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;![image](https://github.com/RaVa03/Last-Hope-3D-First-Person-Shooter/assets/133386404/f9b2854e-b790-4362-ad37-c5bae6d6e1f7)
  <p align="center"><sup> The Start Menu scene </sup></p>
  
  ![image](https://github.com/RaVa03/Last-Hope-3D-First-Person-Shooter/assets/133386404/053ffa4d-d02b-45cb-ada7-5be61be7d476)
  <p align="center"><sup> Other menu scenes from the game </sup></p>

  ## Additional info
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Since the runnable file with all dependencies is too large to be added on GitHub, this repository only includes the C# scripts. There is a video playthrough of the game in 2 .mp4 files (Video_demo_part1 and Video_demo_part2) to get a better idea of the game and it's mechanics.

