using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Luminous_Insignia.Content.Projectiles;
using System.Linq;
namespace Luminous_Insignia.Content.Items.Weapons
{
	public class EntityDecompiler : ModItem
	{
        // Programmer joke magic weapon. Fires one of three random attacks based on weight.
        readonly int[] AttackWeights = [5, 3, 1]; // Weights for each attack
        readonly int[] Ammo = [ModContent.ProjectileType<EntityDecompilerGreen>(), ModContent.ProjectileType<EntityDecompilerYellow>(), ModContent.ProjectileType<EntityDecompilerRed>()]; // Projectile types for each attack

        public override void SetStaticDefaults() {
			Item.staff[Type] = true; // This makes the useStyle animate as a staff instead of as a gun.
		}
		public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic; // Damage type
			// Set damage and knockBack
			Item.SetWeaponValues(40, 5, 16);
            Item.mana = 10; // Mana cost
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item20;

            Item.noMelee = true; // No melee hitbox
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<EntityDecompilerGreen>();
            Item.shootSpeed = 12f;
        }
        // public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
		// 	// Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
		// 	// type = Main.rand.Next([type, ProjectileID.GoldenBullet, ModContent.ProjectileType<Projectiles.ExampleBullet>()]);
        //     int totalWeight = AttackWeights.Sum();
        //     int randomValue = Main.rand.Next(totalWeight);
        //     int cumulativeWeight = 0;

        //     for (int i = 0; i < AttackWeights.Length; i++)
        //     {
        //         cumulativeWeight += AttackWeights[i];
        //         if (randomValue < cumulativeWeight)
        //         {
        //             type = Ammo[i];
        //             return;
        //         }
        //     }
		// }
		public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            int attackType = GetWeightedRandomAttack();

            switch (attackType)
            {
                case 0: // Green Tracking Bullet
                    Projectile.NewProjectile(source, position, velocity*2, ModContent.ProjectileType<EntityDecompilerGreen>(), damage, knockBack, player.whoAmI);
                    break;

                case 1: // Yellow Beams
                    for (int i = 0; i < 5; i++)
                    {
						Vector2 offset = new(Main.rand.Next(-20, 21), Main.rand.Next(0, 11));
                        Vector2 direction = Vector2.Normalize(Main.MouseWorld - player.Center + offset) * Item.shootSpeed * 0.2f;
                        Projectile.NewProjectile(source, player.Center + offset, direction, ModContent.ProjectileType<EntityDecompilerYellow>(), damage, knockBack, player.whoAmI);
                    }
                    break;

                case 2: // Mega Fireball
                    Projectile.NewProjectile(source, position, velocity * 0.5f, ModContent.ProjectileType<EntityDecompilerRed>(), damage, knockBack, player.whoAmI);
                    break;
            }

            return false; // Prevent vanilla projectile from firing
        }

        private int GetWeightedRandomAttack()
        {
            
            int totalWeight = 0;

            foreach (int weight in AttackWeights)
            {
                totalWeight += weight;
            }

            int randomValue = Main.rand.Next(totalWeight);
            int cumulativeWeight = 0;

            for (int i = 0; i < AttackWeights.Length; i++)
            {
                cumulativeWeight += AttackWeights[i];
                if (randomValue < cumulativeWeight)
                {
                    return i;
                }
            }

            return 0; // Default to the first attack
        }
    
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.BoneKey)
				.AddIngredient(ItemID.SoulofNight, 10)
				.AddIngredient(ItemID.SoulofLight, 10)
				.AddIngredient(ItemID.Wire, 64)
				.AddIngredient(ItemID.Nanites, 64)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
