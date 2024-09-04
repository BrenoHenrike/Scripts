/*
name: (Chaos Finale Bonus) Killer Catacombs
description: This will finish the Killer Catacombs quest.
tags: story, quest, chaos-saga, 13-lords-of-chaos, chaos-finale, bonus, killer-catacombs, member
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class KillerCatacombs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        KillerCatacombs_Line();

        Core.SetOptions(false);
    }

    public void KillerCatacombs_Line()
    {
        if (Core.isCompletedBefore(3679))
            return;

        if (!Core.IsMember)
        {
            Core.Logger("You need to be a member for complete this questline.");
            return;
        }

        Story.PreLoad(this);

        // The Coward
        Story.KillQuest(3660, "killercatacombs", "Tomb Robber ");
    
        // Welcome Party
        Story.KillQuest(3661, "killercatacombs", "Tomb Robber ");
     
        // Scout Ahead
        Story.KillQuest(3662, "killercatacombs", "Ravenous Maw");
      
        // Dinner Party
        Story.KillQuest(3663, "killercatacombs", "Ravenous Maw");
      
        // Illumination Preparation
        Story.MapItemQuest(3664, "killercatacombs", 2762, 8);
      
        // Bones Picked Clean
        Story.KillQuest(3665, "killercatacombs", "Sundered Darkblood ");
      
        // Biased Window
        Story.MapItemQuest(3666, "killercatacombs", 2761, 3);
        Story.KillQuest(3666, "killercatacombs", "Sundered Darkblood ");
      
        // Room For Seconds
        Story.KillQuest(3667, "killercatacombs", "Starved Maw ");
       
        // Magic Doorstop
        Story.KillQuest(3668, "killercatacombs", "Sundered Darkblood ");
      
        // Silverware Cipher
        Story.KillQuest(3669, "killercatacombs", "Ravenous Maw");
       
        // Dinner Bell
        Story.KillQuest(3670, "killercatacombs", new[] { "Starved Maw ", "Ravenous Maw" });
       
        // Faceless Challengers
        Story.KillQuest(3671, "killercatacombs", "Living Armor ");
       
        // Doorway
        Story.KillQuest(3672, "killercatacombs", "Living Armor ");
       
        // Eternal Hunger
        Story.KillQuest(3673, "killercatacombs", new[] { "Starved Maw ", "Ravenous Maw", "Living Armor " });
       
        // You Weren't Invited
        Story.KillQuest(3674, "killercatacombs", "Living Armor ");
       
        // Teeth and Plates
        Story.KillQuest(3675, "killercatacombs", new[] { "Starved Maw ", "Ravenous Maw", "Living Armor " });
        
        // The Moment of Silence
        Story.KillQuest(3676, "killercatacombs", "Living Armor");
        Story.MapItemQuest(3676, "killercatacombs", 2763, 10);

        // Eternal Servitude
        Story.KillQuest(3677, "killercatacombs", new[] { "Living Armor ", "Sundered Darkblood ", "Unstable Spirit Orb " });

        // A Reanimated Dragon
        if (!Story.QuestProgression(3678))
        {
            Core.EnsureAccept(3678);
            Core.HuntMonsterMapID("killercatacombs", 73, "Dracolich Slain");
            Core.EnsureComplete(3678);
        }

        // Lonesome Twin Guardian
        if (!Story.QuestProgression(3679))
        {
            Core.EnsureAccept(3679);
            Core.HuntMonsterMapID("killercatacombs", 74, "Dracolich Necromancer Ended");
            Core.EnsureComplete(3679);
        }
    }
}
