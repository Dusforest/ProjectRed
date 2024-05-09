using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

namespace Elvenwood
{
    public interface ITimeSystem : ISystem
    {
        float CurrentSeconds { get; }
        void AddDelayTask(float second, Action onDelayFinish);
    }

    public class TimeSystem : AbstractSystem, ITimeSystem
    {
        public class TimeSystemUpdateBehaviour : MonoBehaviour
        {
            public event Action OnUpdate;

            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }
        protected override void OnInit()
        {
            var updateBehaviourGameObj = new GameObject(nameof(TimeSystemUpdateBehaviour));
            var updateBehaviour = updateBehaviourGameObj.AddComponent<TimeSystemUpdateBehaviour>();

            updateBehaviour.OnUpdate += OnUpdate;
            
            CurrentSeconds = 0;
        }

        void OnUpdate() 
        {
            CurrentSeconds += Time.deltaTime;

            if (mDelayTasks.Count > 0)
            {
                var currentNode = mDelayTasks.First;
                
                while (currentNode!=null)
                {
                    var nextNode = currentNode.Next;
                    
                    var delayTask = currentNode.Value;

                    if (delayTask.State == DelayTaskState.NoStart)
                    {
                        delayTask.State = DelayTaskState.Started;
                        delayTask.StartSeconds = CurrentSeconds;
                        delayTask.FinishSeconds = CurrentSeconds + delayTask.Seconds;
                    }
                    else if(delayTask.State == DelayTaskState.Started)
                    {
                        if (CurrentSeconds >= delayTask.FinishSeconds)
                        {
                            delayTask.State = DelayTaskState.Finish;
                            delayTask.OnFinish();
                            
                            delayTask.OnFinish = null;
                            mDelayTasks.Remove(currentNode);
                        }
                    }

                    currentNode = nextNode;
                }
            }
        }

        public float CurrentSeconds { get; private set; }

        private LinkedList<DelayTask> mDelayTasks = new LinkedList<DelayTask>();

        public void AddDelayTask(float second, Action onDelayFinish)
        {
            var delayTask = new DelayTask()
            {
                Seconds = second,
                OnFinish = onDelayFinish,
                State = DelayTaskState.NoStart
            };

            mDelayTasks.AddLast(delayTask);
        }
        
        
        
        
    }
    
}