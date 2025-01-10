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
        public bool isVengeanceEmblemEquip;
        public override void ResetEffects() {
            AdditiveCritDamageBonus = 0f;
            isVengeanceEmblemEquip = false;
            // isVengeanceEmblemEquip = Player.HasBuff(ModContent.BuffType<VengeanceEmblemBuff>());
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            // Increases critical damage proportional to total HP lost, capped at 400 HP lost
            // if (Player.HasBuff(ModContent.BuffType<VengeanceEmblemBuff>())) {
                // int healthLost = Min(Player.statLifeMax2 - Player.statLife, 400);
                // AdditiveCritDamageBonus += healthLost * VengeanceEmblemBonus;
            // }
            if (AdditiveCritDamageBonus > 0) {
                modifiers.CritDamage += AdditiveCritDamageBonus;
            }
        }
        public override void OnHurt(Player.HurtInfo info) {
            if (isVengeanceEmblemEquip) {
                // give the player crit damage bonus for N seconds
                Player.AddBuff(ModContent.BuffType<VengeanceEmblemBuff>(), 180);
        //         int healthLost = Min(Player.statLifeMax2 - Player.statLife, 400);
        //         AdditiveCritDamageBonus += healthLost * VengeanceEmblemBonus;
            }
        }
    }
}