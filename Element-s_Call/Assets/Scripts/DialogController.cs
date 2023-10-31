using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class DialogController : MonoBehaviour
{
    [SerializeField] private AudioSource[] characters;
    [SerializeField] private DialogClass[] audios;
    [SerializeField] private bool dialogScene = true;
    [SerializeField] private int debugIndex = 0;

    private int cont = 0;
    private int characterIndex = 0;
    private List<int> stopQueue = new List<int>();

    private void Awake()
    {
        if (debugIndex != 0) cont = debugIndex;

        Invoke("playDialog", audios[cont].initialDelay);

        for(int i = 0; i < audios.Length; i++)
        {
            if (audios[i].duration == 0) audios[i].duration = audios[i].clip.length;
        }
    }

    void playDialog()
    {
        characterIndex = audios[cont].characterIndex;
        if(audios[cont].duration != audios[cont].clip.length)
        {
            stopQueue.Add(cont);
            Invoke("stopDialog", audios[cont].duration);
        }
        if (audios[cont].duration > audios[cont].clip.length)
        {
            characters[characterIndex].loop = true;
            characters[characterIndex].clip = audios[cont].clip;
            characters[characterIndex].Play();
        }
        else
        {
            characters[characterIndex].PlayOneShot(audios[cont].clip);
        }

        cont++;

        if(cont < audios.Length)
        {
            Invoke("playDialog", (audios[cont - 1].duration + audios[cont].initialDelay));
        }
        if (cont == audios.Length && dialogScene)
        {
            Invoke("end", audios[cont-1].duration);
        }
    }

    void stopDialog()
    {
        float minDuration = 50000;
        int shortestClip = 0;

        for(int i = 0; i<stopQueue.Count; i++)
        {
            if(audios[stopQueue[i]].duration < minDuration)
            {
                minDuration = audios[stopQueue[i]].duration;
                shortestClip = i;
            }
        }

        int characterIndex = audios[stopQueue[shortestClip]].characterIndex;
        characters[characterIndex].Stop();
        stopQueue.RemoveAt(shortestClip);
    }

    void end()
    {
        FindObjectOfType<SceneController>().nextScene();
    }
}
