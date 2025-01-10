using System;
using Luminous_Insignia.Content.Buffs;
using Terraria;
using Terraria.ModLoader;
using static System.Math;

namespace Luminous_Insignia.Common.Players
{
	internal class DamageStatsModificationPlayer : ModPlayer
    {
        private const float MaxCritDmgBoost = 1.5f;
        private const int MaxDefenseBoost = 30;
        private const int ChargeDuration = 600;
        private const int RechargeCooldown = 480;
        private int BoostChargeCounter = 0;
        private int rechargeCooldownCounter = 0;
        public float AdditiveCritDamageBonus;
        public bool isVengeanceEmblemEquip;
        public override void ResetEffects() {
            AdditiveCritDamageBonus = 0f;
            isVengeanceEmblemEquip = false;
        }
        public override void PostUpdateEquips()
        {
            if (!isVengeanceEmblemEquip) return;
            if (rechargeCooldownCounter <= 0)
            {
                if (BoostChargeCounter < ChargeDuration)
                {
                    BoostChargeCounter++;
                }
            }
            else
            {
                rechargeCooldownCounter--;
            }
            AdditiveCritDamageBonus += MaxCritDmgBoost * BoostChargeCounter / ChargeDuration;
            Player.statDefense += MaxDefenseBoost * BoostChargeCounter / ChargeDuration;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
            if (AdditiveCritDamageBonus > 0) {
                modifiers.CritDamage += AdditiveCritDamageBonus;
            }
        }
        public override void OnHurt(Player.HurtInfo info) {
            rechargeCooldownCounter = RechargeCooldown;
            if (isVengeanceEmblemEquip && info.Damage > Player.statLifeMax2 * 0.025f && BoostChargeCounter >= ChargeDuration/2) {
                // give the player crit damage bonus for N seconds
                Player.AddBuff(ModContent.BuffType<VengeanceEmblemBuff>(), 180);
                // reset the boost charge
                BoostChargeCounter = 0;
            }
        }
    }
}