# Skills

It is recommended you load skills from an XML skills file created from the Skills form. If you would like to manually manage skills in your script, you can do this through `ScriptInterface#Skills`. Manual management is overridden by the UI by default.

- [Skills](#skills)
  - [Properties](#properties)
  - [Methods](#methods)
  - [Skill Use Rules](#skill-use-rules)
  - [Patterns](#patterns)
  - [Advanced Skills](#advanced-skills)

## Properties

The following properties can be modified to change the behaviour of the skill timer thread:

| Property | Type | Description |
|---|:---:|---|
| `SkillTimer` | *int* | The time period in ms at which the timer cycles through skills and tries to use them. The default is 100ms. |
| `SkillTimeout` | *int* | The timeout in **multiples** of SkillTimer milliseconds it will wait before skipping the current unavailable skill when using SkillMode.Wait (which comes with [Patterns](/docs/5%20Skills#patterns) or your own [SkillProvider](/docs/5%20Skills#skill-provider)). The default is -1 (no timeout). |
| `TimerRunning` | *bool* | Whether the skill timer thread is currently running. |

## Methods

The following methods can be used to programatically manage skills. The method's return types are ommitted as they are all void.

| Method Definition | Description |
|---|---|
| `StartTimer()` | Starts the skill timer thread. This must be called for the bot to use skills whether you are using the skills form or manually managing skills (see example below). If the thread is already running, this method does nothing but replace the currently loaded skills with the UI override skills if they are set. |
| `StopTimer()` | Kills the skill timer thread if it is running. This will stop the bot from using any skills. |
| `StartPattern(string def)` | Loads the pattern definition file and restarts the skill timer thread. |
| `StartSkills(string xml)` | Loads the skill definition txt file and restarts the skill timer thread. |
| `Add(int index, float useThresh)` | Adds the skill with the given index (1-4) to the timer. This skill will only be used when the player's health is below `useThresh`, where `useThresh` is a fraction of the player's health over the player's maximum health. A `useThresh` of `1f` indicates that the skill can always be used. |
| `Add(int index, UseRule rule)` | Adds the skill with the given index (1-4) to ther timer to only be used when the given `UseRule` allows. See below about use rules. |
| `Remove(int index)` | Removex the skill with the given index from the timer. This skill will no longer be used unless it is added again. |
| `Clear()` | Clears all skills from the timer. |
| `LoadSkills(string xml)` | Loads a skill definition XML file which was created by the UI. `xml` is the path to the definition file which can be absolute or relative to the running directory of the bot. |
| `LoadPattern(string def)` | Loads a pattern definition file (can be any type of text file). `def` is the path to the definition file which can be absolute or relative to the running directory of the bot. |
| `StartSkills(string xml)` | Loads a skill definition XML file and then restarts the skill timer. |

## Skill Use Rules

If you would like finer grained control over when skills are used, you can apply a `UseRule` to a skill at the given index. There are 3 rules which can be used in the UI:

1. `HealthUseRule` - Specifies a minimum and maximum health at which the skill can be used.
2. `ManaUseRule` - Specifies a minimum and maximum mana at which the skill can be used.
3. `CombinedUseRule` - Combines any number of other rules with the specified logical operator.

These are very simply understood using the UI skills editor.

You can also create your own use rules using a script. To do this, create a class that extends `UseRule` and override the `UseRule#ShouldUse` method. For example:

```csharp
using RBot;
using System;

public class Script
{
    public void ScriptMain(ScriptInterface bot)
    {
        bot.Skills.Add(1, new ExampleUseRule());
        // ...add other skills
        bot.Skills.StartTimer();

        // ...other bot code
    }

    public class ExampleUseRule : UseRule
    {
        public int Interval { get; set; } = 2000;

        private int lastUse = 0;

        public override bool ShouldUse(ScriptInterface bot)
        {
            if(Environment.TickCount - lastUse > Interval)
            {
                lastUse = Environment.TickCount;
                return true;
            }
            return false;
        }
    }
}
```

This allows the skill at index `1` to only be used every 2 seconds. In this example, `Interval` is configurable to change this 2 seconds to any other time.

## Patterns

Alongside with Skill XMLs, you can also define ordered skill patterns using skill pattern definition files. They are very simple and contain 3 commands:

- `repeat <n>`: Repeats a sublist of commands `n` times.
- `pattern <pattern>`: Executes the skill indexes in `pattern` in the given order.
- `end`: Ends the current repeat sublist. Note that nested repeats are supported.

For example, the skill pattern definition file for Immortal Chronomancer might look like this:

```csharp
//Repeat the sublist 7 times
repeat 7
    //Use the skills 1 then 4
    pattern 14
//End of first repeat
end
//Use the skills 2 then 3
pattern 23
//Repeat the sublist 5 times
repeat 5
    //Use the skills 4 then 1
    pattern 41
//End of second repeat
end
//Use the skills 4, 2 and 3
pattern 423
```

You can create it in any text editor and then save it like `fileName.txt` or `fileName.def`. To use a skill pattern definition, simply call `ScriptSkills#StartPattern`:

```csharp
bot.Skills.StartPattern("Skills/ic.def");
```

This will override any other options set up with the skill timer (including manually added skills and loaded xml definitions). To clear the loaded skill pattern, call `ScriptSkills#ClearPattern`:

```csharp
bot.Skills.ClearPattern();
```

**Note:** The execution state of skill patterns are reseted when the player has no target (i.e. the skill timer starts again from the beginning of the pattern definition file when the player loses their target, and targets another monster).

## Advanced Skills

In RBot 3.6.2 you are able to set up advanced skills that allow you to manage how your skills are used. This way you can create skill sequences for any class that wasn't possible before, in special Chronos. The Advanced Skill is an implementation of the Core Skill Plugin for RBot with small changes. Bellow I will attach the guide I made for the plugin creating a sequence for Void Highlord, after it I will add explanation for the save menu.

In the main menu you can click **Skills** and then **Advanced** to show the UI of the plugin:

<p align="center"><img src="https://imgur.com/AUIOhFe.png"></p>

1. This is the list where the skills you add will end up. After adding them you can use:
   - Arrow keys (Up/Down) to navigate;
   - Ctrl + Arrow keys to move them in the sequence;
   - Delete to remove from the sequence;
   - Ctrl + Delete to clear the sequence;
   - You can also drag them with your mouse or do any of the commands with a right click.
2. Here you can add how the bot will use the skills inside the game. If you set the values and want to reset them, right click and then click _"Reset"_. Each line does, respectively:
   - Checkbox that determines if you want to add the use rule to the skill;
   - Health percentage the skill need to be used, you can click in the '>' signal to invert it;
   - Mana quantity the skill need used, you can click in the '>' signal to invert it;
   - How many time, in milliseconds, the bot will wait to use the skill;
   - Checkbox that determine whether the skill should be skipped if it is not available.
3. Will add the skill with the index (1-4) of the numeric field with the use rules (if checked to do so) to the list.
4. The use mode of the skill:
   - Optimistic - If the bot can use the skill, it will use it. If not the skill will be skipped.
   - Wait (Default) - Will wait for the skill to cooldown (or the timeout time) before using the skill;
5. A simple calculator of the SkillTimeout property. To use you enter the value of the SkillTimer (default is 100) and the longest skill cooldown of your class, after pressing enter it will calculate the SkillTimeout and save it with your skill sequence;
6. This button will convert all data in the list sequence to a string you can use in the method I will list bellow. After clicking that button it will automatically copy it to your clipboard.
7. Where the string you need will show up.

The Void Highlord damage skill sequence is `4-5-3-2-4-3-2`, to make it in this plugin we first need to consider that the index of the skill is its number -1 (minus one) as we do not count the auto attack, so the skill sequence will look like `3-4-2-1-3-2-1`. First we add the 3 which is _Unshackled_ and it uses 20% of your health, so to not embarrassingly die we set it up like this:

<p align="center"><img src="https://imgur.com/X4bDDxG.png"></p>

And then click the **Add** button. Note that I setted the health to be 25% so it give us a breathing range and checked _"Skip if not available"_ so we don't sit waiting this skill while bellow 25% HP. Following this step we can complete the sequence so it will look like that in the list:

<p align="center"><img src="https://imgur.com/QNOASl5.png"></p>

The **Use Mode** is optional but I recommend you always use the Wait mode (unless your class doesn't need a defined sequence). With everything right you can click the **"Convert"** button.

<p align="center"><img src="https://imgur.com/AKGlJY8.png"></p>

For the **SkillTimeout** (only calculate it if you're using the **Wait Use Mode**) of VHL we can use the longest CD which is of _Armageddon_ with 15 seconds, convert it to milliseconds (15000) and if our **SkillTimer** is 100ms it will give us **SkillTimeout = 150**.

Now after converting the string you have 2 ways of using it which is saving with the class name (or whatever you want, the class name is good because this way you can set it to auto equip it):

<p align="center"><img src="https://imgur.com/vt7Gf7e.png"></p>

Then you can use it in your script like:

```cs
// You specify the name of the saved skill sequence and whether you want to equip the class,
// this allows you to use multiple skill sequences for the same class.
bot.Skills.StartAdvanced("Void Highlord", true);
```

If you don't want to save it, you can input the skill sequence and timeout directly like:

```cs
// You specify the skill sequence and the skill timeout.
// Unlike the previous method, this can't read the timeout so you need to pass it as a parameter (default is -1).
bot.Skills.StartAdvanced("3 H>25S | 4 | 2 | 3 H>25S | 1 H>25S | 2 | 1 H>25S | Timeout:150", 150)
```

For now you can only save and override skills with the UI which should be enough. If you want to delete a sequence you can open the *AdvancedSkills.txt* and delete the unwanted line.

---------
<center><a href="/Rbot-Scripts/Monsters" title="Monsters">◄ Previous</a> — <a href="/Rbot-Scripts/" title="Back to Index">Index</a> — <a href="/Rbot-Scripts/Shops" title="Shops">Next ►</a></center>