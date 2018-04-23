using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceHandler : MonoBehaviour {


    public AudioClip[] soundFiles;
    private Dictionary<string, int> soundBites;


    // Use this for initialization
    void Start () {
        soundBites = new Dictionary<string, int>();
        InitialiseAudioFiles();
    }
	

    private void InitialiseAudioFiles()
    {
        for (int i = 0; i < soundFiles.Length; i++)
        {
            Debug.Log(soundFiles[i].name);
            soundBites.Add(soundFiles[i].name, i);
        }
    }

    public IEnumerator PlaySoundBite(GameObject gameObject)
    {
        AudioSource wordOfMouth = GetComponent<AudioSource>();
        Debug.Log(gameObject.name);
        wordOfMouth.PlayOneShot(soundFiles[soundBites[gameObject.name]]);
        yield return new WaitForSeconds(soundFiles[soundBites[gameObject.name]].length);
    }
}
