using UnityEngine;
using System.Collections;

public class EnemyBullet : BaseNpcEntity {
	// Use this for initialization
	void Start() {
		speed = 15.0f;
		obtainScreenBounds();
	}
	
	// Update is called once per frame
	void Update() {
        base.moveEntityDownward();
        // If the bullet leaves the screen, destroy it. 
        destroyOutOfBoundsBullet();
	}


    void OnTriggerEnter(Collider other) {        
        if (checkForCollisions(other)) {
			Destroy(this.gameObject);
		}
    }
	
	private bool checkForCollisions(Collider other) {
		return other.tag.Equals("player") || other.tag.Equals("asteroid");
	}

    private void destroyOutOfBoundsBullet() {
        if (transform.position.y >= maxScreenBounds.y + 3) {
            Destroy(this.gameObject);
        }
    }
}
