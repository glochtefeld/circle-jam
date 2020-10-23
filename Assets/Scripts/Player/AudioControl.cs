﻿// #define DIAGNOSTIC_MODE
using System.Collections;
using UnityEngine;

public enum SFX
{
    Death,
    HookCollideFail,
    HookCollideSucceed,
    HookFire,
    Jump,
    Swing,
    Land,
    GetTreasure,
    Splash
}

public enum BGM
{
    Main,
    Forest,
    City,
    Sky
}

public class AudioControl : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    [Header("BGM")]
    public AudioSource bgm;
    public AudioInLevel main;
    public AudioInLevel forest;
    public AudioInLevel city;
    public AudioInLevel sky;
    [Header("SFX")]
    public AudioSource sfx;
    public AudioClip death;
    public AudioClip hookCollideFail;
    public AudioClip hookCollideSuccess;
    public AudioClip hookFire;
    public AudioClip jump;
    public AudioClip swing;
    public AudioClip land;
    public AudioClip getTreasure;
    public AudioClip splash;
#pragma warning restore CS0649
#endregion
    
    public void PlaySFX(SFX sound, bool loop=false)
    {
        switch (sound)
        {
            case SFX.Death:
                PlaySFX(death,loop);
                    break;
            case SFX.HookCollideFail:
                PlaySFX(hookCollideFail,loop);
                    break;
            case SFX.HookCollideSucceed:
                PlaySFX(hookCollideSuccess,loop);
                    break;
            case SFX.HookFire:
                PlaySFX(hookFire,loop);
                    break;
            case SFX.Jump:
                PlaySFX(jump,loop);
                    break;
            case SFX.Swing:
                PlaySFX(swing,loop);
                    break;
            case SFX.Land:
                PlaySFX(land,loop);
                    break;
            case SFX.GetTreasure:
                PlaySFX(getTreasure,loop);
                    break;
            case SFX.Splash:
                PlaySFX(splash,loop);
                    break;
        }
    }

    private void PlaySFX(AudioClip clip, bool loop)
    {
        sfx.PlayOneShot(clip);
        sfx.loop = loop;
    }

    public void PlayBGM(BGM track) =>
        StartCoroutine(PlayLevelAudio(track));

    private IEnumerator PlayLevelAudio(BGM track)
    {
        switch(track)
        {
            case BGM.Main:
                if (main.intro != null)
                {
                    bgm.PlayOneShot(main.intro);
                    while (bgm.isPlaying)
                        yield return null;
                }
                bgm.PlayOneShot(main.mainLoop);
                bgm.loop = true;
                break;
            case BGM.Forest:
                if (forest.intro != null)
                {
                    bgm.PlayOneShot(forest.intro);
                    while (bgm.isPlaying)
                        yield return null;
                }
                bgm.PlayOneShot(forest.mainLoop);
                bgm.loop = true;
                break;
            case BGM.City:
                if (city.intro != null)
                {
                    bgm.PlayOneShot(city.intro);
                    while (bgm.isPlaying)
                        yield return null;
                }
                bgm.PlayOneShot(city.mainLoop);
                bgm.loop = true;
                break;
            case BGM.Sky:
                if (sky.intro != null)
                {
                    bgm.PlayOneShot(sky.intro);
                    while (bgm.isPlaying)
                        yield return null;
                }
                bgm.PlayOneShot(sky.mainLoop);
                bgm.loop = true;
                break;
        }
        yield return null;
    }

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
