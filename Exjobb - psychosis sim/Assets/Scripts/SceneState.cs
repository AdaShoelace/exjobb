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
   
      public LightState light = LightState.initial();
    
    }

    [Serializable]
    public class LightState {
      public float r, g, b;
      public float range;

      public LightState(float r, float g, float b, float range) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.range = range;
      }

      public static LightState initial() {
        return new LightState(1.0f, 1.0f, 1.0f, 1.0f);
      }
    }
}
