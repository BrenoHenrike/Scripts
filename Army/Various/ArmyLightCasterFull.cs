/*
name: Light Caster (Army)
description: Farms LightCaster class using your army.
tags: army, light, caster, mage, class, doall
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAbezeth.cs
//cs_include Scripts/Other/Classes/LightCaster.cs
//cs_include Scripts/Other/Classes/LightMage.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/AvatarOfDeathsScythe.cs
//cs_include Scripts/Other/Weapons/GuardianOfSpiritsBlade.cs
//cs_include Scripts/Other/Weapons/LanceOfTime.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyLightCaster
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public LightMage LM = new LightMage();
    public BurningBladeOfAbezeth BBOA = new BurningBladeOfAbezeth();
    public AvatarOfDeathsScythe AODS = new AvatarOfDeathsScythe();
    public GuardianOfSpiritsBlade GOSB = new GuardianOfSpiritsBlade();
    public LanceOfTime LOT = new LanceOfTime();
    public BurningBlade BB = new BurningBlade();
    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLightCaster";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("sellToSync", "Sell to Sync", "Sell items to make sure the army stays syncronized.\nIf off, there is a higher chance your army might desyncornize", false),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Aranx's Pure Light, Lightcaster, Lightmage, Burning Blade, Burning Blade of Abezeth, Guardian of Spirits' Blade, Lance of Time, Avatar Of Death's Scythe" });
        Core.SetOptions();

        LightCaster();

        Core.SetOptions(false);
    }

    public void LightCaster()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Bot.Events.PlayerAFK += PlayerAFK;
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop(38153, 31058, 30266, 31019, 31028);

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { 38153, 31058 }))
        {
            Core.EnsureAccept(4510, 4511, 4512);
            if (!Core.CheckInventory(30266))
                ArmyHunt("lostruinswar", new[] { "Fallen Knight" }, "Trapped Spirits", ClassType.Farm, isTemp: false, 500);
            if (!Core.CheckInventory(31019))
                ArmyHunt("lostruinswar", new[] { "Infernal Imp" }, "Energy of Death", ClassType.Farm, isTemp: false, 500);
            if (!Core.CheckInventory(31028))
                ArmyHunt("lostruinswar", new[] { "Underworld Hound" }, "Captured Time", ClassType.Farm, isTemp: false, 500);
            BB.GetBurningBlade();
            LM.GetLM(true);
            Bot.Quests.UpdateQuest(6042);
            Core.EnsureAccept(6495);
            BBOA.GetBBoA();
            Core.Logger("\"Aranx\" is  Solo only boss, cannot part.");
            Adv.BoostHuntMonster("celestialarenad", "Aranx", "Aranx's Pure Light", isTemp: false);
            Core.EnsureComplete(6495);
            Bot.Wait.ForPickup("LightCaster");
            Adv.tempNameHere("LightCaster");
        }
        Bot.Events.PlayerAFK -= PlayerAFK;
    }


    void ArmyHunt(string map, string[] monsters, string item, ClassType classType, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);

        Core.EquipClass(classType);
        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }

    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }

}
