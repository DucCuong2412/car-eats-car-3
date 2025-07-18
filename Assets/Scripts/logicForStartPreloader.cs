using System;
using System.Collections;
using UnityEngine;

public class logicForStartPreloader : MonoBehaviour
{
    public Animation anim;

    private void OnEnable()
    {
        AchievementsManager.OnAchievementsLoaded = (Action)Delegate.Combine(AchievementsManager.OnAchievementsLoaded, new Action(OnAchievementsLoaded));
        //StartCoroutine(playSound());
    }

    private void OnDisable()
    {
        AchievementsManager.OnAchievementsLoaded = (Action)Delegate.Remove(AchievementsManager.OnAchievementsLoaded, new Action(OnAchievementsLoaded));
    }

    //private IEnumerator playSound()
    //{
    //    Audio.PlayAsync("logo_engine_idle");
    //    float t = 1.5f;
    //    while (t >= 0f)
    //    {
    //        t -= Time.deltaTime;
    //        yield return null;
    //    }
    //    Audio.PlayAsync("logo_sound");
    //}

    private void Start()
    {
        anim.gameObject.SetActive(false);
        //while (anim.isPlaying)
        //{
        //	yield return 0;
        //}
        //SetMusicState(Progress.settings.isMusic);
        //SetSoundState(Progress.settings.isSound);
        //OpenDebug();
        if (Progress.levels.InUndeground)
        {
            Game.LoadLevel("scene_underground_map_new");
        }
        else
        {
            Game.LoadLevel("map_new");
        }
        //yield return null;
    }

    //private void SetMusicState(bool enabled)
    //{
    //}

    //private void SetSoundState(bool enabled)
    //{
    //}

    protected void OnAchievementsLoaded()
    {
    }

    //private void OpenDebug()
    //{
    //}
}
