//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Other/Classes/LightMage.cs
//cs_include Scripts/Other/Weapons/AvatarOfDeathsScythe.cs
//cs_include Scripts/Other/Weapons/GuardianOfSpiritsBlade.cs
//cs_include Scripts/Other/Weapons/LanceOfTime.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAbezeth.cs
using RBot;
public class LightCaster
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public LightMage LM = new LightMage();
    public AvatarOfDeathsScythe AODS = new AvatarOfDeathsScythe();
    public GuardianOfSpiritsBlade GOSB = new GuardianOfSpiritsBlade();
    public LanceOfTime LOT = new LanceOfTime();
    public BurningBlade BB = new BurningBlade();
    public BurningBladeOfAbezeth BBOA = new BurningBladeOfAbezeth();

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
        GOSB.GetGoSB();
        AODS.GetAoDS();
        LOT.GetLoT();
        BB.GetBurningBlade();
        LM.GetLM(false);

        Core.EquipClass(ClassType.Solo);
        Bot.Quests.UpdateQuest(6042);
        Core.EnsureAccept(6495);
        BBOA.GetBBoA();
        Adv.BoostHuntMonster("celestialarenad", "Aranx", "Aranx's Pure Light", isTemp: false);
        Core.EnsureComplete(6495);
        Bot.Wait.ForPickup("LightCaster");

        if (rankUpClass)
            Adv.rankUpClass("LightCaster");
    }
}