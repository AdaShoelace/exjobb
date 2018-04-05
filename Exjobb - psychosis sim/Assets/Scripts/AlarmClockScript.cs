using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using VRTK;
using UniRx;

namespace Pierre.Unidux
{
	[InitializeOnLoad]
	public class AlarmClockScript : VRTK_InteractableObject {
	public ActionType TurnOffAlarmClock = ActionType.TurnOffAlarmClock;
			
	public void Awake() {
			AudioSource clock = gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
			(gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody).constraints = RigidbodyConstraints.FreezeAll;
			//clock.Play();
			clock.mute = false;
			Unidux.Subject
				.TakeUntilDisable(this)
				.StartWith(Unidux.State)
				.Subscribe(state => {
						if(clock.isPlaying && state.alarmClockMute) {
							clock.Stop();
						} else if(!clock.isPlaying && !state.alarmClockMute) {
							clock.Play();
						}
						
					})
				.AddTo(this);
		}

		public override void StartUsing(VRTK_InteractUse ob) {
			base.StartUsing(ob);
			Unidux.Store.Dispatch(Actions.ActionCreator.Create(ActionType.TurnOffAlarmClock));
		}

	}
}
