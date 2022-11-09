//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class CoreFireIsland
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.RunCore();

        Core.SetOptions(false);
    }

    public void CompleteFireIsland()
    {
        if (Core.isCompletedBefore(4235))
            return;

        Embersea();
        Pyrewatch();
        Feverfew();
        Phoenixrise();
        Fireforge();
        Lavarun();
        Brimstone();
        Nightmare();
    }

    public void Embersea()
    {
        if (Core.isCompletedBefore(4055))
            return;

        Story.PreLoad(this);

        //Heat of Battle 4054
        Story.KillQuest(4054, "Embersea", new[] { "Flame Soldier", "Storm Scout" });

        //Light the Flame 4055
        Story.MapItemQuest(4055, "Embersea", 3153, 22);
        Story.KillQuest(4055, "Embersea", "Living Lava");

        //Kill It With Fire [Member] 4056
        if (Core.IsMember)
            Story.KillQuest(4056, "Embersea", new[] { "Coal Creeper", "Pyradon", "Fyresyn" });
    }

    public void Pyrewatch()
    {
        if (Core.isCompletedBefore(4081))
            return;

        Story.PreLoad(this);

        //Flee the Flames
        Story.KillQuest(4070, "pyrewatch", new[] { "Fyreborn Tiger", "Caustocrush", "Lavazard" });

        //Taste of their own Medicine
        Story.KillQuest(4071, "pyrewatch", "Fire Pikeman");

        //A Jarring Discovery
        Story.MapItemQuest(4072, "pyrewatch", 3159, 12);

        //Push on to Pyrewatch
        Story.KillQuest(4073, "pyrewatch", new[] { "Firestorm Knight", "Firestorm Knight" });

        //Friends of Pyrewatch Peak
        Story.KillQuest(4074, "pyrewatch", new[] { "Caustocrush", "Fire Pikeman", "Flame Soldier", "Fire Pikeman" });

        //A Salve to Soothe
        Story.MapItemQuest(4075, "pyrewatch", 3160, 5);
        Story.KillQuest(4075, "pyrewatch", "Lavazard");

        //Protect the Plague Sufferers 4076
        Story.KillQuest(4076, "Pyrewatch", new[] { "Coal Creeper", "Lavazard", "Caustocrush" });

        //Ease the Ill 4077
        Story.MapItemQuest(4077, "Pyrewatch", 3161, 5);
        Story.KillQuest(4077, "Pyrewatch", new[] { "Lavazard", "Lavazard", "Living Lava" });

        //Defend Pyrewatch Peak 4078
        Story.KillQuest(4078, "Pyrewatch", "Storm Scout");

        //Signal Fire 4079
        Story.KillQuest(4079, "Pyrewatch", new[] { "Storm Scout", "Flame Soldier", "Flame Soldier", "Fyreborn Tiger" });

        //Spreading Like Wildfire 4080
        Story.MapItemQuest(4080, "Pyrewatch", 3162, 4);

        //Pyrewatch Defender Badge 4081
        Story.KillQuest(4081, "Pyrewatch", new[] { "Fire Pikeman", "Firestorm Knight", "Flame Soldier", "Storm Scout" });
    }

    public void Feverfew()
    {
        if (Core.isCompletedBefore(4142))
            return;

        Story.PreLoad(this);

        //Quench the Flames
        Story.KillQuest(4128, "feverfew", "Firestorm Knight");

        //Through the Fog and Flame
        Story.KillQuest(4129, "feverfew", "Locked Chest");

        //Restore the Lady of Waters
        if (!Story.QuestProgression(4130))
        {
            Story.MapItemQuest(4130, "feverfew", new[] { 3246, 3247 });
            Core.HuntMonster("feverfew", "Coral Creeper", "Statue Torso");
            Core.HuntMonster("feverfew", "Twisted Undine", "Statue Base");
            Core.HuntMonster("feverfew", "Salamander", "Statue Core");
            Core.HuntMonster("feverfew", "Firestorm Knight", "Statue Plaque");
            Core.EnsureComplete(4130);
        }

        //Rumors and Smoke
        Story.MapItemQuest(4131, "feverfew", 3245);
        Story.KillQuest(4131, "feverfew", new[] { "Firestorm Knight", "Firestorm Knight", "Firestorm Major", "Firestorm Major" });

        //Dam the Flood
        Story.MapItemQuest(4132, "feverfew", 3244, 5);

        //Salvage Mission
        Story.MapItemQuest(4133, "feverfew", 3243, 5);
        Story.KillQuest(4133, "feverfew", "Twisted Undine");

        //Fear the Fog
        Story.KillQuest(4134, "feverfew", new[] { "Firestorm Knight", "Twisted Undine", "Feverfew Vase", "Coral Creeper", "Salamander" });

        //When There's Smoke...
        Story.MapItemQuest(4135, "feverfew", 3248);

        //Firin' This Guy
        Story.KillQuest(4136, "feverfew", "Blazebinder");

        //Blessings of the Lady
        Story.MapItemQuest(4137, "feverfew", 3242, 10);

        //Parting the Waters
        Story.KillQuest(4138, "feverfew", new[] { "Firestorm Knight", "Twisted Undine", "Coral Creeper", "Salamander" });

        //The Power to Heal
        Story.KillQuest(4139, "feverfew", new[] { "Locked Chest", "Feverfew Vase", "Twisted Undine" });

        //The Deadsea Caverns
        Story.KillQuest(4140, "feverfew", new[] { "Coral Creeper", "Twisted Undine", "Salamander" });

        //Open the Floodgates
        Story.MapItemQuest(4141, "feverfew", 3241);

        //Tiger, Tiger Burning Bright
        Story.KillQuest(4142, "feverfew", "Major Thermas");
    }

    public void Phoenixrise()
    {
        if (Core.isCompletedBefore(4213))
            return;

        Story.PreLoad(this);

        //Stonecold Defense
        Story.KillQuest(4201, "phoenixrise", new[] { "Lava Troll", "Infernal Goblin" });

        //Preying for a Good Offense
        Story.KillQuest(4202, "phoenixrise", new[] { "Gargrowl", "Infernal Goblin", "Lava Troll" });

        //Red Alert
        Story.MapItemQuest(4203, "phoenixrise", 3283, 4);

        //Disguise Fur a Good Cause
        Story.KillQuest(4204, "phoenixrise", new[] { "Firestorm Tiger", "Lava Troll", "Infernal Goblin" });

        //Hunt for the Stolen
        Story.MapItemQuest(4205, "phoenixrise", 3285);

        //Rescue Run
        Story.MapItemQuest(4206, "phoenixrise", 3282, 6);

        //Recover the Remainder
        Story.KillQuest(4207, "phoenixrise", "Pyrric Ursus");

        //Rune Chances of a Backstab
        Story.MapItemQuest(4208, "phoenixrise", 3284, 7);
        Story.KillQuest(4208, "phoenixrise", "Infernal Goblin");

        //Clear out the Caverns
        Story.KillQuest(4209, "phoenixrise", new[] { "Pyrric Ursus", "Lava Troll", "Infernal Goblin", "Firestorm Tiger" });

        //Strengthen the Survivors
        Story.KillQuest(4210, "phoenixrise", new[] { "Pyrric Ursus", "Lava Troll", "Infernal Goblin", "Firestorm Tiger", });

        //Bridge to Salvation
        Story.KillQuest(4211, "phoenixrise", "Lava Troll");

        //Growling Pains
        Story.KillQuest(4212, "phoenixrise", "Gargrowl");

        //Defeat Cinderclaw
        Story.KillQuest(4213, "phoenixrise", "Cinderclaw");
    }

    public void Fireforge()
    {
        if (Core.isCompletedBefore(4226))
            return;

        Story.PreLoad(this);

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
        Story.KillQuest(4230, "Fireforge", "Tyndarius");
    }

    public void Lavarun()
    {
        if (Core.isCompletedBefore(4235))
            return;

        //Defeat Phedra 4231
        Story.KillQuest(4231, "Lavarun", "Phedra");

        //Defeat Mega Tyndarius 4232
        Story.KillQuest(4232, "Lavarun", "Mega Tyndarius");

        //The Onslaught Fights On 4235
        Story.KillQuest(4235, "Lavarun", "Firestorm Soldier|Firestorm Scout");
    }

    public void Brimstone()
    {
        if (Core.isCompletedBefore(4115))
            return;

        if (!Core.IsMember)
        {
            Core.Logger("You need to be a member for complete the /brimestone questline.");
            return;
        }

        //The Hard Way 4107
        Story.KillQuest(4107, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" });

        //Encrypt Keepers 4108
        Story.KillQuest(4108, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" });

        //Catching Couriers  4109
        Story.KillQuest(4109, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" });

        //Steppe Quickly 4110
        Story.KillQuest(4110, "Brimstone", new[] { "Brimstone Marauder", "Fyreborn Tiger", "Fyresyn", "Brimstone Looter", "Brimstone Bandit" });

        //Reclamation 4111
        Story.KillQuest(4111, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter" });

        //Bad Press 4112
        Story.KillQuest(4112, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter" });

        //Good Hunting 4113
        Story.KillQuest(4113, "Brimstone", new[] { "Fyresyn", "Fyreborn Tiger", "Pyradon" });

        //One Last Push 4114
        Story.KillQuest(4114, "Brimstone", new[] { "Brimstone Marauder", "Brimstone Looter", "Brimstone Bandit" });

        //Redemption 4115
        Story.KillQuest(4115, "Brimstone", "Chief Talmin");
    }

    public void Nightmare()
    {
        if (Core.isCompletedBefore(4157))
            return;

        if (!Core.IsMember)
        {
            Core.Logger("You need to be a member for complete the /nightmare questline.");
            return;
        }
        Story.PreLoad(this);

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