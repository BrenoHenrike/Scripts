/*
name: MoglinPunter
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class MoglinPunter
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        Bot.Options.LagKiller = false;
        if (Core.HasWebBadge(badge) || !Core.isSeasonalMapActive("punt"))
        {
            Core.Logger($"Already have the {badge} badge, or the map is not available.");
            return;
        }
        Core.OneTimeMessage("Minigame Explanation", "This minigame works off of a \"value\" system for ponts, so 9999 is 99, for the quest so youll need to get a value of 10000 points which may take a while.", forcedMessageBox: true);


        int Punt = 1;
        double lowestScore = 100;

        Core.Logger($"Doing quest for {badge} badge, Purely Rng based, good luck");
        Core.Join("punt");
        Bot.Events.ExtensionPacketReceived += puntingPacketReader;
        while (!Bot.ShouldExit && !Core.HasWebBadge(badge))
        {
            Core.Jump("Enter", "Spawn");
            Core.Sleep();
            Core.SendPackets("%xt%zm%ia%1%rval%btnPuntting%%");
            Bot.Wait.ForCellChange("Punt");
            Core.Sleep(3500);
            if (Core.CheckInventory("Twilly Be Punted"))
            {
                Core.ChainComplete(8532);
                Core.Logger($"Punts to get the badge: {Punt}");
                return;
            }
        }
        Bot.Events.ExtensionPacketReceived -= puntingPacketReader;

        void puntingPacketReader(dynamic packet)
        {
            string type = packet["params"].type;
            dynamic data = packet["params"].dataObj;
            if (type is not null and "json")
            {
                string cmd = data.cmd.ToString();
                switch (cmd)
                {
                    case "ia":
                        if (data.oName.ToString() == "btnPuntting" && data.unm.ToString() == Core.Username())
                        {
                            double score = data.val;
                            score = Math.Round(float.Parse($"{score.ToString()[..^2]}.{score.ToString()[^2..]}"));
                            lowestScore = lowestScore > score ? score : lowestScore;
                            Core.Logger($"Punt: #{Punt++} | Score: {score}");
                        }
                        break;
                }
            }
        }
    }
    private string badge = "Moglin Punter";

}
