
# CoreStory

As with any [property](#properties) or [method](#methods) from the `CoreStory.cs` file, you can call upon them by starting with `Story.` (*Story Dot*)

- [CoreStory](#corestory)
  - [Methods](#methods)

## Methods

<table style="width:100%">
    <tr>
        <th style="width:40%">Method Definition</th>
        <th>Return Type</th>
        <th>Description</th>
    </tr>
    <tr>
        <td>
            <code>
                KillQuest(<br>
                &emsp;&emsp;int&nbsp;QuestID,<br>
                &emsp;&emsp;string&nbsp;MapName,<br>
                &emsp;&emsp;string&nbsp;MonsterName,<br>
                <span style="color:lightgray">
                    &emsp;&emsp;bool&nbsp;GetReward&nbsp;=&nbsp;true,<br>
                    &emsp;&emsp;string&nbsp;Reward&nbsp;=&nbsp;"All",<br>
                    &emsp;bool&nbsp;AutoCompleteQuest&nbsp;=&nbsp;true<br>
                </span>
                )
            </code>
        </td>
        <td><i>void</i></td>
        <td>
            Kills a monster for a Quest, and turns in the quest if possible. 
            Automatically checks if the next quest is unlocked. If it is, it will skip this one.
        </td>
    </tr>
    <tr>
        <td>
            <code>
                KillQuest(<br>
                &emsp;&emsp;int&nbsp;QuestID,<br>
                &emsp;&emsp;string&nbsp;MapName,<br>
                &emsp;&emsp;string[]&nbsp;MonsterNames,<br>
                <span style="color:lightgray">
                    &emsp;&emsp;bool&nbsp;GetReward&nbsp;=&nbsp;true,<br>
                    &emsp;&emsp;string&nbsp;Reward&nbsp;=&nbsp;"All",<br>
                    &emsp;bool&nbsp;AutoCompleteQuest&nbsp;=&nbsp;true<br>
                </span>
                )
            </code>
        </td>
        <td><i>void</i></td>
        <td>
            Kills an array of monsters for a Quest, and turns in the quest if possible. 
            Automatically checks if the next quest is unlocked. If it is, it will skip this one.
        </td>
    </tr>
    <tr>
        <td>
            <code>
                MapItemQuest(<br>
                &emsp;&emsp;int&nbsp;QuestID,<br>
                &emsp;&emsp;string&nbsp;MapName,<br>
                &emsp;&emsp;int&nbsp;MapItemID, <br>
                <span style="color:lightgray">
                    &emsp;&emsp;int&nbsp;Amount&nbsp&nbsp;1,<br>
                    &emsp;&emsp;bool&nbsp;GetReward&nbsp;=&nbsp;true,<br>
                    &emsp;&emsp;string&nbsp;Reward&nbsp;=&nbsp;"All",<br>
                    &emsp;bool&nbsp;AutoCompleteQuest&nbsp;=&nbsp;true<br>
                </span>
                )
            </code>
        </td>
        <td><i>void</i></td>
        <td>
            Gets MapItems X times for a Quest, and turns in the quest if possible. 
            Automatically checks if the next quest is unlocked. If it is, it will skip this one.
        </td>
    </tr>
    <tr>
        <td>
            <code>
                MapItemQuest(<br>
                &emsp;&emsp;int&nbsp;QuestID,<br>
                &emsp;&emsp;string&nbsp;MapName,<br>
                &emsp;&emsp;int[]&nbsp;MapItemIDs,<br>
                <span style="color:lightgray">
                    &emsp;&emsp;int&nbsp;Amount&nbsp;=&nbsp;1,<br>
                    &emsp;&emsp;bool&nbsp;GetReward&nbsp;=&nbsp;true,<br>
                    &emsp;&emsp;string&nbsp;Reward&nbsp;"All",<br>
                    &emsp;bool&nbsp;AutoCompleteQuest&nbsp;=&nbsp;true<br>
                </span>
                )
            </code>
        </td>
        <td><i>void</i></td>
        <td>
            Gets MapItems X times for a Quest, and turns in the quest if possible. 
            Automatically checks if the next quest is unlocked. If it is, it will skip this one.
        </td>
    </tr>
    <tr>
        <td>
            <code>
                BuyQuest(<br>
                &emsp;&emsp;int&nbsp;QuestID,<br>
                &emsp;&emsp;string&nbsp;MapName,<br>
                &emsp;&emsp;int&nbsp;ShopID,<br>
                &emsp;&emsp;string&nbsp;ItemName,<br>
                <span style="color:lightgray">
                    &emsp;&emsp;int&nbsp;Amount&nbsp;=&nbsp;1,<br>
                    &emsp;&emsp;bool GetReward = true,<br>
                    &emsp;&emsp;string&nbsp;Reward&nbsp&nbsp;"All",<br>
                    &emsp;bool&nbsp;AutoCompleteQuest&nbsp;=&nbsp;true<br>
                </span>
                )
            </code>
        </td>
        <td><i>void</i></td>
        <td>
            Buys an item X times for a quest, and turns in the quest if possible. 
            Automatically checks if the next quest is unlocked. If it is, it will skip this one.
        </td>
    </tr>
    <tr>
        <td>
            <code>
                QuestProgression(<br>
                &emsp;&emsp;int QuestID,<br>
                <span style="color:lightgray">
                    &emsp;&emsp;bool GetReward&nbsp;=&nbsp;true,<br>
                    &emsp;&emsp;string&nbsp;Reward&nbsp;=&nbsp;"All"<br>
                </span>
                )
            </code>
        </td>
        <td><i>bool</i></td>
        <td>
            Accepts a quest and then turns it in again.
            Automatically checks if the next quest is unlocked. If it is, it will skip this one.
        </td>
    </tr>
    <tr>
        <td>
            <code>PreLoad()</code>
        </td>
        <td><i>void</i></td>
        <td>
            Put this at the start of your story script so that the bot will load all quests that are used in the bot. 
            This will speed up any progression checks tremendiously.
        </td>
    </tr>
</table>

---------