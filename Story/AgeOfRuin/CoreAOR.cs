/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story\ShadowsOfWar\CoreSoW.cs
using System.Dynamic;
using Newtonsoft.Json;
using Skua.Core.Interfaces;
using Skua.Core.Models.Auras;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Skills;

public class CoreAOR
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreAdvanced Adv = new();
    public CoreStory Story = new();
    private CoreSoW SoW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public void DoAll()
    {
        TerminaTemple(true);
        AshrayVillage();
        SunlightZone();
        TwilightZone();
        YulgarAria();
        MidnightZone();
        AbyssalZone();
        DeepWater();
        SeaVoice();
        Balemorale();
        Castleeblana();
    }

    private bool isSeaVoiceCalled = false;
    public void TerminaTemple(bool seaVoice = false)
    {
        if (Core.isCompletedBefore(seaVoice ? 9351 : 9214))
            return;

        SoW.ManaCradle();

        Story.PreLoad(this);

        // Familiar Faces (9213)
        Story.KillQuest(9213, "terminatemple", "Termina Defender");
        Story.MapItemQuest(9213, "terminatemple", new[] { 11625, 11626, 11627 });

        // Loaded Resume (9214)
        Story.KillQuest(9214, "terminatemple", "Clandestine Guard");
        Story.MapItemQuest(9214, "terminatemple", new[] { 11628, 11629, 11630 });

        if (!seaVoice)
            return;

        if (!isSeaVoiceCalled)
        {
            isSeaVoiceCalled = true;
            SeaVoice();
        }
        // Mopping Up (9351)
        if (isSeaVoiceCalled)
            Story.MapItemQuest(9351, "terminatemple", new[] { 12050, 12051 });
    }

    public void AshrayVillage()
    {
        if (Core.isCompletedBefore(9234))
            return;

        TerminaTemple();

        Story.PreLoad(this);

        // Big ol' Eyes (9225)
        Story.KillQuest(9225, "ashray", "Kitefin Shark Bait");

        // Angry Angler (9226)
        Story.KillQuest(9226, "ashray", "Ashray Fisherman");
        Story.MapItemQuest(9226, "ashray", new[] { 11663, 11664 });

        // Slimy Scavenger (9227)
        Story.KillQuest(9227, "ashray", "Ghostly Eel");

        // Troubled Waters (9228)
        Story.KillQuest(9228, "ashray", "Stagnant Water");
        Story.MapItemQuest(9228, "ashray", 11665);

        // Washed Ink (9229)
        Story.KillQuest(9229, "ashray", "Ashray Fisherman");
        Story.MapItemQuest(9229, "ashray", 11666);

        // Fishy Hospitality (9230)
        Story.KillQuest(9230, "ashray", "Kitefin Shark Bait");

        // Doctoring Papers (9231)
        Story.KillQuest(9231, "ashray", "Ghostly Eel");
        Story.MapItemQuest(9231, "ashray", 11667);

        // Psychic Pollution (9232)
        Story.KillQuest(9232, "ashray", "Stagnant Water");
        Story.MapItemQuest(9232, "ashray", 11668);

        // Duck Dive (9233)
        Story.MapItemQuest(9233, "ashray", new[] { 11669, 11670 });

        // Faces in the Foam (9234)
        Story.KillQuest(9234, "ashray", "Seafoam Elemental");
    }

    public void SunlightZone()
    {
        if (Core.isCompletedBefore(9251))
            return;

        AshrayVillage();

        Story.PreLoad(this);

        // Detergent Shortage (9242)
        Story.KillQuest(9242, "sunlightzone", "Blighted Water");

        // Ghost in the Machine (9243)
        Story.KillQuest(9243, "sunlightzone", "Spectral Jellyfish");

        // Efficient Division (9244)
        Story.KillQuest(9244, "sunlightzone", "Blighted Water");
        Story.MapItemQuest(9244, "sunlightzone", new[] { 11705, 11706 });

        // Tech Illiterate (9245)
        Story.KillQuest(9245, "sunlightzone", "Spectral Jellyfish");
        Story.MapItemQuest(9245, "sunlightzone", 11707, 3);

        // Plugging Leaks (9246)
        Story.KillQuest(9246, "sunlightzone", new[] { "Spectral Jellyfish", "Blighted Water" });

        // Shared History (9247)
        Story.MapItemQuest(9247, "sunlightzone", new[] { 11708, 11709, 11710 });

        // Flat Scares (9248)
        Story.KillQuest(9248, "sunlightzone", "Astravian Illusion");
        Story.MapItemQuest(9248, "sunlightzone", 11711);

        // Fishy Bully (9249)
        Story.KillQuest(9249, "sunlightzone", "Infernal Illusion");
        Story.MapItemQuest(9249, "sunlightzone", 11712, 5);

        // Faint Howls (9250)
        Story.MapItemQuest(9250, "sunlightzone", 11713);
        Story.KillQuest(9250, "sunlightzone", "Seraphic Illusion");

        // Down the Digestive Tract (9251)
        Story.KillQuest(9251, "sunlightzone", "Marine Snow");
    }

    public void TwilightZone()
    {
        if (Core.isCompletedBefore(9268))
            return;

        SunlightZone();

        Story.PreLoad(this);

        // Marshmallows With Bite (9258)
        Story.KillQuest(9258, "twilightzone", "Whale Louse");

        // Meaty Cold Spaghetti (9259)
        Story.KillQuest(9259, "twilightzone", "Polymelia Lamprey");

        // Songs in the Seams (9260)
        Story.MapItemQuest(9260, "twilightzone", 11749);
        Story.MapItemQuest(9260, "twilightzone", 11750, 4);

        // Parched Throats (9261)
        Story.KillQuest(9261, "twilightzone", new[] { "Whale Louse", "Polymelia Lamprey" });

        // Morning Stretches (9262)
        Story.MapItemQuest(9262, "twilightzone", new[] { 11751, 11752 });

        // Natural Empathy (9263)
        Story.KillQuest(9263, "twilightzone", "Decay Spirit");

        // Comfort Blanket of Snow (9264)
        Story.KillQuest(9264, "twilightzone", "Ice Guardian");

        // Whale Watching (9265)
        Story.MapItemQuest(9265, "twilightzone", new[] { 11753, 11754, 11755 });

        // Exhausted Spirits (9266)
        Story.KillQuest(9266, "twilightzone", new[] { "Decay Spirit", "Ice Guardian" });

        // Singing to Whales (9267)
        if (!Story.QuestProgression(9267))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9267);
            Core.HuntMonster("twilightzone", "Leviathan", "Leviathan Fought");
            Core.EnsureComplete(9267);
        }

        // The Sea's Commitment (9268)
        Story.MapItemQuest(9268, "twilightzone", 11756);
    }

    public void YulgarAria()
    {
        if (Core.isCompletedBefore(9274))
            return;

        TwilightZone();

        Story.PreLoad(this);

        // Octotree (9270)
        Story.KillQuest(9270, "twilightzone", "Polymelia Lamprey");

        // Thirsty Roots (9271)
        Story.KillQuest(9271, "sunlightzone", "Blighted Water");

        // Dollar Store Mogloween Costume (9272)
        Story.KillQuest(9272, "sunlightzone", new[] { "Astravian Illusion", "Infernal Illusion" });

        // Sea Snow Angels (9273)
        Story.KillQuest(9273, "sunlightzone", "Marine Snow");

        // Ten Klicks (9274)
        if (!Story.QuestProgression(9274))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9274);
            Core.HuntMonster("twilightzone", "Leviathan", "Leviathan's Tendril", 3);
            Core.EnsureComplete(9274);
        }
    }

    public void MidnightZone()
    {
        if (Core.isCompletedBefore(9301))
            return;

        TwilightZone();

        Story.PreLoad(this);

        // Motivation Malady (9292)
        Story.MapItemQuest(9292, "midnightzone", new[] { 11842, 11843, 11844 });

        // Radical Renovation (9293)
        Story.KillQuest(9293, "midnightzone", "Polymelia Lamprey");
        Story.MapItemQuest(9293, "midnightzone", 11845);

        // Graveyard Shift (9294)
        Story.KillQuest(9294, "midnightzone", new[] { "Vowed ShadowSlayer", "Vowed ShadowSlayer" });

        // Educational Execution (9295)
        Story.MapItemQuest(9295, "midnightzone", 11846);
        Story.KillQuest(9295, "midnightzone", "Undead Prisoner");

        // Vows For Ignorance (9296)
        Story.KillQuest(9296, "midnightzone", new[] { "Undead Prisoner", "Vowed ShadowSlayer" });
        Story.MapItemQuest(9296, "midnightzone", 11847);

        // Protein Shake (9297)
        Story.MapItemQuest(9297, "midnightzone", 11848, 3);
        Story.KillQuest(9297, "midnightzone", "Shadow Viscera");

        // Duty Beyond Death (9298)
        Story.KillQuest(9298, "midnightzone", "Venerated Wraith");

        // Designated Taunters (9299)
        Story.KillQuest(9299, "midnightzone", new[] { "Venerated Wraith", "Shadow Viscera" });

        // Beloved Simulacrum (9230)
        Story.MapItemQuest(9300, "midnightzone", 11849, 4);
        Story.MapItemQuest(9300, "midnightzone", 11850);

        // Roko's Royal Basilisk (9301)
        if (!Story.QuestProgression(9301))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9301);
            Core.HuntMonster("midnightzone", "Sparagmos", "Sparagmos A.I. Defeated");
            Core.EnsureComplete(9301);
        }
    }

    public void AbyssalZone()
    {
        if (Core.isCompletedBefore(9315))
            return;

        MidnightZone();

        Story.PreLoad(this);

        // Shark Kiting (9306)
        Story.KillQuest(9306, "abyssalzone", "Kitefin Shark Bait");
        Story.MapItemQuest(9306, "abyssalzone", 11914);

        // Suckered Blockade (9307)
        Story.MapItemQuest(9307, "abyssalzone", 11893, 6);
        Story.MapItemQuest(9307, "abyssalzone", 11894);

        // Digestive Fluids (9308)
        Story.KillQuest(9308, "abyssalzone", "Blighted Water");

        // Sodden Secrets (9309)
        Story.KillQuest(9309, "abyssalzone", "Shadow Viscera");
        Story.MapItemQuest(9309, "abyssalzone", 11895, 3);

        // The Hidden Corpse (9310)
        Story.KillQuest(9310, "abyssalzone", new[] { "Shadow Viscera", "Blighted Water" });

        // Octo-Flake Fish Feed (9311)
        Story.KillQuest(9311, "abyssalzone", "Foam Scavenger");
        Story.MapItemQuest(9311, "abyssalzone", 11896);

        // Completely Surrounded (9312)
        Story.MapItemQuest(9312, "abyssalzone", 11897, 6);
        Story.MapItemQuest(9312, "abyssalzone", 11898);

        // Sea Salt Soap (9313)
        Story.KillQuest(9313, "abyssalzone", "Necro Adipocere");

        // In the Grip of Justice (9314)
        Story.KillQuest(9314, "abyssalzone", new[] { "Necro Adipocere", "Foam Scavenger" });
        Story.MapItemQuest(9314, "abyssalzone", 11899);

        // Together as One (9315)
        if (!Story.QuestProgression(9315))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9315);
            Core.HuntMonster("abyssalzone", "The Ashray", "The Ashray Vanquished");
            Core.EnsureComplete(9315);
        }
    }

    public void DeepWater(bool panopticonMerge = false)
    {
        if (Core.isCompletedBefore(9338))
            return;

        AbyssalZone();

        Story.PreLoad(this);

        // Unsung Heroes (9329)
        Story.KillQuest(9329, "trenchobserve", "Venerated Wraith");
        Story.MapItemQuest(9329, "trenchobserve", 11975);

        // Watertight Guarantee (9330)
        if (!Story.QuestProgression(9330))
        {
            Core.EnsureAccept(9330);
            Core.GetMapItem(11976, map: "trenchobserve");
            Core.HuntMonster("trenchobserve", "Seabase Turret", "Turret Screws", 8);
            Core.EnsureComplete(9330);
        }

        // Core Electrolytes (9331)
        Story.MapItemQuest(9331, "trenchobserve", 11977, 4);

        // Guardian Spirits (9332)
        Story.KillQuest(9332, "trenchobserve", "Venerated Wraith");
        if (panopticonMerge)
            return;

        // Enemy in Need (9333)
        Story.MapItemQuest(9333, "trenchobserve", 11978, 4);
        Story.KillQuest(9333, "trenchobserve", "Seabase Turret");

        // Here Lies Shadow (9334)
        Story.MapItemQuest(9334, "trenchobserve", new[] { 11979, 11981 });
        Story.MapItemQuest(9334, "trenchobserve", 11980, 2);

        // Nature's White Noise (9335)
        Story.KillQuest(9335, "trenchobserve", "Sea Spirit");

        // Dreams Seep into Reality (9336)
        Story.KillQuest(9336, "trenchobserve", "Necro Adipocere");

        // Hadal Havoc (9337)
        Story.KillQuest(9337, "trenchobserve", new[] { "Necro Adipocere", "Sea Spirit" });
        Story.MapItemQuest(9337, "trenchobserve", 11982);

        // See You on the Other Side (9338)
        if (!Story.QuestProgression(9338))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9338);
            Core.HuntMonster("trenchobserve", "Lady Noelle", "Lady Noelle Defeated");
            Core.EnsureComplete(9338);
        }
    }

    public void SeaVoice()
    {
        if (Core.isCompletedBefore(9348))
            return;

        DeepWater();

        if (!Core.isCompletedBefore(9125))
        {
            Core.Logger(" \"Your Hero\" [9125] Quest *REQUIRED* to start SeaVoice quests");
            return;
        }

        Story.PreLoad(this);

        if (!Core.isCompletedBefore(9348))
        {
            Core.EnsureAccept(9348);

            // Define the possible solo classes
            string[] PossibleSoloClasses = new[] { "Chaos Avenger", "Verus Doomknight", "Void Highlord", "ArchPaladin" };

            if (!Core.CheckInventory(PossibleSoloClasses, any: true))
                Core.Logger("no Soloing classes found stopping (go get AP atleast and rerun)", stopBot: true);

            // Find the first available class in inventory or bank
            string? selectedClass = PossibleSoloClasses.FirstOrDefault(className =>
                Bot.Inventory.Items.Any(item => item.Name == className) ||
                Bot.Bank.Items.Any(item => item.Name == className)
            );

            Core.Logger($"Soloing \"Voice of the Sea\" with {selectedClass}");

            Adv.GearStore();

            // Adv.SmartEnhance(selectedClass);

            // Call the KillThing method with the specified parameters
            KillThing(
                map: "seavoice",
                mobMapID: 1,
                targetAuraName: "Oxidize",
                ItemUsed: 78994,
                Class: selectedClass,
                item: "Voice in the Sea Defeated",
                quant: 1,
                isTemp: true
            );
            Adv.GearStore(true);
            Core.EnsureComplete(9348);
            Core.SellItem("Vigil", all: true);
        }

        TerminaTemple(true);
    }

    public void Balemorale()
    {
        if (Core.isCompletedBefore(9729))
            return;

        SeaVoice();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Estrangement (9719)
        Story.MapItemQuest(9719, "balemorale", 12933);

        // Searchlights (9720)
        Story.KillQuest(9720, "balemorale", "Lightguard Paladin");

        // Queensmen (9721)
        Story.KillQuest(9721, "balemorale", "Noble's Knight");

        // Cellar Secrets (9722)
        Story.MapItemQuest(9722, "balemorale", new[] { 13177, 13178 });

        // Chaotic Roots (9723)
        Story.KillQuest(9723, "balemorale", "Chaos Spider");
        Story.MapItemQuest(9723, "balemorale", new[] { 13179, 13180 });

        // Eroding Era (9724)
        Story.KillQuest(9724, "balemorale", "Chaos Crystal");

        // Old Wolf (9725)
        Story.MapItemQuest(9725, "balemorale", 13181, 5);
        Story.MapItemQuest(9725, "balemorale", 13182);

        // Abandoned Cradle (9726)
        Story.KillQuest(9726, "balemorale", new[] { "Chaos Spider", "Chaos Crystal" });
        Story.MapItemQuest(9726, "balemorale", 13183);

        // Shockwaves (9727)
        Story.KillQuest(9727, "balemorale", "Skye Warrior");

        // Sleight of Hand (9728)
        Story.MapItemQuest(9728, "balemorale", 13184, 7);
        Story.MapItemQuest(9728, "balemorale", 13185);

        // Double Fianchetto (9729)
        if (!Story.QuestProgression(9729))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(9729);
            Core.HuntMonster("balemorale", "Queen Victoria", "Queen Victoria Defeated");
            Core.EnsureComplete(9729);
            Core.EquipClass(ClassType.Farm);
        }
    }

    public void Castleeblana()
    {
        if (Core.isCompletedBefore(9741))
            return;

        Balemorale();

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        // Skye's Raindrops 9732 
        Story.KillQuest(9732, "castleeblana", "Skye Warrior");

        // Doctor's Orders 9733 
        Story.MapItemQuest(9733, "castleeblana", 13202, 5);
        Story.MapItemQuest(9733, "castleeblana", 13203);

        // Shockwave's Ripples 9734
        Story.KillQuest(9734, "castleeblana", "Skye Executor");

        // Caretaker's Shadow 9735 
        Story.MapItemQuest(9735, "castleeblana", 13204);
        Story.KillQuest(9735, "castleeblana", new[] { "Skye Warrior", "Skye Executor" });

        // Harbinger's Tears 9736 
        Story.KillQuest(9736, "castleeblana", "Bananach Raven");

        // Spectre of Hunger 9737 
        Story.MapItemQuest(9737, "castleeblana", 13205);
        Story.KillQuest(9737, "castleeblana", "Fear Gorta");

        // InnJustice 9738 
        Story.MapItemQuest(9738, "castleeblana", 13206);
        Story.KillQuest(9738, "castleeblana", new[] { "Bananach Raven", "Fear Gorta" });

        // Find Shelter in... 9739 
        Story.MapItemQuest(9739, "castleeblana", new[] { 13207, 13208 });

        // Heavy Handed 9740 
        Story.KillQuest(9740, "castleeblana", "Skye Warrior");

        // Miserable Monsoon 9741
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9741, "castleeblana", "Warden Indradeep");
    }


    public void KillThing(string? map = null, int mobMapID = 1, string? targetAuraName = null, int ItemUsed = 1, string? Class = null, string? item = null, int quant = 1, bool isTemp = false)
    {
        Adv.BuyItem("seavoice", 2320, "Vigil", 1000, 12023);
        // ItemCheckingAndBuying();

        Core.Join(map);
        Bot.Wait.ForMapLoad(map!);
        Bot.Wait.ForTrue(() => Bot.Player.Loaded, 20);

        Core.Logger($"map: {map}");
        Core.Logger($"mobMapID: {mobMapID}");
        Core.Logger($"targetAuraName: {targetAuraName}");
        Core.Logger($"ItemUsed: {ItemUsed} [Vigil]");
        Core.Logger($"Class: {Class}");
        Core.Logger($"item: {item}");
        Core.Logger($"quant: {quant}");
        Core.Logger($"isTemp: {isTemp}");

        Core.Equip(Class!);
        if (Class == "Void Highlord")
            Bot.Skills.StartAdvanced("Void HighLord", true, ClassUseMode.Def);
        Core.Equip(ItemUsed);
        Core.Logger($"{ItemUsed} [Vigil] Equiped? {Bot.Inventory.IsEquipped("Vigil")}");

        Monster? mob = Bot.Monsters.MapMonsters.FirstOrDefault(m => m.MapID == mobMapID);
        if (targetAuraName != null)
        {
            Aura? targetAura = Bot.Target.Auras.Concat(Bot.Self.Auras).FirstOrDefault(a => a.Name == targetAuraName);
        }

        if (Bot.Player.Cell != mob!.Cell)
            Core.Jump(mob.Cell);

        #region  UltraSpeaker
        // if (map == "ultraspeaker")
        // {
        //     Random random = new();
        //     int xpos = random.Next(1, 30);
        //     int ypos = random.Next(1, 30);
        //     Bot.Player.WalkTo(x: xpos, y: ypos);
        // }
        #endregion
        Bot.Player.SetSpawnPoint();

        while (!Bot.ShouldExit && item != null && isTemp ? !Bot.TempInv.Contains(item!, quant) : !Core.CheckInventory(item!, quant))
        {
            //Check if/move to /in mob cell && Bot.Player.Alive)
            if (Bot.Player.Cell != mob.Cell)
                Core.Jump(mob.Cell);

            #region  UltraSpeaker
            if (map == "ultraspeaker")
            {
                while (!Bot.ShouldExit && !Bot.Player.Alive)
                    Core.Sleep();
            }
            #endregion

            if (targetAuraName != null)
                AuraHandling(targetAuraName);

            if (Bot.Player.Alive && !Bot.Self.HasActiveAura(targetAuraName!) && !Bot.Target.HasActiveAura(targetAuraName!))
                Bot.Combat.Attack(mob);
            Core.Sleep();

            if (isTemp ? Bot.TempInv.Contains(item!, quant) : Core.CheckInventory(item, quant))
            {
                break;
            }
        }

        void AuraHandling(string? targetAuraName)
        {
            foreach (Aura A in Bot.Target.Auras.Concat(Bot.Self.Auras))
            {
                if (targetAuraName == null)
                    continue;

                switch (A.Name)
                {
                    case "Oxidize":
                        while (!Bot.ShouldExit && !Bot.Self.HasActiveAura("Vigil"))
                        {
                            UsePotion();
                            Core.Sleep();

                            // Check if targetAura is not null before accessing its SecondsRemaining() method
                            // Assuming `targetAura` is the aura you're referring to
                            if (Bot.Self.HasActiveAura("Vigil"))
                            {
                                Core.Logger($"Vigil Active!");
                                break;
                            }
                        }
                        break;

                    case null:
                        break;
                }
            }
        }

        void UsePotion()
        {
            var skill = Bot.Flash.GetArrayObject<dynamic>("world.actions.active", 5);
            if (skill == null) return;
            Bot.Flash.CallGameFunction("world.testAction", JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(skill))!);
        }
    }

}
