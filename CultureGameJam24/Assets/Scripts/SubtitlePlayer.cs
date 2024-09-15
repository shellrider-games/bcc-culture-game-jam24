using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitlePlayer : MonoBehaviour
{
    [SerializeField] List<Subtitle> subtitles = new List<Subtitle>();
    [SerializeField] AudioSource audioSource;
    [SerializeField] TextMeshProUGUI subtitleText;

    private int currentSubtitleIndex = -1;
    
    void Start()
    {
        subtitleText.text = "";
    }

    void Update()
    {
        float currentTime = audioSource.time;
        
        if (currentSubtitleIndex + 1 < subtitles.Count && currentTime >= subtitles[currentSubtitleIndex + 1].startTime)
        {
            currentSubtitleIndex++;
            subtitleText.text = subtitles[currentSubtitleIndex].text;
        }

        if (currentSubtitleIndex >= 0 && currentTime >= subtitles[currentSubtitleIndex].endTime)
        {
            subtitleText.text = "";
        } 
    }
}
