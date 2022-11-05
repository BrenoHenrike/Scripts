//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
using Skua.Core.Interfaces;

public class LegionDoomKnight
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreAdvanced Adv => new();
    public CoreSDKA SDKA = new();

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

        if (!Core.CheckInventory("Sepulchure's DoomKnight Armor"))
            SDKA.DoAll();

        Core.RegisterQuests(4099);
        Core.HuntMonster("sepulchure", "Dark Sepulchure", "Dark Sepulchure's Badge", 100, isTemp: false);
        Core.CancelRegisteredQuests();

        Core.BuyItem("battleon", 1106, "Legion DoomKnight");

        if (rankUpClass)
            Adv.rankUpClass("Legion DoomKnight");
    }
}
