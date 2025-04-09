using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class AutomaticRifle : Weapon
    {
        protected internal override void Shoot()
        {
            base.Shoot();
        }

        protected internal override void Reload()
        {
            StartCoroutine(Recargar());
        }

        IEnumerator Recargar()
        {
            if (base.actualAmmo == 0)
            {
                yield return new WaitForSeconds(base.reloadTime);
                base.actualAmmo = base.maxAmmo;
            }
        }

    }
