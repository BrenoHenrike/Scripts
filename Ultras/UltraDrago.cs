//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UltraDrago
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public string primaryTaunter;
    public string secondaryTaunter;
    public bool DontPreconfigure = true;
    public string OptionsStorage = "UltraDrago";
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

        Bot.Quests.UpdateQuest(8395);
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
        Core.Join("ultradrago");
        Core.WaitForArmy(3);
        Core.ChooseBestCell("King Drago");
        Core.EnableSkills();

        while (Core.MonsterAlive("King Drago") && !Bot.ShouldExit)
            if (Core.HasClassEquipped(primaryTaunter) || Core.HasClassEquipped(secondaryTaunter))
            {
                while (Core.MonsterAlive("Executioner Dene") && !Bot.ShouldExit)
                {
                    if (Core.HasClassEquipped(primaryTaunter))
                        Core.TauntCycle(primaryTaunter, "Executioner Dene", "Focus", 250);
                    else if (Core.HasClassEquipped(secondaryTaunter))
                        Core.TauntCycle(secondaryTaunter, "Executioner Dene", "Focus", 700);
                }
            }
            else
            {
                Core.KillWithPriority("King Drago", "Bowmaster Algie", "Executioner Dene");
                Bot.Skills.UseSkill(5);
            }
    }
}
