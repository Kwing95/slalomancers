LENGTH:		TRAINING / SINGLE ROOM / SINGLE FLOOR / DUNGEON
MODE:		REMOTE / LOCAL (1-4 players)
DIFFICULTY:	EASY / NOVICE / NORMAL / HARD / EXPERT
SEED:		UNIX / CUSTOM (input seed)

CONTROLS
	- Tap your button to change the direction your character is moving. Tapping repeatedly will
	cause the player to swerve back and forth but ultimately move in a somewhat straight line.
	- Hold your button to stop your character completely and fire in the direction it's facing.
	Shoot enemies to damage them.
	- Being hit by enemy attacks will damage your GREEN HEALTH. When your GREEN HEALTH reaches
	zero, you will be stunned and therefore unable to move or shoot.
	- While stunned, your RED HEALTH will decrease. If it reaches zero, your character dies
	permanently.
	- Shooting a stunned ally will heal their GREEN HEALTH. This allows them to revive faster
	and therefore also causes them to lose less RED HEALTH while stunned.

ARCADE
	LEVEL 1		4 TARGETS
	LEVEL 2		2 ASTEROIDS
	LEVEL 3		1 FIGHTER 1 ASTEROID
	LEVEL 4		2 ASTEROIDS 1 GEYSER
	LEVEL 5		1 FIGHTER 1 ASTEROID 1 BOMBARDIER
	LEVEL 6		2 FIGHTERS 1 GEYSER 1 ASTEROID
	LEVEL 7		1 GEYSER 1 ASTEROID 1 FIGHTER 1 BOMBARDIER
	LEVEL 8		3 ASTEROIDS 1 GEYSER 1 BOMBARDIER
	LEVEL 9		2 GEYSERS 2 FIGHTERS 1 BOMBARDIER
	LEVEL 10	2 GEYSER 1 ASTEROID 2 FIGHTERS 1 BOMBARDIER

SINGLE FLOOR
	10 ROOMS
	1-2 TRAPS
	1-2 SECRETS

ENEMIES
	TARGET		Deals no damage, stays in one place. Used for training
	FIGHTER		Can use rush attack or fire bullets
	ASTEROID	Splits into smaller copies upon damage
	BOMBARDIER	Randomly places mines around the map
	GEYSER		Shoots a spiral of bullets around the map
	SCATTER		Shoots a scatter shot
	LAUNCHER	Shoots a homing missile

FEATURES
	S ROOM		A hidden room that can be revealed with bullets
	TRAP		Invisible trap that can be disarmed with bullets
	CHEST		Contains a code which buffs a random stat
	P CHEST		Comes in sets and contains part of a code

				MULTIPLAYER?	SPLIT CHESTS?		SHELL CHESTS?	PARTIAL CHESTS?
SINGLE ROOM		LOCAL ONLY		NO					NO				NO
SINGLE FLOOR	LOCAL/REMOTE	2					SET OF 2		NO
DUNGEON			LOCAL/REMOTE	YES					SET OF 2-4		SET OF 2-4

10000 inputs from 0 to 9999
1 charm per room = 10 codes
Max 3 chests per floor = 3 codes
13/1000 = 1.3% chance of random guessing

REMOTE MULTIPLAYER
	- Randomize moveset of each enemy type
	- Randomize map (with alphanumeric grid)
	- Hidden traps/rooms that a player may tell teammates about
	- Randomize booby-trapped chests
	- Each room has a unique difficulty rating that can only be seen after entering (1-N for N rooms)
	  - Playing rooms in order maximizes stat gains while minimizing risk
	  - Players can coordinate to guide each other into easier rooms, and use logic to estimate
	    which rooms are easiest
    - A chest contains a code (SET OF SCALES)
	  - (MAYBE) The player chooses between one item for themselves and one for their teammate
	- One of two identical chests contain a code, and accessing it destroys both chests (3 CUPS)
	  - Players must access different chests; the player that gets lucky shares the code
	- Multiple chests in a set contain parts of a code; together all players can use it (PIE IN 6 SLICES)
	  - (NO) With part of a code, players can just guess
	  - (MAYBE) Inputting a bad code may penalize a player's health; one player can sacrifice health
	    to make guesses and help their teammates
	- Terminals containing minigames that require different players to access different terminals
	
POWERUPS
	- Base health
	- Recovery during KO
	- Bullet speed
	- Bullet damage
	- Rate of fire

CHARMS (INCREASE BASE STATS)
	Health Charm: Increases base Green health
	Recovery Charm: Increases Red health recovery rate
	Velocity Charm: Increases bullet velocity
	Destruction Charm: Increases bullet damage
	Fury Charm: Increases rate of fire

RUNES (AFFECTS SHOTS)
	Duality Rune: Double shots										X
	Trinity Rune: Triple shot
	Rune of Fate: Homing shot									X
	Rune of the Sniper: Long distance buff						X
	Rune of the Skirmisher: Short distance buff						X
	Combustion Rune: Exploding shot
	Laser Rune: Damaging beam									X
	Rune of Premeditation: Single charged shot						X
	Rune of Piercing: Piercing shot								X
	Rune of Force: Knockback shot									X
	Rune of Elasticity: Bouncing shots							X

	Rune of Momentum: Rate of fire increases during press		X
	Rune of the Assassin: One shot per press						X
	Rune of Idling: Damage increases during no press

	Rune of Venom: Poison shot									X
	Rune of Stasis: Stop shot										X
	Arctic Rune: Slow shot

	Aura Rune: Damage on contact									X
	Fortune Rune: Secret rooms are revealed
	Rune of Warding: Traps are revealed


FLASKS (FINITE RESOURCES)
	Flask of Passage: Unlocks doors
	Green Flask: Recovers green health
	Red Flask: Recovers red health
	Coin: Used for purchases

	- Shots: Triple, homing, long/short distance damage buff, bomb, beam, drain, charged,
			 piercing, knockback, bounce, accelerating rate of fire, 
	- Shots (ctd): One shot per press, increasing/decreasing damage during press
	               damage increase during no press
	- Status: Bullets inflict slow, poison, freeze, 
	- Familiar: Shot, contact, shield, 
	- Keys, coins, green/red pickups