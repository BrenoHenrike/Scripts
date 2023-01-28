/*
name:  Army Light Caster
description:  
tags: army, lightcaster, lightmage, class, 0file, doall
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
        new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1),
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
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop(38153, 31058, 30266, 31019, 31028);

        while (!Bot.ShouldExit && !Core.CheckInventory(new[] { 38153, 31058 }))
        {
            Core.EnsureAccept(4510);
            Core.EnsureAccept(4511);
            Core.EnsureAccept(4512);
            if (!Core.CheckInventory(30266))
                ArmyThing(4510, "lostruinswar", new[] { "Fallen Knight" }, "Trapped Spirits", false, 500);
            if (!Core.CheckInventory(31019))
                ArmyThing(4511, "lostruinswar", new[] { "Infernal Imp" }, "Energy of Death", false, 500);
            if (!Core.CheckInventory(31028))
                ArmyThing(4512, "lostruinswar", new[] { "Underworld Hound" }, "Captured Time", false, 500);
            Core.EquipClass(ClassType.Solo);
            BB.GetBurningBlade();
            LM.GetLM(true);
            Bot.Quests.UpdateQuest(6042);
            Core.EnsureAccept(6495);
            BBOA.GetBBoA();
            Adv.BoostHuntMonster("celestialarenad", "Aranx", "Aranx's Pure Light", isTemp: false);
            Core.EnsureComplete(6495);
            Bot.Wait.ForPickup("LightCaster");
            Adv.rankUpClass("LightCaster");
        }
    }

    void ArmyThing(int questID, string map = null, string[] monsters = null, string item = null, bool isTemp = false, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
            return;

        if (item == null)
            return;

        Bot.Drops.Add(item);

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Core.Join(map);
        WaitCheck();
        Core.EnsureAccept(questID);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);
        Core.EnsureComplete(questID);
    }

    void WaitCheck()
    {
        while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(5000);
        }
        Core.Logger($"Squad All Gathered [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
    }

}
