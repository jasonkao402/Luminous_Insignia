using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Luminous_Insignia.Content.Items.Accessories
{
    public class LuminousInsigniaItem : ModItem
    {
        public static readonly int MoveSpeedBonus = 25;
        // public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MoveSpeedBonus);
        public override void SetDefaults()
        {
            // Item.width = 28;
            // Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // Apply Soaring Insignia effects
            // player.wingTimeMax = int.MaxValue; // Infinite flight
            player.moveSpeed += MoveSpeedBonus * 0.01f; // Increased movement speed

            // Apply Magiluminescence effects
            player.runAcceleration *= 1f + MoveSpeedBonus * 0.01f; // Increased acceleration
            player.runSlowdown *= 1f + MoveSpeedBonus * 0.01f; // Increased deceleration
            player.maxRunSpeed *= 1f + MoveSpeedBonus * 0.01f; // Increased max speed
			player.accRunSpeed *= 1f + MoveSpeedBonus * 0.01f; // Increased max speed
            Lighting.AddLight(player.Center, 1.5f, 0.75f, 0.5f); // Add light around the player
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.EmpressFlightBooster)
                .AddIngredient(ItemID.Magiluminescence)
                .AddIngredient(ItemID.Ectoplasm, 30)
                .AddIngredient(ItemID.SoulofFlight, 30)
                .AddIngredient(ItemID.SoulofLight, 30)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
    // Some movement effects are not suitable to be modified in ModItem.UpdateAccessory due to how the math is done.
	// ModPlayer.PostUpdateRunSpeeds is suitable for these modifications.
	// public class ExampleStatBonusAccessoryPlayer : ModPlayer {
	// 	public bool exampleStatBonusAccessory = false;

	// 	public override void ResetEffects() {
	// 		exampleStatBonusAccessory = false;
	// 	}

	// 	public override void PostUpdateRunSpeeds() {
	// 		// We only want our additional changes to apply if ExampleStatBonusAccessory is equipped and not on a mount.
	// 		if (Player.mount.Active || !exampleStatBonusAccessory) {
	// 			return;
	// 		}

	// 		// The following modifications are similar to Shadow Armor set bonus
	// 		Player.runAcceleration *= 1.75f; // Modifies player run acceleration
	// 		Player.maxRunSpeed *= 1.15f;
	// 		Player.accRunSpeed *= 1.15f;
	// 		Player.runSlowdown *= 1.75f;
	// 	}
	// }
}