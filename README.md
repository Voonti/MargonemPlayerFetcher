<h2>MargonemPlayerFetcherApi</h2>
MargonemPlayerFetcherApi is a project that aims to collect and provide
data related to Margonem - a popular browser-based MMORPG game. 
The data collected includes information about the players' levels,
the items they have equipped, item ownership through a time period,
and item stats. This data can be used for various purposes such as 
statistical analysis, data mining, and machine learning.
<br /><br /><br />

> MargonemPlayerFetcherApi stores data in a database to persist the collected player and item data. Here are the various data types stored in the database:



1. Player 
	- User id (Player's unique identifier)
	- Char id (Character's unique identifier)
	- Nick (Characters nickname)
	- Server
	- Profession
	- Rank (It describes player type:
		- 0: Normal Player
		- 1: Super Game Master
		- 2: Game Master
		- 4: Forum Moderator
		- 32: Chat Moderator
)
	- Level
	- LastFetchDate
	- FirstFetchDate


2. Item 
	- Hid (Item's unique identifier)
	- Name
	- Icon (Item graphic relative path)
	- St (It describes what type of equipment it is:
	 	- 1: Helmet
		- 2: Ring
		- 3: Necklace
		- 4: Gauntlet
		- 5: Main weapon
		- 6: Armor
		- 7: Shield/Second weapon
		- 8: Boots
)
	- Stat (Whole item description)
	- Tpl (Item enchantment point)
	- Rarity
	- LastFetchDate
	- FetchDate

3. PlayerLevel
	- Player id { get; set; }
	- Level { get; set; }
    - Fetch date { get; set; }

4. PlayerNick
	- Player id { get; set; }
	- Nick { get; set; }
    - FetchDate { get; set; }



Data as of 10.09.2023
	Players: 83189
	PlayersNick: 83569
	PlayersLevels: 86983
	Items: 318828
