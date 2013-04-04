using UnityEngine;
using System.Collections;

public class GuardAI : MonoBehaviour {
	GameObject[] gos;
	GameObject closest;
	public int speed = 2;
	public int sightRange=200;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        gos = GameObject.FindGameObjectsWithTag("player_shank");
        
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
		if (distance<sightRange){
		Transform playerTransform = closest.transform;
		// get player position
		Vector3 v1 = playerTransform.position;
         transform.position = Vector3.MoveTowards(transform.position, v1, speed*Time.deltaTime);
			
			
		//Check if in same spot as any player
		
		}
	}
	
	void OnTriggerEnter(Collider other) {
        if (other.tag==("player_shank")){
		other.SendMessage("Injured",gameObject.tag);
		}
    }
}
