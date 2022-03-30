using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance;

    private FMOD.Studio.EventInstance soundInstance;

    public FMODUnity.EventReference fmodEvent;


    [SerializeField][Range(0f, 1f)]
    private float reverb, delay;

    [SerializeField][Range(0f, 5000f)]
    private float delayTime;
     
    [SerializeField][Range(-12f, 12f)]
    private float pitch;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static SoundManager GetInstance()
    {
        return instance;
    }

    void Start()
    {
        soundInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        soundInstance.start();
    }


    public void BattleSoundSetup()
    {
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        soundInstance.setParameterByNameWithLabel("MusicStates", "Battle");

        soundInstance.start();
    }

    public void FreeRoamSoundSetup()
    {
        soundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        soundInstance.setParameterByNameWithLabel("MusicStates", "Freeroam");

        soundInstance.start();
    }

}
