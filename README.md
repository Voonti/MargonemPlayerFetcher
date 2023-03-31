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
	- User Id (Player's unique identifier)
	- Char Id (Character's unique identifier)
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


2. Item 
	- User Id (Player's unique identifier)
	- Char Id (Character's unique identifier)
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
	- Last Fetch Date
	- Fetch Date (First fetch date)
