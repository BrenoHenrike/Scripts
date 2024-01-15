/*
name: KisstheVoid
description: This bot will farm Blood Gem of the Archfiend and also the Betrayals (if selected)
tags: betrayal, blood, gem, archfiend
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using Skua.Core.Models;
using Skua.Core.Options;
using Skua.Core.Utils;

public class KisstheVoid
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public string[] MultiOptions = { "Generic", "Select" };
    public string OptionsStorage = "Kiss_The_Void";
    public List<IOption> Select = new()
    {
        new Option<bool>("25150", "1st Betrayal Blade of Nulgath", "Should the bot farm \"1st Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25151", "2nd Betrayal Blade of Nulgath", "Should the bot farm \"2nd Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25152", "3rd Betrayal Blade of Nulgath", "Should the bot farm \"3rd Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25153", "4th Betrayal Blade of Nulgath", "Should the bot farm \"4th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25154", "5th Betrayal Blade of Nulgath", "Should the bot farm \"5th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25155", "6th Betrayal Blade of Nulgath", "Should the bot farm \"6th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25156", "7th Betrayal Blade of Nulgath", "Should the bot farm \"7th Betrayal Blade of Nulgath\"?", false),
        new Option<bool>("25238", "8th Betrayal Blade of Nulgath", "Should the bot farm \"8th Betrayal Blade of Nulgath\"?", false),
    };
    public CoreNation Nation = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Kiss();

        Core.SetOptions(false);
    }

    private void Kiss() {
        // Farm selected Betrayals of Nulgath
        Select.ForEach(x => Betrayal(x));

        // Just in case the stack is not maxed out
        Nation.KisstheVoid(100);
    }

    private void Betrayal(IOptions details) {
        Core.Logger(details);
        Core.Logger($"ID: {details.Name}, Name: {details.DisplayName}");

        string itemId = details.Name;
        string itemName = details.DisplayName;
        bool farm = Bot.Config.Get<bool>(itemId);

        if (!farm) return;

        if (Core.CheckInventory(itemName)) {
            Core.Logger($"You already own the \"{itemName}\".");
            return;
        }

        Nation.KisstheVoid(0, itemName);
    }
}
