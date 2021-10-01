using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject PanelLose;
    public GameObject PanelWin;
    // Start is called before the first frame update
    void Start()
    {
        GoalSystem.Player.playerLost += OnPlayerLost;
        GoalSystem.Player.playerWon += OnPlayerWon;
    }
    public void OnPlayerLost()
    {
        if(PanelLose)
            PanelLose.SetActive(true);
    }
    public void OnPlayerWon()
    {
        if (PanelWin)
            PanelWin.SetActive(true);
    }
    private void OnDestroy()
    {
        GoalSystem.Player.playerLost -= OnPlayerLost;
        GoalSystem.Player.playerWon -= OnPlayerWon;
    }
}
