using UnityEngine;

namespace Kore
{

    public class ObjHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int actualDurability;
        [SerializeField] private int maxDurability;

        [SerializeField] private ParticleSystem dust;

        public void TakeDamage(int damage)
        {
            Debug.Log("Le hiciste " + damage + " daño a " + name + " le queda " + actualDurability + " puntos de durabilidad");
            actualDurability -= damage;

            if (actualDurability < 0)
            {
                Death();
            }
        }


        private void Death()
        {
            Debug.Log("Destruiste la estructura " + name);
            Destroy(gameObject);
        }
    }

}