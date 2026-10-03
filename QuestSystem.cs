using System;
using System.Collections.Generic;
using UnityEngine;

namespace EchoBound
{
    [Serializable]
    public class QuestObjective
    {
        public string description;
        public bool completed;
    }

    [CreateAssetMenu(menuName = "EchoBound/Quest")]
    public class QuestDefinition : ScriptableObject
    {
        public string questId;
        public string title;
        [TextArea] public string description;
        public int xpReward = 50;
        public List<QuestObjective> objectives = new();
    }

    public class QuestSystem : MonoBehaviour
    {
        public event Action<QuestDefinition> QuestCompleted;

        public void CompleteQuest(QuestDefinition quest)
        {
            if (quest == null) return;
            QuestCompleted?.Invoke(quest);
        }
    }
}
