using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Luminous_Insignia.Content.Items.Weapons
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class MistSplitter : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.InstaHook.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}
        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click launch
            {
                Item.useTime = 60;
                Item.useAnimation = 60;
                Item.useStyle = ItemUseStyleID.Thrust;
                Item.noMelee = true;
                // Item.shoot = ModContent.ProjectileType<Projectiles.LeechStrikeProjectile>();
                Item.shoot = ProjectileID.IchorBullet;
                Item.shootSpeed = 16f;
            }
            else // Left-click attack
            {
                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = false;
                Item.shoot = ProjectileID.None;
            }

            return base.CanUseItem(player);
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) {
            // Apply Leech Seed Debuff
            target.AddBuff(ModContent.BuffType<Buffs.LeechSeedDebuff>(), 600);
        }
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
