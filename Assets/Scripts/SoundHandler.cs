using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public FMODUnity.EventReference RootGrowth;
    FMOD.Studio.EventInstance rootGrowth;

    public FMODUnity.EventReference WaterCollection;
    FMOD.Studio.EventInstance waterCollect;

    public FMODUnity.EventReference HomeSweetHome;
    FMOD.Studio.EventInstance homeSweetHome;

    public string rootState = null;
    public string gridState = null;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        homeSweetHome = FMODUnity.RuntimeManager.CreateInstance(HomeSweetHome);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(homeSweetHome, Camera.main.transform);
        homeSweetHome.start();

        rootGrowth = FMODUnity.RuntimeManager.CreateInstance(RootGrowth);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(rootGrowth, Camera.main.transform);

        waterCollect = FMODUnity.RuntimeManager.CreateInstance(WaterCollection);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(waterCollect, Camera.main.transform);
    }


    void Update()
    {
        if(rootState == "stretching"){
            FMOD.Studio.PLAYBACK_STATE playbackState;
            rootGrowth.getPlaybackState(out playbackState);
            if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
                rootGrowth.start();
            }
        } else {
            rootGrowth.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        if(gridState == "water") {
            FMOD.Studio.PLAYBACK_STATE playbackState;
            waterCollect.getPlaybackState(out playbackState);
            if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
                waterCollect.start();
            }
        }
    }
}
