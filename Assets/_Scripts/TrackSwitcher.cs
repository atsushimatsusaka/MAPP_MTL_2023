using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[RequireComponent(typeof(PlayableDirector))]
public class TrackSwitcher : MonoBehaviour
{
    private PlayableDirector pd;

    void Awake()
    {
        pd = GetComponent<PlayableDirector>();

        if (pd.playOnAwake) Debug.Log("<color=yellow>Warning: </color> TrackSwitcher does not work. You should set Play On Awake to false if you want to work the script");

        var sceneName = SceneManager.GetActiveScene().name;
        var m = "";
        var um = "";

        switch (sceneName)
        {
            case "Snow":
                m = "YukakuScene";
                um = "SnowScene";
                break;

            case "Yukaku":
                m = "SnowScene";
                um = "YukakuScene";
                break;

            default:
                Debug.Log("<color=red>Error: </color> Specified scene name is not found");
                break;
        }

        MuteTrackGP<PlayableAsset>(m, um);
    }


    private T MuteTrackGP<T>(string mutedTrackName, string unmutedTrackName) where T : PlayableAsset
    {
        //Get a list of root tracks in timeline
        IEnumerable<TrackAsset> tracks = (pd.playableAsset as TimelineAsset).GetRootTracks();

        //Get two tracks with a specified name
        TrackAsset track_m = tracks.FirstOrDefault(x => x.name == mutedTrackName);
        TrackAsset track_um = tracks.FirstOrDefault(x => x.name == unmutedTrackName);

        track_m.muted = true;
        track_um.muted = false;

        pd.Play();

        return null;
    }
}
