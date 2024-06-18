/*
name: Donated ACs Checker
description: This will check all of the accounts you provided that stored locally for ACs recieved from the event.
tags: donated-acs-checker, seasonal, frostvale
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Seasonal/Frostvale/ChillysParticipation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models;
using Skua.Core.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

public class CheckForDonatedACs
{
    private static IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreFarms Farm = new();
    private readonly ChillysQuest CQ = new();
    private readonly CoreDailies Daily = new();
    private readonly CoreArmyLite Army = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CheckACs();

        Core.SetOptions(false);
    }

    public void CheckACs()
    {
        string logPath = Path.Combine(ClientFileSources.SkuaOptionsDIR, "FrostvaleDonationLog.txt");
        bool firstTime = !File.Exists(logPath);
        List<string> ACs = new();
        List<string> oldACs = new();
        List<string> newACs = new();
        List<string> warnings = new();

        if (firstTime)
            File.WriteAllText(logPath, string.Empty);
        else
            oldACs = File.ReadAllLines(logPath).ToList();

        Bot.Events.ExtensionPacketReceived += ACsListener;

        while (Army.doForAll())
        {
            Core.Sleep(2000);
            Bot.Wait.ForMapLoad("battleon");

            //just adding all the checks sometimes u still get your char as a flame.. and unlaoded ._.
            while (!Bot.ShouldExit && Bot.Player.LoggedIn && !Bot.Player.Loaded && Bot.Player.Playing && Bot.Map.Loaded)
                Core.Sleep(1500);

            Bot.Send.Packet($"%xt%zm%house%1%{Bot.Player.Username}%");
            Bot.Wait.ForMapLoad("house");

            Daily.WheelofDoom();
            Daily.MonthlyTreasureChestKeys();


            //Requierments:
            // Level 30
            Farm.Experience(30);

            // Two week old account
            string _output = Bot.Flash.GetGameObject("world.myAvatar.objData.dCreated")!;
            //"Fri Dec 3 08:32:00 GMT+0100 2021"
            string[] output = _output[1..^1].Split(' ');
            string[] time = output[3].Split(':');
            var creationDate = new DateTime(
                int.Parse(output[5]),
                Months.First(x => x.Key == output[1]).Value,
                int.Parse(output[2]),
                int.Parse(time[0]),
                int.Parse(time[1]),
                int.Parse(time[2]),
                DateTimeKind.Unspecified
                );
            double accountAgeInDays = DateTime.Now.Subtract(creationDate).TotalDays;
            if (accountAgeInDays < (double)14)
            {
                Core.Logger($"Account too young: {Core.Username()} ({accountAgeInDays}/14 days) - Skipping");
                warnings.Add($"- {Core.Username()}: account is too young ({accountAgeInDays}/14 days)");
                continue;
            }

            // Verified Email
            if (Bot.Flash.CallGameFunction<bool>("world.myAvatar.isEmailVerified"))
            {
                //Edit for future years quests vv
                // Participation Quest 9493
                CQ.ChillysParticipation();
                Bot.Wait.ForQuestComplete(9493);
            }
            else
            {
                Core.Logger($"Unverified Email: {Core.Username()} - Skipping");
                warnings.Add($"- {Core.Username()}: email is unverified ({Bot.Flash.GetGameObject("world.myAvatar.objData.strEmail")?[1..^1]})");
                continue;
            }
        }
        Bot.Events.ExtensionPacketReceived -= ACsListener;

        List<string> writeACs = new();
        writeACs.AddRange(newACs);
        foreach (var p in oldACs)
        {
            string name = p.Split(':').First();
            if (!writeACs.Any(x => x.StartsWith(name)))
                writeACs.Add(p);
        }
        Core.WriteFile(logPath, writeACs);

        if (newACs.Count == 0)
            Bot.ShowMessageBox($"We checked {Army.doForAllAccountDetails!.Length} accounts, but none of them have gained any {(firstTime ? "ACs" : "more ACs since last time")}." +
            $"{(warnings.Count > 0 ? "\n\nPlease be aware of the following things:\n" + string.Join('\n', warnings) : "")}",
            Bot.Random.Next(1, 100) == 100 ? "No Maidens" : "No ACs");
        else
            Bot.ShowMessageBox($"{newACs.Count} out of {Army.doForAllAccountDetails!.Length} accounts received ACs! Below you will find more details:\n\n" + string.Join('\n', ACs) +
            $"{(warnings.Count > 0 ? "\n\nPlease be aware of the following things:\n" + string.Join('\n', warnings) : "")}", "Got ACs!");

        void ACsListener(dynamic packet)
        {
            string type = packet["params"].type;
            dynamic data = packet["params"].dataObj;
            if (type is not null and "str")
            {
                string cmd = data[0];
                switch (cmd)
                {
                    case "server":
                        if (data[2] == null)
                            break;
                        string text = data[2].ToString();
                        if (text.Contains("AdventureCoins from other players. Happy Frostval!"))
                        {
                            int ac = int.Parse(text.Split(' ')[2]);
                            Core.Logger($"{Core.Username()} has received {ac} ACs!");
                            int acLog = int.Parse((oldACs.Find(x => x.StartsWith(Core.Username())) ?? "a:0").Split(':').Last()) + ac;

                            ACs.Add($"{Core.Username()}: +{ac} (received {acLog} ACs total)");
                            newACs.Add($"{Core.Username()}:{acLog}");
                        }
                        break;
                }
            }
        }
    }
    private readonly Dictionary<string, int> Months = new()
        {
            { "Jan", 1 },
            { "Feb", 2 },
            { "Mar", 3 },
            { "Apr", 4 },
            { "May", 5 },
            { "Jun", 6 },
            { "Jul", 7 },
            { "Aug", 8 },
            { "Sep", 9 },
            { "Oct", 10 },
            { "Nov", 11 },
            { "Dec", 12 }
        };
}
