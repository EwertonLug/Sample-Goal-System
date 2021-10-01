using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoalSystem;
public class Portal : MonoBehaviour {
	public GameObject _portal;
    public GameObject canvas;
	void Update () {
		_portal.transform.Rotate (new Vector3 (0f, 0f, 3f));
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") 
            && !GoalManager.Instance.IsCompletedGoals)
        {


            canvas.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {

            canvas.SetActive(false);
        }
    }
}
