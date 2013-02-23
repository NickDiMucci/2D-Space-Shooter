using UnityEngine;
using System.Collections;

public class Enemy : BaseNpcEntity {
	public Transform gun;
	public Transform bullet;
	
	public AudioClip laserClip;
	private AudioSource laser;
	
	private const float MAX_FIRE_RATE = 0.55f;
	private float fireRate = MAX_FIRE_RATE;
	
	// Use this for initialization
	void Start() {
		speed = Random.Range(6, 15);
		laser = addAudio(laserClip, false, false, 128.0f);
		obtainScreenBounds();
	}
	
	// Update is called once per frame
	void Update() {
		this.moveEntityDownward();
		fireRate -= Time.deltaTime;
		if (fireRate <= 0.10f) {
			fireBullet();
			fireRate = MAX_FIRE_RATE;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (checkForCollisions(other)) {
			if (explosionParticle) {
				createExplosionParticle();
				audio.Play();
			}
			base.resetRandomPosition();
		}
	}
	
	private bool checkForCollisions(Collider other) {
		return other.tag.Equals("playerbullet") || 
			other.tag.Equals("player") || 
			other.tag.Equals("shield") ||
			other.tag.Equals("asteroid");
	}
	
	protected override void moveEntityDownward() {
		base.moveEntityDownward();
		
        if (outOfScreenLowerBounds()) {
            base.resetRandomPosition();
			base.resetRandomSpeed();
        }
	}
	
	private AudioSource addAudio(AudioClip clip, bool loop, bool playAwake, float volume) {
		AudioSource audioSource = (AudioSource) gameObject.AddComponent("AudioSource");
		audioSource.clip = clip;
		audioSource.loop = loop;
		audioSource.playOnAwake = playAwake;
		audioSource.volume = volume;
		
		return audioSource;
	}
	
	private void fireBullet() {				
		Instantiate(bullet, gun.position, gun.rotation);
		laser.Play();
	}
}
