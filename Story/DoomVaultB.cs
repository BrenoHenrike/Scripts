//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/DoomVault.cs
using Skua.Core.Interfaces;

public class DoomVaultB
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public DoomVaultA DoomVaultA = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        DoomVaultA.StoryLine();

        if (Core.isCompletedBefore(3004))
            return;

        Story.PreLoad(this);

        Core.AcceptandCompleteTries = 1;

        // Grim Underdungeon I 2972
        Story.KillQuest(2972, "doomvaultb", "Grimmer Soldier");

        // Grim Underdungeon II 2973
        Story.KillQuest(2973, "doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon III 2975
        Story.KillQuest(2975, "doomvaultb", "Grimmer Lich");

        // Grim Underdungeon IV 2976
        Story.KillQuest(2976, "doomvaultb", "Weeping Spyball");

        // Grim Underdungeon V 2977
        Story.KillQuest(2977, "doomvaultb", "Grimmer Ectomancer");

        // Grim Underdungeon VI 2978
        Story.KillQuest(2978, "doomvaultb", "Grimmer Shelleton");

        // Grim Underdungeon VII 2979
        Story.KillQuest(2979, "doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon VIII 2980
        Story.KillQuest(2980, "doomvaultb", "Grimmer Fire Mage");

        // Grim Underdungeon IX 2984
        Story.KillQuest(2984, "doomvaultb", "Grimmer Lich");

        // Grim Underdungeon X 2985
        Story.KillQuest(2985, "doomvaultb", "Weeping Spyball");

        // Grim Underdungeon XI 2986
        Story.KillQuest(2986, "doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon XII 2987
        Story.KillQuest(2987, "doomvaultb", "Grimmer Lich");

        // Grim Underdungeon XIII 2988
        Story.KillQuest(2988, "doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon XIV 2989
        Story.KillQuest(2989, "doomvaultb", "Weeping Spyball");

        // Grim Underdungeon XV 2990
        Story.ChainQuest(2990);

        // Grim Underdungeon XVI 2991
        Story.KillQuest(2991, "doomvaultb", "Grimmer Shelleton");

        // Grim Underdungeon XVII 2992
        Story.KillQuest(2992, "doomvaultb", "Grimmer Soldier");

        // Grim Underdungeon XVIII 2993
        Story.KillQuest(2993, "doomvaultb", "Grimmer Lich");

        // Grim Underdungeon XIX 2994
        Story.KillQuest(2994, "doomvaultb", "Grimmer Lich");

        // Grim Underdungeon XX 2995
        Story.ChainQuest(2995);

        // Grim Underdungeon XXI 2996
        Story.KillQuest(2996, "doomvaultb", "Grimmer Fire Mage");

        // Grim Underdungeon XXII 2997
        Story.KillQuest(2997, "doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon XXIII 2998
        Story.KillQuest(2998, "doomvaultb", "Grimmer Ectomancer");

        // Grim Underdungeon XXIV 2999
        Story.KillQuest(2999, "doomvaultb", "Grimmer Soldier");

        // Grim Underdungeon XXV 3000
        Story.ChainQuest(3000);

        // Grim Underdungeon XXIX 3004
        if (!Story.QuestProgression(3004))
        {
            Core.EnsureAccept(3004);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Raxgore Slain", publicRoom: false);
            Core.EnsureComplete(3004);
        }
    }
}