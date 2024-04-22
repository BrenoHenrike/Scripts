/*
name: Fiend of Light
description: This script will obtain the items from Fiend of Light set that you choose.
tags: learn, from, the, past, fiend, light
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class FiendofLight
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv => new();
    private CoreSepulchure CoreSS = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "FiendOfLight";
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<RewardsSelection>("selectReward", "Choose Your Reward", "", RewardsSelection.All)
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        FiendOfLight();

        Core.SetOptions(false);
    }

    public void FiendOfLight()
    {
        RewardsSelection reward = Bot.Config!.Get<RewardsSelection>("RewardSelect");
        string[] chosenReward =
            reward == RewardsSelection.All ?
                Core.QuestRewards(6408) :
                new[] { Core.QuestRewards(6408).First(x => x == reward.ToString().ToLower().Replace('_', ' ')) };

        if (Core.CheckInventory(chosenReward))
            return;

        CoreSS.SepulchuresRise();

        while (!Bot.ShouldExit && !Core.CheckInventory(chosenReward))
        {
            Core.EnsureAccept(6408);
            Core.HuntMonster("darkplane", "Light Spirit", "Crystallized Memory", 10);
            Core.EnsureCompleteChoose(6408, chosenReward);
        }
    }

    public enum RewardsSelection
    {
        All,
        Fiend_of_Light = 44276,
        Fiend_of_Light_Helm = 44278,
        Fiend_of_Light_Hair = 44279,
        Fiend_of_Light_Winged_Hair = 44280,
        Fiend_of_Light_Blinded_Hair = 44281,
        Fiend_of_Light_Locks = 44282,
        Fiend_of_Light_Helm_Locks = 44283,
        Fiend_of_Light_Backblades = 44284,
        Fiend_of_Light_Wings = 44285,
        Fiend_of_Light_Wings_Tail = 44286,
        Fiend_of_Light_Tail = 44287,
        Fiend_of_Light_Blade = 44288,
        Doomed_Fiend_of_Light_Blade = 44289,
        Fiend_of_Light_Blades = 44290
    };
}
