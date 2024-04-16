using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * NOTE: End position is to the RIGHT
 */

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float speed;
    [SerializeField] private float rotateTime = 3f;
    [SerializeField] private float fovAngle = 60;
    [SerializeField] private float fovRange = 10;
    [SerializeField] private float playerPositionUpdateDelay = 0.5f;
    [SerializeField] private float maxDistanceFromPlayer = 10;

    private Vector3 startPosition;
    private bool isFollowingPlayer = false;
    private bool isRotating = false;
    private NavMeshAgent agent;
    private EnemySight enemySight;

    void InitializeAgent() {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.enabled = false;
    }

    void InitializeSight() {
        enemySight = GetComponentInChildren<EnemySight>();
        enemySight.InitializeSight(fovAngle, fovRange, this);
    }
    
    void Start()
    {
        InitializeAgent();
        InitializeSight();
        startPosition = transform.position;
        Debug.DrawRay(startPosition, Vector3.up, Color.red, 100);
        Debug.DrawRay(endPosition, Vector3.up, Color.blue, 100);

        StartCoroutine(InitialRotation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private float GetAngleFromForward(Vector3 target) {
        Vector3 normalizedTargetPosition = new Vector3(target.x, transform.position.y, target.z);
        Vector3 targetDirection = normalizedTargetPosition - transform.position;
        float angleToEndPosition = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);

        return angleToEndPosition;
    }

    private IEnumerator RotateRight(float degrees) {
        if (isRotating) {
            yield break;
        }

        isRotating = true;
        Quaternion originalRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.AngleAxis(degrees, new Vector3(0, 1, 0)) * originalRotation;
        float elapsedTime = 0f;

        while (elapsedTime < rotateTime) {
            float t = elapsedTime / rotateTime;
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame(); // Wait for the next frame
        }

        // Ensure the final rotation is exactly what we want
        transform.rotation = targetRotation;
        isRotating = false;
    }

    private IEnumerator RotateLeft(float degrees) {
        if (isRotating) {
            yield break;
        }

        isRotating = true;
        Quaternion originalRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.AngleAxis(-degrees, new Vector3(0, 1, 0)) * originalRotation;
        float elapsedTime = 0f;

        while (elapsedTime < rotateTime) {
            float t = elapsedTime / rotateTime;
            transform.rotation = Quaternion.Slerp(originalRotation, targetRotation, t);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame(); // Wait for the next frame
        }

        // Ensure the final rotation is exactly what we want
        transform.rotation = targetRotation;
        isRotating = false;
    }

    private IEnumerator InitialRotation() {
        float angleToEnd = GetAngleFromForward(endPosition);
        if (angleToEnd > 0) {
            StartCoroutine(RotateRight(Mathf.Abs(angleToEnd)));
        } else {
            StartCoroutine(RotateLeft(Mathf.Abs(angleToEnd)));
        }
        StartCoroutine(RotateRight(90));
        while (isRotating) {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(MoveToEndPosition());
    }

    private IEnumerator MoveToEndPosition() {
        while (Vector3.Distance(transform.position, endPosition) > Vector3.kEpsilon) {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(RotateRight(180));
        while (isRotating) {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(MoveToStartPosition());
    }

    private IEnumerator MoveToStartPosition() {
        while (Vector3.Distance(transform.position, startPosition) > Vector3.kEpsilon) {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(RotateLeft(180));
        while (isRotating) {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(MoveToEndPosition());
    }

    public void FollowPlayer(GameObject player) {
        if (isFollowingPlayer) {
            return;
        }
        isFollowingPlayer = true;
        StopAllCoroutines();
        StartCoroutine(TrackPlayer(player));

    }

    private IEnumerator TrackPlayer(GameObject player) {
        // Repeat while not captured and not too far
        agent.enabled = true;
        while (Vector3.Distance(player.transform.position, transform.position) <= maxDistanceFromPlayer) {
            agent.SetDestination(player.transform.position);
            yield return new WaitForSeconds(playerPositionUpdateDelay);
        }
        // Return to start position
        isFollowingPlayer = false;
        agent.SetDestination(startPosition);
        // Wait until gaurd has returned to start position
        while (agent.remainingDistance > 0) {
            yield return new WaitForFixedUpdate();
        }
        agent.enabled = false;
        StartCoroutine(InitialRotation());
    }
}
