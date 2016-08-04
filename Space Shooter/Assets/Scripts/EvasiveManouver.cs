using UnityEngine;
using System.Collections;

public class EvasiveManouver : MonoBehaviour {

    public Boundary boundary;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 manouverTime;
    public Vector2 manouverWait;

    private float targetManouver;
    private float currentSpeed;
    private Rigidbody rigidBody;

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentSpeed = rigidBody.velocity.z;
        StartCoroutine(Evade());
	}
	
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x,startWait.y));

        while (true)
        {
            targetManouver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manouverTime.x,manouverTime.y));
            targetManouver = 0;
            yield return new WaitForSeconds(Random.Range(manouverWait.x,manouverWait.y));
        }
    }
	void FixedUpdate ()
    {
        float newManouver = Mathf.MoveTowards(rigidBody.velocity.x,targetManouver,Time.deltaTime * smoothing);
        rigidBody.velocity = new Vector3(newManouver, 0.0f, currentSpeed);
        rigidBody.position = new Vector3
        (
            Mathf.Clamp(rigidBody.position.x,boundary.xMin,boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z,boundary.zMin,boundary.zMax)
        );
        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * tilt);
	}
}
