using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Pig
{
    public class Hero : ApplicationCommandModule
    {
        static Random _random = new Random();

        [SlashCommand("hero", "Get a random spell from a top deck")]
        public async Task HeroCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

            string response = "Kamikaze";
            string mpCost = "";
            string damage = "";
            string description = "";

            int random = _random.Next(0, 21);

            switch (random)
            {
                case 0:
                    response = "Bang";
                    mpCost = "9";
                    damage = "1.5% (spark) 14%/12% (explosion)";
                    description = "A small, spark-like projectile that generates a small explosion after a set distance or on contact with opponents. Deals decent knockback, KOing vertically starting at around 130%. Cannot appear alongside Kaboom.";
                    break;
                case 1:
                    response = "Kaboom";
                    mpCost = "37";
                    damage = "2% (spark), 26%/21% (explosion)";
                    description = "A small, spark-like projectile that generates a large explosion after a set distance or on contact with opponents. The instant before the explosion pulls opponents in if they're close enough. Possesses high knockback and a very large explosion hitbox, KOing around 85% vertically. Cannot appear alongside Bang.";
                    break;
                case 2:
                    response = "Sizz";
                    mpCost = "8";
                    damage = "1.5% (bolt), 12% (flames)";
                    description = "A fiery, bolt-like projectile that travels quickly. Lacks KO potential onstage, but can KO opponents offstage. Cannot appear alongside Sizzle.";
                    break;
                case 3:
                    response = "Sizzle";
                    mpCost = "20";
                    damage = "3% (bolt), 22% (flames)";
                    description = "A fiery, bolt-like projectile that travels quickly and leaves damaging flames on the ground in its wake. Deals high damage despite its speed. Deals decent knockback, KOing around 100% near the edge. Cannot appear alongside Sizz.";
                    break;
                case 4:
                    response = "Flame Slash";
                    mpCost = "12";
                    damage = "22% (blade), 17% (flames)";
                    description = "A quick slash with Hero's sword cloaked in flame. Deals considerable damage and knockback, especially in regard to its blade hitbox, and has notable range both horizontally and vertically. It can KO middleweights at 90% from the center of Final Destination.";
                    break;
                case 5:
                    response = "Kacrackle Slash";
                    mpCost = "11";
                    damage = "17% (blade), 13% (frost)";
                    description = "A quick slash with Hero's sword cloaked in frost. It freezes opponents, with its blade hitbox freezing them for considerably longer than the frost itself.";
                    break;
                case 6:
                    response = "Hatchet Man";
                    mpCost = "15";
                    damage = "35%";
                    description = "A delayed downward slash which resembles his forward smash, triggering Special Zoom and a Critical Hit sound effect on hit. Has considerable start-up, but possesses extremely high damage and knockback output, making it capable of KOing early, around 45% from center stage and much earlier near the edge. It can also instantly break a full shield, similarly to a fully charged Shield Breaker.";
                    break;
                case 7:
                    response = "Snooze";
                    mpCost = "16";
                    damage = "-";
                    description = "Hero extends his hand forward to launch a spherical, pulse-like projectile that sends opponents to sleep if it connects. It can pass through multiple opponents, but can also be blocked. The projectile puts opponents to sleep for longer at close range, but becomes larger the farther it travels. Infamous for its ability to grant Hero a free charged forward smash, and therefore an early KO, if it hits a grounded opponent at mid percentages. Snooze can affect aerial opponents, which can KO offstage opponents should they be too far to maneuver towards the stage. However, opponents in the air are put to sleep for a much shorter amount of time, due to them receiving 40% of the knockback growth of the grounded hitbox. As a result, Snooze's clean aerial hitbox puts opponents to sleep for less time than its latest grounded hitbox. The projectile will not disappear until its active frames conclude, so it can be thrown at edges to affect recovering enemies.";
                    break;
                case 8:
                    response = "Whack";
                    mpCost = "10";
                    damage = "1%";
                    description = "Hero swings his hand forward to launch a slow-moving dark purple sphere. Has a chance to instantly KO the opponent, with a higher chance for it to occur the more damage the opponent has; the formula for Whack to instantly KO is 1 + (pt - 20) / (300 - 20) * 200 + 20 * (pH / 300), where pt is the target's damage pre-hit limited to between 20% and 300%, and pH is Hero's damage limited to 300 % or less.As a result, the final rate is always 1 % when the opponent is below 20 %, and scales up to 100 % when the opponent is just below 160 %, with a maximum extra 20 % chance if Hero is at 300 %.The move is notoriously difficult to reflect, as the speed and direction of the reflected projectile depends on the speed and direction the erratically moving projectile was travelling at the moment of reflection. As such, it will often deflect into the ground or up into the air, and in some cases will move so slowly that it seemingly stays in place.Cannot appear alongside Thwack.";
                    break;
                case 9:
                    response = "Thwack";
                    mpCost = "30";
                    damage = "3%";
                    description = "	Hero swings his hand forward to emit a large, dark purple blast directly in front of himself. Has a chance to instantly KO the opponent, with a higher chance for it to occur the more damage the opponent has. It has a very large hitbox, and a windbox around it which pulls nearby opponents in. However, it also has considerable start-up lag, a minimal damage output and weak knockback. It can also be reflected, absorbed, and shielded. Cannot appear alongside Whack.";
                    break;
                case 10:
                    response = "Metal Slash";
                    mpCost = "6";
                    damage = "1%";
                    description = "	Hero slashes his sword forward. instantly KOs any metal opponent, including another Hero using Kaclang. When used against non-metal opponents, it will only deal 1% and weak knockback. If there is a metal opponent present when the menu is opened, it has a 4× greater chance of appearing. In Spirit Battles, however, a multiplier of 0.2× is applied to its appearance rate.";
                    break;
                case 11:
                    response = "Kaclang";
                    mpCost = "6";
                    damage = "15% (falling)";
                    description = "	Encases Hero in metal and makes him impervious to both damage and knockback for a period of time, but also renders him inert. It cannot be canceled once activated. However, Hero can still be KO'd by another Hero using Metal Slash, and any well-timed attack is sufficient to punish him during the ending lag of the move. Using Kaclang in the air will grant Hero a hitbox during his descent (similarly to Stone) and allow him to move left and right until he lands.";
                    break;
                case 12:
                    response = "Kamikazee";
                    mpCost = "1";
                    damage = "0.1% (hit 1), 50% (hit 2, close), 35% (hit 2, far)";
                    description = "Hero crosses his arms and begins to glow. After a few seconds, he explodes, instantly KOing himself and inflicting a large amount of damage and knockback to any opponent caught in its large blast radius. The explosion is unblockable, and its sweetspot is strong enough to KO many opponents at 50%-60%. The hitbox only lasts briefly, allowing opponents to dodge and avoid the explosion, even with sidesteps. Using this will count as a KO for the last opponent to hit him. Hero is intangible during the charge up. Kamikazee does not appear to be available as a menu option during the World of Light Finale.";
                    break;
                case 13:
                    response = "Magic Burst";
                    mpCost = "All remaining MP";
                    damage = "maximum: 4.2% (loop), 11% (last)";
                    description = "An enormous, spherical explosion that grows in size, similarly to a Smart Bomb's explosion. It hits multiple times and is active for a few seconds. Consumes all of Hero's remaining MP; the spell's power depends on how much MP Hero has left. The attack has extremely large range, covering half of Final Destination, and with full MP, it can KO at merely 20%. Due to these factors, along with the move itself being surprisingly quick to activate, it is widely considered to be one of Hero's best command specials (if not his absolute best one), and arguably the most feared, albeit only in circumstances where Hero has at least around half of his MP remaining. Additionally, the move is blockable, with each hit having slightly reduced shield damage. Projectiles can also cancel out the move, and he can be hit by physical attacks at the very beginning of the move's start-up, leaving Hero with no MP and at a great disadvantage, making Magic Burst a rather high risk, high reward move. The loop hits do very little knockback; as a result, Bowser and Kazuya can use their passive armor to retaliate quickly. The damage dealt is multiplied by 0.2 + 0.008MP, with the value being between 0.2× and 1×.";
                    break;
                case 14:
                    response = "Oomph";
                    mpCost = "16";
                    damage = "-";
                    description = "Increases Hero's damage output by 1.6×, knockback output by 1.1×, and damage received by 1.2×. Only affects melee attacks. Lasts for 12 seconds.";
                    break;
                case 15:
                    response = "Psyche Up";
                    mpCost = "14";
                    damage = "-";
                    description = "Increases Hero's damage output by 1.2×, knockback output by 1.2×, and shield damage output by 1.65×. Functions similarly to Revenge and if stacked with Oomph, many of Hero's normal attacks will instantly break a full shield. Only affects melee attacks. Expires after landing a melee attack. As it only boosts the next melee hit that connects, pummels will use up Psyche Up, and stronger versions of Zap (Zapple and Kazap) will not gain increased KO potential, as both moves consist of two hits.";
                    break;
                case 16:
                    response = "Acceleratle";
                    mpCost = "13";
                    damage = "-";
                    description = "Multiplies Hero's walking speed, dashing speed, air speed, air acceleration, and air friction stats by 2×-2.5×, depending on the stat. Functions similarly to Lightweight, but to a sufficiently high degree that Hero can outrun Sonic and can reach a maximum air speed that is more than 1.5× faster than Yoshi's. However, the sheer speed can make Hero more difficult to control, and it increases the knockback he receives by 1.1×. Lasts for 10 seconds.";
                    break;
                case 17:
                    response = "Bounce";
                    mpCost = "14";
                    damage = "-";
                    description = "	A lingering reflector that increases projectile damage by 1.5×, projectile speed by 1.4×, and projectile health by 1.1×. Like a Franklin Badge, Bounce lets Hero act while it is active, and overrides Hero's passive shield, allowing reflections even if he is standing and facing an opponent. One of Hero's best approach options against projectile users, due to him being able to perform any action while keeping an active reflector. Lasts for 12 seconds.";
                    break;
                case 18:
                    response = "Heal";
                    mpCost = "7";
                    damage = "Heals 11%";
                    description = "Heals Hero by 11%. Starts with two charges, and resets to two after Hero is KO'd or scores a KO.";
                    break;
                case 19:
                    response = "Zoom";
                    mpCost = "8";
                    damage = "-";
                    description = "Hero jumps directly upward, going past the limits of the camera, then falls directly onto a random part of the stage, no matter where he was when the spell was used. Hero can act out of his descent while maintaining momentum, unless he performs an action that alters momentum. If used where there is a ceiling above Hero, he will collide against it, while still being able to act afterward. It is invulnerable as soon as Hero starts to fly. Has a 2× (≈53%) chance of appearing in Command Selection when 40 units (or closer) to the blast line.";
                    break;
                default:
                    response = "Hocus Pocus";
                    mpCost = "4";
                    damage = "-";
                    description = "	A spell with random effects, either positive or negative, that can also take the form of other Command Selection spells.";
                    break;
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"{response}; Mana Cost: {mpCost}, Damage: {damage}; {description}"));
        }
    }
}
