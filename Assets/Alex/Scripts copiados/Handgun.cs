using UnityEngine;
using static UnityEngine.GraphicsBuffer;

    public class Handgun : Weapon
    {
        protected internal override void Shoot()
        {
            base.Shoot();
        }

        protected internal override void Reload()
        {
            Debug.Log("Recargo");
        }

    }

