//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class MysteriousEgg
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

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

        Story.PreLoad();
        Core.AddDrop("Mysterious Egg");

        Core.EnsureAccept(6171);

        Core.KillMonster("pride", "Valsarian", "r13", "Left", "Key of Pride", isTemp: false);
        Core.KillMonster("gluttony", "Enter2", "Top", "Deflated Glutus", "Key of Gluttony", isTemp: false);
        Core.KillMonster("greed", "Goregold", "r16", "Left", "Key of Greed", isTemp: false);

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

        Core.EnsureCompleteChoose(6171, new[] { "Mysterious Egg" });
    }
}