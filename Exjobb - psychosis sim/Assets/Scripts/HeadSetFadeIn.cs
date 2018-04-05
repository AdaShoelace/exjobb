using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HeadSetFadeIn : MonoBehaviour {

	void Awake() {
		VRTK_HeadsetFade hs_fade = new VRTK_HeadsetFade();
		hs_fade.Fade(Color.black, 0.1f);
		hs_fade.Unfade(2.5f);
	}	
	// Update is called once per frame
	void Update () {
		
	}
}
