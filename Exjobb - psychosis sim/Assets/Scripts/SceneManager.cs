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

        // Use this for initialization
        void Start()
        {

            Unidux.Subject
                .TakeUntilDisable(this)
                .StartWith(Unidux.State)
                .Subscribe(state =>
                {
                    if(state.hasSceneEnded)
                    {
                        GetComponent<VRTK_HeadsetFade>().Fade(Color.black, 3);
                        Application.Quit();
                    }
                })
                .AddTo(this);
        }

    }
}
