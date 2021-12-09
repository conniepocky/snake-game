using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeController : MonoBehaviour
{

    public float speed;
    public float angular_speed;

    public SnakeController parent;
    public SnakeController child;

    public List<Vector3> positions;
    public int positionsLength;

    public UnityEvent onCreateChild;
    public UnityEvent onGameOver;

    public Vector2 planeSize;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();

	for (int i = 0; i < positionsLength; i++) {
		positions.Add(transform.position);
	}
    }

    // Update is called once per frame
    void Update()
    {
	positions.RemoveAt(0);
	positions.Add(transform.position);

	if (parent == null) {
		transform.position += transform.forward * speed * Time.fixedDeltaTime;
		float inp_horizontal = Input.GetAxis("Horizontal");
		Vector3 axis = transform.up;
		float angle = inp_horizontal * angular_speed * Time.fixedDeltaTime;
		transform.Rotate(axis, angle);
	} else {
		transform.position = parent.positions[0];
		transform.LookAt(parent.transform);	
	}
    }

    private void OnTriggerEnter(Collider other) {
	if (other.CompareTag("apple")) {
		if (parent == null) {
			Eat(other.transform);
			CreateChild();
		} else {
			Eat(other.transform);
		}
	} else if (other.CompareTag("player")) {
		SnakeController otherPart = other.GetComponent<SnakeController>();

		if (otherPart != child) {
			onGameOver.Invoke();
		}
	} else if (other.CompareTag("respawn")) {
		onGameOver.Invoke();
	}     
    }

    private void CreateChild() {
	if (child == null) {
		GameObject temp = Instantiate(gameObject, Vector3.up * 3, Quaternion.identity);
		child = temp.GetComponent<SnakeController>();
        	child.parent = this;
	} else {
		child.CreateChild();
	}

	onCreateChild.Invoke();
    }

    private void Eat(Transform food) {
	Debug.Log("eat");
	food.position = new Vector3(planeSize.x * (Random.value * 2 - 1), 2.3f, planeSize.y * (Random.value * 2 - 1));
    }
}
