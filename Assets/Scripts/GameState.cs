using UnityEngine;

namespace CockroachRunner
{
    public class GameState
    {
        public int Currency { get; set; }

        public int RaceTime { get; set; }  

        public int FinishedOpponentsCount { get; set; }

        public int PlayerPlace { get; set; }

        public string CurrentTaskId { get; set; }
    }
}