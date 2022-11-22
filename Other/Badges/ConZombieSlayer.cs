//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class ConZombieSlayer

{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        Core.Logger($"Doing Artix quest for {badge} badge");

        Core.EquipClass(ClassType.Farm);

        //I Guess We DO Need Steenkin' Badges 3135
        Story.KillQuest(3135, "vendorbooths", "Ravin' Skelly");
        
        //Con Kit 3136
        if (!Story.QuestProgression(3136))
        {
            Core.EnsureAccept(3136);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Con Survival Guide", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Water Bottle", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Hoopy Frood brand Towel", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Event Schedule", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Xtra-Strength Energy Potion", log: false);
            Core.HuntMonster("battlecon", "Cosplay Zombie", "Anti-Con Rot Sanitation Device", log: false);            
            Core.EnsureComplete(3136);
        }

        //BrutalCorn Barrier 3137
        Story.KillQuest(3137, "battlecon", "BrutalCorn");

        //Badge Quest - Cosplay Zombies On Parade 3144
        Core.EnsureAccept(3144);
        Core.HuntMonster("battlecon", "Cosplay Zombie", "Defeat Cosplay Zombie", 100, log: false);
        Core.EnsureComplete(3144);
    }

    private string badge = "ConZombie Slayer";
}
