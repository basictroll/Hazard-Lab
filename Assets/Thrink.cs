﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrink : MonoBehaviour {

    public float thrinkRate = 3.0f;
    public int speedDecreaseRate = 5;
    public float thrinkMin = 5.0f;
    GameObject station;

    private void Start()
    {
        station = GameObject.Find("ThrinkStation(Clone)");

    }

    private void OnTriggerStay(Collider other)
    {
        if (station.GetComponent<StationStatus>().activated) {
            GameObject hitTarget = other.transform.root.gameObject;

            if (hitTarget.transform.localScale.x > thrinkMin)
            {
                // Change size
                hitTarget.transform.localScale = hitTarget.transform.localScale -
                                                 new Vector3(1f, 1f, 1f) * thrinkRate * Time.deltaTime;

                // Change position y
                hitTarget.transform.position = new Vector3(hitTarget.transform.position.x,
                                                           hitTarget.transform.localScale.y / 2,
                                                           hitTarget.transform.position.z);

                // Change speed
                float ratio = Mathf.Pow((1.0f - speedDecreaseRate * thrinkRate * Time.deltaTime / hitTarget.transform.position.x), speedDecreaseRate);
                if (hitTarget.name == "P1(Clone)")
                {
                    hitTarget.GetComponent<p1_movement>().speed = hitTarget.GetComponent<p1_movement>().speed * ratio;
                }
                else if (hitTarget.name == "P2(Clone)")
                {
                    hitTarget.GetComponent<p2_movement>().speed = hitTarget.GetComponent<p2_movement>().speed * ratio;
                }
                else if (hitTarget.tag == "monster")
                {
                    hitTarget.GetComponent<EnemyMovement>().forwardSpeed = hitTarget.GetComponent<EnemyMovement>().forwardSpeed * ratio;

                }
            }
        }

    }
}
