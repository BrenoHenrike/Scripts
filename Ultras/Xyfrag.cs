//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class Xyfrag
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public string taunterClass;
    public bool DontPreconfigure = true;
    public string OptionsStorage = "Xyfrag";
    public List<IOption> Options = new() { new Option<string>("taunterClass", "Taunter Class", "Insert the name of the class that will taunt", ""), };

    public void ScriptMain(IScriptInterface bot)
    {
        taunterClass = Bot.Config.Get<string>("taunterClass") ?? string.Empty;
        if (string.IsNullOrEmpty(taunterClass))
        {
            Bot.Log("Taunter not filled in! Please edit Script Options.");
            Bot.Stop();
        }

        Core.Boot();

        Kill(taunterClass);

        Bot.Stop();
    }

    void Kill(string taunterClass)
    {
        Core.UseAlchemyPotions(Core.GetBestTonicPotion());
        Core.UseAlchemyPotions(Core.GetBestElixirPotion());

        if (Core.HasClassEquipped(taunterClass))
        {
            Bot.Events.ExtensionPacketReceived += Core.ChargeListener;
            Core.GetScrollOfEnrage();
        }
        else
        {
            Core.BuyAlchemyPotion("Potent Honor Potion");
            Core.EquipConsumable("Potent Honor Potion");
        }

        Core.Join("voidxyfrag");
        Core.WaitForArmy(6);
        Core.ChooseBestCell("Xyfrag");
        Core.EnableSkills();

        while (Core.MonsterAlive("Xyfrag") && !Bot.ShouldExit)
        {
            if (Core.HasClassEquipped(taunterClass))
                Core.TauntCharge(taunterClass, "Xyfrag", "Focus", 250);
            else
            {
                Core.Kill("Xyfrag");
                Bot.Skills.UseSkill(5);
            }
        }
    }
}
