using UnityEngine;
using UnityEngine.Events;

namespace EchoBound
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField] private UnityEvent onPressed;
        [SerializeField] private UnityEvent onReleased;

        private int occupants;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Echo")) return;

            occupants++;
            if (occupants == 1)
                onPressed?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Echo")) return;

            occupants = Mathf.Max(0, occupants - 1);
            if (occupants == 0)
                onReleased?.Invoke();
        }
    }
}
