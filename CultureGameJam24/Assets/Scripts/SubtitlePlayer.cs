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
    
    // Start is called before the first frame update
    void Start()
    {
        subtitleText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = audioSource.time;  // Get the current playback time of the audio
        
        // Check if we should display the next subtitle
        if (currentSubtitleIndex + 1 < subtitles.Count && currentTime >= subtitles[currentSubtitleIndex + 1].startTime)
        {
            currentSubtitleIndex++;
            subtitleText.text = subtitles[currentSubtitleIndex].text;
        }

        // Hide the subtitle once the endTime has passed
        if (currentSubtitleIndex >= 0 && currentTime >= subtitles[currentSubtitleIndex].endTime)
        {
            subtitleText.text = "";
        } 
    }
}
