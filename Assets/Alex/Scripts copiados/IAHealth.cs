using System.Collections;
using UnityEngine;

    public class IAHealth : MonoBehaviour, IDamageable

    {
        [SerializeField, Range(0, 100)] private int actualHealth;
        [SerializeField] private int maxHealth;

        [SerializeField] private ParticleSystem blood;

    private void Start()
    {
        blood.Stop();
    }

    public void TakeDamage(int damage)
        {
            Debug.Log("El personaje " + name + " recibio daño");
            actualHealth -= damage;
            ParticleSystem bloodEffect = Instantiate(blood, transform.position, Quaternion.identity);
            StartCoroutine(DestroyBlood(bloodEffect, .5f));

            if (actualHealth < 0)
            {
                Death();
            }
        }
    

        public void Death()
        {
            blood.Play();
            Debug.Log("Mataste a " + name);
            Destroy(gameObject);
        }

         IEnumerator DestroyBlood(ParticleSystem bloodEffect, float delay)
        {
            yield return new WaitForSeconds(delay);

            Destroy(bloodEffect.gameObject);
        }
    }