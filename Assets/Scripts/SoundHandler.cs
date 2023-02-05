using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundHandler : MonoBehaviour
{
    public enum JukeboxSong
    {
        HoverMenu,
        ConfirmMenu,
        rockCollide,
        waterCollect,
        rootGrowth,
        DootDoot
    }

//instances to be able to track
    public FMODUnity.EventReference RootGrowth;
    FMOD.Studio.EventInstance rootGrowth;

    public FMODUnity.EventReference WaterCollection;
    FMOD.Studio.EventInstance waterCollect;

    public FMODUnity.EventReference HomeSweetHome;
    FMOD.Studio.EventInstance homeSweetHome;

    public FMODUnity.EventReference RockCollision;
    FMOD.Studio.EventInstance rockCollide;

    // private RockCell rockCell;

//one shots
    public FMODUnity.EventReference HoverMenu;
    public FMODUnity.EventReference ConfirmMenu;
    public FMODUnity.EventReference DootDoot;


    public string rootState = null;
    public string gridState = null;

    // void Awake () {
    //     rockCell = GetComponent<RockCell>(); //i dont understand getcomponent stuff at all yet
    // }

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

        //place holder
        rockCollide = FMODUnity.RuntimeManager.CreateInstance(RockCollision);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(rockCollide, Camera.main.transform);
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
            case JukeboxSong.ConfirmMenu: {
                FMODUnity.RuntimeManager.PlayOneShot(ConfirmMenu, Camera.main.transform.position);
                break;
            }
            case JukeboxSong.DootDoot: {
                FMODUnity.RuntimeManager.PlayOneShot(DootDoot, Camera.main.transform.position);
                break;
            }
            case JukeboxSong.rockCollide: {
                rockCollide.start();
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

        // if(rockCell.IsColliding()){
        //     FMOD.Studio.PLAYBACK_STATE playbackState;
        //     rockCollide.getPlaybackState(out playbackState);
        //     if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
        //         rockCollide.start();
        //     }    
        // }

        if(gridState == "water") {
            FMOD.Studio.PLAYBACK_STATE playbackState;
            waterCollect.getPlaybackState(out playbackState);
            if(playbackState != FMOD.Studio.PLAYBACK_STATE.PLAYING){
                waterCollect.start();
            }
        }
    }
}