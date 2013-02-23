using UnityEngine;
using System.Collections;

public class PlayerBullet : BaseNpcEntity {
	private SceneManager sceneManagerScript;
	

	// Use this for initialization
	void Start() {
		speed = 15.0f;
		obtainScreenBounds();
		sceneManagerScript = GameObject.Find("SceneManager").GetComponent("SceneManager") as SceneManager;
	}
	
	// Update is called once per frame
	void Update() {
        base.moveEntityUpward();
        // If the bullet leaves the screen, destroy it. 
        destroyOutOfBoundsBullet();
	}


    void OnTriggerEnter(Collider other) {        
        if (checkForCollisions(other)) {			
			increasePlayerScore();
			Destroy(this.gameObject);
		}
    }
	
	private bool checkForCollisions(Collider other) {
		return other.tag.Equals("asteroid") || other.tag.Equals("enemy");
	}
	
	private void increasePlayerScore() {
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
