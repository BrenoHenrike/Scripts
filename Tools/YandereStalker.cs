/*
name: Player Finder
description: Finds a player in a range of roomnumbers, with a specific map name, player name, stop location (wether u want to stop at the player or your default location), and a start and stop range for room#
tags: find, player, 
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class YandereStalker
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public string OptionsStorage = "FindPlayer";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("Stay at player", "Stay at player", "stay at player, or goto default stop location", false),
        new Option<string>("Player Name", "Player Name", "Insert playername to find", ""),
        new Option<string>("Map Name", "Map Name", "Insert map name to find", ""),
        new Option<int>("Starting Room Number", "Starting Room Number", "RoomNumber to start at.", 0),
        new Option<int>("Ending Room Number", "Ending Room Number", "RoomNumber to end at.", 0),
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();
        FindPlayerThroughMapNumbers();
        Core.SetOptions(false);
    }

    public void FindPlayerThroughMapNumbers()
    {
        string? player = Bot.Config!.Get<string>("Player Name");
        string? map = Bot.Config!.Get<string>("Map Name");

        if (Bot.Config!.Get<bool>("Stay at player"))
            Core.CustomStopLocation = "None";

        int lowestMapnumber = Bot.Config!.Get<int>("Starting Room Number");
        int highestMapNumber = Bot.Config!.Get<int>("Ending Room Number");

        Core.Logger($"Lowest Map Number: {lowestMapnumber}, Highest Map Number: {highestMapNumber}");

        if (player == null || map == null)
        {
            Core.Logger("Player Name or Map Name is not specified. Stopping script.");
            return;
        }

        Core.Logger("Joining whitemap to then move to the next room (in case of navigation issues)");

        for (int mapnumber = lowestMapnumber; mapnumber <= highestMapNumber; mapnumber++)
        {
            Core.Join($"{map}-{mapnumber}");
            Bot.Wait.ForMapLoad(map);

            if (Bot.Map.PlayerExists(player))
            {
                Core.Logger($"{player} Found in {Bot.Map.FullName}!");
                return;
            }
            else
            {
                Core.Logger($"{player} not found in {Bot.Map.FullName}");
                Core.Join("whitemap");
            }
        }

        Core.Logger($"{player} not found in {Bot.Map.FullName}, stopping (bot will go to the default home location)", stopBot: true);
    }








}
