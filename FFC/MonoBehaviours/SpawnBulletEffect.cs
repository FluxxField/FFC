using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FFC.MonoBehaviours {
    public class SpawnBulletsEffect : MonoBehaviour {
        private List<Vector3> _directionsToShoot = new List<Vector3>();
        private Gun _gunToShootFrom;
        private float _initialDelay = 1f;
        private GameObject _newWeaponsBase;
        private int _numBullets = 1;
        private int _numShot;
        private Player _player;
        private List<Vector3> _positionsToShootFrom = new List<Vector3>();
        private float _timeBetweenShots;
        private float _timeSinceLastShot;

        private void Awake() {
            _player = gameObject.GetComponent<Player>();
        }

        private void Start() {
            ResetTimer();
            _timeSinceLastShot += _initialDelay;
        }

        private void Update() {
            if (_numShot >= _numBullets || _gunToShootFrom == null)
                Destroy(this);
            else if (Time.time >= _timeSinceLastShot + _timeBetweenShots) Shoot();
        }

        private void OnDisable() {
            Destroy(this);
        }

        private void OnDestroy() {
            Destroy(_newWeaponsBase);
        }
        
        private void Shoot() {
			var currentNumberOfProjectiles = _gunToShootFrom.lockGunToDefault ? 1 : (_gunToShootFrom.numberOfProjectiles + Mathf.RoundToInt(_gunToShootFrom.chargeNumberOfProjectilesTo * 0f));
            
			for (var i = 0; i < _gunToShootFrom.projectiles.Length; i++) {
				for (var j = 0; j < currentNumberOfProjectiles; j++) {
					Vector3 directionToShootThisBullet;
                    
					if (_directionsToShoot.Count == 0) {
						directionToShootThisBullet = Vector3.down;
                    } else {
						directionToShootThisBullet = _directionsToShoot[_numShot % _directionsToShoot.Count];
                    }
                    
					if (_gunToShootFrom.spread != 0f) {
						// randomly spread shots
						var d = _gunToShootFrom.multiplySpread;
						var num = Random.Range(-_gunToShootFrom.spread, _gunToShootFrom.spread);
						num /= (1f + _gunToShootFrom.projectileSpeed * 0.5f) * 0.5f;
						directionToShootThisBullet += Vector3.Cross(directionToShootThisBullet, Vector3.forward) * num * d;
					}

					if ((bool)typeof(Gun).InvokeMember("CheckIsMine", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, _gunToShootFrom, new object[] { })) {
						Vector3 positionToShootFrom;
						if (_positionsToShootFrom.Count == 0) {
							positionToShootFrom = Vector3.zero;
						} else {
							positionToShootFrom = _positionsToShootFrom[_numShot % _positionsToShootFrom.Count];
						}
                        
						var view = PhotonNetwork.Instantiate(_gunToShootFrom.projectiles[i].objectToSpawn.gameObject.name, positionToShootFrom, Quaternion.LookRotation(directionToShootThisBullet)).GetComponent<PhotonView>();
                        var viewId = view.ViewID;
                        
						if (PhotonNetwork.OfflineMode) {
							RPCA_Shoot(viewId,currentNumberOfProjectiles, 1f, Random.Range(0f,1f));
                        } else {
                            view.RPC("RPCA_Shoot", RpcTarget.All, new [] {
                                viewId,
								currentNumberOfProjectiles,
								1f,
								Random.Range(0f, 1f)
							});
                        }
					}
				}
			}
            
			ResetTimer();
        }

        [PunRPC]
        private void RPCA_Shoot(
            int bulletViewID,
            int numProj,
            float dmgM,
            float seed
        ) {
            var bulletObj = PhotonView.Find(bulletViewID).gameObject;
            _gunToShootFrom.BulletInit(bulletObj, numProj, dmgM, seed);
            _numShot++;
        }

        public void SetGun(
            Gun gun
        ) {
            _newWeaponsBase = Instantiate(
                _player.GetComponent<Holding>().holdable.GetComponent<Gun>().gameObject,
                new Vector3(500f, 500f, -100f), Quaternion.identity);
            DontDestroyOnLoad(_newWeaponsBase);

            foreach (Transform child in _newWeaponsBase.transform)
                if (child.GetComponentInChildren<Renderer>() != null)
                    foreach (var renderer in child.GetComponentsInChildren<Renderer>())
                        renderer.enabled = false;

            _gunToShootFrom = _newWeaponsBase.GetComponent<Gun>();
            CopyGunStats(gun, _gunToShootFrom);
        }

        public void SetNumBullets(
            int num
        ) {
            _numBullets = num;
        }

        public void SetPosition(
            Vector3 pos
        ) {
            _positionsToShootFrom = new List<Vector3> {pos};
        }

        public void SetDirection(
            Vector3 dir
        ) {
            _directionsToShoot = new List<Vector3> {dir};
        }

        public void SetPositions(
            List<Vector3> pos
        ) {
            _positionsToShootFrom = pos;
        }

        public void SetDirections(
            List<Vector3> dir
        ) {
            _directionsToShoot = dir;
        }

        public void SetTimeBetweenShots(
            float delay
        ) {
            _timeBetweenShots = delay;
        }

        public void SetInitialDelay(
            float delay
        ) {
            _initialDelay = delay;
        }

        private void ResetTimer() {
            _timeSinceLastShot = Time.time;
        }

        public static void CopyGunStats(
            Gun copyFromGun,
            Gun copyToGun
        ) {
            copyToGun.ammo = copyFromGun.ammo;
            copyToGun.ammoReg = copyFromGun.ammoReg;
            copyToGun.attackID = copyFromGun.attackID;
            copyToGun.attackSpeed = copyFromGun.attackSpeed;
            copyToGun.attackSpeedMultiplier = copyFromGun.attackSpeedMultiplier;
            copyToGun.bodyRecoil = copyFromGun.bodyRecoil;
            copyToGun.bulletDamageMultiplier = copyFromGun.bulletDamageMultiplier;
            copyToGun.bulletPortal = copyFromGun.bulletPortal;
            copyToGun.bursts = copyFromGun.bursts;
            copyToGun.chargeDamageMultiplier = copyFromGun.chargeDamageMultiplier;
            copyToGun.chargeEvenSpreadTo = copyFromGun.chargeEvenSpreadTo;
            copyToGun.chargeNumberOfProjectilesTo = copyFromGun.chargeNumberOfProjectilesTo;
            copyToGun.chargeRecoilTo = copyFromGun.chargeRecoilTo;
            copyToGun.chargeSpeedTo = copyFromGun.chargeSpeedTo;
            copyToGun.chargeSpreadTo = copyFromGun.chargeSpreadTo;
            copyToGun.cos = copyFromGun.cos;
            copyToGun.currentCharge = copyFromGun.currentCharge;
            copyToGun.damage = copyFromGun.damage;
            copyToGun.damageAfterDistanceMultiplier = copyFromGun.damageAfterDistanceMultiplier;
            copyToGun.defaultCooldown = copyFromGun.defaultCooldown;
            copyToGun.dmgMOnBounce = copyFromGun.dmgMOnBounce;
            copyToGun.dontAllowAutoFire = copyFromGun.dontAllowAutoFire;
            copyToGun.drag = copyFromGun.drag;
            copyToGun.dragMinSpeed = copyFromGun.dragMinSpeed;
            copyToGun.evenSpread = copyFromGun.evenSpread;
            copyToGun.explodeNearEnemyDamage = copyFromGun.explodeNearEnemyDamage;
            copyToGun.explodeNearEnemyRange = copyFromGun.explodeNearEnemyRange;
            copyToGun.forceSpecificAttackSpeed = copyFromGun.forceSpecificAttackSpeed;
            copyToGun.forceSpecificShake = copyFromGun.forceSpecificShake;
            copyToGun.gravity = copyFromGun.gravity;
            copyToGun.hitMovementMultiplier = copyFromGun.hitMovementMultiplier;
            copyToGun.ignoreWalls = copyFromGun.ignoreWalls;
            copyToGun.isProjectileGun = copyFromGun.isProjectileGun;
            copyToGun.isReloading = copyFromGun.isReloading;
            copyToGun.knockback = copyFromGun.knockback;
            copyToGun.lockGunToDefault = copyFromGun.lockGunToDefault;
            copyToGun.multiplySpread = copyFromGun.multiplySpread;
            copyToGun.numberOfProjectiles = copyFromGun.numberOfProjectiles;
            copyToGun.objectsToSpawn = copyFromGun.objectsToSpawn;
            copyToGun.overheatMultiplier = copyFromGun.overheatMultiplier;
            copyToGun.percentageDamage = copyFromGun.percentageDamage;
            copyToGun.player = copyFromGun.player;
            copyToGun.projectielSimulatonSpeed = copyFromGun.projectielSimulatonSpeed;
            copyToGun.projectileColor = copyFromGun.projectileColor;
            copyToGun.projectiles = copyFromGun.projectiles;
            copyToGun.projectileSize = copyFromGun.projectileSize;
            copyToGun.projectileSpeed = copyFromGun.projectileSpeed;
            copyToGun.randomBounces = copyFromGun.randomBounces;
            copyToGun.recoil = copyFromGun.recoil;
            copyToGun.recoilMuiltiplier = copyFromGun.recoilMuiltiplier;
            copyToGun.reflects = copyFromGun.reflects;
            copyToGun.reloadTime = copyFromGun.reloadTime;
            copyToGun.reloadTimeAdd = copyFromGun.reloadTimeAdd;
            copyToGun.shake = copyFromGun.shake;
            copyToGun.shakeM = copyFromGun.shakeM;
            copyToGun.ShootPojectileAction = copyFromGun.ShootPojectileAction;
            copyToGun.sinceAttack = copyFromGun.sinceAttack;
            copyToGun.size = copyFromGun.size;
            copyToGun.slow = copyFromGun.slow;
            copyToGun.smartBounce = copyFromGun.smartBounce;
            copyToGun.spawnSkelletonSquare = copyFromGun.spawnSkelletonSquare;
            copyToGun.speedMOnBounce = copyFromGun.speedMOnBounce;
            copyToGun.spread = copyFromGun.spread;
            copyToGun.teleport = copyFromGun.teleport;
            copyToGun.timeBetweenBullets = copyFromGun.timeBetweenBullets;
            copyToGun.timeToReachFullMovementMultiplier = copyFromGun.timeToReachFullMovementMultiplier;
            copyToGun.unblockable = copyFromGun.unblockable;
            copyToGun.useCharge = copyFromGun.useCharge;
            copyToGun.waveMovement = copyFromGun.waveMovement;

            Traverse.Create(copyToGun).Field("attackAction")
                .SetValue((Action) Traverse.Create(copyFromGun).Field("attackAction").GetValue());
            Traverse.Create(copyToGun).Field("gunID")
                .SetValue((int) Traverse.Create(copyFromGun).Field("gunID").GetValue());
            Traverse.Create(copyToGun).Field("spreadOfLastBullet")
                .SetValue((float) Traverse.Create(copyFromGun).Field("spreadOfLastBullet").GetValue());

            Traverse.Create(copyToGun).Field("forceShootDir")
                .SetValue((Vector3) Traverse.Create(copyFromGun).Field("forceShootDir").GetValue());
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}