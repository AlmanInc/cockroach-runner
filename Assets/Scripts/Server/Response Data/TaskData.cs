using System;

namespace CockroachRunner
{
    [Serializable]
    public class TaskData
    {
        public string task_id;
        public string name;
        public string description;
        public int cost;
        public string type;
        public string param;
        public bool done;

        public TaskKinds Kind { get; set; }
    }
}