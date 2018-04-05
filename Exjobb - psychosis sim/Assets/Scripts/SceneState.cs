using System;
using Unidux;
using UnityEngine;

namespace Pierre.Unidux
{
	[Serializable]
    public class SceneState : StateBase
    {
		public bool radioMute = true;
		public bool phonePlay = false;
    public bool alarmClockMute = false;
    public float lightIntensity = 1.0f;
    }
}
