//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Phoenixrise.cs

using Skua.Core.Interfaces;

public class Fireisland
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public PhoenixriseStory PhoenixriseStory = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Core.RunCore();
        Core.SetOptions(false);
    }

    public void CompleteFireIsland()
    {
        if (Core.isCompletedBefore(4235))
        {
            Core.Logger("You have already completed FireIsland storyline");
            return;
        }

        Embersea();
        Pyrewatch();
        PhoenixriseStory.Feverfew();
        PhoenixriseStory.Phoenixrise();
        Fireforge();
        Lavarun();
        Brimstone();
        Nightmare();
    }

    public void Embersea()
    {
        if (Core.isCompletedBefore(4055))
        {
            Core.Logger("You have already completed Embersea storyline");
            return;
        }

        Story.PreLoad();

        //Heat of Battle 4054
        Story.KillQuest(4054, "Embersea", new[] { "Flame Soldier", "Storm Scout" });

        //Light the Flame 4055
        Story.MapItemQuest(4055, "Embersea", 3153, 22);
        Story.KillQuest(4055, "Embersea", "Living Lava");

        //Kill It With Fire [Member] 4056
        if (!Core.IsMember)
            return;
        Story.KillQuest(4056, "Embersea", new[] { "Coal Creeper", "Pyradon", "Fyresyn" });
    }

    public void Pyrewatch()
    {
        if (Core.isCompletedBefore(4081))
        {
            Core.Logger("You have already completed Pyrewatch storyline");
            return;
        }

        Story.PreLoad();

        if (Story.QuestProgression(4070))
        {
            PhoenixriseStory.Pyralis();
        }

        //Protect the Plague Sufferers 4076
        Story.KillQuest(4076, "Pyrewatch", new[] { "Coal Creeper", "Lavazard", "Caustocrush" });

        //Ease the Ill 4077
        Story.KillQuest(4077, "Pyrewatch", new[] { "Lavazard", "Living Lava" });
        Story.MapItemQuest(4077, "Pyrewatch", 3161, 5);

        //Defend Pyrewatch Peak 4078
        Story.KillQuest(4078, "Pyrewatch", "Storm Scout");

        //Signal Fire 4079
        Story.KillQuest( 4079, "Pyrewatch", new[] { "Storm Scout", "Fire Pikeman", "Flame Soldier", "Fyreborn Tiger" } );

        //Spreading Like Wildfire 4080
        Story.MapItemQuest(4080, "Pyrewatch", 3162, 4);

        //Pyrewatch Defender Badge 4081
        Story.KillQuest( 4081, "Pyrewatch", new[] { "Fire Pikeman", "Firestorm Knight", "Flame Soldier", "Storm Scout" } );
    }

    public void Fireforge()
    {
        if (Core.isCompletedBefore(4226))
        {
            Core.Logger("You have already completed Fireforge storyline");
            return;
        }

        Story.PreLoad();

        //Round 1: Firestorm Scouts 4216
        Story.KillQuest(4216, "Fireforge", "Firestorm Scout");

        //Round 2: Firestorm Soldiers 4217
        Story.KillQuest(4217, "Fireforge", "Firestorm Soldier");

        //Round 3: Firestorm Pikemen 4218
        Story.KillQuest(4218, "Fireforge", "Fire Pikeman");

        //Round 4: Firestorm Knights 4219
        Story.KillQuest(4219, "Fireforge", "Firestorm Knight");

        //Round 5: Firestorm Corporals 4220
        Story.KillQuest(4220, "Fireforge", "Firestorm Corporal");

        //Round 6: Firestorm Tigers 4221
        Story.KillQuest(4221, "Fireforge", "Armored Tiger|Firestorm Tiger");

        //Round 7: Firestorm Cavalry 4222
        Story.KillQuest(4222, "Fireforge", "Tiger Cavalry");

        //Round 8: Firestorm Majors 4223
        Story.KillQuest(4223, "Fireforge", "Firestorm Major");

        //Round 9: Firestorm Blazebinders 4224
        Story.KillQuest(4224, "Fireforge", "Blazebinder");

        //Round 10: Firestorm General 4225
        Story.KillQuest(4225, "Fireforge", "Firestorm General");

        //Bonus Round: Flamewing the Bright 4226
        Story.KillQuest(4226, "Fireforge", "Flamewing");

        //Final Round: Tyndarius Tigermaster 4230
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4230, "Fireforge", "Tyndarius");
    }

    public void Lavarun()
    {
        if (Core.isCompletedBefore(4235))
        {
            Core.Logger("You have already completed Lavarun storyline");
            return;
        }

        Story.PreLoad();

        //Defeat Phedra 4231
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4231, "Lavarun", "Phedra");

        //Defeat Mega Tyndarius 4232
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(4232, "Lavarun", "Mega Tyndarius");

        //The Onslaught Fights On 4235
        Story.KillQuest(4235, "Lavarun", "Firestorm Soldier|Firestorm Scout");
    }

    public void Brimstone()
    {
        if (Core.isCompletedBefore(4115))
        {
            Core.Logger("You have already completed Brimstone storyline");
            return;
        }
        if (!Core.IsMember)
        {
            Core.Logger("You need to be a member for complete Brimestone questline.");
            return;
        }

        //The Hard Way 4107
        Story.KillQuest( 4107, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" } );

        //Encrypt Keepers 4108
        Story.KillQuest( 4108, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" } );

        //Catching Couriers  4109
        Story.KillQuest( 4109, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" } );

        //Steppe Quickly 4110
        Story.KillQuest( 4110, "Brimstone", new[] { "Brimstone Marauder", "Fyreborn Tiger", "Fyresyn", "Brimstone Looter", "Brimstone Bandit" } );

        //Reclamation 4111
        Story.KillQuest(4111, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter" });

        //Bad Press 4112
        Story.KillQuest(4112, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter" });

        //Good Hunting 4113
        Story.KillQuest(4113, "Brimstone", new[] { "Fyresyn", "Fyreborn Tiger", "Pyradon" });

        //One Last Push 4114
        Story.KillQuest( 4114, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" } );

        //Redemption 4115
        Story.KillQuest(4115, "Brimstone", "Chief Talmin");
    }

    public void Nightmare()
    {
        if (Core.isCompletedBefore(4157))
        {
            Core.Logger("You have already completed Nightmare storyline");
            return;
        }
        if (!Core.IsMember)
        {
            Core.Logger("You need to be a member for complete Nightmare questline.");
            return;
        }
        Story.PreLoad();

        //Fear of Clowns? 4143
        Story.KillQuest(4143, "Nightmare", "Bobble Clown");

        //FEAR OF CLOWNS!!! 4144
        Story.KillQuest(4144, "Nightmare", "Crazy Clown|Creepy Clown");

        //Fear of Spiders 4145
        Story.KillQuest(4145, "Nightmare", "Castle Spider|Cocoon Spider|Tomb Spider");

        //Fear of Snakes? 4146
        Story.KillQuest(4146, "Nightmare", new[] { "Wrasp", "Sneak" });

        //Fear of Falling? 4147
        Story.MapItemQuest(4147, "Nightmare", 3262);

        //Fear of Germs? 4148
        Story.KillQuest(4148, "Nightmare", "Germs|Sewage Elemental");

        //Fear of Needles? 4149
        Story.KillQuest(4149, "Nightmare", "Needle");

        //Fear of Dolls? 4150
        Story.KillQuest(4150, "Nightmare", "Broken Toy|Undead Dolly");

        //Fear of Being Buried Alive? 4151
        Story.KillQuest(4151, "Nightmare", new[] { "Unearthed Skeleton", "Rotfeeder Worm" });

        //Fear of Burning? 4152
        Story.KillQuest(4152, "Nightmare", "Fire Imp|Flame Elemental");

        //FEAR OF BURNING!!! 4153
        Story.KillQuest(4153, "Nightmare", "Flame Elemental");

        //Fear of Drowning? 4154
        Story.KillQuest(4154, "Nightmare", new[] { "Anglerfish", "Deep Dweller", "Merdraconian" });

        //Fear of Inadequacy? 4155
        Story.KillQuest(4155, "Nightmare", "Unearthed Skeleton");

        //Fear of Loneliness? 4156
        Story.KillQuest(4156, "Nightmare", "Nothing");

        //Fear of Failure? 4157
        Story.KillQuest(4157, "Nightmare", "Devourax");
    }
}
