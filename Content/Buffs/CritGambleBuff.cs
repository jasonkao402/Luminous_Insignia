using Luminous_Insignia.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Luminous_Insignia.Content.Buffs
{
    public class CritGambleBuff : ModBuff
    {
        public static readonly float critMultiplier = 1.0f;
        // public override void SetStaticDefaults()
        // {
        //     // DisplayName.SetDefault("Gamble");
        //     Tooltip.SetDefault("Critical damage increased by 2x!");
        //     Main.buffNoSave[Type] = false; // Buff persists after reloading the world
        //     Main.debuff[Type] = false;    // Not a debuff
        // }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DamageStatsModificationPlayer>().AdditiveCritDamageBonus += critMultiplier;
            player.GetCritChance(DamageClass.Generic) += 30;
        }
    }
}
