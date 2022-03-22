//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs

using RBot;

public class WeaponImprint
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreDailys Dailys = new CoreDailys();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetWeaponImprint();

        Core.SetOptions(false);
    }

    public void GetWeaponImprint(int Imprintquant = 15)
    {
        if (Core.CheckInventory("weapon imprint", Imprintquant))
            return;

        Core.AddDrop("weapon imprint");

        if (!Core.CheckInventory("weapon imprint", Imprintquant))
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Weapon Imprint", Imprintquant, false);
    }
}