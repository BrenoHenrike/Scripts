//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs

using Skua.Core.Interfaces;

public class DarkWitchyAndCurstedJester
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreMogloween CoreMogloween = new();


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetAll();

        Core.SetOptions(false);
    }

    public void GetAll()
    {
        List<Skua.Core.Models.Items.ItemBase> RewardOptions = Core.EnsureLoad(8375).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (Skua.Core.Models.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();
        Bot.Drops.Add(Rewards);

        if (Core.CheckInventory(Rewards, toInv: false))
            return;

        CoreMogloween.NecroCarnival();

        
        for (int i = 0; i < Rewards.Count(); i++)
        {
            if (!Core.CheckInventory(Rewards[i]))
            {
                //Scare Uniforms 8375
                Core.EnsureAccept(8375);

                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("necrocarnival", "Mooch Treeant", "Cherry Lemonade", 10, log: false);
                Core.HuntMonster("necrocarnival", "Gummy Tapeworm", "Crunchy Fried Clusters", 5, log: false);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("necrocarnival", "Deva", "Felt Patch", log: false);

                Core.EnsureComplete(8375);
                Core.JumpWait();
                Core.ToBank(Rewards);
            }
        }
    }
}
