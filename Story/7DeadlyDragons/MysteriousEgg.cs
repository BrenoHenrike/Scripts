//cs_include Scripts/CoreBots.cs
using RBot;

public class MysteriousEgg
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetMysteriousEgg();

        Core.SetOptions(false);
    }

    public void GetMysteriousEgg()
    {
        if (Core.CheckInventory("Mysterious Egg"))
            return;

        Core.AddDrop("Mysterious Egg");
        Core.EnsureAccept(6171);

        Core.KillMonster("pride", "r13", "Left", "Valsarian", "Key of Pride", isTemp: false);
        Core.KillMonster("gluttony", "Enter2", "Top", "Deflated Glutus", "Key of Gluttony", isTemp: false);
        Core.KillMonster("greed", "r16", "Left", "Goregold", "Key of Greed", isTemp: false);

        if (!Core.CheckInventory("Key of Sloth"))
        {
            Core.EnsureAccept(5944);
            Core.GetMapItem(5380, map: "sloth");
            Core.GetMapItem(5381, map: "sloth");
            Core.EnsureComplete(5944);
            Core.JumpWait();
            Bot.SendPacket($"%xt%zm%equipItem%{Bot.Map.RoomID}%40710%");
            Core.HuntMonster("sloth", "Phlegnn", "Key of Sloth", isTemp: false);
        }

        Core.HuntMonster("lust", "Lascivia", "Key of Lust", isTemp: false);
        Bot.Quests.UpdateQuest(6000);
        Core.HuntMonster("maloth", "Maloth", "Key of Envy", isTemp: false);
        Core.HuntMonster("wrath", "Gorgorath", "Key of Wrath", isTemp: false);

        Core.EnsureComplete(6171, 42497);
        Bot.Wait.ForPickup("Mysterious Egg");
    }
}