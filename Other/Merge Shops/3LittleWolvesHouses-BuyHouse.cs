//cs_include Scripts/CoreBots.cs
using RBot;

public class LittleWolvesHousesMerge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetQuestItems();

        Core.SetOptions(false);
    }

    public void GetQuestItems()
    {
        //Needed AddDrop
        Core.AddDrop("Building Material", "Foundation Material", "Decor Material");

        while (!Core.CheckInventory(new[] {"Building Material", "Foundation Material", "Decor Material"}, 100))
        {
            //Building Material - Building Supplies
            if (!Core.CheckInventory("Building Material", 100))
            {
                Core.EnsureAccept(6915);
                Core.HuntMonster("farm", "Treeant", "Wooden Planks", 5);
                Core.HuntMonster("bloodtusk", "Rhison", "Glue");
                Core.HuntMonster("crashsite", "ProtoSartorium", "Nails", 10);
                Core.EnsureComplete(6915);
            }

            //Foundation Material - Cover Your Bases
            if (!Core.CheckInventory("Foundation Material", 100))
            {
                Core.EnsureAccept(6916);
                Core.HuntMonster("river", "Zardman Fisher", "River Stones", 5);
                Core.HuntMonster("dwarfprison", "Balboa", "Boulder", 3);
                Core.HuntMonster("dragonplane", "Earth Elemental", "Marble");
                Core.HuntMonster("gilead", "Fire Elemental", "Flames", 3);
                Core.EnsureComplete(6916);
            }

            //Decor Material - Decor
            if (!Core.CheckInventory("Decor Material", 100))
            {
                Core.EnsureAccept(6917);
                Core.HuntMonster("farm", "Scarecrow", "Fabric", 5);
                Core.HuntMonster("goose", "Can of Paint", "Paint", 5);
                Core.HuntMonster("undergroundlabb", "Window", "Glass", 5);
                Core.EnsureComplete(6917);
            }
        }
    }
}