using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour{
    public GameObject controller;
    public Transform currentTarget;
    public Transform desiredTarget;

    float dist;
    public float speed = 6.8f;
    static float reachRange;
    public float footStartingX = 0;

    public bool inContact = true;
    float easeDiv;

    Vector2 targetInitialPos;
    float targetInitialPosX;

    public float xx = 0;
    public float yy = 0;

    private void Start() {
        easeDiv = controller.GetComponent<BodyControl>().easeDiv;
        reachRange = controller.GetComponent<BodyControl>().reachRange;
        targetInitialPosX = Mathf.Abs(currentTarget.transform.position.x);
    }

    private void Update() {
        SetTargetPosition();
        SetLegPosition();
        CheckContact();
    }

    // change position of the target if controller point is out of range
    void SetTargetPosition() {
        dist = Vector2.Distance(currentTarget.transform.position, desiredTarget.position);

        if (dist > reachRange) {
            currentTarget.position = desiredTarget.position;
            footStartingX = transform.position.x;
        }
    }

    // move the leg towards the point
    void SetLegPosition() {
        //xx = Mathf.Abs(transform.position.x - footStartingX);// (Mathf.Abs(transform.position.x) - Mathf.Abs(footStartingX));
        //xx = Mathf.Clamp(xx, 0, reachRange);
        //yy = Parabola(xx, -(25/36), 5/3, 0);

        //float targetX = currentTarget.position.x;
        //float targetY = currentTarget.position.y + yy;
        //Vector2 targetPointFinal = new Vector2(targetX, targetY);

        //transform.position = Vector2.MoveTowards(transform.position, targetPointFinal, speed * Time.deltaTime);

        xx = Mathf.Abs(transform.position.x - footStartingX);
        
        if (xx < reachRange * 0.5) {
            yy = reachRange * 1.5f;
        }
        else {
            yy = yy = (currentTarget.transform.position.y - transform.position.y) / easeDiv / 2;
        }

        float targetX = currentTarget.position.x;
        float targetY = currentTarget.position.y + yy;
        Vector2 targetPointFinal = new Vector2(targetX, targetY);

        float xChange = (currentTarget.transform.position.x - transform.position.x) / easeDiv / 2;
        float yChange = (currentTarget.transform.position.y - transform.position.y + yy) / easeDiv / 2;

        transform.position = Vector2.MoveTowards(transform.position, targetPointFinal, speed * Time.deltaTime);

        transform.position = new Vector2(transform.position.x + xChange, transform.position.y + yChange);

        // send height increase to the body
        if (inContact == false) {
            controller.GetComponent<BodyControl>().heightStepIncrease = yy / 2;
        }
    }

    // calculate parabola
    float Parabola(float x, float a, float b, float c) {
        float eq = a * x * x + b * x + c;
        return eq;
    }

    // check if leg is in the contact with the ground
    void CheckContact() {
        float targetDist = Vector2.Distance(transform.position, currentTarget.transform.position);

        if (targetDist < reachRange * 0.05 && inContact == false) {
            controller.GetComponent<BodyControl>().inContact += 1;
            inContact = true;
        }
    }
}
