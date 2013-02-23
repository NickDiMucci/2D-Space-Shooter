using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public GameObject player;
	public AudioClip hitClip;
	
	public int health = 3;
	
	private AudioSource hit;

	// Use this for initialization
	void Start () {
		hit = addAudio(hitClip, false, false, 128.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			health = 3;
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (checkForCollisions(other)) {
			--health;
			hit.Play();
		}
	}
	
	private bool checkForCollisions(Collider other) {
		return other.tag.Equals("asteroid") || other.tag.Equals("enemybullet") || other.tag.Equals("enemy");
	}
		
	private AudioSource addAudio(AudioClip clip, bool loop, bool playAwake, float volume) {
		AudioSource audioSource = (AudioSource) gameObject.AddComponent("AudioSource");
		audioSource.clip = clip;
		audioSource.loop = loop;
		audioSource.playOnAwake = playAwake;
		audioSource.volume = volume;
		
		return audioSource;
	}
}
