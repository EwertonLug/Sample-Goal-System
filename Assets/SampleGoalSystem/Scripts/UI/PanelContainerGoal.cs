using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoalSystem;
using System;
using System.Linq;

public class PanelContainerGoal : MonoBehaviour
{

    public GameObject prefabGoalText;
    public List<GoalHud> goalsHud = new List<GoalHud>();
    private void Start()
    {
        
        Goal.Started += CreateUIGoal;

    }

    private void CreateUIGoal(Goal g)
    {
        var ui = Instantiate(prefabGoalText, transform.position, Quaternion.identity);
        ui.transform.SetParent(transform,false);
        GoalHud hud = ui.GetComponent<GoalHud>();
        ui.name = "GoalHud (seq= "+g.sequenceID+")";
        hud.Inite(g.sequenceID,g.title);

        goalsHud.Add(hud);

        ReorderList();
        



    }
    public void ReorderList()
    {
        goalsHud = goalsHud.OrderBy(goal => goal.sequencia).ToList();

        foreach (var hud in goalsHud)
        {
            hud.GetComponent<RectTransform>().SetSiblingIndex(hud.sequencia);
        }

    }
    private void OnDestroy()
    {
        Goal.Started -= CreateUIGoal;
    }
}
