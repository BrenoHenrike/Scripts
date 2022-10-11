//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/LuckyDay/Pooka.cs
using Skua.Core.Interfaces;

public class PookasGiftsMerge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public PookaStory Pooka = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetGiftItems();

        Core.SetOptions(false);
    }

    public void GetGiftItems()
    {
        if (!Core.isSeasonalMapActive("pooka"))
            return;
        Core.AddDrop("Golden Clover", "Golden Oak Leaf", "Enchanted Gold");

        Pooka.CompletePooka();

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { "Golden Clover", "Golden Oak Leaf", "Enchanted Gold" }, 100))
        {
            //Golden Clover
            if (!Core.CheckInventory("Golden Clover", 100))
            {
                Core.EnsureAccept(7963);
                Core.HuntMonster("pooka", "Sneevilchaun", "Lucky Clover", 10);
                Core.EnsureComplete(7963);
            }

            //Golden Oak Leaf
            if (!Core.CheckInventory("Golden Oak Leaf", 100))
            {
                Core.EnsureAccept(7964);
                Core.EnsureAccept(7965);
                Core.HuntMonster("pooka", "Lucky Treeant", "Oak Leaf", 6);
                Core.EnsureComplete(7964);
            }

            //Enchanted Gold
            if (!Core.CheckInventory("Enchanted Gold", 100))
            {
                Core.HuntMonster("pooka", "Lucky Treeant", "Treeant Wood", 5);
                Core.HuntMonster("pooka", "Faerie", "Can of Gold Paint");
                Core.EnsureComplete(7965);
            }
        }
    }
}