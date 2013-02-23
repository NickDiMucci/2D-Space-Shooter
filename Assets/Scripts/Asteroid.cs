using UnityEngine;
using System.Collections;

public class Asteroid : BaseNpcEntity {
	// Use this for initialization
	void Start() {
		speed = Random.Range(6, 15);
		lowerScaleRandomRange = 0.5f;
		upperScaleRandomRange = 2.25f;
		obtainScreenBounds();
	}
	
	// Update is called once per frame
	void Update() {
		this.moveEntityDownward();
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
	
	protected override void moveEntityDownward() {
        base.moveEntityDownward();
        
        if (outOfScreenLowerBounds()) {
            base.resetRandomPosition();
			base.resetRandomSpeed();
			base.resetRandomScale();
        }
	}
	
	private bool checkForCollisions(Collider other) {
		return other.tag.Equals("playerbullet") || 
			other.tag.Equals("enemybullet") ||
			other.tag.Equals("player") || 
			other.tag.Equals("shield") ||
			other.tag.Equals("enemy") ||
			other.tag.Equals("asteroid");
	}
}
