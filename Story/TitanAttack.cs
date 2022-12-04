//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class TitanAttackStory
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        DoAll();

        Core.SetOptions(false);
    }

    public void DoAll()
    {
        if (Core.isCompletedBefore(8777))
            return;

        Story.PreLoad(this);

        // Those Who Remain 8759
        Story.MapItemQuest(8759, "titanattack", new[] { 10345, 10346, 10347, 10348, 10349 });

        // Who Helps the Healer? 8760
        Story.KillQuest(8760, "titanattack", new[] { "Corrosive Crawler", "Chaorrupted Bandit" });

        // Supply in Demand 8761
        Story.KillQuest(8761, "titanattack", "Supply Caravan");

        // Creature Comforts 8762
        Story.KillQuest(8762, "titanattack", new[] { "Chaos Wyvern", "Corrosive Crawler" });

        // Infernal Investigation 8763
        Story.KillQuest(8763, "titanattack", new[] { "AntiTitan Corps", "Chaorrupted Bandit" });

        // Another Brick in the Wall 8764        
        if (!Story.QuestProgression(8764))
        {
            Story.MapItemQuest(8764, "titanattack", 10350, 3);
            Core.HuntMonster("titanattack", "Supply Caravan", "Building Brick", 15);
            Core.HuntMonster("titanattack", "Corrosive Crawler", "Chitin Coating", 8);
            Core.HuntMonster("titanattack", "Chaos Dweller", "Adhesive Saliva", 7);
            Core.EnsureComplete(8764);
        }

        // Rotten to the Corps 8765
        Story.KillQuest(8765, "titanattack", "AntiTitan Corps");

        // A Fighting Chance 8766
        Story.MapItemQuest(8766, "titanattack", 10351, 8);
        Story.KillQuest(8766, "titanattack", new[] { "Corrosive Crawler", "Supply Caravan" });

        // Flash of the Titans 8767
        if (!Story.QuestProgression(8767))
        {
            Core.EnsureAccept(8767);
            Core.HuntMonster("titanattack", "Supply Caravan", "Shine Powder", 5);
            Core.HuntMonster("titanattack", "Supply Caravan", "Explosive Casing", 10);
            Core.HuntMonster("titanattack", "Chaotic Beholder", "Smoke Sac", 5);
            Core.EnsureComplete(8767);
        }

        // Vein Ambition 8768
        Story.KillQuest(8768, "titanattack", "Corrosive Crawler");

        // Asset Acquisition 8769
        if (!Story.QuestProgression(8769))
        {
            Core.EnsureAccept(8769);
            Core.HuntMonster("titanattack", "Supply Caravan", "Food Crate", 5);
            Core.HuntMonster("titanattack", "Supply Caravan", "Medicine Vial", 15);
            Core.HuntMonster("titanattack", "Chaorrupted Bandit", "Bandit Blade", 15);
            Core.EnsureComplete(8769);
        }

        // Enemy at the Gate 8770
        Story.MapItemQuest(8770, "titanattack", 10352);

        // Titanic Terror 8771
        Story.KillQuest(8771, "titanattack", "Titanic Vindicator");

        // Titanic Terror Times Two 8772
        Story.KillQuest(8772, "titanattack", new[] { "Titanic Paladin", "Titanic DoomKnight" });

        // A Little Light... 8773
        Story.KillQuest(8773, "titanstrike", "Titanic Paladin");

        // A Dose of Darkness... 8774
        Story.KillQuest(8774, "titanstrike", "Titanic DoomKnight");

        // And A Cup of Chaos 8775
        Story.KillQuest(8775, "titanstrike", "Titanic Destroyer");

        // Topple Some Titans! 8776
        Core.AddDrop("Heroic Titan's Greatsword");
        Story.KillQuest(8776, "titanstrike", new[] { "Titanic Paladin", "Titanic DoomKnight", "Titanic Destroyer" });

        // The BIG Finish 8777
        Adv.EnhanceItem("Heroic Titan's Greatsword", EnhancementType.Lucky, CapeSpecial.None, HelmSpecial.None, WeaponSpecial.Spiral_Carve);
        Adv.BestGear(GearBoost.Drakath);

        Story.KillQuest(8777, "titandrakath", "Titan Drakath");
    }
}
