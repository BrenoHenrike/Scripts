//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FireHouse
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Storyline();

        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(1564))
            return;

        if (!Core.IsMember || CalculateFriday13())
        {
            Core.Logger("/firehouse and other maps of this storyline are only accessable with Membership or during a Friday the 13th event.");
            return;
        }

        Story.PreLoad(this);

        //1552 | Gaining Trust
        Story.KillQuest(1552, "firetown", new[] { "Obsidian Golem", "Obsidian Golem", "Obsidian Golem" });

        //1553 | If You Can't Stand the Heat...
        Story.KillQuest(1553, "firetown", "Fire Elemental");

        //1554 | Retrieving Recollections
        Story.MapItemQuest(1554, "firetown", 790, 10);

        //1555 | Under Orders
        if (!Story.QuestProgression(1555)) //--map cutscene fucks the kill
        {
            Core.EnsureAccept(1555);
            Core.HuntMonster("fireriver", "Swamp Thing", "Wet Sheet of Paper", 13);
            Core.EnsureComplete(1555);
        }

        //1556 | Locket Holds the Key
        Story.KillQuest(1556, "fireriver", "Lava Bat|Lava Garou");

        //1557 | Plans Fit for a King
        Story.MapItemQuest(1557, "fireriver", 792, 10);

        //1558 | Bound by Fire
        Story.KillQuest(1558, "firetunnel", "Volcanic Ash Imp");

        //1559 | Heart of Fire
        Story.MapItemQuest(1559, "firetunnel", 791, 10);

        //1560 | Spirit of a Dragon
        Story.KillQuest(1560, "firetunnel", "Elder Magma Wyrm");

        //1561 | Initiate Shutdown Sequence
        Story.ChainQuest(1561);

        //1562 | Aura of Dragon's Flame
        Story.KillQuest(1562, "firetunnel", "Elder Magma Wyrm");

        //1563 | Spirit of the Black Unicorn
        if (!Story.QuestProgression(1563))
        {
            Core.EnsureAccept(1563);
            Core.HuntMonster("firetown", "Obsidian Golem", "Heart of Stone", 5);
            Core.HuntMonster("firetown", "Fire Spirit", "Fiery Will", 5);
            Core.HuntMonster("fireriver", "Loup-Garou", "Primal Spirit", 5);
            Core.EnsureComplete(1563);
        }

        //1564 | Tie a Black Ribbon 'Round an Old Burnt Tree
        Story.KillQuest(1564, "firetown", "Burnt Tree");

        bool CalculateFriday13()
            => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 13).DayOfWeek == DayOfWeek.Friday && DateTime.Now.Day >= 5;
    }
}
