# leaderboard-task
 
MADE CHANGES TO PopupManager:
The class is divided into several entities.<br />
PopupService - opens/closes popups, has a list of open ones and does nothing else.< br / >

AssetLoader/AssetProvider - encapsulates asset loading in itself.< br / >
If suddenly it is necessary to switch to another mechanism for obtaining them - it will be necessary to simply substitute another implementation in di.< br / >

PopupFactory - gets the prefab through the loader, puts it in the right canvas, calls the injection method< br / >
