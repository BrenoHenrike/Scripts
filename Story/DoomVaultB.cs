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
    public DoomVaultA DV = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Bot.Quests.IsUnlocked(2972))
        {
            Core.Logger("DoomVaultB not unlocked, Completing DoomVault");
            DV.StoryLine();
        }

        if (Core.isCompletedBefore(3004))
            return;

        Core.AcceptandCompleteTries = 1;

        Story.PreLoad(this);

        // Grim Underdungeon I        
        Story.KillQuest(2972, "Doomvaultb", "Grimmer Soldier");

        // Grim Underdungeon II        
        Story.KillQuest(2973, "Doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon III        
        Story.KillQuest(2975, "Doomvaultb", "Grimmer Lich");

        // Grim Underdungeon IV        
        Story.KillQuest(2976, "Doomvaultb", "Weeping Spyball");

        // Grim Underdungeon V        
        Story.KillQuest(2977, "Doomvaultb", "Grimmer Ectomancer");

        // Grim Underdungeon VI   
        Story.KillQuest(2978, "Doomvaultb", "Grimmer Shelleton");

        // Grim Underdungeon VII        
        Story.KillQuest(2979, "Doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon VIII        
        Story.KillQuest(2980, "Doomvaultb", "Grimmer Fire Mage");

        // Grim Underdungeon IX        
        Story.KillQuest(2984, "Doomvaultb", "Grimmer Lich");

        // Grim Underdungeon X        
        Story.KillQuest(2985, "Doomvaultb", "Weeping Spyball");

        // Grim Underdungeon XI        
        Story.KillQuest(2986, "Doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon XII        
        Story.KillQuest(2987, "Doomvaultb", "Grimmer Lich");

        // Grim Underdungeon XIII        
        Story.KillQuest(2988, "Doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon XIV        
        Story.KillQuest(2989, "Doomvaultb", "Weeping Spyball");

        // Grim Underdungeon XV        
        Story.ChainQuest(2990);

        // Grim Underdungeon XVI        
        Story.KillQuest(2991, "Doomvaultb", "Grimmer Shelleton");

        // Grim Underdungeon XVII        
        Story.KillQuest(2992, "Doomvaultb", "Grimmer Soldier");

        // Grim Underdungeon XVIII        
        Story.KillQuest(2993, "Doomvaultb", "Grimmer Lich");

        // Grim Underdungeon XIX      
        Story.KillQuest(2994, "Doomvaultb", "Grimmer Lich");

        // Grim Underdungeon XX     
        Story.ChainQuest(2995);

        // Grim Underdungeon XXI        
        Story.KillQuest(2996, "Doomvaultb", "Grimmer Fire Mage");

        // Grim Underdungeon XXII
        Story.KillQuest(2997, "Doomvaultb", "Grimmer Fighter");

        // Grim Underdungeon XXIII        
        Story.KillQuest(2998, "Doomvaultb", "Grimmer Ectomancer");

        // Grim Underdungeon XXIV        
        Story.KillQuest(2999, "Doomvaultb", "Grimmer Soldier");

        // Grim Underdungeon XXV        
        Story.ChainQuest(3000);

        // Grim Underdungeon XXIX    
        if (!Story.QuestProgression(3004))
        {
            Core.EnsureAccept(3004);
            Adv.KillUltra("doomvaultb", "r26", "Left", "Undead Raxgore", "Raxgore Slain", publicRoom: false);
            Core.EnsureComplete(3004);
        }
    }
}