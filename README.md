# EECS 290: Project 02 - Platformer #
## Andrew Heckman, Blake Needleman, Brendan Herlacher and Wes Rupert ##

This project is a 2D platformer for Case Western Reserve University's eecs290 Inro to Game Design class. It is a 2D sidescroller platformer with a 3D twist. Or to be more specific, a 3D _flip_.

The game has mostly basic platformer mechanics. The player jumps from platform to platform, going from left to right in an attempt to reach the goal. The platforms can be stationary or moving between two points. The only hazard is missing a platform and falling into a bottomless pit (There are checkpoints that save you from having to start a level all over).

The mechanic that makes this game unique is the ability for the player to _flip_ the world, making the ceiling of the world become the floor and vice versa. This allows for the player to pass otherwise insurmountable obstacles, though it introduces challenges of its own. The game player is forced to think more abstractly about the surface they are running on, and determine how to pass challenges using both the top and the bottom of the world to move ahead.

The game aims to be a puzzle platformer, using the spatial fuzziness of objects only existing maybe on one side of the map. The game is single player, since there is no good way to share the world-flipping mechanic. There are no enemies in the world, but there are bottomless pits that the player can fall in, forcing the player to start the level (or possibly the game) over. Some of the platforms are moving, and some of them aren't affected by the world flipping as well, adding to the types of puzzles alloted.
