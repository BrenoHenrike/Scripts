//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Other/Classes/LightCaster.cs
//cs_include Scripts/Other/Classes/LightMage.cs
//cs_include Scripts/Other/Weapons/AvatarOfDeathsScythe.cs
//cs_include Scripts/Other/Weapons/GuardianOfSpiritsBlade.cs
//cs_include Scripts/Other/Weapons/LanceOfTime.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAbezeth.cs
using Skua.Core.Interfaces;

public class ArmyLightCasterFull
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();
    public LightMage LM = new LightMage();
    public BurningBladeOfAbezeth BBOA = new BurningBladeOfAbezeth();
    public string OptionsStorage = "ArmyLightCasterFull";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sCore.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        FullLC();
        Core.SetOptions(false);
    }

    public void FullLC()
    {   
        Core.BankingBlackList.AddRange(Loot);
        bot.Options.RestPackets = false;
        /*Checking if missing burning blade or the other 3 quest swords from lostruinswar, didn't work without using item ids specifically*/
        if (!Core.CheckInventory(31058) || !Core.CheckInventory(30266) || 
            !Core.CheckInventory(31019) || !Core.CheckInventory(31028))
        {
            ///AggroPart();
            while (!Bot.ShouldExit && !Core.CheckInventory("Trapped Spirits", 500) && !Core.CheckInventory("Energy of Death", 500) && !Core.CheckInventory("Captured Time", 500))
                sArmy.SmartAggroMonStart("lostruinswar", monsters);
        }
        GetLC();
    }
    
    
    /*public void AggroPart()
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
        Core.EnsureAccept(4510, 4511, 4512);
        Army.AggroMonMIDs(2381,2382,2383,2386,2388,2390);
        Army.AggroMonStart("lostruinswar");
        Army.DivideOnCells("r2", "r3", "r4", "r5", "r6", "r7");

        while (!Bot.ShouldExit && !Core.CheckInventory("Trapped Spirits", 500) && !Core.CheckInventory("Energy of Death", 500) && !Core.CheckInventory("Captured Time", 500))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.EnsureComplete(4510, 4511, 4512);
    }*/

    public void GetLC()
    {
        if (Core.CheckInventory(38153))
            return;

        Core.AddDrop("LightCaster", "Aranx's Pure Light");
        LM.GetLM(false);

        Core.EquipClass(ClassType.Solo);
        Bot.Quests.UpdateQuest(6042);
        Core.EnsureAccept(6495);
        BBOA.GetBBoA();
        Adv.BoostHuntMonster("celestialarenad", "Aranx", "Aranx's Pure Light", isTemp: false);
        Core.EnsureComplete(6495);
        Bot.Wait.ForPickup("LightCaster");
        Adv.rankUpClass("LightCaster");
    }
    private string[] Loot = {"Trapped Spirits", "Energy of Death", "Captured Time"};
    private string[] monsters = {"Fallen Knight", "Infernal Imp", "Underworld Hound"};
}
