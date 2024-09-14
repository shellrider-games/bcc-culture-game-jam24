using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public struct Objective
{
    public PuzzleItem item;
    [NonSerialized] public bool complete;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Objective> objectives = new List<Objective>();
    
    public event Action OnQuestComplete;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var objective in objectives)
        {
            objective.item.OnSnapped += CompleteObjective;
        }
    }

    void CompleteObjective(PuzzleItem item)
    {
        var completeObjective = objectives.Find((objective) => objective.item == item);
        if (completeObjective.item != null)
        {
            completeObjective.complete = true;
        }

        CheckQuestState();
    }

    void CheckQuestState()
    {
        foreach (var objective in objectives)
        {
            if (objective.complete == false) return;
        }
        
        OnQuestComplete?.Invoke();
    }
}
