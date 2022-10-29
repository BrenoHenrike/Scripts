//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class AnniversaryofDoom
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Anniversary();

        Core.SetOptions(false);
    }

    public void Anniversary()
    {
        if (Core.isCompletedBefore(8898) && !Core.isSeasonalMapActive("yulgarparty"))
            return;

        Story.PreLoad(this);

        Yulgarparty();
        Zombae();
        Mermaidsushi();
        FREE500ACSPLEASE();
        Spacepwny();
        Deathofgames();
    }
    
        public void Yulgarparty()
        {
            if (Core.isCompletedBefore(8876))
                return;

            // 8874 Friends by the Fireplace
            Story.MapItemQuest(8874, "yulgarparty", new[] { 10675, 10676, 10677, 10678, 10679, 10680, 10681, 10682, 10683 });

            // 8875 Allies at the Entrance,
            Story.MapItemQuest(8875, "yulgarparty", new[] { 10684, 10685, 10686, 10687, 10688, 10689, 10690 });

            // 8876 Mates near the Music,
            Story.MapItemQuest(8876, "yulgarparty", new[] { 10691, 10692, 10693, 10694, 10695, 10696, 10697, 10698, 10699 });
        }
        public void Zombae()
        {
            if (Core.isCompletedBefore(8883))
                return;

            Core.EquipClass(ClassType.Farm);

            // 8877 O-(De)kay Cupid,
            Story.KillQuest(8877, "zombae", "Bachelor");

            // 8878 Ghosting my Ex-Goulfiends,
            Story.KillQuest(8878, "zombae", "Bachelorette");

            // 8879 You're Dead to Me,
            Story.MapItemQuest(8879, "zombae", 10700);
            Story.KillQuest(8879, "zombae", "Zombette");

            // 8880 Swipe 4 Rich lich
            Story.MapItemQuest(8880, "zombae", 10701);
            Story.KillQuest(8880, "zombae", "Zombro");

            // 8881 A Real Grave Digger
            Story.MapItemQuest(8881, "zombae", 10702);
            Story.KillQuest(8881, "zombae", "Zombroski");

            // 8882 The Love of your Unlife
            Story.MapItemQuest(8882, "zombae", 10703);
            Story.KillQuest(8882, "zombae", "Zombetter");

            Core.EquipClass(ClassType.Solo);
            // 8883 The Ultimate Ghoul Fiend
            Story.KillQuest(8883, "zombae", "Ghoul Fiend");
        }
        public void Mermaidsushi()
        {
            if (Core.isCompletedBefore(8892))
                return;

            Core.EquipClass(ClassType.Farm);
            // 8884 S'KARIN WANT MOGLIN POPPERS,
            Story.KillQuest(8884, "mermaidsushi", "Undead Moglin");

            // 8885 S'KARIN WANT HAMSTEAK,
            Story.KillQuest(8885, "mermaidsushi", "Haunted Hamster");

            // 8886 S'KARIN WANT A SKELLO MOLD,
            Story.KillQuest(8886, "mermaidsushi", "SkeleKitten");

            // 8887 S'KARIN WANT OFFAL BIRD FOOD,
            Story.KillQuest(8887, "mermaidsushi", "Undead Turkey");

            Core.EquipClass(ClassType.Solo);
            // 8888 S'KARIN WANT WINGS,
            Story.KillQuest(8888, "mermaidsushi", "Dedbull");

            // 8889 S'KARIN WANT MERMAID SUSHI 
            Story.MapItemQuest(8889, "mermaidsushi", 10719);

            // 8890 Mer-Angel Fish Food?!
            Story.KillQuest(8890, "mermaidsushi", "Necro Minion");

            // 8891 S'KARIN NO WANT GO VIRAL
            Story.KillQuest(8891, "mermaidsushi", "Customer S'karin");

            // 8892 Gore-Made for This
            Story.KillQuest(8892, "mermaidsushi", "Die Fieri");
        }
        public void FREE500ACSPLEASE()
        {
            if (Core.isCompletedBefore(8893))
                return;

            Bot.Options.RestPackets = false;
            Bot.Handlers.Remove("AFK Handler");

            // 8893 AFK Quest
            if (!Story.QuestProgression(8893))
            {
                Core.Join("afkquest");
                Bot.Send.Packet("%xt%zm%afk%0%false%");
                Core.Logger($"**DO NOT CLICK THE GAME SCREEN** this Will Take ~5minutes, Go touch some grass 👍");
                Bot.Sleep(360000);
                Core.Logger("Game Complete. You're Welcome for the Acs");
            }
        }
        public void Spacepwny()
        {
            if (Core.isCompletedBefore(8898))
                return;

            Bot.Options.RestPackets = true;

            Core.EquipClass(ClassType.Farm);
            // 8894 Plague of Zom-Bays
            if (!Story.QuestProgression(8894))
            {
                Core.EnsureAccept(8894);
                Core.KillMonster("spacepwny", "Enter", "Spawn", "Zom-Bay", "Zom-Bay Defeated", 10);
                Core.EnsureComplete(8894);
            }

            // 8895 Foal of (Thorough) Dread
            if (!Story.QuestProgression(8895))
            {
                Core.EnsureAccept(8895);
                Core.KillMonster("spacepwny", "r2", "Left", "Thoroughdred", "Thoroughdred Defeated", 10);
                Core.EnsureComplete(8895);
            }

            // 8896 Create a Stable Base
            if (!Story.QuestProgression(8896))
            {
                Core.EnsureAccept(8896);
                Core.KillMonster("spacepwny", "r2", "Left", "NecroPrancer", "NecroPrancer Defeated", 10);
                Core.EnsureComplete(8896);
            }

            // 8897 Bonies & Bonies & Bonies
            if (!Story.QuestProgression(8897))
            {
                Core.EnsureAccept(8897);
                Core.KillMonster("spacepwny", "r3", "Left", "*", "Mare Carnage", 100);
                Core.EnsureComplete(8897);
            }

            Core.EquipClass(ClassType.Solo);
            // 8898 Defeat Mr DED
            Story.KillQuest(8898, "spacepwny", "Mr DED");
        }
        public void Deathofgames()
        {
            if (Core.isCompletedBefore(8924))
                return;

            Story.PreLoad(this);

            //Dungeons & DOOMFights 8899
            Story.KillQuest(8899, "deathofgames", new[] { "8-Bit Skelly", "8-Bit Sepulchure" });

            //AQ3Destruction 8900
            Story.KillQuest(8900, "deathofgames", new[] { "3D Flying Eye", "Clawg", "Trolluk" });

            //OverSoul Under Attack 8901
            Story.KillQuest(8901, "deathofgames", new[] { "Vampire Knight", "Black Dragon" });

            //HeroSmashed 8902
            Story.KillQuest(8902, "deathofgames", new[] { "Rider", "Blaster Master", "Super Death" });

            //EpicDuel vs DoG 8907
            Story.KillQuest(8907, "deathofgames", new[] { "Cyber Hunter", "God of War" });

            //AQWorlds At Risk 8920
            Story.KillQuest(8920, "deathofgames", new[] { "Skeletal Fire Mage", "Drakath" });

            //MechQuest For Glory 8921
            Story.KillQuest(8921, "deathofgames", new[] { "Newbatron Prime", "ShadowScythe Mecha", "SkullCrusher Mecha" });

            //DragonFables &amp; Lore 8922
            Story.KillQuest(8922, "deathofgames", new[] { "Fire Elemental", "Xan", "Titan Fluffy" });

            //AdventureQuest for Victory 8923
            Story.KillQuest(8923, "deathofgames", new[] { "Moglin Ghost", "Halenro the Paladin", "Mysterious Stranger" });

            //Battle On... FOREVER! 8924
            Story.KillQuest(8924, "deathofgames", "Death of Games");
        }
    
}