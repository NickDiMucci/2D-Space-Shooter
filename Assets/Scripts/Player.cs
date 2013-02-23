using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Transform bullet;
    public Transform rightGun;
	public Transform leftGun;
	public Transform explosionParticle;
	public Transform shield;
	public Transform jet;
	
	public AudioClip hitClip;
	public AudioClip laserClip;
	
	private AudioSource hit;
	private AudioSource laser;
	
	private Vector3 minScreenBounds;
	private Vector3 maxScreenBounds;
	
	public short lives = 3;
	
	public int shieldEnergyLevel = 4;
	
	private bool shieldOn = false;
	private bool alternateShot = true;
	
    public float playerSpeedXAxis = 10.0f;
    public float playerSpeedYAxis = 10.0f;
	

	// Use this for initialization
	void Start () {
		laser = addAudio(laserClip, false, false, 128.0f);
		hit = addAudio(hitClip, false, false, 128.0f);
		obtainScreenBounds();
	}
	
#if UNITY_ANDROID
	void OnGUI() {
		if (GUI.RepeatButton(new Rect(50, Screen.height - 50, 80, 50), "Left")) {
			transform.Translate(Vector3.left * playerSpeedXAxis * Time.deltaTime);
		}
		
		if (GUI.RepeatButton(new Rect(180, Screen.height - 50, 80, 50), "Right")) {
			transform.Translate(Vector3.right * playerSpeedXAxis * Time.deltaTime);
		}
		if (GUI.Button(new Rect(Screen.width - 80, Screen.height - 50, 50, 50), "Fire")) {
			fireBullet();
		}
	}
#endif
	
	// Update is called once per frame
	void Update() {		
        movePlayer();
        checkScreenBoundaries();
#if UNITY_ANDROID
		// TODO: Gotta be a better way to ignore non-android specific code. 
#else
		if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)) {
        	fireBullet();
		}
#endif
		createShield();
		checkPlayerLives();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("asteroid")) {
//			lives--;
			SceneManager sceneManager = GameObject.Find("SceneManager").GetComponent("SceneManager") as SceneManager;
			sceneManager.SetLives(lives);
			hit.Play();
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
#if UNITY_ANDROID
//		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
//            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//            transform.Translate(-touchDeltaPosition.x * playerSpeedXAxis, -touchDeltaPosition.y * playerSpeedYAxis, 0);
//        }
#else
		float xTranslation = Input.GetAxis("Horizontal") * playerSpeedXAxis * Time.deltaTime;        
		transform.Translate(xTranslation, 0, 0);
#endif

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
}
