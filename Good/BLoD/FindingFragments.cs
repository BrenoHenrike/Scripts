/*
name: Finding Fragments
description: Does the "Finding Fragments with Blinding [weapon] of Destiny" for the quest rewards.
tags: finding fragments, blinding light fragments, spirit, orb, loyal, bright, brilliant, blinding, aura
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Items;

public class FindingFragments_Any
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreBLOD BLOD = new();
    private CoreStory Story = new();

    public string OptionStorage = "Finding_FragmentsV2";
    public List<IOption> Options = new()
    {
        new Option<WeaponOfDestiny>("questID", "Weapon Type", "Select which quest variant you want to do", WeaponOfDestiny.Blade)
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Blinding Light Fragments", "Spirit Orb", "Loyal Spirit Orb", "Bright Aura", "Brilliant Aura", "Blinding Aura" });
        Core.SetOptions();

        FindingFragments();

        Core.SetOptions(false);
    }

    public void FindingFragments()
    {
        foreach (ItemBase item in Core.EnsureLoad((int)Bot.Config!.Get<WeaponOfDestiny>("questID")).Rewards)
        {
            BLOD.FindingFragments(Bot.Config.Get<WeaponOfDestiny>("questID"), item.Name, item.MaxStack);
            Core.Logger($"{item.Name} has reached max stack ({item.MaxStack})");
        }
    }
}
