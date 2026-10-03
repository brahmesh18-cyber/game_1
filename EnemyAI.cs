using UnityEngine;

namespace EchoBound
{
    public abstract class EnemyAI : MonoBehaviour
    {
        public abstract void TickAI();
    }

    public class BasicEnemyAI : EnemyAI
    {
        [SerializeField] private Transform target;
        [SerializeField] private float chaseDistance = 8f;
        [SerializeField] private float moveSpeed = 2.5f;

        public override void TickAI()
        {
            if (target == null) return;

            Vector3 offset = target.position - transform.position;
            if (offset.sqrMagnitude > chaseDistance * chaseDistance) return;

            offset.y = 0f;
            if (offset.sqrMagnitude < 0.01f) return;

            transform.position += offset.normalized * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(offset.normalized, Vector3.up);
        }

        private void Update()
        {
            TickAI();
        }
    }
}
