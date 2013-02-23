using UnityEngine;
using System.Collections;

public class Enemy : BaseNpcEntity {
	// Use this for initialization
	void Start() {
		speed = Random.Range(6, 15);
		obtainScreenBounds();
	}
	
	// Update is called once per frame
	void Update() {
		this.moveEntityDownward();
	}
	
	void OnTriggerEnter(Collider other) {
		base.checkForCollisions(other);
		base.resetRandomPosition();
	}
	
	protected override void moveEntityDownward() {
		base.moveEntityDownward();
		
        if (outOfScreenLowerBounds()) {
            base.resetRandomPosition();
			base.resetRandomSpeed();
        }
	}	
}
