using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using Unidux;
using UniRx;

namespace Pierre.Unidux
{
    public class DoorAnimation : VRTK.Examples.Openable_Door
    {
		public float doorOpenAngle = -120f;
		
		public override void StartUsing(VRTK_InteractUse usingObject)
		{
			base.StartUsing(usingObject);
		}

		protected override void Update()
		{
			if(Time.time > 5) {
				base.Update();
			}
		}
    }


}

