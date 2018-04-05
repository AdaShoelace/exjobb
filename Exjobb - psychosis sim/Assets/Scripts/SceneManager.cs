using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

namespace Pierre.Unidux {
	public class SceneManager : MonoBehaviour {

		// Use this for initialization
		void Awake () {
			var hs = gameObject.GetComponent<VRTK_HeadsetFade>();
			hs.Fade(Color.black, .1f);
			hs.Unfade(3.0f);
		}
		
		// Update is called once per frame
		void Update () {
			
		}
	}
}
