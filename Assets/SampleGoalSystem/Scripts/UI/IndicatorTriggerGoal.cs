using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalSystem
{

    public class IndicatorTriggerGoal : MonoBehaviour
    {
        private int sequenceID;
        public GameObject indicatorObj;

        private void Reset()
        {
            CheckDependencies();
        }

        void Start()
        {
            if (CheckDependencies())
            {
                sequenceID = GetComponent<Goal>().sequenceID;
            }
            
            Goal.Activated += ActiveIndicator;
            Goal.Deactivated += DeactiveIndicator;
        }

        private bool CheckDependencies()
        {
            var flag = true;

            if (!GetComponent<Goal>())
            {
                Debug.LogError("[Goal System] - a class derived from Goal is needed for the IndicatorTriggerGoal class to work");
                flag = false;
            }
            if(flag == false)
            {
                enabled = false;
            }
            return flag;
        }
        private void ActiveIndicator(Goal g)
        {
            if (this.sequenceID == g.sequenceID)
            {
                indicatorObj.SetActive(true);
            }

        }
        private void DeactiveIndicator(Goal g)
        {
            if (this.sequenceID == g.sequenceID)
            {
                indicatorObj.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            Goal.Activated -= ActiveIndicator;
            Goal.Deactivated -= DeactiveIndicator;
        }

    }
}
