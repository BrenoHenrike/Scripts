/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class BestGearAttemptX
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();

    public string OptionsStorage = "BestGearAttemptX";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<bool>("Restore Equipment After", "GearRestore", "This will Store Your Current Gear *before* swapping so it can swap back afterwards", false),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        EquipBestItemsForMetaTest();

        Core.SetOptions(false);
    }

    public void EquipBestItemsForMetaTest()
    {
        Bot.ShowMessageBox("For the moment its purely manaul input from me with it being 90% dmg all with some gold/xp/classpoints MetaTypes (MetaTypes = Boost Type)", "READ ME");

        Core.Join("Battleon-999999");

        if (Bot.Config.Get<bool>("Restore Equipment After"))
            Adv.GearStore();

        Core.EquipBestItemsForMeta(new Dictionary<string, string[]>
        {
            { "Cape", new[] { "dmgAll", "Undead", "Chaos", "Elemental", "Dragonkin", "Human", "gold", "cp", "rep" } },
            { "Helm", new[] { "dmgAll", "Undead", "Chaos", "Elemental", "Dragonkin", "Human", "gold", "cp", "rep" } },
            { "Armor", Core.CheckInventory("Polly Roger") ?
                new[] { "gold", "cp", "rep" } :
                new[] { "dmgAll", "gold", "cp", "rep" } },
            { "Weapon", new[] { "dmgAll", "gold", "cp", "rep" } }, // Special case: includes all weapon types
            { "Pet", new[] {
                "Undead", "Chaos", "Elemental", "Dragonkin", "Human",
                "gold", "cp", "rep", "dmgAll" } }
        });

        if (Bot.Config.Get<bool>("Restore Equipment After"))
            Adv.GearStore(true);

    }
}


