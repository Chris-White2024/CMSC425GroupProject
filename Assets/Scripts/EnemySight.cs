using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{

    private float sightAngle;
    private EnemyController enemyController;
    private SphereCollider col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSight(float sightAngle, float sightRange, EnemyController controller) {
        this.sightAngle = sightAngle;
        enemyController = controller;
        col = GetComponent<SphereCollider>();
        col.radius = sightRange;
    }

    bool IsInSight(Collider other) {
        Vector3 normalizedHitPosition =
            new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
        Vector3 hitDirection = normalizedHitPosition - transform.position;
        float angle = Vector3.Angle(transform.forward, hitDirection);

        if (Mathf.Abs(angle) < sightAngle) {
            return true;
        } else {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        //TODO: if is player...
        if (IsInSight(other)) {
            enemyController.FollowPlayer(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        //TODO: if is player...
        if (IsInSight(other)) {
            enemyController.FollowPlayer(other.gameObject);
        }
    }
}
