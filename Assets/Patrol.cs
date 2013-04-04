//Justin White (jrw152)
using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	GameObject[] gos;
	GameObject closest;
	public int speed = 2;
	public int sightRange=200;
	bool up;
	// Use this for initialization
	void Start () {
	up=true;
	}
	
	// Update is called once per frame
	void Update () {
		 Vector3 pos = transform.position;
    	 pos.z = 0;
     	transform.position = pos;
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
		}
		else{
		if (up==true && transform.localPosition.x>=92 && transform.localPosition.x<=93){
			MoveUp();
			if (transform.localPosition.y>7.3){
					up=false;
				}
			}
			if (up==false && transform.localPosition.x>=92 && transform.localPosition.x<=93){
			MoveDown();
			if (transform.localPosition.y<-32){
					up=true;
				}
			}
			if (!(transform.localPosition.x>=92 && transform.localPosition.x<=93)){
			if (transform.localPosition.x<92){
				MoveRight();
				}
			if (transform.localPosition.x>93){
				MoveLeft();
				}	
			}
			
		}
	}
	
	
	void OnTriggerEnter(Collider other) {
        if (other.tag==("player_shank")){
		other.SendMessage("DeadPlayer");
		}
    }
	
	void MoveUp() {
		Vector3 speed = new Vector3(0, 4f, 0);
	   rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
	}
	
	void MoveDown() {
		Vector3 temp = new Vector3(0,-1f,0);
		this.transform.position += temp;
	}
	
	void MoveRight() {
		Vector3 speed = new Vector3(4f, 0, 0);
	   rigidbody.MovePosition(rigidbody.position + speed * Time.deltaTime);
	}
	
	void MoveLeft() {
		Vector3 temp = new Vector3(-1f,0,0);
		this.transform.position += temp;
	}
}
