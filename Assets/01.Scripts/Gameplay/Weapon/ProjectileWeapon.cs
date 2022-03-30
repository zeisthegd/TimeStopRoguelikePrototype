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

        public override void HandleWeaponRequestInput()
        {
            base.HandleWeaponRequestInput();
            for (int i = 0; i < CurrentData.BulletPerShot; i++)
            {
                SpawnProjectile();
            }
        }

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
