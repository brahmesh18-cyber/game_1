using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EchoBound
{
    /// <summary>
    /// Coordinates recording and playback. Keep this system independent from player combat.
    /// </summary>
    public class EchoSystem : MonoBehaviour
    {
        [SerializeField] private RecordingSystem recorder;
        [SerializeField] private EchoPlayback echoPrefab;
        [SerializeField] private Transform echoSpawnRoot;
        [SerializeField] private int maxActiveEchoes = 1;

        private readonly List<EchoPlayback> activeEchoes = new();

        public bool IsRecording => recorder != null && recorder.IsRecording;
        public int ActiveEchoCount => activeEchoes.Count;
        public int MaxActiveEchoes => maxActiveEchoes;

        public event Action<EchoPlayback> EchoCreated;
        public event Action RecordingStarted;
        public event Action RecordingStopped;

        private void Awake()
        {
            if (recorder == null)
                recorder = GetComponent<RecordingSystem>();

            if (recorder != null)
            {
                recorder.Started += () => RecordingStarted?.Invoke();
                recorder.Stopped += () => RecordingStopped?.Invoke();
            }
        }

        public void StartRecording()
        {
            if (recorder == null) return;
            recorder.StartRecording();
        }

        public void StopRecording()
        {
            if (recorder == null) return;
            recorder.StopRecording();
        }

        public EchoPlayback CreateEcho()
        {
            if (recorder == null || echoPrefab == null || !recorder.Frames.Any())
                return null;

            if (activeEchoes.Count >= maxActiveEchoes)
                RemoveOldestEcho();

            var recording = recorder.CreateRecording();
            if (!recording.IsValid) return null;

            Transform root = echoSpawnRoot != null ? echoSpawnRoot : transform;
            EchoPlayback echo = Instantiate(echoPrefab, recording.Frames[0].position, recording.Frames[0].rotation, root);

            echo.Play(recording);
            echo.Finished += () => RemoveEcho(echo);

            activeEchoes.Add(echo);
            EchoCreated?.Invoke(echo);
            return echo;
        }

        public void StopAndCreateEcho()
        {
            StopRecording();
            CreateEcho();
        }

        private void RemoveOldestEcho()
        {
            if (activeEchoes.Count == 0) return;
            var oldest = activeEchoes[0];
            RemoveEcho(oldest);
        }

        private void RemoveEcho(EchoPlayback echo)
        {
            if (echo == null) return;
            activeEchoes.Remove(echo);
            if (echo.gameObject != null)
                Destroy(echo.gameObject);
        }
    }
}
