/*
name: Kiss The Void
description: This bot will farm Blood Gem of the Archfiend and also the Betrayals (if needed)
tags: betrayal, blood, gem, archfiend
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class KisstheVoid
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreNation Nation = new();
    
    public string OptionsStorage = "Kiss_The_Void";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<int>("22332", "Blood Gem of the Archfiend", "How many \"Blood Gem of the Archfiend\" you need? 0 = none", 100),
        new Option<bool>("25150", "1st Betrayal Blade of Nulgath", "Should the bot farm \"1st Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25151", "2nd Betrayal Blade of Nulgath", "Should the bot farm \"2nd Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25152", "3rd Betrayal Blade of Nulgath", "Should the bot farm \"3rd Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25153", "4th Betrayal Blade of Nulgath", "Should the bot farm \"4th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25154", "5th Betrayal Blade of Nulgath", "Should the bot farm \"5th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25155", "6th Betrayal Blade of Nulgath", "Should the bot farm \"6th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25156", "7th Betrayal Blade of Nulgath", "Should the bot farm \"7th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25238", "8th Betrayal Blade of Nulgath", "Should the bot farm \"8th Betrayal Blade of Nulgath\"?", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        FarmSelected();

        Core.SetOptions(false);
    }

    private void FarmSelected() {
        // Farm selected Betrayals of Nulgath
        Options.ForEach(x => Betrayal(x));

        // Farm the Blood Gems last
        int bloodGems = Bot.Config.Get<int>("22332");
        if (bloodGems > 0) Nation.KisstheVoid(bloodGems);
    }

    private void Betrayal(IOption details) {
        string itemId = details.Name;
        if (itemId == "22332") return;

        bool farm = Bot.Config.Get<bool>(itemId);
        if (!farm) return;

        string itemName = details.DisplayName;
        if (Core.CheckInventory(itemName)) {
            Core.Logger($"You already own the \"{itemName}\".");
            return;
        }

        Nation.KisstheVoid(0, itemName);
    }
}
