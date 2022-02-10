//cs_include Scripts/CoreBots.cs
using RBot;
public class ShiftingPyramid
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        ShiftingPyramidSaga();

        Core.SetOptions(false);
    }

    public void ShiftingPyramidSaga()
    {
        // Hunt for the Infinity Codex
        Core.KillQuest(5166, "whitehole", "Vortex Mage");
        // Sacrific and Survival
        Core.KillQuest(5167, "whitehole", new[] { "Vortex Naga", "Vortex Hawk" });
        // The Cartouche of Isis
        Core.KillQuest(5168, "whitehole", "Gate Goblin");
        // The Cartouche of Ma'at
        Core.KillQuest(5169, "whitehole", "Vortex Walker");
        // Bound to Do Good
        Core.KillQuest(5170, "whitehole", new[] { "Dimensional Crystal", "Gate Goblin", "Vortex Matter" });
        // Honor the Goddess Isis
        Core.MapItemQuest(5171, "whitehole", 4539, 1);
        // Stick to the Task
        Core.KillQuest(5172, "whitehole", new[] { "Dimensional Crystal", "Gate Goblin", "Vortex Matter" });
        // Honor the Goddess Ma'at
        Core.MapItemQuest(5173, "whitehole", 4540, 4);
        Core.MapItemQuest(5173, "whitehole", 4542, 1);
        // Duty is light as a feather
        Core.KillQuest(5174, "whitehole", "Vortex Hawk");
        // Judgement... or Justice?
        Core.KillQuest(5175, "whitehole", "Hand of Ma'at");
        // The Cartouche of Thoth
        Core.KillQuest(5176, "whitehole", "Vortex Mage");
        // Make Your Mark
        Core.MapItemQuest(5177, "whitehole", 4541, 4);
        // The Cartouche of Kebechet
        Core.KillQuest(5178, "whitehole", "Vortex Naga");
        // Honor the Goddess Kebechet
        Core.MapItemQuest(5179, "whitehole", 4543, 1);
        // Destroy to Purify
        Core.KillQuest(5180, "whitehole", "Vortex Crystal");
        // Guardian of the Vortex
        Core.KillQuest(5181, "whitehole", "Vortex Guardian");
        // Stick with it
        Core.KillQuest(5182, "whitehole", new[] { "Dimensional Crystal", "Gate Goblin", "Vortex Matter" });
        // Honor the God Thoth
        Core.MapItemQuest(5183, "whitehole", 4544, 1);
        // The Brightest Cartouches
        if (!Core.QuestProgression(5184))
        {
            Core.EnsureAccept(5184);
            Core.HuntMonster("whitehole", "Dimensional Crystal", "Sun Cartouche");
            Core.HuntMonster("whitehole", "Dimensional Crystal", "Sky Cartouche");
            Core.HuntMonster("whitehole", "Vortex Matter", "Star Cartouche");
            Core.HuntMonster("whitehole", "Vortex Crystal", "Moon Cartouche");
            Core.EnsureComplete(5184);
        }
        // Honor the Astral Deities
        Core.MapItemQuest(5185, "whitehole", 4545, 4);
        // Serpent of the Stars
        Core.KillQuest(5186, "whitehole", "Mehensi Serpent");
        // The Infinity Shield
        Core.MapItemQuest(5187, "whitehole", 4546, 1);
    }
}
