//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Other/Classes/LightMage.cs
//cs_include Scripts/Other/Weapons/AvatarOfDeathsScythe.cs
//cs_include Scripts/Other/Weapons/GuardianOfSpiritsBlade.cs
//cs_include Scripts/Other/Weapons/LanceOfTime.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAzabeth.cs
using RBot;
public class LightCaster
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public LightMage LM = new LightMage();
    public AvatarOfDeathsScythe AODS = new AvatarOfDeathsScythe();
    public GuardianOfSpiritsBlade GOSB = new GuardianOfSpiritsBlade();
    public LanceOfTime LOT = new LanceOfTime();
    public BurningBlade BB = new BurningBlade();
    public BurningBladeOfAzabeth BBOA = new BurningBladeOfAzabeth();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetLC();

        Core.SetOptions(false);
    }

    public void GetLC(bool rankUpClass = true)
    {
        if (Core.CheckInventory("LightCaster"))
            return;

        Core.AddDrop("LightCaster", "Aranx's Pure Light");

        Farm.Experience(80);
        BB.GetBurningBlade();
        if (!Core.CheckInventory("Guardian of Spirits' Blade"))
            Core.EnsureAccept(4510);
        if (!Core.CheckInventory("Avatar Of Death's Scythe"))
            Core.EnsureAccept(4511);
        if (!Core.CheckInventory("Lance of Time"))
            Core.EnsureAccept(4512);
        AODS.GetAoDS();
        GOSB.GetGoSB();
        LOT.GetLoT();
        LM.GetLM(false);

        Core.EnsureAccept(6495);
        Core.EquipClass(ClassType.Solo);
        BBOA.GetBBoA(); //Put that in here because its basically both killing the same mod anyway
        Core.HuntMonster("celestialareand", "Aranx", "Aranx's Pure Light", isTemp: false);
        Core.EnsureComplete(6495);
        Bot.Wait.ForPickup("LightCaster");

        if (rankUpClass)
            Farm.rankUpClass("LightCaster");
    }
}