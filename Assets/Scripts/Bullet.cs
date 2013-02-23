using UnityEngine;
using System.Collections;

public class Bullet : BaseNpcEntity {
    public GameObject sceneManager;

	// Use this for initialization
	void Start() {
		speed = 15.0f;
		obtainScreenBounds();
	}
	
	// Update is called once per frame
	void Update() {
        base.moveEntityUpward();
        // If the bullet leaves the screen, destroy it. 
        destroyOutOfBoundsBullet();
	}


    void OnTriggerEnter(Collider other) {        
        if (other.gameObject.tag.Equals("asteroid") || other.gameObject.tag.Equals("enemy")) {			
			increasePlayerScore();
			Destroy(this.gameObject);
		}
    }
	
	private void increasePlayerScore() {
		SceneManager sceneManagerScript = sceneManager.transform.GetComponent<SceneManager>();
		if (sceneManagerScript) {
			sceneManagerScript.AddScore();
		}
	}

    private void destroyOutOfBoundsBullet() {
        if (transform.position.y >= maxScreenBounds.y + 3) {
            Destroy(this.gameObject);
        }
    }
}
