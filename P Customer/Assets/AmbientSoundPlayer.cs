using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundPlayer : MonoBehaviour
{

    [SerializeField] private AudioSource sound;
    [SerializeField] private float TimeBetweenPlays;
    private float playNow = 0;
    private float playNextTime;
    // Start is called before the first frame update
    void Start()
    {
        playNextTime = Random.Range(1f, TimeBetweenPlays + 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sound.isPlaying)
        {
            playNow += Time.deltaTime;
            if (playNow >= playNextTime)
            {
                playNow = 0;
                sound.Play();
                playNextTime = Random.Range(1f, TimeBetweenPlays + 1f);
            }
        }
    }
}
