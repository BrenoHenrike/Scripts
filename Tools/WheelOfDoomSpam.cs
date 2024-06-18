/*
name: Wheel Of Doom Spam
description: Wastes (with a warning) your extra ACs on Wheel of Doom spins
tags: wheel, doom, spin, waste, AC, treasure, potions, IODA
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

public class WheelOfDoomSpam
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        WhalingTime();
    }

    public void WhalingTime(bool? stopForIoDA = null)
    {
        Core.Logger("Pay attention to the popup's, they are crucial to this bot");
        bool? accept = Bot.ShowMessageBox("This bot will use up a lot of your ACs (which costs real money).\n" +
        "We are not liable for any loss of ACs whilst using this bot.\n\n" +
        "Do you wish to proceed?", "Your AC is about to be spend", true);
        if (accept != true)
            return;

        if (Core.CheckInventory("Epic Item of Digital Awesomeness", toInv: false))
            stopForIoDA = false;
        else stopForIoDA ??= Bot.ShowMessageBox(
            "Do you wish for the bot to stop after the Epic Item of Digital Awesomeness have been obtained?",
            "EIoDA?", true) == true;

        var goForbroke = Bot.ShowMessageBox("Do you wish to use select how many tickets to use?\n" +
            "Or maybe you wanna go for broke",
            "Mode Selector", "Select Amount", "GO FOR BROKE!");

        int amount = 0;
        int currentAC = Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intCoins");
        if (goForbroke.Text == "Select Amount")
        {
            InputDialogViewModel diag = new("Input Amount", "How many tickets would you like to buy and use?", true);
            while (!Bot.ShouldExit)
            {
                if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(diag) == true)
                {
                    amount = int.Parse(diag.DialogTextInput);
                    if (amount <= 0)
                        Bot.ShowMessageBox("Please provide a number greater than zero (0) in order to continue.", "Invalid input");
                    else
                    {
                        int afterAC = currentAC - (amount * 200);
                        if (afterAC < 0)
                        {
                            Bot.ShowMessageBox(
                                $"Your input: {amount} tickets\n" +
                                $"This will cost you {amount * 200} ACs. (200 ACs per ticket)\n" +
                                $"This would leave you with {afterAC} ACs afterwards, which is impossible.\n\n" +
                                "Please choose a different amount of tickets",
                                $"Calculating based on {amount} tickets"
                            );
                        }
                        else
                        {
                            bool? acceptResult = Bot.ShowMessageBox(
                                $"Your input: {amount} tickets\n" +
                                $"This will cost you {amount * 200} ACs. (200 ACs per ticket)\n" +
                                $"This would leave you with {afterAC} ACs afterwards.\n\n" +
                                "Do you wish to proceed?",
                                $"Calculating based on {amount} tickets", true
                            );
                            if (acceptResult == true)
                                break;
                        }
                    }
                }
                else Bot.Stop(false);
            }
        }
        else if (goForbroke.Text == "GO FOR BROKE!")
            amount = currentAC / 200;

        Core.Join("doom");
        Bot.Shops.Load(707);
        int preTicketTP = Bot.Inventory.GetQuantity("Treasure Potion");
        var rewards = Core.QuestRewards(3074);

        for (int i = 0; i < amount; i++)
        {
            Bot.Shops.BuyItem(45252, 26665);

            Core.Sleep();
            var preTicketItems = Bot.Inventory.Items;

            Core.ChainComplete(3074);
            Core.Sleep();

            var newItems = Bot.Inventory.Items.Except(preTicketItems);
            foreach (var item in newItems)
            {
                if (item.Name != "Treasure Potion")
                    Core.Logger("New Item: " + item.Name);
            }

            if (stopForIoDA == true && Core.CheckInventory("Epic Item of Digital Awesomeness"))
            {
                Bot.ShowMessageBox("Epic Item of Digital Awesomeness", "EIoDA!");
                break;
            }

            if (Core.CheckInventory(rewards, toInv: false))
            {
                Bot.ShowMessageBox("All Wheel of Doom items obtained!", "You maxed out the Wheel of Doom");
                break;
            }
        }
        Core.Logger($"You have earned {preTicketTP - Bot.Inventory.GetQuantity("Treasure Potion")} more Treasure Potions");
    }
}
