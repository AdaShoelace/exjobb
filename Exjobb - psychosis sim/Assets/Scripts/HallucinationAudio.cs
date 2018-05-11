using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unidux;
using UniRx;

namespace Pierre.Unidux
{
    public class HallucinationAudio : MonoBehaviour
    {
        Dictionary<string, AudioSource> playerSources = new Dictionary<string, AudioSource>();
        List<AudioSource> ambientSources = new List<AudioSource>();
        private bool isDelayCoroutineExecuting = false;
        public void Start()
        {
            GameObject temp =
                GameObject.Find("SDKManager/SDKSetups/SteamVR").gameObject.activeSelf ?
                    temp = GameObject.Find("SDKManager/SDKSetups/SteamVR/[CameraRig]/Camera (head)/AudioParent")
                    : temp = GameObject.Find("SDKManager/SDKSetups/Simulator/VRSimulatorCameraRig/AudioParent");
            foreach (AudioSource go in temp.GetComponentsInChildren<AudioSource>())
            {
                AudioSource aso = go.GetComponent("AudioSource") as AudioSource;
                aso.enabled = true;
                playerSources.Add(go.transform.name, aso);
            }

            Unidux.Subject
                        .TakeUntilDisable(this)
                        .StartWith(Unidux.State)
                        .Subscribe(state =>
                        {
                            if (state.playCrowdWhisper && !state.crowdWhisperIsPlaying)
                            {
                                PlayCloseProximityAmbientWhisper();
                                print("Time to play hallucination");
                                Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.CrowdIsWhispering));
                            }
                            if (state.playAccidentHallucination)
                            {
                                var clipToPlay = Resources.Load("Sound clips/bike-honking", typeof(AudioClip)) as AudioClip;
                                StartCoroutine(PlayCloseProximityAudioAfterDelay(clipToPlay, 3));
                            }
                        })
                        .AddTo(this);
        }
        private void PlayCloseProximityAudio(AudioClip audioClip)
        {
            AudioSource left = playerSources["AudioLeft"];
            left.clip = audioClip;
            AudioSource right = playerSources["AudioRight"];
            right.clip = audioClip;
            left.Play();
            right.Play();
        }

        private IEnumerator PlayCloseProximityAudioAfterDelay(AudioClip audioClip, int timeInSeconds)
        {
            if (isDelayCoroutineExecuting)
                yield break;

            isDelayCoroutineExecuting = true;

            yield return new WaitForSeconds(timeInSeconds);

            PlayCloseProximityAudio(audioClip);
            isDelayCoroutineExecuting = false;
        }

        private void PlayCloseProximityAmbientWhisper()
        {
            AudioClip clip = Resources.Load("Voices/CrowdWhisper", typeof(AudioClip)) as AudioClip;
            AudioSource front = playerSources["AudioFront"];
            front.clip = clip;
            AudioSource back = playerSources["AudioBack"];
            back.clip = clip;
            front.loop = true;
            back.loop = true;
            front.Play();
            back.Play();
        }
    }
}
