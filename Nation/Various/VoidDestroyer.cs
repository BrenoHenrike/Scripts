//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using RBot;

public class VoidDestroyer
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNation Nation = new();

    public readonly string[] Rewards =
    {
        "Void Destroyer",
        "Void Destruction Blade",
        "Void Spear of War",
        "Horned Void War Helm",
        "Crested Void War Helm",
        "Wrap of the Void",
        "Tainted Destruction Blade",
        "Toxic Void Katana",
        "Dual Toxic Void Katanas"
    };
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetDestroyer();

        Core.SetOptions(false);
    }

    public void GetDestroyer()
    {
        int i = 1;
        while (!Bot.ShouldExit() && !Core.CheckInventory(Rewards, toInv: false))
        {
            Nation.Supplies("Unidentified 4");
            Nation.SwindleBulk(1);
            Nation.FarmDarkCrystalShard(1);
            Nation.EssenceofNulgath(1);
            Nation.FarmGemofNulgath(1);

            Core.ChainComplete(5661);
            Bot.Player.Pickup(Rewards);
            Core.Logger($"Completed x{i++}");
        }
    }
}