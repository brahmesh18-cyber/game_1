using UnityEngine;

namespace EchoBound
{
    /// <summary>
    /// Tiny keyboard prototype bridge.
    /// Replace this with Unity Input System actions for production.
    /// R = start/stop recording
    /// P = create/play Echo
    /// WASD = move
    /// </summary>
    public class EchoInputExample : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private EchoSystem echoSystem;

        private void Update()
        {
            Vector2 move = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));

            player?.Move(move);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (echoSystem != null && echoSystem.IsRecording)
                    echoSystem.StopRecording();
                else
                    echoSystem?.StartRecording();
            }

            if (Input.GetKeyDown(KeyCode.P))
                echoSystem?.StopAndCreateEcho();
        }
    }
}
