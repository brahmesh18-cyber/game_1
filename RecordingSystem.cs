using System;
using System.Collections.Generic;
using UnityEngine;

namespace EchoBound
{
    /// <summary>
    /// Records structured movement and gameplay events for an Echo.
    /// Designed to remain independent from PlayerController.
    /// </summary>
    public class RecordingSystem : MonoBehaviour
    {
        [SerializeField] private float maxRecordingDuration = 15f;
        [SerializeField] private float positionSampleInterval = 0.05f;

        private readonly List<EchoFrame> frames = new();
        private readonly List<EchoAction> actions = new();

        private float recordingTime;
        private float nextSampleTime;

        public bool IsRecording { get; private set; }
        public float RecordingTime => recordingTime;
        public IReadOnlyList<EchoFrame> Frames => frames;
        public IReadOnlyList<EchoAction> Actions => actions;

        public event Action Started;
        public event Action Stopped;

        public void StartRecording()
        {
            if (IsRecording) return;

            frames.Clear();
            actions.Clear();
            recordingTime = 0f;
            nextSampleTime = 0f;
            IsRecording = true;
            Started?.Invoke();
        }

        public void StopRecording()
        {
            if (!IsRecording) return;

            IsRecording = false;
            Stopped?.Invoke();
        }

        public void RecordAction(EchoActionType type, string targetId = "", string abilityId = "")
        {
            if (!IsRecording) return;
            actions.Add(new EchoAction(recordingTime, type, targetId, abilityId));
        }

        private void Update()
        {
            if (!IsRecording) return;

            recordingTime += Time.deltaTime;

            if (recordingTime >= nextSampleTime)
            {
                var t = transform;
                frames.Add(new EchoFrame(recordingTime, t.position, t.rotation));
                nextSampleTime += positionSampleInterval;
            }

            if (recordingTime >= maxRecordingDuration)
            {
                StopRecording();
            }
        }

        public EchoRecording CreateRecording()
        {
            return new EchoRecording(new List<EchoFrame>(frames), new List<EchoAction>(actions), recordingTime);
        }
    }

    [Serializable]
    public class EchoRecording
    {
        [SerializeField] private List<EchoFrame> frames;
        [SerializeField] private List<EchoAction> actions;
        [SerializeField] private float duration;

        public IReadOnlyList<EchoFrame> Frames => frames;
        public IReadOnlyList<EchoAction> Actions => actions;
        public float Duration => duration;

        public EchoRecording(List<EchoFrame> frames, List<EchoAction> actions, float duration)
        {
            this.frames = frames;
            this.actions = actions;
            this.duration = duration;
        }

        public bool IsValid => frames != null && frames.Count > 0 && duration > 0f;
    }
}
