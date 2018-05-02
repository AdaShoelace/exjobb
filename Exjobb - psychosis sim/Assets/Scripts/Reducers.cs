using System.Collections;
using System.Collections.Generic;
using Unidux;
using UnityEngine;

namespace Pierre.Unidux
{
	public class Reducers : MonoBehaviour {

		public class Reducer : ReducerBase<SceneState, Action>
        {
            public override SceneState Reduce(SceneState state, Action action)
            {
                switch (action.ActionType)
                {
                    case ActionType.ToggleRadio:
                        state.radioMute = !state.radioMute;
                        break;
					case ActionType.PlayPhoneSound:
						state.phonePlay = true;
						break;
					case ActionType.StopPhoneSound:
						state.phonePlay = false;
						break;
                    case ActionType.TurnOffAlarmClock:
                        state.alarmClockMute = !state.alarmClockMute;
                        break;
                    case ActionType.SetLight:
                        state.light = lightChange(state.light);
                        break;
                    case ActionType.BottleTilted:
                        state.spawnPill = true;
                        break;
                    case ActionType.PillHasSpawned:
                        state.pillHasSpawned = true;
                        break;
                    case ActionType.BottleDropped:
                        state.changeBottleLabel = true;
                        break;
                    case ActionType.BottleLabelHasChanged:
                        state.bottleLabelHasChanged = true;
                        break;

                    //test for pill bottle audio
                    case ActionType.PillBottleAudioHallucination:
                        state.crowdWhisperIsPlaying = true;
                        break;
                }

                return state;
            }

            private LightState lightChange(LightState oldState) {
                if(oldState.range < 100) {
					oldState.range += 2.0f;
				}
						
                if(oldState.r > 0.2f && oldState.g > 0.2f) {
                    return new LightState(oldState.r - .2f, oldState.g - .2f, oldState.b, oldState.range);
                }
                return oldState;
            }
        }
	}
}

