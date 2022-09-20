//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class JirabinChallenge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        RunedWoods();
        DetherTombs();
        VoidBattle();

        Core.SetOptions(false);
    }

    public readonly string[] Drops =
    {
        "Blood of the Void Blade",
        "Blood of the Void Daggers",
        "Phantasmagoric Vengeance",
        "Purified Void Blade",
        "Purified Void Daggers"
    };

    public void RunedWoods()
    {
        if (Core.isCompletedBefore(3986))
            return;

        Story.PreLoad(this);

        //Void Dragon Invasion
        Story.ChainQuest(3976);

        //Meet the Za'narians 3977
        Story.MapItemQuest(3977, "runedwoods", new[] { 3104, 3105, 3106, 3107, 3108, 3109 });

        //Ruining the Dragonwood 3978
        Story.KillQuest(3978, "runedwoods", new[] { "Jies", "Void Warrior", "Frask" });

        //Catch the Ki'lks! 3979
        Story.KillQuest(3979, "northlandlight", "Ice Ki'lk");
        Story.KillQuest(3979, "northlands", new[] { "Fire Ki'lk", "Energy Ki'lk" });

        //Discover the Darkness 3980
        Story.KillQuest(3980, "runedwoods", "Frask");
        Story.KillQuest(3980, "doomwood", new[] { "Doomwood Bonemuncher", "Doomwood Ectomancer" });

        //Shoot for Victory 3981
        Story.KillQuest(3981, "uppercity", "Drow Assassin");
        Story.KillQuest(3981, "runedwoods", "Frask");

        //Find the Frasks! 3982
        Story.KillQuest(3982, "sandsea", "Sandsea Frask");
        Story.KillQuest(3982, "pines", "Dwarfhold Frask");
        Story.KillQuest(3982, "hacchiko", "Yokai Frask");

        //Analogues for Rath 3983
        Story.KillQuest(3983, "runedwoods", "Jies");
        Story.KillQuest(3983, "cloister", new[] { "Karasu", "Wendigo" });

        //The Shades Grow Darker 3984
        Story.KillQuest(3984, "northlands", "Northlands Shade");
        Story.KillQuest(3984, "mythsong", "Mythsong Shade");
        Story.KillQuest(3984, "pines", "Dwarfhold Shade");
        Story.KillQuest(3984, "darkoviagrave", "Darkovia Shade");

        //Discover the Void 3985
        Story.KillQuest(0000, "void", new[] { "Void Elemental", "Void Bear" });

        //Confront Jir'abin 3986
        Story.KillQuest(3986, "runedwoods", "Jir'abin");

    }

    public void DetherTombs()
    {
        if (Core.isCompletedBefore(4005))
            return;
            
        RunedWoods();

        Story.PreLoad(this);

        //Enter the Tomb 3996
        Story.KillQuest(3996, "dethertombs", "Sand Frask");

        //The Relic of Light Tome 3997
        Story.KillQuest(3997, "dethertombs", "De'ther Shade");

        //Disarm the Traps 3998
        Story.MapItemQuest(3998, "dethertombs", 3132, 12);

        //Find the Missing Pages 3999
        Story.KillQuest(3999, "dethertombs", new[] { "De'ther Shade" });

        //Find the Resonator Components 4000
        Story.KillQuest(4000, "dethertombs", new[] { "Sand Frask", "Tomb Drifter" });

        //More Components needed 4001
        Story.KillQuest(4001, "dethertombs", new[] { "Darkfang", "Tomb Shark" });

        //Final Components 4002
        Story.KillQuest(4002, "dethertombs", new[] { "De'ther Vase", "Jies" });
        // if (!Story.QuestProgression(4002))
        // {
        //     Core.EnsureAccept(4002);
        //     Core.HuntMonster("dethertombs", "De'ther Vase", "Bottle Of Oil");
        //     Core.HuntMonster("dethertombs", "Jies", "Diamond Tooth", 6);
        //     Core.EnsureComplete(4002);
        // } //Keep //-ed incase something is wrong here 

        //Gain Entrance to the Vault 4003
        Story.MapItemQuest(4003, "dethertombs", 3134);

        //Defeat the Spirit 4004
        Story.KillQuest(4004, "dethertombs", "Spirit of De'ther");

        //Battle Jir'abin 4005
        Story.KillQuest(4005, "xiex", "Jir'abin");
    }

    public void VoidBattle()
    {
        if (Core.CheckInventory(Drops, toInv: false))
            return;
        DetherTombs();
        Core.AddDrop(Drops);

        Core.Logger($"Hunting Jir'abin Challenge for drop items.");
        while (!Bot.ShouldExit && !Core.CheckInventory(Drops, toInv: false))
        {
            Core.HuntMonster("voidbattle", "Jir'abin Challenge", log: false);
            Core.ToBank(Drops);
        }
        Core.Logger($"All Drops Acquired.");
    }

}
