using System;

namespace CockroachRunner
{
    [Serializable]
    public class GetTasksResponseData
    {
        public TaskData[] tasks;
    }
}