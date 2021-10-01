using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GoalSystem;
public class GoalHud : MonoBehaviour
{
    public int sequencia;
    public TextMeshProUGUI title;
    public GameObject imgCompleted;
    public void Inite(int sequence, string text)
    {
        title.text = text;
        this.sequencia = sequence;
        Goal.Achieved += OnCompleted;
    }
    public void OnCompleted(Goal g)
    {
        if (this.sequencia == g.sequenceID)
        {
            imgCompleted.SetActive(true);
            title.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;

        }
      

    }
    public void OnDestroy()
    {
        Goal.Achieved -= OnCompleted;
    }
}
