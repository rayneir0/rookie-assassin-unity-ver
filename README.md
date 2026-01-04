# Rookie Assassin Remake in Unity

## Changes made:
- More levels, more traps
- Implemented timers on traps and incremental timed traps.
- Pressure plates mechanic
- Created a game timer for high score tracking

##How to Play the Game

### Main Menu Screen
- First when you get into the game, you can use WADS or Arrow keys to move around
- To start the game go to the red tiles that can be found on the right if you keep going
- The scoreboard will contain your latest highscore and your current score.
	(Current score only updates after you complete the game)
### Level Screen 
- To play the game, go close to the levers and press the F key on your keyboard to activate the animation
- Each lever corresponds to a trap/a set of traps.
- Levers have a cooldown as shown by the number at the bottom of the gameobject.
	- During that time you cannot use the lever, so you have to time the trap activation properly
- You need to figure out which traps kill the enemies the most efficiently
- Traps can kill both player and enemies. Enemies can also kill players.
- Enemies die as soon as they hit the trap, but players have more health and only lose a bit of damage
- Health will persist throughout the levels so watch out.
- If your health goes to zero, the player will take a second to respawn back to the starting position. (The green tiles)
- Only when the player kills all the enemies will the red tiles appear. Go to the red tiles to proceed to the next level.

#### UI on Level Screen
- There is a timer on the bottom right to keep track of your time
- There is a HUD which keeps track of how many enemies there are left to kill and your player health.

### 2nd and 3rd Level Info
- In the second and third level, there are also pressure plates to activate a certain fire trap. Stand on it to activate it, and leave the tile to deactivate it.

### Level finished
- When you have finished all three levels, you can go back to the main screen and see your fastest time.
- You can go again to try to get through the dungeon faster.

## Assets Used
Player, Enemy, Tileset, Lever, Pressure plate all made by me
Nav Mesh Plus: https://github.com/h8man/NavMeshPlus 
Animated Traps: https://stealthix.itch.io/animated-traps
Music: https://pixabay.com/music/video-games-castle-of-athanasius-151010/
Font: https://www.dafont.com/pixel-operator.font

