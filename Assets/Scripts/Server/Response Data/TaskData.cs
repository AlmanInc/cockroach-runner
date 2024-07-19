using System;

namespace CockroachRunner
{
    // {"tasks": [{"id": 7, "user_id": "15", "task_id": "48654a91-3836-4091-ac42-1b4716088e55", "done": false, "done_date": null, "created": "2024-07-19T07:17:48.813Z"},
    //            {"id": 8, "user_id": "15", "task_id": "a7789b68-2129-4f06-aa2a-730deccdd356", "done": false, "done_date": null, "created": "2024-07-19T07:17:48.824Z"}]}


    [Serializable]
    public class TaskData
    {
        public int id;
        public string user_id;
        public string task_id;
        public bool done;
        public DateTime done_date;
        public string created;
    }
}