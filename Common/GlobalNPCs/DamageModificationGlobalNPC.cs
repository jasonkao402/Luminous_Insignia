using Luminous_Insignia.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Luminous_Insignia.Common.GlobalNPCs
{
	internal class DamageModificationGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public bool hasLeechSeedDebuff;

		public override void ResetEffects(NPC npc) {
			hasLeechSeedDebuff = false;
		}

		public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers) {
			if (hasLeechSeedDebuff) {
				// For best results, defense debuffs should be multiplicative
				modifiers.Defense *= LeechSeedDebuff.DefenseMultiplier;
			}
		}

		public override void DrawEffects(NPC npc, ref Color drawColor) {
			// This simple color effect indicates that the buff is active
			if (hasLeechSeedDebuff) {
				drawColor.R = 0;
			}
		}
		public override void UpdateLifeRegen(NPC npc, ref int damage) {
			if (hasLeechSeedDebuff) {
				// This effect is similar to the vanilla poisoned debuff, but it's based on the LeechSeedDebuff
				if (npc.lifeRegen > 0) {
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 30;
				// if (damage < 2) {
				// 	damage = 2;
				// }
			}
		}
	}
}
