/*
name: Void Chasm Story
description: This script will complete the storyline in /voidchasm.
tags: voidchasm, chasm, nation, nulgath, story, ana di carcano, gravelyn, jadzia
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Nation/VoidRefuge.cs
using Skua.Core.Interfaces;

public class VoidChasm
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private VoidRefuge VR = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(9552))
            return;

        VR.Storyline();

        Story.PreLoad(this);

        // Light Headed 9543
        Story.KillQuest(9543, "voidchasm", "Paladin Ascendant");

        // Forgiveness of Debt 9544
        Story.KillQuest(9544, "voidchasm", "Nation Outrider");

        // Hyperspace-Plasia 9545
        Story.KillQuest(9545, "voidchasm", "Paladin Ascendant");
        Story.MapItemQuest(9545, "voidchasm", 12619, 6);

        // Tangled Aortas 9546        
        Story.MapItemQuest(9546, "voidchasm", new[] { 12620, 12621 });

        // Nation Code 9547
        Story.KillQuest(9547, "voidchasm", "Nation Outrider");
        Story.MapItemQuest(9547, "voidchasm", 12622);

        // Snake Ears 9548
        Story.KillQuest(9548, "voidchasm", "Void Fang");

        // Faustian Leftovers 9549
        Story.KillQuest(9549, "voidchasm", "The Hushed");
        Story.MapItemQuest(9549, "voidchasm", new[] { 12623, 12627, 12628 });

        // Broken Lives 9550
        Story.KillQuest(9550, "voidchasm", new[] { "The Hushed", "Void Fang" });
        Story.MapItemQuest(9550, "voidchasm", 12624, 4);

        // Call of the Fiend 9551
        Core.EquipClass(ClassType.Solo);
        Story.MapItemQuest(9551, "voidchasm", new[] { 12625, 12626 });
        Story.KillQuest(9551, "voidchasm", "Carnage");

        // Famiglia di Carcano 9552
        Story.KillQuest(9552, "voidchasm", "Carcano");
    }
}
