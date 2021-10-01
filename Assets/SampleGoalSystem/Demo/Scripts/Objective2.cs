using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoalSystem
{
    public class Objective2 : Goal
    {


        protected override GoalStatus IsRunning()
        {

            if (Input.GetKey(KeyCode.E))
            {
                return GoalStatus.COMPLETED;
            }
            return GoalStatus.RUNNING;
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player") && !IsActivated())
            {
      
               
                Activate();
            }
        }
        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
               
                Deactivate();
            }
        }


    }
}
