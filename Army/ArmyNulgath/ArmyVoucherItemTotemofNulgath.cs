/*
name:  Army Totem And Gem
description:  uses an army to farm Totems Of Nulgath or Gem of Nulgath from "voucher item totem of nulgath"
tags: totems of nulgath, gem of nulgath, army, voucher item totem of nulgath
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyTotemAndGem
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreArmyLite Army = new();
    private CoreNation Nation = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyTotemAndGem";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        new Option<Rewards>("QuestReward", "Totems or Gems", "Select the reward to max stack first, will continue farming for the other later", Rewards.TotemofNulgath),
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();

        Setup(Bot.Config.Get<Rewards>("QuestReward"));

        Core.SetOptions(false);
    }

    public void Setup(Rewards reward)
    {
        Nation.FarmVoucher(false);

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);

        if (reward.ToString() == "TotemofNulgath")
        {
            Core.Logger("Totems Of Nulgath selected, farming max Totems first - then Gems");
            Totems();
        }
        else if (reward.ToString() == "GemofNulgath")
        {
            Core.Logger("Gems Of Nulgath selected, farming max Gems first - then Totems");
            Gems();
        }
    }

    void Totems(int quant = 100)
    {
        if (Core.CheckInventory("Totem of Nulgath", quant))
            Gems();
        else if (Core.CheckInventory("Gem of Nulgath", 300))
            Slave();

        Core.FarmingLogger("Totem of Nulgath", quant);
        Army.SmartAggroMonStart("tercessuinotlim", "Dark Makai");
        while (!Bot.ShouldExit && !Core.CheckInventory("Totem of Nulgath", quant))
        {
            Bot.Combat.Attack("*");
            if (Core.CheckInventory("Essence of Nulgath", 65))
                Core.EnsureComplete(4778, (int)Rewards.TotemofNulgath);
        }
        Core.Jump(Bot.Player.Cell);
        if (!Core.CheckInventory("Gem of Nulgath", 300))
            Gems();
        else Slave();
    }

    void Gems(int quant = 300)
    {
        if (Core.CheckInventory("Gem of Nulgath", quant))
            Totems();
        else if (Core.CheckInventory("Totem of Nulgath", 100))
            Slave();

        Core.FarmingLogger("Gem of Nulgath", quant);
        Army.SmartAggroMonStart("tercessuinotlim", "Dark Makai");
        while (!Bot.ShouldExit && !Core.CheckInventory("Gem of Nulgath", quant))
        {
            Bot.Combat.Attack("*");
            if (Core.CheckInventory("Essence of Nulgath", 65))
                Core.EnsureComplete(4778, (int)Rewards.TotemofNulgath);
        }
        Core.Jump(Bot.Player.Cell);

        if (!Core.CheckInventory("Totem of Nulgath", 100))
            Totems();
        else Slave();
    }

    void Slave()
    {
        Core.Logger("You have max stack of Totem and Gem, armying for the squad");
        Army.SmartAggroMonStart("tercessuinotlim", "Dark Makai");
        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");
    }

    public enum Rewards
    {
        TotemofNulgath = 5357,
        GemofNulgath = 6136
    }

    private string[] Loot =
    {
        "Totem of Nulgath",
        "Gem of Nulgath",
        "Essence of Nulgath"
    };
}
