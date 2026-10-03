using System;
using UnityEngine;

namespace EchoBound
{
    public enum EchoActionType
    {
        None,
        Move,
        Look,
        Interact,
        LightAttack,
        HeavyAttack,
        Dodge,
        Ability
    }

    [Serializable]
    public struct EchoAction
    {
        public float time;
        public EchoActionType type;
        public string targetId;
        public string abilityId;

        public EchoAction(float time, EchoActionType type, string targetId = "", string abilityId = "")
        {
            this.time = time;
            this.type = type;
            this.targetId = targetId;
            this.abilityId = abilityId;
        }
    }

    [Serializable]
    public struct EchoFrame
    {
        public float time;
        public Vector3 position;
        public Quaternion rotation;

        public EchoFrame(float time, Vector3 position, Quaternion rotation)
        {
            this.time = time;
            this.position = position;
            this.rotation = rotation;
        }
    }
}
