using UnityEngine;

    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected int actualAmmo; // Municion actual de el arma
        [SerializeField] protected int magazineSize; //  tamaño de el cargador
        [SerializeField] protected int maxAmmo; // capacidad maxima de almacenamiento de municion
        [SerializeField] protected float reloadTime; //  tiempo de recarga

        [SerializeField] protected float fireRate; // cadencia
        [SerializeField] protected internal float range; // alcance de el arma

        [SerializeField] protected int damage; // daño

        [SerializeField] protected LayerMask detection; // a que se le puede disparar

        protected RaycastHit target;

       // public float FireRate { get { return fireRate; } }

        // Instruccion 1, es una relga hecha por el maestro
        protected internal virtual void Shoot()
        {
            if (actualAmmo >= 0)
            {

                if (Physics.Raycast(transform.position, transform.forward * range, out target, range, detection))
                {
                    target.collider.GetComponent<IDamageable>().TakeDamage(damage);
                }

                actualAmmo--;
            }

        }

        // Instruccion 2, es una regla hecha por la escuela
        protected internal abstract void Reload();

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * range);
        }
    bool Disparar()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    }
