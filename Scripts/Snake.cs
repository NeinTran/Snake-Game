using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
  private Vector2 direction = Vector2.right;
  private List<Transform> segments = new List<Transform>();
  public Transform segmentsPrefab;
  public int initialSize = 4;

    private void Start() {
        ResetState();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate() {
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i-1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
        );
    }

    private void Grow() {
        Transform segment = Instantiate(this.segmentsPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState() {
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++) {
            segments.Add(Instantiate(this.segmentsPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Food") {
            Grow();
            FindObjectOfType<GameManager>().IncreaseScore();
        } else if (other.tag == "Obstacle") {
            FindObjectOfType<GameManager>().GameOver();
        } else if (other.tag == "Segment") {
            FindObjectOfType<GameManager>().GameOver();
        }
    }
}
