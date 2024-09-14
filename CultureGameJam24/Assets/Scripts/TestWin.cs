using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestWin : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private QuestManager questManager;
    
    // Start is called before the first frame update
    void Start()
    {
        textMesh.gameObject.SetActive(false);
        questManager.OnQuestComplete += Win;
    }

    void Win()
    {
        textMesh.gameObject.SetActive(true);
    }
}
