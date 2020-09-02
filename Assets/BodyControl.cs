using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyControl : MonoBehaviour{
    public GameObject frontLeg;
    public GameObject backLeg;
    public GameObject legPoint;

    public GameObject solverF;
    public GameObject solverB;

    public GameObject rayTargetF;
    public GameObject rayTargetB;

    public int inContact = 2;

    public float height = 2.3f;
    public float speed = 2.0f;
    public float easeDiv = 3.0f;
    public float reachRange = 1.2f;

    public float heightStepIncrease = 0;

    // Update is called once per frame
    void Update(){
        MoveLegTarget();
        CheckRange();
        MoveBody();
        AdjustVertically();
    }

    // move around target point for the leg
    void MoveLegTarget() {
        Vector2 pos = legPoint.transform.position;

        if (Input.GetKey("d")) {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a")) {
            pos.x -= speed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, transform.position.x - reachRange * 1.8f, transform.position.x + reachRange * 1.8f);

        legPoint.transform.position = pos;
    }

    // check if should make a step
    void CheckRange() {
        float distF = Vector2.Distance(legPoint.transform.position, frontLeg.transform.position);
        float distB = Vector2.Distance(legPoint.transform.position, backLeg.transform.position);

        // check if both legs are in contact with the ground
        if (inContact == 2) {
            // front leg is further away
            if (distF > distB) {
                float dist = Vector2.Distance(legPoint.transform.position, rayTargetB.transform.position);

                if (dist > reachRange) {
                    rayTargetF.transform.position = legPoint.transform.position;
                    solverF.GetComponent<MoveTarget>().inContact = false;
                    inContact -= 1;
                }
            }
            // back leg is further away
            else {
                float dist = Vector2.Distance(legPoint.transform.position, rayTargetF.transform.position);

                if (dist > reachRange) {
                    rayTargetB.transform.position = legPoint.transform.position;
                    solverB.GetComponent<MoveTarget>().inContact = false;
                    inContact -= 1;
                }
            }
        }
    }

    // moves body horizontally so it's in the middle of the three points
    void MoveBody() {
        Vector2 pos = transform.position;
        float targetX = (((
            rayTargetF.transform.position.x +
            rayTargetB.transform.position.x +
            legPoint.transform.position.x) 
            / 3) - transform.position.x) 
            / easeDiv;

        pos.x += targetX;
        transform.position = pos;
    }

    // adjust vertical position depending if one of the legs is higher
    void AdjustVertically() {
        Vector2 pos = transform.position;

        pos.y += ((((frontLeg.transform.position.y + backLeg.transform.position.y) / 2) + height + heightStepIncrease) - transform.position.y) / easeDiv;

        transform.position = pos;
    }
}
