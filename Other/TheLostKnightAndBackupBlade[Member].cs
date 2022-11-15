//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Friday13th/CoreFriday13th.cs
using Skua.Core.Interfaces;

public class TheLostKnightAndBackupBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreFriday13th CoreFriday13th = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {

        string[] AllRewards = (Core.EnsureLoad(7401).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(7403).Rewards.Select(i => i.Name)).Concat(Core.EnsureLoad(7405).Rewards.Select(i => i.Name)).ToArray();

        if (Core.CheckInventory(AllRewards, toInv: false))
            return;
            
        CoreFriday13th.Splatterwar();

        Bot.Drops.Add(AllRewards);

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(7401, 7403, 7405);
        while (!Bot.ShouldExit && !Core.CheckInventory(AllRewards, toInv: false))
            //Legion Medals 7401   //Mega Legion Medals 7403 //Jagged Canines 7405
            Core.KillMonster("splatterwarshrade", "r3", "Right", "*", log: false);
        Core.JumpWait();
        Core.ToBank(AllRewards);
        Core.CancelRegisteredQuests();

    }
}