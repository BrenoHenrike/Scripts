# Options & Lite

The bot has a range of options that can be changed through the `ScriptInterface`. Some of these options can be changed through the UI as well (Options > Bot Options). Changes to options in a script will update their state in the UI accordingly.
Also there is `Lite` options that let you set the new **Advanced Options** in game.

## ScriptInterface#Options

| Definition | Type | Description |  
|---|:---:|---|  
| `AggroAllMonsters` | *bool* | Causes all living monster in the MAP to attack the player. As a result, when any monster in the room is killed, the player gets the reward. |  
| `AggroMonsters` | *bool* | Causes all living monsters in the room to attack the player. As a result, when any monster in the room is killed, the player gets the reward. |  
| `AutoRelogin` | *bool* | Enables auto-relogin. When the player disconnects or the game is taking too long to load, the client will logout and relogin, and restart the loaded script. |  
| `AutoReloginAny` | *bool* | When relogging in, the client will pick the first server that isn't the last server the player was connected to. This is typically unnecessary to use. |  
| `DisableCollisions` | *bool* | Disables all collisions in game, allowing you to move anywhere in the screen. |  
| `DisableFX` | *bool* | Disables all player animations. This can slightly reduce CPU load. |  
| `ExitCombatBeforeQuest` | *bool* | Ensures a player is out of combat before attempting to turn in quests. This is done by jumping to the player's current cell and pad. |  
| `GlitchedRooms` | *bool* | Glitched Rooms have been patched, this command does nothing. |  
| `HidePlayers` | *bool* | Hides all player avatars except the user's. |  
| `InfiniteRange` | *bool* | Allows the player to attack monsters from any distance. |  
| `LagKiller` | *bool* | Removes the world from the stage to drastically reduce CPU load while the bot is running. |  
| `Magnetise` | *bool* | Causes any targeted monster to teleport to the player. |  
| `PrivateRooms` | *bool* | All calls to `Join` are forced to transfer to a random room. |  
| `RestPackets` | *bool* | Sends a rest packet every second while the player is not in combat, causing the player to heal. |  
| `SafeRelogin` | *bool* | Causes a 75 second delay before attempting a relogin. This is typically unnecessary to use. |  
| `SafeTimings` | *bool* | Forces the bot to wait for any action taken to complete before continuing execution of the script, with a 5 second timeout. |  
| `SkipCutscenes` | *bool* | Forces all cutsenes in the game to be skipped. (Currently it skips but don't jump off the cutscene `Cell`, make sure to jump after you know a cutscene will play) |  
| `CustomGuild` | *string* | Sets the player's guild on the client side. |  
| `CustomName` | *string* | Sets the player's name on the client side. |  
| `HasteOverride` | *float* | Overrides the haste stat on the client side reducing skill cooldowns. This iis capped at 0.5 (50%). |  
| `HuntBuffer` | *int* | Sets how many kills hunt will wait before picking up drops. |  
| `HuntDelay` | *int* | The minimum time in milliseconds between jumping between cells when hunting for monsters (using `Hunt`, or any method that uses it). The default is `1000` (1 second). |  
| `HuntPriority` | *Priorities (enum)* | Sets the priority mode for hunting monsters. Default is `HuntPriorities.None`. |  
| `LoadTimeout` | *int* | The time in milliseconds that the game is allowed to load before it is considered to be a timeout, and an autorelogin is triggered. The default is `10000` (10 seconds). |  
| `LoginServer` | *RBot.Servers.Server* | The server to relogin to (this is automatically setted by `AutoRelogin`). |  
| `RunOnExit` | *string* | A string that, if set, will indicate a program to run when the script finishes. |  
| `WalkSpeed` | *int* | An integer that sets the player's walking speed. The default is `8` and the maximum is `32`. |  

## ScriptInterface#Lite

These Options can be changed in the **Advanced Options** tab in-game.

| Definition | Type | Description |
|---|:---:|---|
| `CustomDropsUI` | *bool* | Sets whether or no will use the Custom Drop UI. If true, make sure it is open and/or detached for drops be accepted. |
| `DisableSkillAnimations` | *bool* | Disables class animations only. |
| `HideUI` | *bool* | Hides the player and enemy portraits and the map name. |
| `ShowMonsterType` | *bool* | Display the monster's type as a tag under their name. |
| `SmoothBackground` | *bool* | Removes map background pixelation. |
| `UntargetDead` | *bool* | Untargets monsters that are dead. |
| `UntargetSelf` | *bool* | Prevents skills from targeting yourself. |

## Setting options

Some options can be set through the UI. All options can be set programatically in a script as follows:

```csharp
using RBot;

public void ScriptMain(ScriptInterface bot)
{
	bot.Options.SafeTimings = true;
	bot.Options.InfiniteRange = true;
	bot.Options.CustomName = "ARTIX";
	bot.Lite.UntargetSelf = true;
	//You can also set then using their game object name
	bot.Lite.Set("bReaccept", false);
}
```

**Note:** It is strongly recommended that `SafeTimings` is always enabled so you do not have to manage the timings of the bot yourself. Also, always make sure **_Reaccept Quest_** from **Advanced Options** is turned **OFF**.

---------
<center><a href="/Rbot-Scripts/Intro" title="Intro">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Timings and Handlers" title="Timings and Handlers">Next ►</a></center>