using UnityEngine;
using System.Collections;

public class BaseNpcEntity : MonoBehaviour {
	public Transform explosionParticle;
	
    protected float speed = 6.0f;
	protected float lowerScaleRandomRange = 1.0f;
	protected float upperScaleRandomRange = 3.0f;
	
	protected Vector3 minScreenBounds;
	protected Vector3 maxScreenBounds;
	
	virtual protected void checkForCollisions(Collider other) {
		if (other.tag.Equals("bullet") || other.tag.Equals("player") || other.tag.Equals("shield")) {
			if (explosionParticle) {
				createExplosionParticle();
				audio.Play();
			}
		}
	}

	virtual protected void createExplosionParticle() {
		Instantiate(explosionParticle, transform.position, transform.rotation);
	}
	
    virtual protected void moveEntityDownward() {
		transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
	
	virtual protected void moveEntityUpward() {
		transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
	
	virtual protected void moveEntityLeftward() {
		transform.Translate(Vector3.left * speed * Time.deltaTime);
	}
	
	virtual protected void moveEntityRightward() {
		transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
	
	virtual protected void resetRandomSpeed() {
		speed = Random.Range(6, 15);
	}
	
	virtual protected void resetRandomScale() {
		float scale  = Random.Range(lowerScaleRandomRange, upperScaleRandomRange);
		transform.localScale = new Vector3(scale, scale, 1);
	}
	
    virtual protected void resetRandomPosition() {
        float randomX = Random.Range(minScreenBounds.x - 2, maxScreenBounds.x);
        transform.position = new Vector3(randomX, maxScreenBounds.y + 3, 0);
    }

    virtual protected bool outOfScreenLowerBounds() {
        return transform.position.y <= minScreenBounds.y - 3;
    }
	
	protected void obtainScreenBounds () {
		float cameraToEntityDistance = Mathf.Abs(Camera.mainCamera.transform.position.z - transform.position.z);
		minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, cameraToEntityDistance));
		maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraToEntityDistance));
	}
}
