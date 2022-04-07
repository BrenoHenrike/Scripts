using RBot;

public class CoreCelestialRealm
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreFarms Farm = new CoreFarms();

    public void CompleteCoreCelestialRealm()
    {
        //Progress Check
        if (Core.isCompletedBefore(5387))
            return;

        //Preload Quests
        Story.PreLoad();

        CelestialRealm();
        LostRuins();
        LostRuinsWar();
        InfernalSpire();
    }

    public void CelestialRealm()
    {
        //Progress Check
        if (Core.isCompletedBefore(4499))
            return;

        //Preload Quests
        Story.PreLoad();

        //Summon Help
        Story.MapItemQuest(4495, "celestialrealm", 3698);
        Story.KillQuest(4495, "celestialrealm", new[] { "Fallen Knight|Infernal Knight", "Celestial Bird of Paradise" });

        //Power Up!
        Core.BuyItem("embersea", 1100, "Basic Guard Potion", 10);
        Story.KillQuest(4496, "celestialrealm", new[] { "Celestial Bird of Paradise", "Fallen Knight|Infernal Knight" });

        //The Final Spell Fragment
        Story.MapItemQuest(4497, "celestialrealm", 3696);

        //Find the Map
        if (!Story.QuestProgression(4498))
        {
            Core.AddDrop("Dwakel Decoder");
            Core.GetMapItem(106, 1, "crashsite");
            Story.KillQuest(4498, "celestialrealm", "Infernal Knight");
        }

        //Reveal the Portal!
        Story.MapItemQuest(4499, "celestialrealm", 3693);
    }

    public void LostRuins()
    {
        //Progress Check
        if (Core.isCompletedBefore(4507))
            return;

        //Preload Quests
        Story.PreLoad();

        //Investigate the Ruins
        Story.MapItemQuest(4500, "lostruins", 3694, 3);
        Story.KillQuest(4500, "lostruins", "Underworld Hound");

        //Take out the Knights
        Story.KillQuest(4501, "lostruins", "Fallen Knight|Infernal Knight");

        //Find the Clues
        Story.KillQuest(4502, "lostruins", new[] { "Infernal Imp|Underworld Hound", "Infernal Imp|Underworld Hound", "Fallen Knight", "Infernal Knight" });

        //Recover the Cage Key!
        if (!Story.QuestProgression(4503))
        {
            Farm.FishingREP(2);
            if (!Core.CheckInventory("Holy Oil"))
                Core.BuyItem("fishing", 356, "Holy Oil");
            Story.KillQuest(4503, "lostruins", "Fallen Knight");
        }

        //Protect Them
        if (!Story.QuestProgression(4504))
        {
            if (!Core.CheckInventory("Potent Guard Potion", 10))
                Core.BuyItem("embersea", 1100, "Potent Guard Potion", 10);
            Story.KillQuest(4504, "lostruins", "Fallen Knight|Infernal Knight");
        }

        //Break the Spell
        Story.KillQuest(4505, "lostruins", new[] { "Fallen Knight|Infernal Knight", "Underworld Hound" });
        Story.MapItemQuest(4505, "lostruins", 3697, 5);

        //Open the Cage
        Story.MapItemQuest(4506, "lostruins", 3695);

        //Defeat the Infernal Warlord
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4507, "lostruins", "Infernal Warlord");
    }

    public void LostRuinsWar()
    {
        //Progress Check
        if (Core.isCompletedBefore(4508))
            return;

        //Preload Quests
        Story.PreLoad();

        //Celestial Realm at War
        Story.KillQuest(4509, "lostruinswar", "Fallen Knight|Infernal Knight");

        //Defeat the Diabolical Warlord!
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4508, "lostruinswar", "Diabolical Warlord");
    }

    public void InfernalSpire()
    {
        //Progress Check
        if (Core.isCompletedBefore(5387))
            return;

        //Preload Quests
        Story.PreLoad();

        //Infernal Destruction
        Story.KillQuest(5374, "infernalspire", new[] { "Fallen Knight", "Underworld Hound" });

        //Gone Without A Trace
        Story.KillQuest(5375, "infernalspire", new[] { "Fallen Knight", "Underworld Hound" });
        Story.MapItemQuest(5375, "infernalspire", 4729);
        Story.MapItemQuest(5375, "infernalspire", 4730);

        //Helzekiel
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(5376, "infernalspire", "Helzekiel");

        //Get the Keys
        Story.KillQuest(5377, "infernalspire", new[] { "Dungeon Fiend|Infernal Hound", "Dungeon Fiend", "Infernal Hound" });

        //Free the Captives
        Story.MapItemQuest(5378, "infernalspire", 4731, 6);

        //Energy Needed
        Story.KillQuest(5379, "infernalspire", "Dungeon Fiend");
        Story.MapItemQuest(5379, "infernalspire", 4732, 6);

        //Interrogate the Jailer
        Story.KillQuest(5380, "infernalspire", "Garvodeus");

        //Get the Code
        if (!Story.QuestProgression(5381))
        {
            Core.EnsureAccept(5381);
            Core.KillMonster("infernalspire", "r13", "Left", "Fallen Knight", "Override Code");
            Core.KillMonster("infernalspire", "r13", "Left", "Fallen Knight", "Fallen Knight Slain", 6);
            Core.HuntMonster("infernalspire", "Infernal Imp", "Infernal Imp Slain", 6);
            Core.EnsureComplete(5381);

        }

        //Enter the Code
        Story.MapItemQuest(5382, "infernalspire", 4733);

        //Smash it Up
        Story.MapItemQuest(5383, "infernalspire", 4734, 4);

        //Take out the Overseer
        Story.KillQuest(5384, "infernalspire", "Azkorath");

        //Clear the Invaders
        Story.KillQuest(5385, "infernalspire", new[] { "Infernal Knight", "Grievous Fiend" });

        //Find the Weapon
        Story.MapItemQuest(5386, "infernalspire", 4735);

        //What is THAT?
        Story.KillQuest(5387, "infernalspire", "Malxas");
    }
}