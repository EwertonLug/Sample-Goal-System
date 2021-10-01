using System;
using UnityEngine;
using UnityEngine.Events;
namespace GoalSystem
{
    public enum GoalType
    {
        OPCIONAL,
        REQUIRED
    }
    public enum GoalStatus
    {
        COMPLETED,
        FAILED,
        RUNNING
    }

    // This is the abstract base class for all goals:
    public abstract class Goal : MonoBehaviour
    {
        //Type
        public GoalType type = GoalType.REQUIRED;

        public int sequenceID;
        public string title;
        [TextArea]
        public string explanation;
        private bool activeGoal = false;
       
        //events
        public static Action<Goal> Started;//Iniciado
        public static Action<Goal> Achieved;//Alcançado
        public static Action<Goal> Failed;//Falhou

        //States
        public GoalStatus goalStatus = GoalStatus.RUNNING;
        //Unity Event
        public UnityEvent OnActivated;
        public UnityEvent OnDeactivated;
        private void Start()
        {
            Started?.Invoke(this);
            Debug.Log("Started Goal ID:"+ sequenceID);
            
        }
        private void Update()
        {
            if (activeGoal && goalStatus != GoalStatus.COMPLETED 
                           && goalStatus != GoalStatus.FAILED)
            {
                goalStatus = IsRunning();

                switch (goalStatus)
                {
                    case GoalStatus.COMPLETED:
                        Achieved?.Invoke(this);
                        gameObject.SetActive(false);
                        break;
                    case GoalStatus.FAILED:
                        Failed?.Invoke(this);
                        break;
                    case GoalStatus.RUNNING:
                        break;
                    default:
                        break;
                }
            }
        }
        protected abstract GoalStatus IsRunning();

        public void Activate()
        {
            activeGoal = true;
            OnActivated.Invoke();
        }
        public void Deactivate()
        {
            activeGoal = false;
            OnDeactivated.Invoke();
        }
        public bool IsActivated()
        {
            return activeGoal;
        }
        

    }


}

