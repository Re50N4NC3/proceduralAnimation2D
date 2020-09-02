using UnityEngine;

public class RaycastTarget : MonoBehaviour {
    float desiredYPosition;
    public Transform desiredTarget;

    void Update() {
        RaycastCollider();
        desiredTarget.position = new Vector2(desiredTarget.position.x, desiredYPosition);
    }

    // create raycast poiting downwards
    void RaycastCollider() {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(desiredTarget.position.x, transform.position.y + 5), -Vector2.up, 12f);

        if (hit.collider != null) {
            desiredYPosition = hit.point.y;
        }
        else {
            desiredYPosition = transform.position.y;
        }
    }
}
