using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Transform bullet;
    public Transform rightGun;
	public Transform leftGun;
	public Transform explosionParticle;
	public Transform shield;
	public Transform jet;
	
	private SceneManager sceneManagerScript;
	
	public AudioClip hitClip;
	public AudioClip laserClip;
	
	private AudioSource hit;
	private AudioSource laser;
	
	private Vector3 minScreenBounds;
	private Vector3 maxScreenBounds;
	
	public Color[] flashColors;
	
	public short lives = 5;
	
	public int shieldEnergyLevel = 4;
	
	private bool shieldOn = false;
	private bool alternateShot = true;
	
    public float playerSpeedXAxis = 10.0f;
    public float playerSpeedYAxis = 10.0f;
	private float flashLerpDuration = 0.10f;
	private float flashInvokeDuration = 0.5f;	

	// Use this for initialization
	void Start() {
		laser = addAudio(laserClip, false, false, 128.0f);
		hit = addAudio(hitClip, false, false, 128.0f);
		obtainScreenBounds();
		sceneManagerScript = GameObject.Find("SceneManager").GetComponent("SceneManager") as SceneManager;
		flashColors = new Color[2];
		flashColors[0] = Color.magenta;
		flashColors[1] = Color.white;
	}
	
	// Update is called once per frame
	void Update() {
        movePlayer();
        checkScreenBoundaries();
		
		if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
        	fireBullet();
		}
		
		createShield();
		checkPlayerLives();
		
		if (IsInvoking("colorFlash")) {
			checkColorFlashInvoking();
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (checkForCollisions(other)) {
			InvokeRepeating("colorFlash", 0, 0.10f);
			--lives;
			if (sceneManagerScript) {
				sceneManagerScript.SetLives(lives);
			}
			hit.Play();
		}
	}
	
	private bool checkForCollisions(Collider other) {
		return other.tag.Equals("asteroid") || other.tag.Equals("enemy") || other.tag.Equals("enemybullet");
	}
	
	private AudioSource addAudio(AudioClip clip, bool loop, bool playAwake, float volume) {
		AudioSource audioSource = (AudioSource) gameObject.AddComponent("AudioSource");
		audioSource.clip = clip;
		audioSource.loop = loop;
		audioSource.playOnAwake = playAwake;
		audioSource.volume = volume;
		
		return audioSource;
	}
	
	private void checkPlayerLives() {
		if (lives <= 0) {
			if (explosionParticle) {
				createExplosionParticle();
				hit.Play();
			}
			Destroy(this.gameObject);
		}
	}
	
	private void createExplosionParticle() {
		Instantiate(explosionParticle, transform.position, transform.rotation);
	}

	private void fireBullet() {
		if (alternateShot) {				
			Instantiate(bullet, rightGun.position, rightGun.rotation);
			alternateShot = false;
		} else {				
			Instantiate(bullet, leftGun.position, leftGun.rotation);
			alternateShot = true;
		}
		laser.Play();
	}

    private void movePlayer() {		
		float xTranslation = Input.GetAxis("Horizontal") * playerSpeedXAxis * Time.deltaTime;        
		transform.Translate(xTranslation, 0, 0);
    }

    private void checkScreenBoundaries() {
        transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, (minScreenBounds.x) + 1, (maxScreenBounds.x) - 1),
            Mathf.Clamp(transform.position.y, (minScreenBounds.y) + 1, (maxScreenBounds.y) - 1), 
			transform.position.z);
    }
	
	private void obtainScreenBounds() {
		float cameraToPlayerDistance = Mathf.Abs(Camera.mainCamera.transform.position.z - transform.position.z);
		minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, cameraToPlayerDistance));
		maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraToPlayerDistance));
	}
	
	private void createShield() {
		if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)) {
			if (!shieldOn) {
				Transform clone;
				clone = Instantiate(shield, transform.position, transform.rotation) as Transform;
				clone.transform.parent = transform;
				shieldOn = true;
			}
		}
	}
	
	public void setShieldOn(bool shieldOn) {
		this.shieldOn = shieldOn;
	}
	
	private void colorFlash() {
		float lerp = Mathf.PingPong(Time.time, flashLerpDuration) / flashLerpDuration;
		renderer.material.color = Color.Lerp(flashColors[0], flashColors[1], lerp);
	}
	
	private void checkColorFlashInvoking() {
		flashInvokeDuration -= Time.deltaTime;
		if (flashInvokeDuration <= 0) {
			CancelInvoke("colorFlash");
			flashInvokeDuration = 0.5f;
			renderer.material.color = Color.blue;
		}
	}
}
