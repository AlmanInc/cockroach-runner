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
        public int type;
        public string param;
        public bool done;
    }
}