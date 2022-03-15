//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/LuckyDay/Pooka.cs
using RBot;

public class PookasGiftsMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public PookaStory Pooka = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetGiftItems();

        Core.SetOptions(false);
    }

    public void GetGiftItems()
    {
        Core.AddDrop("Golden Clover", "Golden Oak Leaf", "Enchanted Gold");

        Pooka.CompletePooka();

        while (!Core.CheckInventory(new[] { "Golden Clover", "Golden Oak Leaf", "Enchanted Gold" }, 100))
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