//cs_include Scripts/Ultras/CoreUltras.cs

using System;
using System.Dynamic;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UltraDage
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreUltras Core = new();

    public string taunterClass;
    public bool DontPreconfigure = true;
    public string OptionsStorage = "UltraDage";
    public List<IOption> Options = new() {
        new Option<string>("taunterClass", "Taunter Class", "Insert the name of the class that will taunt", "")
    };

    public void ScriptMain(IScriptInterface bot)
    {
        taunterClass = Bot.Config.Get<string>("taunterClass") ?? string.Empty;
        if (string.IsNullOrEmpty(taunterClass))
        {
            Bot.Log("Taunter not filled in! Please edit Script Options.");
            Bot.Stop();
        }

        Core.Boot();
        Bot.Events.ExtensionPacketReceived += UltraDageListener;

        Bot.Quests.UpdateQuest(793);
        PreparePotions(taunterClass);
        Kill(taunterClass);

        Bot.Events.ExtensionPacketReceived -= UltraDageListener;
        Bot.Stop();
    }

    void PreparePotions(string taunterClass)
    {
        Core.UseAlchemyPotions(Core.GetBestTonicPotion());
        Core.UseAlchemyPotions(Core.GetBestElixirPotion());

        Core.BuyAlchemyPotion("Potent Honor Potion");
        Core.EquipConsumable("Potent Honor Potion");

        if (Core.HasClassEquipped(taunterClass))
            Core.GetScrollOfEnrage();
    }

    void Kill(string taunterClass)
    {
        Core.Join("ultradage");
        Core.WaitForArmy(3);
        Core.ChooseBestCell("Dage the Dark Lord");
        Core.EnableSkills();

        while (Core.MonsterAlive("Dage the Dark Lord") && !Bot.ShouldExit)
        {
            if (Core.HasClassEquipped(taunterClass))
                Core.TauntCycle(taunterClass, "Dage the Dark Lord", "Focus", 250);
            else
            {
                Core.Kill("Dage the Dark Lord");
                Bot.Skills.UseSkill(5);
            }
        }
    }

    async void UltraDageListener(dynamic packet)
    {
        if (packet?["params"]?.type?.ToString() != "json") return;

        dynamic data = packet["params"].dataObj;
        if (data?.cmd?.ToString() != "event") return;

        string zone = data?.args?.zoneSet?.ToString();

        if (string.Equals(zone, "A", System.StringComparison.OrdinalIgnoreCase))
        {
            Bot.Send.Packet($"%xt%zm%mv%{Bot.Map.RoomID}%122%411%8%");
            return;
        }
        if (string.Equals(zone, "B", System.StringComparison.OrdinalIgnoreCase))
        {
            Bot.Send.Packet($"%xt%zm%mv%{Bot.Map.RoomID}%856%422%8%");
            return;
        }
        if (string.IsNullOrEmpty(zone))
        {
            Bot.Send.Packet($"%xt%zm%mv%{Bot.Map.RoomID}%491%421%8%");
            return;
        }
    }
}
