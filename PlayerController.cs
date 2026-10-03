using UnityEngine;

namespace EchoBound
{
    /// <summary>
    /// Lightweight prototype movement controller.
    /// Replace input wiring with Unity Input System actions for production.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 12f;

        public void Move(Vector2 input)
        {
            Vector3 direction = new Vector3(input.x, 0f, input.y);
            if (direction.sqrMagnitude > 1f) direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    target,
                    rotationSpeed * Time.deltaTime);
            }
        }
    }
}
