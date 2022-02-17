//cs_include Scripts/CoreBots.cs
using RBot;
public class KillerCatacombs
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
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
            Core.Logger("You need to be a member for complete this questline.", messageBox: true, stopBot: true);

        Core.EquipClass(ClassType.Solo);

        // The Coward
        Core.KillQuest(3660, "killercatacombs", "Tomb Robber ");
        // Welcome Party
        Core.KillQuest(3661, "killercatacombs", "Tomb Robber ");
        // Scout Ahead
        Core.KillQuest(3662, "killercatacombs", "Ravenous Maw");
        // Dinner Party
        Core.KillQuest(3663, "killercatacombs", "Ravenous Maw");
        // Illumination Preparation
        Core.MapItemQuest(3664, "killercatacombs", 2762, 8);
        // Bones Picked Clean
        Core.KillQuest(3665, "killercatacombs", "Sundered Darkblood ");
        // Biased Window
        Core.MapItemQuest(3666, "killercatacombs", 2761, 3);
        Core.KillQuest(3666, "killercatacombs", "Sundered Darkblood ");
        // Room For Seconds
        Core.KillQuest(3667, "killercatacombs", "Starved Maw ");
        // Magic Doorstop
        Core.KillQuest(3668, "killercatacombs", "Sundered Darkblood ");
        // Silverware Cipher
        Core.KillQuest(3669, "killercatacombs", "Ravenous Maw");
        // Dinner Bell
        Core.KillQuest(3670, "killercatacombs", new[] { "Starved Maw ", "Ravenous Maw" });
        // Faceless Challengers
        Core.KillQuest(3671, "killercatacombs", "Living Armor ");
        // Doorway
        Core.KillQuest(3672, "killercatacombs", "Living Armor ");
        // Eternal Hunger
        Core.KillQuest(3673, "killercatacombs", new[] { "Starved Maw ", "Ravenous Maw", "Living Armor " });
        // You Weren't Invited
        Core.KillQuest(3674, "killercatacombs", "Living Armor ");
        // Teeth and Plates
        Core.KillQuest(3675, "killercatacombs", new[] { "Starved Maw ", "Ravenous Maw", "Living Armor " });
        // The Moment of Silence
        Core.KillQuest(3676, "killercatacombs", "Living Armor |Ravenous Maw|Starved Maw |Sundered Darkblood ");
        Core.MapItemQuest(3676, "killercatacombs", 2763, 10);
        // Eternal Servitude
        Core.KillQuest(3677, "killercatacombs", new[] { "Living Armor ", "Sundered Darkblood ", "Unstable Spirit Orb " });
        // A Reanimated Dragon
        Core.KillQuest(3678, "killercatacombs", "Dracolich ");
        // Lonesome Twin Guardian
        Core.KillQuest(3679, "killercatacombs", "Dracolich ");
    }
}
