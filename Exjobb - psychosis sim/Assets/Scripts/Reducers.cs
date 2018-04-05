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
                        state.lightIntensity = ((LightIntensityAction)action).intensity;
                        break;
                }

                return state;
            }
        }
	}
}

