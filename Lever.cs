using UnityEngine;

namespace EchoBound
{
    public class Lever : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactionId = "lever_01";
        [SerializeField] private bool isOn;

        public string InteractionId => interactionId;
        public bool IsOn => isOn;

        public void Interact()
        {
            isOn = !isOn;
        }
    }
}
