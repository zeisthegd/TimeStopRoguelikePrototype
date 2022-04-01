using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class ProjectileWeapon : Weapon
    {
        protected ObjectPooler _projectilePooler;

        public override void Initialization()
        {
            base.Initialization();
            _projectilePooler = GetComponent<ObjectPooler>();
            _projectilePooler.ObjectToPool = CurrentData.Projectile;
            _projectilePooler.Init();
        }

        protected override void UseWeapon()
        {
            base.UseWeapon();
            StartCoroutine(UseWeaponCoroutine());
        }

        protected virtual IEnumerator UseWeaponCoroutine()
        {
            for (int i = 0; i < CurrentData.BulletPerShot; i++)
            {
                SpawnProjectile();
                if (CurrentData.BulletPerShot > 1)
                    yield return new WaitForSeconds(CurrentData.DelayBetweenBullets);
            }
        }

        /// <summary>
        /// Create a projectile, direction is based on the weapon's rotation.
        /// </summary>
        public virtual void SpawnProjectile()
        {
            Projectile projectile = (Projectile)_projectilePooler.PullOneObject();
            projectile.transform.position = this.transform.position;
            projectile.transform.rotation = this.transform.rotation;
            projectile.gameObject.SetActive(true);
            projectile.FlyTowards((transform.rotation * Vector3.right));
        }

        public override void LoadWeapon(WeaponData data)
        {
            base.LoadWeapon(data);
        }

        public override void LoadWeapon()
        {
            base.LoadWeapon();
        }
    }

}
