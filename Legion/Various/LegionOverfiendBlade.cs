/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class LegionBlade
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    private string[] Pets = { "Paragon Fiend Quest Pet", "Shogun Dage Pet", "Shogun Paragon Pet", "Paragon Ringbearer" };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Blade();

        Core.SetOptions(false);
    }

    public void Blade()
    {
        string item = null;
        int questID = 0000;
        int quant1 = 1;
        int quant2 = 1;

        Petcheck();

        List<ItemBase> RewardOptions = Core.EnsureLoad(questID).Rewards;

        Core.AddDrop(RewardOptions.Select(x => x.Name).ToArray());
        Core.EquipClass(ClassType.Farm);
        foreach (ItemBase RewardOption in RewardOptions)
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(RewardOption.Name))
            {
                Core.EnsureAccept(questID);
                Core.HuntMonster("styx", "Sullen Soul", "Sullen Soul Received", quant1, log: false);
                Core.HuntMonster("styx", "Wrathful Soul", "Wrathful Soul Taken", quant2, log: false);
                Core.EnsureComplete(questID);
            }
            Core.Logger($"{RewardOption.Name} Obtainted.");
        }

        void Petcheck()
        {
            foreach (string Pet in Pets)
            {
                switch (Pet)
                {
                    case "Paragon Fiend Quest Pet":
                        if (!Core.CheckInventory(Pet))
                            break;
                        item = Pet;
                        questID = 6748;
                        quant1 = 1;
                        quant2 = 1;
                        Core.Logger($"Using {Pet} For the Quest.");
                        return;

                    case "Shogun Dage Pet":
                        if (!Core.CheckInventory(Pet))
                            break;
                        item = Pet;
                        questID = 5752;
                        quant1 = 1;
                        quant2 = 1;
                        Core.Logger($"Using {Pet} For the Quest.");
                        return;

                    case "Paragon Ringbearer":
                        if (!Core.CheckInventory(Pet))
                            break;
                        item = Pet;
                        questID = 7071;
                        quant1 = 1;
                        quant2 = 1;
                        Core.Logger($"Using {Pet} For the Quest.");
                        return;

                    case "Shogun Paragon Pet":
                        if (!Core.CheckInventory(Pet))
                            break;
                        item = Pet;
                        questID = 5751;
                        quant1 = 8;
                        quant2 = 10;
                        Core.Logger($"Using {Pet} For the Quest.");
                        return;
                }
            }
        }
    }
}
