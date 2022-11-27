//cs_include Scripts/CoreBots.cs

using Skua.Core.Interfaces;
using Skua.Core.Options;

public class EternalDragonDrops
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("CanSolo", "Can solo boss?", "unchecking this will take you to public room", false),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetDrops();

        Core.SetOptions(false);
    }

    public void GetDrops()
    {
        string[] MemDrops = {
            "Dark Embrace Of The Queen",
            "ShadowFlame Aura",
        };

        string[] NonMemDrops = {
            "Dark Stars",
            "Embrace of the Queen",
            "Enchanted Dragon's Battle Gear",
            "Monster Queen's Locks",
            "Monster Queen's Malicious Morph",
            "Monster Queen's Wings",
            "Requiescat Regina",
            "Requiescat Regina Cane",
            "Requiescat Regina Hair",
            "Requiescat Regina Locks",
            "ShadowFlame Portal",
            "Timestream Ravager's Sigil"
        };

        if (Core.CheckInventory(NonMemDrops))
        {
            Core.Logger("You already have the items");
            return;
        }

        if (Core.IsMember)
            Core.AddDrop(MemDrops);
        Core.AddDrop(NonMemDrops);

        Core.EquipClass(ClassType.Solo);


        Core.Logger("Starting to get drops");
        //Can't Solo section
        if (!Bot.Config.Get<bool>("CanSolo"))
            while (!Bot.ShouldExit && !Core.CheckInventory(NonMemDrops))
                Core.HuntMonster("deadlines", "Eternal Dragon", "*", isTemp: false, publicRoom: true);
        else
            while (!Bot.ShouldExit && !Core.CheckInventory(NonMemDrops))
                Core.HuntMonster("deadlines", "Eternal Dragon", "*", isTemp: false);
    }
}