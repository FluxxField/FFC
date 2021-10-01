using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using FFC.MonoBehaviours;

namespace FFC.Cards {
    class Sniper : CustomCard {
        protected override string GetTitle() {
            return "Sniper!";
        }

        protected override string GetDescription() {
            return "Get down!!";
        }

        public override void SetupCard(
            CardInfo cardInfo,
            Gun gun,
            ApplyCardStats cardStats,
            CharacterStatModifiers statModifiers
        ) {
            UnityEngine.Debug.Log($"[{FFC.AbbrModName}] Setting up {GetTitle()}");
            
            cardInfo.allowMultiple = false;
            cardInfo.categories = new []
            {
                CustomCardCategories.instance.CardCategory(FFC.SniperClassCategory)
            };
            cardInfo.blacklistedCategories = new []
            {
                CustomCardCategories.instance.CardCategory(FFC.SniperClassCategory)
            };
        }

        public override void OnAddCard(
            Player player,
            Gun gun,
            GunAmmo gunAmmo,
            CharacterData data,
            HealthHandler health,
            Gravity gravity,
            Block block,
            CharacterStatModifiers characterStats
        ) {
            GameObject explosiveBulletCard = (GameObject) Resources.Load("0 cards/Explosive bullet");
            GameObject explosiveBulletCardEffect = explosiveBulletCard.GetComponent<Gun>().objectsToSpawn[0].effect;
            GameObject explosionEffect = Instantiate(explosiveBulletCardEffect);
            explosionEffect.transform.position = new Vector3(1000, 0, 0);
            explosionEffect.hideFlags = HideFlags.HideAndDontSave;
            explosionEffect.name = "customExplosionEffect";
            DestroyImmediate(explosionEffect.GetComponent<RemoveAfterSeconds>());
            Explosion explosion = explosionEffect.GetComponent<Explosion>();
            
            explosion.range = 10f;
            
            explosion.hitPlayerAction = (characterData, f) => {
                UnityEngine.Debug.Log("Splash Hit");
                UnityEngine.Debug.Log(characterData.player.playerID);
            };

            List<ObjectsToSpawn> newObjectsToSpawns = gun.objectsToSpawn.ToList();
            newObjectsToSpawns.Add(new ObjectsToSpawn {
                effect = explosionEffect,
                numberOfSpawns = 1,
                scaleFromDamage = 1f
            });
            gun.objectsToSpawn = newObjectsToSpawns.ToArray();
            gun.unblockable = true;
            gun.gravity = 0f;
            gun.projectileSpeed *= 2f;
            gunAmmo.reloadTime = 3f;
            gunAmmo.maxAmmo = 1;
            player.gameObject.GetOrAddComponent<InstantKillHitEffect>();
        }

        public override void OnRemoveCard() {
        }

        protected override CardInfoStat[] GetStats() {
            return new[] {
                new CardInfoStat() {
                    positive = true,
                    amount = "Insta Kill",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat() {
                    positive = true,
                    amount = "Unblockable",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat() {
                    positive = true,
                    stat = "Bullet Speed",
                    amount = "x2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat() {
                    positive = false,
                    stat = "Reload Speed",
                    amount = "3s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
                new CardInfoStat() {
                    positive = false,
                    stat = "Max Ammo",
                    amount = "1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned,
                },
            };
        }

        protected override CardInfo.Rarity GetRarity() {
            return CardInfo.Rarity.Rare;
        }

        protected override CardThemeColor.CardThemeColorType GetTheme() {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        protected override GameObject GetCardArt() {
            return null;
        }

        public override string GetModName() {
            return FFC.AbbrModName;
        }
    }
}