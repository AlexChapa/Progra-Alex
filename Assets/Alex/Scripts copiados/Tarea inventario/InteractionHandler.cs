using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] protected internal float range;

        [SerializeField] protected LayerMask detection;

        protected RaycastHit target;

        void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward * range, out target, range, detection))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    target.collider.GetComponent<Interactable>().Interact();
                }
            }
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * range);
        }
    }
}
