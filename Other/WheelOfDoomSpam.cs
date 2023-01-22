/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

public class WheelOfDoomSpam
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        WhalingTime();
    }

    public void WhalingTime()
    {
        Core.Logger("Pay attention to the popup's, they are crucial to this bot");
        bool? accept = Bot.ShowMessageBox("This bot will use up a lot of your ACs (which costs real money).\n" +
        "We are not liable for any loss of ACs whilst using this bot.\n\n" +
        "Do you wish to proceed?", "Your AC is about to be spend", true);
        if (accept != true)
            return;

        var goForbroke = Bot.ShowMessageBox("Do you wish to use select how many tickets to use?\n" +
        "Or maybe you wanna go for broke",
        "Mode Selector", "Select Amount", "GO FOR BROKE!");

        int amount = 0;
        int currentAC = Bot.Flash.GetGameObject<int>("world.myAvatar.objData.intCoins");
        if (goForbroke.Text == "Select Amount")
        {
            InputDialogViewModel diag = new("Input Amount", "How many tickts would you like to buy and use?", true);
            while (!Bot.ShouldExit)
            {
                if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(diag) == true)
                {
                    amount = Int32.Parse(diag.DialogTextInput);
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

        for (int i = 0; i < amount; i++)
        {
            Bot.Shops.BuyItem(45252, 26665);
            Bot.Sleep(Core.ActionDelay);
            var preTicketItems = Bot.Inventory.Items;
            Core.ChainComplete(3074);
            Bot.Sleep(Core.ActionDelay);
            var newItems = Bot.Inventory.Items.Except(preTicketItems);
            foreach (var item in newItems)
            {
                if (item.Name != "Treasure Potion")
                    Core.Logger("New Item: " + item.Name);
            }
        }
        Core.Logger($"You have earned {preTicketTP - Bot.Inventory.GetQuantity("Treasure Potion")} more Treasure Potions");
    }
}
