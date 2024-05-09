using System;

namespace Elvenwood
{
    public enum DelayTaskState
    {
        NoStart,
        Started,
        Finish
    }
    
    public class DelayTask
    {
        public float Seconds { get; set; }
        public Action OnFinish { get; set; }
        
        public float StartSeconds { get; set; }
        public float FinishSeconds { get; set; }
        public DelayTaskState State { get; set; }
    }

}