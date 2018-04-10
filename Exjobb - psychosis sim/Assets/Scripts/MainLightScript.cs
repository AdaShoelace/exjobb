using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unidux;

namespace Pierre.Unidux {

	public class MainLightScript : MonoBehaviour {

		
		private float cycleTime;
		private GameObject[] lightGroup;
		private LightState previousState = LightState.initial();
		


		// Use this for initialization
		void Awake () {
			//timer	
			lightGroup = GameObject.FindGameObjectsWithTag("Light");
			StartCoroutine(fadeLight(previousState, previousState));
			Unidux.Subject
				.TakeUntilDisable(this)
				.StartWith(Unidux.State)
				.Subscribe(state => {
					if(previousState != state.light) {
						StartCoroutine(fadeLight(previousState, state.light));
						previousState = state.light;
					}
				})
				.AddTo(this);

		}
		
		// Update is called once per frame
		void Update () {
			if(Time.time - cycleTime > 10) {
				Unidux.Dispatch(Actions.ActionCreator.Create(ActionType.SetLight));
				cycleTime = Time.time;
			}
		}

		private IEnumerator fadeLight(LightState previous, LightState next) {
			Color oldColor = new Color(previous.r, previous.g, previous.b, 1);
			Color newColor = new Color(next.r, next.g, next.b, 1);

			for (int i = 0; i < 10; i++)
      {
				foreach(GameObject go in lightGroup) {
						var light = go.GetComponent("Light") as Light;
						light.color = Color.Lerp(oldColor, newColor, 0.1f * i);
						light.range = Mathf.Lerp(previous.range, next.range, 0.1f * i);
					}
					yield return new WaitForSecondsRealtime(.5f);
			}
		}
	}
}
