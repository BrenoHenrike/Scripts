//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class AspiringNecromancer
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example()
    {
        
        string[] Quest1Rewards = (Core.EnsureLoad(7751).Rewards.Select(i => i.Name).ToArray());
        string[] Quest2Rewards = (Core.EnsureLoad(7752).Rewards.Select(i => i.Name).ToArray());
        string[] Quest3Rewards = (Core.EnsureLoad(7753).Rewards.Select(i => i.Name).ToArray());
        string[] AllRewards = Quest1Rewards.Concat(Quest2Rewards.Concat(Quest3Rewards)).ToArray();

        if (Core.CheckInventory(AllRewards, toInv: false))
            return;

        Bot.Drops.Add(AllRewards);

        //I Want to Be The Very Best Necromancer 7751
        Core.RegisterQuests(7751);
        while (!Bot.ShouldExit && !Core.CheckInventory(Quest1Rewards, toInv: false))
            Core.HuntMonster("BattleunderA", "Skeletal Warrior", "Skeleton Captured", 10, log: false);
        Core.JumpWait();
        Core.CancelRegisteredQuests();
        Core.ToBank(AllRewards);
        
        //Like No One Ever Was 7752
        Core.RegisterQuests(7752);
        while (!Bot.ShouldExit && !Core.CheckInventory(Quest2Rewards, toInv: false))
            Core.HuntMonster("DoomWood", "Doomwood Bonemuncher", "Bones Collected", 15, log: false);
        Core.JumpWait();
        Core.CancelRegisteredQuests();
        Core.ToBank(AllRewards);

        
        Bot.Quests.UpdateQuest(2060);
        Bot.Quests.UpdateQuest(3019);
        //To Raise Them is my Real Quest 7753
        Core.RegisterQuests(7753);
        while (!Bot.ShouldExit && !Core.CheckInventory(Quest3Rewards, toInv: false))
        {
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Dracolich Head", log: false, publicRoom: true);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Yet Another Dracolich Head", log: false, publicRoom: true);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "More Dracolich Heads", log: false, publicRoom: true);
            Core.HuntMonster("underrealm", "Agony", "Fresh Agony Wraps", 5, log: false);
        }
        Core.JumpWait();
        Core.CancelRegisteredQuests();
        Core.ToBank(AllRewards);
    }
}
