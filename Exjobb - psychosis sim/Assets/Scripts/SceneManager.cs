using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unidux;
using UniRx;
using VRTK;

namespace Pierre.Unidux
{
    public class SceneManager : MonoBehaviour
    {
        private bool isCoroutineExecuting = false;
        void Awake()
        {
            Unidux.Subject
                      .TakeUntilDisable(this)
                      .StartWith(Unidux.State)
                      .Subscribe(state =>
                      {
                          if (state.isSceneStarting && !state.hasSceneStarted)
                          {
                              print("Starting");
                              //StartCoroutine(FadeIn(10));
                              Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.SceneHasStarted));
                          }
                          if (state.isSceneOver)
                          {
                              GetComponent<VRTK_HeadsetFade>().Fade(Color.black, 2);
                          }
                      })
                      .AddTo(this);
        }
        void Start()
        {
            Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.StartScene));
        }
        IEnumerator FadeIn(float time)
        {
            if (isCoroutineExecuting)
                yield break;

            isCoroutineExecuting = true;
            yield return new WaitForSeconds(time);

            GetComponent<VRTK_HeadsetFade>().Fade(Color.black, 10);
            GetComponent<VRTK_HeadsetFade>().Unfade(10);
            isCoroutineExecuting = false;
        }
    }
}