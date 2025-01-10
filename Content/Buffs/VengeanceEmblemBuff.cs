using Luminous_Insignia.Common.Players;
using Terraria;
using Terraria.ModLoader;
using static System.Math;

namespace Luminous_Insignia.Content.Buffs
{
    public class VengeanceEmblemBuff : ModBuff
    {
        readonly float VengeanceEmblemBonus = 0.01f;
        int tempHealthLost;

        public override void Update(Player player, ref int buffIndex)
        {
            // Increases critical damage proportional to total HP lost, capped at 400 HP lost
            tempHealthLost = Min(player.statLifeMax2 - player.statLife, 400);
            player.GetModPlayer<DamageStatsModificationPlayer>().AdditiveCritDamageBonus += tempHealthLost * VengeanceEmblemBonus;
        }
    }
}
