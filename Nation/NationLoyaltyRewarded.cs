//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/CoreAdvanced.cs

using Skua.Core.Interfaces;

public class NationLoyaletyRewarded
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FarmQuest();

        Core.SetOptions(false);
    }

    public void FarmQuest()
    {
        string[] Rewards = (Core.EnsureLoad(4749).Rewards.Select(i => i.Name).ToArray());
        Core.AddDrop(Rewards);

        Nation.NationRound4Medal();
        Nation.FarmUni13(1);

        for (int i = 0; i < Rewards.Length; i++)
        {
            if (Bot.Inventory.IsMaxStack(Rewards[i]))
                Core.Logger($"{Rewards[i]} is max stack Checking next item in the \"Time is Money\" Quest's Rewards");
            else
            {
                Core.RegisterQuests(4749);
                while (!Bot.Inventory.IsMaxStack(Rewards[i]))
                {
                    //Nation Loyalty Rewarded 4749
                    Core.EquipClass(ClassType.Solo);
                    Adv.BestGear(GearBoost.Chaos);
                    Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Eye", publicRoom: true);
                    Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle", publicRoom: true);
                    Core.KillMonster("dflesson", "r12", "Right", "Fluffy the Dracolich", "Fluffy's Bones", publicRoom: true);
                    Adv.BestGear(GearBoost.Dragonkin);
                    Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang", publicRoom: true);
                    Adv.BestGear(GearBoost.Human);
                    Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade", publicRoom: true);
                    Core.EquipClass(ClassType.Farm);
                    Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Defeated Makai", 25, false);
                    Bot.Wait.ForPickup(Rewards[i]);
                }
            }
            Core.CancelRegisteredQuests();
        }
    }
}