using UnityEngine;
using System.Collections;
using BlurryRoots.Common;

// (╯°□°）╯︵ ┻━┻ i love this emoticon ;)

[RequireComponent (typeof (MeshRenderer))]
public class TextureSprite : MonoBehaviour {

	[Range (0.0001f, float.MaxValue)]
	public float TimeBetweenFrames = 0.5f;
	public bool RandomizeStartFrame = true;
	public bool PlayReversed = false;
	public Texture[] Frames;

	void Awake () {
		this.changeFrameTrigger = new TimedTrigger (this.TimeBetweenFrames);
		this.changeFrameTrigger.TimeIsUp += this.OnChangeFrame;

		this.meshRenderer = this.GetComponent<MeshRenderer> ();
		this.currentFrameIndex = 0;

		if (this.RandomizeStartFrame) {
			this.currentFrameIndex = Mathf.FloorToInt (Random.value * (this.Frames.Length - 1));
		}
		else if (this.PlayReversed) {
			this.currentFrameIndex = (this.Frames.Length - 1);
		}
		this.SetCurrentFrame (this.currentFrameIndex);
	}
	
	void Update () {
		var dt = Time.deltaTime;

		this.changeFrameTrigger.Tick (dt);
	}

	void OnChangeFrame () {
		var nextFrameIndex = this.currentFrameIndex + 1;
		this.currentFrameIndex = this.Frames.Length <=  nextFrameIndex
			? 0
			: nextFrameIndex
			;
		this.SetCurrentFrame (this.currentFrameIndex);

		this.changeFrameTrigger.Reset ();
	}

	void SetCurrentFrame (int index) {
		if (0 == this.Frames.Length) {
			return;
		}

		if (this.PlayReversed) {
			index = (this.Frames.Length - 1) - index;
		}
		var nextTexture = this.Frames[index];
		this.meshRenderer.material.mainTexture = nextTexture;
	}

	void OnDrawGizmos () {
		// Hack to allow updating times in editor while running :/
		if (null != this.changeFrameTrigger) {
			this.changeFrameTrigger.Interval = this.TimeBetweenFrames;
		}
	}

	private TimedTrigger changeFrameTrigger;
	private MeshRenderer meshRenderer;
	private int currentFrameIndex;

}
