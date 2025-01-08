using System;
using Luminous_Insignia.Content.Buffs;
using Terraria;
using Terraria.ModLoader;
using static System.Math;

namespace Luminous_Insignia.Common.Players
{
	internal class DamageStatsModificationPlayer : ModPlayer
    {
        public float AdditiveCritDamageBonus;
        public bool hasVengeanceEmblemEffect;
        readonly float VengeanceEmblemBonus = 0.01f;

        public override void ResetEffects() {
            AdditiveCritDamageBonus = 0f;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            if (hasVengeanceEmblemEffect) {
                int healthLost = Max(Player.statLifeMax2 - Player.statLife, 400);
                AdditiveCritDamageBonus += healthLost * VengeanceEmblemBonus;
            }
            if (AdditiveCritDamageBonus > 0) {
                modifiers.CritDamage += AdditiveCritDamageBonus;
            }
        }
        // public override void OnHurt(Player.HurtInfo info) {
        //     if (hasVengeanceEmblemEffect) {
        //         int healthLost = Max(Player.statLifeMax2 - Player.statLife, 400);
        //         AdditiveCritDamageBonus += healthLost * VengeanceEmblemBonus;
        //     }
        // }
    }
}