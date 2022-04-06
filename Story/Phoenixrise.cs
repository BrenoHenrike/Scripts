//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class PhoenixriseStory
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        PhoenixAll();

        Core.SetOptions(false);
    }

    public void PhoenixAll()
    {
        Kyron();
        Pyralis();
        AsherMurkblade();
        Feverfew();
        Phoenixrise();
    }

    public void Kyron()
    {
        if (Core.isCompletedBefore(4055))
            return;

        Story.PreLoad();

        //Heat of Battle
        Story.KillQuest(4054, "embersea", new[] { "Flame Soldier", "Storm Scout" });
        //Light the Flame
        Story.MapItemQuest(4055, "embersea", 3153, 22);
        Story.KillQuest(4055, "embersea", "Living Lava");
    }

    public void Pyralis()
    {
        if (Core.isCompletedBefore(4075))
            return;

        Story.PreLoad();

        //Flee the Flames
        Story.KillQuest(4070, "pyrewatch", new[] { "Fyreborn Tiger", "Caustocrush", "Lavazard" });
        //Taste of their own Medicine
        Story.KillQuest(4071, "pyrewatch", "Fire Pikeman|Firestorm Knight|Flame Soldier|Fyreborn Tiger|Storm Scout");
        //A Jarring Discovery
        Story.MapItemQuest(4072, "pyrewatch", 3159, 12);
        //Push on to Pyrewatch
        Story.KillQuest(4073, "pyrewatch", new[] { "Fire Pikeman|Firestorm Knight|Flame Soldier|Storm Scout", "Firestorm Knight" });
        //Friends of Pyrewatch Peak
        Story.KillQuest(4074, "pyrewatch", new[] { "Caustocrush", "Fire Pikeman", "Flame Soldier", "Fire Pikeman|Firestorm Knight|Flame Soldier|Storm Scout" });
        //A Salve to Soothe
        Story.MapItemQuest(4075, "pyrewatch", 3160, 5);
        Story.KillQuest(4075, "pyrewatch", "Lavazard|Living Lava");
    }

    public void AsherMurkblade()
    {
        if (Core.isCompletedBefore(4080))
            return;

        Story.PreLoad();

        //Protect the Plague Sufferers
        Story.KillQuest(4076, "pyrewatch", new[] { "Lavazard", "Caustocrush", "Coal Creeper" });
        //Ease the Ill
        Story.MapItemQuest(4077, "pyrewatch", 3161, 5);
        Story.KillQuest(4077, "pyrewatch", new[] { "Lavazard", "Living Lava", "Lavazard" });
        //Defend Pyrewatch Peak
        Story.KillQuest(4078, "pyrewatch", "Fire Pikeman|Storm Scout");
        //Signal Fire
        Story.KillQuest(4079, "pyrewatch", new[] { "Storm Scout", "Flame Soldier", "Fyreborn Tiger", "Fire Pikeman|Flame Soldier" });
        //Spreading Like Wildfire
        Story.MapItemQuest(4080, "pyrewatch", 3162, 4);
    }
    public void Feverfew()
    {
        if (Core.isCompletedBefore(4142))
            return;

        Story.PreLoad();

        //Quench the Flames
        Story.KillQuest(4128, "feverfew", "Firestorm Knight");
        //Through the Fog and Flame
        Story.KillQuest(4129, "feverfew", "Locked Chest");
        //Restore the Lady of Waters
        Story.MapItemQuest(4130, "feverfew", 3246);
        Story.MapItemQuest(4130, "feverfew", 3247);
        Story.KillQuest(4130, "feverfew", new[] { "Coral Creeper", "Twisted Undine", "Salamander", "Firestorm Knight|Twisted Undine" });
        //Rumors and Smoke
        Story.MapItemQuest(4131, "feverfew", 3245);
        Story.KillQuest(4131, "feverfew", new[] { "Firestorm Major", "Firestorm Knight", "Firestorm Knight", "Firestorm Major" });
        //Dam the Flood
        Story.MapItemQuest(4132, "feverfew", 3244, 5);
        //Salvage Mission
        Story.MapItemQuest(4133, "feverfew", 3243, 5);
        Story.KillQuest(4133, "feverfew", "Twisted Undine");
        //Fear the Fog
        Story.KillQuest(4134, "feverfew", new[] { "Salamander", "Feverfew Vase", "Twisted Undine", "Coral Creeper", "Firestorm Knight" });
        //When There's Smoke...
        Story.MapItemQuest(4135, "feverfew", 3248);
        //Firin' This Guy
        Story.KillQuest(4136, "feverfew", "Blazebinder");
        //Blessings of the Lady
        Story.MapItemQuest(4137, "feverfew", 3242, 10);
        //Parting the Waters
        Story.KillQuest(4138, "feverfew", new[] { "Salamander", "Coral Creeper", "Firestorm Knight", "Twisted Undine" });
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

        Story.PreLoad();

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
        Story.KillQuest(4209, "phoenixrise", new[] { "Infernal Goblin", "Lava Troll", "Firestorm Tiger", "Pyrric Ursus" });
        //Strengthen the Survivors
        Story.KillQuest(4210, "phoenixrise", new[] { "Lava Troll", "Infernal Goblin", "Firestorm Tiger", "Pyrric Ursus" });
        //Bridge to Salvation
        Story.KillQuest(4211, "phoenixrise", "Lava Troll");
        //Growling Pains
        Story.KillQuest(4212, "phoenixrise", "Gargrowl");
        //Defeat Cinderclaw
        Story.KillQuest(4213, "phoenixrise", "Cinderclaw");
    }

}