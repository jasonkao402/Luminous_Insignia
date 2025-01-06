using Luminous_Insignia.Content.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Luminous_Insignia.Common.Players
{
	internal class DamageStatsModificationPlayer : ModPlayer
    {
        public float AdditiveCritDamageBonus;

        public override void ResetEffects() {
            AdditiveCritDamageBonus = 0f;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            if (AdditiveCritDamageBonus > 0) {
                modifiers.CritDamage += AdditiveCritDamageBonus;
            }
        }
    }
}