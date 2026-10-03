using System;
using UnityEngine;

namespace EchoBound
{
    public class ProgressionSystem : MonoBehaviour
    {
        [SerializeField] private int level = 1;
        [SerializeField] private int currentXp;
        [SerializeField] private int baseXpRequirement = 100;

        public int Level => level;
        public int CurrentXp => currentXp;
        public int XpRequired => CalculateXpRequirement(level);

        public event Action<int> LevelUp;
        public event Action<int> XpChanged;

        public void AddXp(int amount)
        {
            if (amount <= 0) return;

            currentXp += amount;
            XpChanged?.Invoke(currentXp);

            while (currentXp >= XpRequired)
            {
                currentXp -= XpRequired;
                level++;
                LevelUp?.Invoke(level);
            }
        }

        private int CalculateXpRequirement(int targetLevel)
        {
            return Mathf.RoundToInt(baseXpRequirement * Mathf.Pow(1.18f, targetLevel - 1));
        }
    }
}
