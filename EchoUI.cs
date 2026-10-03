using UnityEngine;
using UnityEngine.UI;

namespace EchoBound
{
    public class EchoUI : MonoBehaviour
    {
        [SerializeField] private EchoSystem echoSystem;
        [SerializeField] private Text statusText;

        private void Update()
        {
            if (echoSystem == null || statusText == null) return;

            statusText.text = echoSystem.IsRecording
                ? "RECORDING ECHO"
                : $"ECHOES ACTIVE: {echoSystem.ActiveEchoCount}/{echoSystem.MaxActiveEchoes}";
        }
    }
}
