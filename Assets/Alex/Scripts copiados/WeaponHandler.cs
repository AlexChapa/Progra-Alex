using System.Collections;
using System.Collections.Generic;
using UnityEngine;

   
    public class WeaponHandler : MonoBehaviour
    {

        [SerializeField] private Weapon[] weapons;
        [SerializeField] private Weapon currentWeapon;

        private void Update()
        {
            Aim();
            Reload();
        }

        private void Reload()
        {
            if(Input.GetKeyUp(KeyCode.R)) 
            {
                currentWeapon.Reload();
            }
        }

        private void Aim()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(Shooot());
            }
        }

        IEnumerator Shooot()
        {            
            yield return new WaitForSeconds(1f);
            currentWeapon.Shoot();
        }

    }

