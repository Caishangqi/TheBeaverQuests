using System;
using UnityEngine;

namespace Core.Character
{
    [Serializable]
    public class PlayerSo
    {
        public int health { get; set; }
        public int experience { get; set; }

        public CubeView carriedObj { get; set; }

        public PlayerSo()
        {
            health = 3;
            experience = 0;
        }

        public PlayerSo(int health, int experience)
        {
            this.health = health;
            this.experience = experience;
        }
    }
}