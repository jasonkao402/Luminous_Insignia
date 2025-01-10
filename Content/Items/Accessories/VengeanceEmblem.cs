using ReLogic.Content;
using Luminous_Insignia.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Luminous_Insignia.Content.Items.Accessories
{
    // Increases critical damage by 50% of total HP lost, capped at 50% of
    public class VengeanceEmblem : ModItem
    {
        public override void SetDefaults() {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(gold: 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            player.GetModPlayer<DamageStatsModificationPlayer>().isVengeanceEmblemEquip = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DestroyerEmblem)
                .AddIngredient(ItemID.SoulofNight, 10)
                .AddIngredient(ItemID.SoulofLight, 10)
                .AddIngredient(ItemID.SoulofFright, 30)
                .AddIngredient(ItemID.SoulofMight, 30)
                .AddIngredient(ItemID.LunarBar, 30)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

        }
    }
}