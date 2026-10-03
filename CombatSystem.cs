using UnityEngine;

namespace EchoBound
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private float lightAttackDamage = 10f;
        [SerializeField] private float heavyAttackDamage = 25f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private LayerMask targetMask;

        public void LightAttack()
        {
            DealDamage(lightAttackDamage);
        }

        public void HeavyAttack()
        {
            DealDamage(heavyAttackDamage);
        }

        private void DealDamage(float damage)
        {
            Vector3 center = transform.position + transform.forward * attackRange * 0.5f;
            Collider[] hits = Physics.OverlapSphere(center, attackRange, targetMask);

            foreach (Collider hit in hits)
            {
                var health = hit.GetComponentInParent<Health>();
                if (health != null)
                    health.TakeDamage(damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(
                transform.position + transform.forward * attackRange * 0.5f,
                attackRange);
        }
    }
}
