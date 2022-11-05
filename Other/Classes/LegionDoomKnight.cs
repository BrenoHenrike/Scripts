//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class LegionDoomKnight
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv => new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetLDK();

        Core.SetOptions(false);
    }

    public void GetLDK(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Legion DoomKnight"))
            return;

        Core.RegisterQuests(4099);
        while (!Bot.ShouldExit && Core.CheckInventory("Dark Sepulchure's Badge", 100))
            Adv.BoostHuntMonster("sepulchure", "Dark Sepulchure", "Dark Sepulchure's Badge", 100, isTemp: false);

        Core.CancelRegisteredQuests();

        Core.BuyItem("battleon", 1106, "Legion DoomKnight");

        if (rankUpClass)
            Adv.rankUpClass("Legion DoomKnight");
    }
}
