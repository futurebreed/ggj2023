using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundHandler : MonoBehaviour
{
    public enum JukeboxSong
    {
        HoverMenu,
        ConfirmMenu,
        RockCollision,
        waterCollect,
        rootGrowth,
        DootDoot,
        Exit
    }

//instances to be able to track
    public FMODUnity.EventReference WaterCollection;
    FMOD.Studio.EventInstance waterCollect;

    public FMODUnity.EventReference HomeSweetHome;
    FMOD.Studio.EventInstance homeSweetHome;

    public FMODUnity.EventReference RootGrowth;
    FMOD.Studio.EventInstance rootGrowth;

    

//one shots
    public FMODUnity.EventReference HoverMenu;
    public FMODUnity.EventReference ConfirmMenu;
    public FMODUnity.EventReference DootDoot;
    public FMODUnity.EventReference Exit;
    public FMODUnity.EventReference RockCollision;



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

    public void PlayJukebox(JukeboxSong jukeboxSong) {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        homeSweetHome.getPlaybackState(out playbackState);
        if(playbackState == FMOD.Studio.PLAYBACK_STATE.PLAYING){
            homeSweetHome.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        switch (jukeboxSong) {
            case JukeboxSong.HoverMenu: {
                FMODUnity.RuntimeManager.PlayOneShot(HoverMenu, Camera.main.transform.position);
                break;
            }
            case JukeboxSong.Exit: {
                FMODUnity.RuntimeManager.PlayOneShot(Exit, Camera.main.transform.position);
                return;
            }
            case JukeboxSong.ConfirmMenu: {
                FMODUnity.RuntimeManager.PlayOneShot(ConfirmMenu, Camera.main.transform.position);
                break;
            }
            case JukeboxSong.DootDoot: {
                FMODUnity.RuntimeManager.PlayOneShot(DootDoot, Camera.main.transform.position);
                break;
            }
            case JukeboxSong.RockCollision: {
                FMODUnity.RuntimeManager.PlayOneShot(RockCollision, Camera.main.transform.position);
                break;
            }
            case JukeboxSong.waterCollect: {
                waterCollect.start();
                break;
            }
            case JukeboxSong.rootGrowth: {
                rootGrowth.start();
                break;
            }
            default: {
                break;
            }
        }

        homeSweetHome.getPlaybackState(out playbackState);
        if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
            homeSweetHome.start();
        }
    }

    public void Growth() {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        rootGrowth.getPlaybackState(out playbackState);
        if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
            rootGrowth.start();
        }
    }

    public void StopGrowth() {
        FMOD.Studio.PLAYBACK_STATE playbackState;
        rootGrowth.getPlaybackState(out playbackState);
        if(playbackState == FMOD.Studio.PLAYBACK_STATE.PLAYING){
            rootGrowth.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }


    void Update()
    {
    }
}