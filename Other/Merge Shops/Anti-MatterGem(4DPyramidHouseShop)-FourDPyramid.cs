//cs_include Scripts/CoreBots.cs

using RBot;

public class FourDPyramidHouseShop
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetMergeItems();

        Core.SetOptions(false);
    }

    public void GetMergeItems()
    {
        //Needed AddDrop
        Core.AddDrop("Anti-Matter Gem");

        while (!Core.CheckInventory("Anti-Matter Gem", 500))
        {
            //Reflections of Victory
            Core.EnsureAccept(5188);
            Core.HuntMonster("whitehole", "Dimensional Crystal", "Crystal Shards", 5);
            Core.EnsureComplete(5188);
            
        }
    }
}