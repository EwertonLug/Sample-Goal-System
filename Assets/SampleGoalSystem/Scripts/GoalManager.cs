using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;
using System.Linq;

namespace GoalSystem
{
    // Add this script to the GoalManager GameObject. It keeps track of all goals.
    public class GoalManager : Singleton<GoalManager>
    {
        public List<Goal> goals = new List<Goal>();

        //Events
        public static event Action CompletedGoals;

        private bool isCompletedGoals = false;

        public bool IsCompletedGoals { get => isCompletedGoals; set => isCompletedGoals = value; }

        private void Start()
        {
            Goal.Started += AddGoal;
        }
       
        private void Update()
        {
            if (CompletedAllGoals())
            {
                CompletedGoals?.Invoke();
                isCompletedGoals = true;
            }
        }
        private void AddGoal(Goal g)
        {
            if (!goals.Exists(goal => goal.sequenceID == g.sequenceID))
            {
                goals.Add(g);
                
            }
            else
            {
                Debug.LogError("[GoalSystem] - Sequence "+g.GetType()+ " already exists. Seq:"+g.sequenceID);
            }

            goals = goals.OrderBy(goal => goal.sequenceID).ToList();
        }
        private bool CompletedAllGoals()
        {
            var value = true;

            foreach (var goal in goals)
            {

                if (goal.type != GoalType.OPCIONAL)
                {
                    if (goal.goalStatus != GoalStatus.COMPLETED)
                    {
                        value = false;
                    }
                }
            }
            return value;
        }

        private void OnDestroy()
        {
            Goal.Started -= AddGoal;
        }

    }
}
