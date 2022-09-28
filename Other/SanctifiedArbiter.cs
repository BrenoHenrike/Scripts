//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class SanctifiedArbiter
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8114).Rewards;
        List<string> RewardsList = new List<string>();
        List<string> RewardList = RewardOptions.Select(x => x.Name).ToList();
        string[] Rewards = RewardList.ToArray();

        if (Core.CheckInventory(Rewards))
            return;
        

        for (int i = 0; i < Rewards.Count(); i++)
        {
            if (!Core.CheckInventory(Rewards[i]))
            {
                Core.Logger($"Getting \"{Rewards[i]}\"");
                //One Holy Discovery 8114
                Core.EnsureAccept(8114);

                Core.EquipClass(ClassType.Farm);
                Farm.BattleUnderB("Bone Dust", 500);
                Core.HuntMonster("Doomwood", "Doomwood Ectomancer", "Raw Essence of the Undead", 10);
        
                Core.EnsureCompleteChoose(8114, new[] { Rewards[i] });
            }
            Core.ToBank(Rewards[i]);
        }
    }
}
