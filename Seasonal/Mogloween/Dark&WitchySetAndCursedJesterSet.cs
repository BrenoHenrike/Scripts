/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Mogloween/CoreMogloween.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

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
        // List<string> RewardsList = new List<string>();
        List<ItemBase> RewardOptions = Core.EnsureLoad(8375).Rewards.ToList();
        // RewardsList.AddRange(Core.EnsureLoad(8375).Rewards.Select(x => x.Name).ToArray());
        Bot.Drops.Add(Core.EnsureLoad(8375).Rewards.Select(x => x.ID).ToArray());

        CoreMogloween.NecroCarnival();

        foreach (ItemBase item in RewardOptions)
        {
            // if (item.Name.Contains($"&"))
            //     item.Name.Replace($"&", "and");
            // if (item.Name.Contains($"&amp;"))
            //     item.Name.Replace($"&amp;", "and");

            if (!Core.CheckInventory(item.ID, toInv: false))
            {
                Core.FarmingLogger($"{item.Name}", 1);

                //Scare Uniforms 8375
                Core.EnsureAccept(8375);

                Core.EquipClass(ClassType.Farm);
                Core.HuntMonster("necrocarnival", "Mooch Treeant", "Cherry Lemonade", 10, log: false);
                Core.HuntMonster("necrocarnival", "Gummy Tapeworm", "Crunchy Fried Clusters", 5, log: false);
                Core.EquipClass(ClassType.Solo);
                Core.HuntMonster("necrocarnival", "Deva", "Felt Patch", log: false);

                Core.EnsureComplete(8375, item.ID);
                Bot.Wait.ForPickup(item.ID);
            }
            else Core.Logger($"{item.Name} Found");
            Core.ToBank(item.Name);
        }
    }
}
