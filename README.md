# leaderboard-task

MY SOLUTION:<br/>
The leaderboard consists of cells that are created after parsing data from the JSON, these cells can be customized for yourself<br/>
Additional design for cells is stored in the SO for convenience, by type name for the type and its colour<br/>
The leaderboard script ensures that the leaderboard remains sorted after each update, which is crucial for maintaining correct rankings.<br/>
Separate script for closing popup, not only Leaderboard, can be used for any popup<br/>
Uses image caching and asynchronous methods to optimize, reduce load and avoid blocking the main thread.<br/>

CHANGES HAVE BEEN MADE TO PopupManager:
The class is divided into several entities.<br/>
PopupService - opens/closes popups, has a list of open ones and does nothing else.<br/>

AssetLoader/AssetProvider - encapsulates asset loading in itself.<br/>
P.S If suddenly it is necessary to switch to another mechanism for obtaining them - it will be necessary to simply substitute another implementation in DI.<br/>

PopupFactory - gets the prefab through the loader, puts it in the right canvas<br/>
