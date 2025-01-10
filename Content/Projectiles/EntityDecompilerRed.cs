using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Luminous_Insignia.Content.Projectiles
{
	public class EntityDecompilerRed : ModProjectile
	{
        // Pull enemies towards the fireball
		public ref float DelayTimer => ref Projectile.ai[1];
        readonly float pullRadius = 400f;
        readonly float pullStrength = 0.02f;
		public override void SetStaticDefaults() {
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
		}

		public override void SetDefaults() {
			Projectile.width = 32; // The width of projectile hitbox
			Projectile.height = 32; // The height of projectile hitbox
			// Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.timeLeft = 900; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
			Projectile.light = 3f; // How much light emit around the projectile
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.extraUpdates = 3; // Set to above 0 if you want the projectile to update multiple time in a frame

			// AIType = ProjectileID.Bullet; // Act exactly like default Bullet
		}
		public override void AI()
        {
			if (DelayTimer < 10) {
				DelayTimer += 1;
				return;
			}
			Projectile.velocity *= 0.99f;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (Vector2.Distance(Projectile.Center, npc.Center) < pullRadius && IsValidTarget(npc))
                {
                    Vector2 pullDirection = Projectile.Center - npc.Center;
                    // pullDirection.Normalize();
					// float mag = pullDirection.LengthSquared();
                    npc.Center += pullDirection * pullStrength / (npc.knockBackResist + 0.5f);
                }
            }
        }

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
			// 3a: target.immune[Projectile.owner] = 20;
			target.immune[Projectile.owner] = 5;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) {
			// If collide with tile, reduce the penetrate.
			// So the projectile can reflect at most 5 times
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0) {
				Projectile.Kill();
			}
			else {
				Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

				// If the projectile hits the left or right side of the tile, reverse the X velocity
				if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
					Projectile.velocity.X = -oldVelocity.X;
				}

				// If the projectile hits the top or bottom side of the tile, reverse the Y velocity
				if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
					Projectile.velocity.Y = -oldVelocity.Y;
				}
			}

			return false;
		}

		public override bool PreDraw(ref Color lightColor) {
			// Draws an afterimage trail. See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#afterimage-trail for more information.

			Texture2D texture = TextureAssets.Projectile[Type].Value;

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = Projectile.oldPos.Length - 1; k > 0; k--) {
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}

		public override void OnKill(int timeLeft) {
			// This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}

		public bool IsValidTarget(NPC target) {
			// This method checks that the NPC is:
			// 1. active (alive)
			// 2. chaseable (e.g. not a cultist archer)
			// 3. max life bigger than 5 (e.g. not a critter)
			// 4. can take damage (e.g. moonlord core after all it's parts are downed)
			// 5. hostile (!friendly)
			// 6. not immortal (e.g. not a target dummy)
			// 7. doesn't have solid tiles blocking a line of sight between the projectile and NPC
			return target.CanBeChasedBy() && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);
		}
	}
}