//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using RBot;

public class DERPBadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBadge();

        Core.SetOptions(false);
    }

    public void GetBadge()
    {

        //Progress Check
        if (Core.isCompletedBefore(8007))
            return;

        // if (!Core.CheckInventory("Rainbow Derpicorn Guard (R)"))
        // {
        //     Core.HuntMonster("battlefools", "Rainbow Derpicorn", "Rainbow Derpicorn Guard (R)", isTemp: false);
        // }

        Core.AddDrop(new[] { "Rainbow Derpicorn Guard (L)", "Rainbow Derpicorn Guard (R)" });

        while (!Bot.Inventory.ContainsHouseItem("Rainbow Derpicorn Guard (R)"))
        {
            Core.HuntMonster("battlefools", "Rainbow Derpicorn");
        }

        Core.EnsureAccept(8007);
        Core.HuntMonster("gardenquest", "Sketchy Chickencow", "Sketchy Chickencow Slain");
        Core.EnsureComplete(8007);

    }
}