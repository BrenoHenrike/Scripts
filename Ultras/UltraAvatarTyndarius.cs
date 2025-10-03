//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UltraAvatarTyndarius
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public string primaryTaunter;
    public string secondaryTaunter;
    public bool DontPreconfigure = true;
    public string OptionsStorage = "UltraAvatarTyndarius";
    public List<IOption> Options = new()
    {
        new Option<string>("primaryTaunter", "First Taunter Class", "Insert the name of the class that will taunt", ""),
        new Option<string>("secondaryTaunter", "Second Taunter Class", "Insert the name of the class that will taunt", ""),
    };

    public void ScriptMain(IScriptInterface bot)
    {
        primaryTaunter = Bot.Config.Get<string>("primaryTaunter") ?? string.Empty;
        if (string.IsNullOrEmpty(primaryTaunter))
        {
            Bot.Log("First taunter not filled in! Please edit Script Options.");
            Bot.Stop();
        }

        secondaryTaunter = Bot.Config.Get<string>("secondaryTaunter") ?? string.Empty;
        if (string.IsNullOrEmpty(secondaryTaunter))
        {
            Bot.Log("Second taunter not filled in! Please edit Script Options.");
            Bot.Stop();
        }

        Core.Boot();

        PreparePotions(primaryTaunter, secondaryTaunter);
        Kill(primaryTaunter, secondaryTaunter);

        Bot.Stop();
    }

    void PreparePotions(string primaryTaunter, string secondaryTaunter)
    {
        Core.UseAlchemyPotions(Core.GetBestTonicPotion());
        Core.UseAlchemyPotions(Core.GetBestElixirPotion());

        if (Core.HasClassEquipped(primaryTaunter) || Core.HasClassEquipped(secondaryTaunter))
            Core.GetScrollOfEnrage();
        else
        {
            Core.BuyAlchemyPotion("Potent Honor Potion");
            Core.EquipConsumable("Potent Honor Potion");
        }
    }

    void Kill(string primaryTaunter, string secondaryTaunter)
    {
        Core.Join("ultratyndarius");
        Core.WaitForArmy(3);
        Core.ChooseBestCell("Ultra Avatar Tyndarius");
        Core.EnableSkills();

        while (Core.MonsterAlive("Ultra Avatar Tyndarius") && !Bot.ShouldExit)
        {
            if (Core.HasClassEquipped(primaryTaunter))
                Core.TauntCycle(primaryTaunter, "Ultra Avatar Tyndarius", "Focus", 250);
            else if (Core.HasClassEquipped(secondaryTaunter))
                Core.TauntCycle(secondaryTaunter, "Ultra Avatar Tyndarius", "Focus", 700);
            else
            {
                Core.KillWithPriority("Ultra Avatar Tyndarius", 2, "Ultra Fire Orb", 3, "Ultra Fire Orb", 1);
                Bot.Skills.UseSkill(5);
            }
        }
    }
}
