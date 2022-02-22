//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class DjinnGateStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        DjinnGate();

        Core.SetOptions(false);
    }

    public void DjinnGate()
    {
        if (Core.isCompletedBefore(6161))
            return;

        Core.AddDrop("Armor of Zular", "Djinn's Essence", "Unseen Essence", "Fangs of the Lion",
        "Claws of the Daeva", "Light of the Serpent", "Pike of the Shimmering Sands", "Reavers of the Gilded Sun");
        Core.EquipClass(ClassType.Farm);

        if (!Story.QuestProgression(6153))
        {
            Core.EnsureAccept(6153);
            Core.HuntMonster("mobius", "Slugfit", "Fragment 1");
            Core.HuntMonster("faerie", "Aracara", "Fragment 2");
            Core.HuntMonster("faerie", "Chainsaw Sneevil", "Fragment 3");
            Core.HuntMonster("faerie", "Cyclops Warlord", "Fragment 4");
            Core.HuntMonster("cornelis", "Gargoyle", "Fragment 5");
            Core.EnsureComplete(6153);
        }
        if (!Story.QuestProgression(6154))
        {
            Core.EnsureAccept(6154);
            Core.HuntMonster("arcangrove", "Seed Spitter", "Fragment 6");
            Core.HuntMonster("cloister", "Karasu", "Fragment 7");
            Core.HuntMonster("gilead", "Bubblin", "Fragment 8");
            Core.HuntMonster("natatorium", "Merdraconian", "Fragment 9");
            Core.HuntMonster("mafic", "Scoria Serpent", "Fragment 10");
            Core.EnsureComplete(6154);
        }
        if (!Story.QuestProgression(6155))
        {
            Core.EnsureAccept(6155);
            Core.HuntMonster("mythsong", "French Horned Toadragon", "Fragment 11");
            Core.HuntMonster("palooza", "Rock Lobster", "Fragment 12");
            Core.HuntMonster("palooza", "Stinger", "Fragment 13");
            Core.HuntMonster("palooza", "Mozard", "Fragment 15");
            Core.HuntMonster("beehive", "Killer Queen Bee", "Fragment 14");
            Core.EnsureComplete(6155);
        }
        if (!Story.QuestProgression(6156))
        {
            Core.EnsureAccept(6156);
            Core.HuntMonster("forestchaos", "Chaorrupted Bear", "Fragment 16");
            Core.HuntMonster("guru", "Leatherwing", "Fragment 17");
            Core.HuntMonster("marsh", "Dark Witch", "Fragment 18");
            Core.HuntMonster("marsh", "Spider", "Fragment 19");
            Core.HuntMonster("marsh2", "Soulseeker", "Fragment 20");
            Core.EnsureComplete(6156);
        }
        if (!Story.QuestProgression(6157))
        {
            Core.EnsureAccept(6157);
            Core.HuntMonster("pirates", "Shark Bait", "Fragment 21");
            Core.HuntMonster("yokairiver", "Kappa Ninja", "Fragment 22");
            Core.HuntMonster("bamboo", "Tanuki", "Fragment 23");
            Core.HuntMonster("yokaiwar", "Samurai Nopperabo", "Fragment 24");
            Core.HuntMonster("pirates", "Fishwing", "Fragment 25");
            Core.EnsureComplete(6157);
        }
        if (!Story.QuestProgression(6158))
        {
            Core.EnsureAccept(6158);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("doomkitten", "DoomKitten", "Potent DoomKitten Mana", publicRoom: true);
            Core.HuntMonster("bloodtitan", "Ultra Blood Titan", "Potent Blood Titan Mana");
            Core.HuntMonster("trigoras", "Trigoras", "Potent Trigoras Mana");
            Core.HuntMonster("phoenixrise", "Cinderclaw", "Potent CinderClaw Mana");
            Core.HuntMonster("thevoid", "Reaper", "Potent Reaper Mana", publicRoom: true);
            Core.EnsureComplete(6158);
        }
        Story.MapItemQuest(6159, "djinngate", 5571, 5, false);

        Core.EquipClass(ClassType.Farm);
        Story.KillQuest(6160, "djinngate", new[] { "Harpy", "Lamia" });

        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6161, "djinngate", "Gedoz the Malignant");
        Story.KillQuest(6162, "djinngate", new[] { "Harpy", "Lamia" });
    }
}