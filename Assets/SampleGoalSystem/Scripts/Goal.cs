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
        public string description;
        [TextArea]
        public string explanation;
        private bool activeGoal = false;
       
        //events
        public static Action<Goal> Started;//Iniciado
        public static Action<Goal> Achieved;//Alcançado
        public static Action<Goal> Failed;//Falhou
        public static Action<Goal> Activated;//Ativado
        public static Action<Goal> Deactivated;//Desativado
        //States
        public GoalStatus goalStatus = GoalStatus.RUNNING;
       
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
            Activated?.Invoke(this);
        }
        public void Deactivate()
        {
            activeGoal = false;
            Deactivated?.Invoke(this);
        }
        public bool IsActivated()
        {
            return activeGoal;
        }
        

    }


}

