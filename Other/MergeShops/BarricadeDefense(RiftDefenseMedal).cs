//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Story/QueenofMonsters/BookofMonstersTheDestroyer(Extriki)/CoreExtriki.cs
//cs_include Scripts/CoreStory.cs

using RBot;

public class BarricadeDefenseMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreExtriki Extriki => new();
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.Add("Rift Defense Medal");

        Core.SetOptions();

        GetItems();

        Core.SetOptions(false);
    }

    public void GetItems()
    {
        if (!Story.QuestProgression(5824))
        {
            Extriki.TheRift();
            Extriki.CharredPath();
        }

        Core.AddDrop("Rift Defense Medal");

        Core.Logger($"Hunting For: Rift Defense Medal, {Bot.Inventory.GetQuantity("Rift Defense Medal")}/500");
        while (!Core.CheckInventory("Rift Defense Medal", 500))
        {
            Core.EnsureAccept(5825);
            Core.HuntMonster("charredpath", "Infected Hare|Ravenous Parasite", "Invader Slain", 10, log: false);
            Core.EnsureComplete(5825);
            Core.Logger($"Rift Defense Medal: {Bot.Inventory.GetQuantity("Rift Defense Medal")}/500");
        }
    }
}