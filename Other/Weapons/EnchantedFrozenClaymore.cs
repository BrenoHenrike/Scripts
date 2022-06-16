//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Akriloth.cs
using RBot;

public class EnchantedFrozenClaymore
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public Akriloth Akriloth = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetSword();

        Core.SetOptions(false);
    }

    public void GetSword()
    {
        if (Core.CheckInventory("Enchanted Frozen Claymore"))
            return;

        Akriloth.Storyline();
        Core.EquipClass(ClassType.Solo);

        Core.AddDrop('Ice Shard');
        
        while (!Core.CheckInventory(43712, 50))
        {
            Core.EnsureAccept(6311);
            Core.HuntMonster("northmountain", "Izotz", "Ice Crystal");
            Core.EnsureComplete(6311);
            Bot.Wait.ForPickup("Ice Shard");
        }
        Core.BuyItem("northmountain", 1595, "Enchanted Frozen Claymore");
    }
}
