using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unidux;

namespace Pierre.Unidux {

	public class MainLightScript : MonoBehaviour {

		
		private float cycleTime;
		private GameObject[] lightGroup;
		


		// Use this for initialization
		void Awake () {
			//timer	
			lightGroup = GameObject.FindGameObjectsWithTag("Light");
			Unidux.Subject
				.TakeUntilDisable(this)
				.StartWith(Unidux.State)
				.Subscribe(state => {
					foreach(GameObject go in lightGroup) {
						var light = go.GetComponent("Light") as Light;
						light.color = new Color(state.light.r, state.light.g, state.light.b, 1);
						light.range = state.light.range;
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
	}
}
