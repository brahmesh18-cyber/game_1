using System;
using UnityEngine;

namespace EchoBound
{
    /// <summary>
    /// Replays a previously captured EchoRecording.
    /// For the first prototype, transform playback is authoritative.
    /// Gameplay events are exposed so interactables/combat can be connected later.
    /// </summary>
    public class EchoPlayback : MonoBehaviour
    {
        private EchoRecording recording;
        private int frameIndex;
        private float playbackTime;

        public bool IsPlaying { get; private set; }
        public float PlaybackTime => playbackTime;

        public event Action Started;
        public event Action Finished;
        public event Action<EchoAction> ActionReached;

        public void Play(EchoRecording source)
        {
            if (source == null || !source.IsValid)
                throw new ArgumentException("A valid EchoRecording is required.", nameof(source));

            recording = source;
            frameIndex = 0;
            playbackTime = 0f;
            IsPlaying = true;

            transform.SetPositionAndRotation(
                recording.Frames[0].position,
                recording.Frames[0].rotation);

            Started?.Invoke();
        }

        public void Stop()
        {
            if (!IsPlaying) return;
            IsPlaying = false;
            Finished?.Invoke();
        }

        private void Update()
        {
            if (!IsPlaying || recording == null) return;

            playbackTime += Time.deltaTime;

            while (frameIndex < recording.Frames.Count - 1 &&
                   recording.Frames[frameIndex + 1].time <= playbackTime)
            {
                frameIndex++;
            }

            var current = recording.Frames[frameIndex];
            transform.SetPositionAndRotation(current.position, current.rotation);

            foreach (var action in recording.Actions)
            {
                if (Mathf.Abs(action.time - playbackTime) <= Time.deltaTime * 0.75f)
                    ActionReached?.Invoke(action);
            }

            if (playbackTime >= recording.Duration)
                Stop();
        }
    }
}
