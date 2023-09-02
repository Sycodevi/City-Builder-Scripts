using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AICarController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float brakeDistance = 2f;
    public LayerMask obstacleLayer;
    public float maxTurnDelay = 3f;
    public float turnAngle = 90f;
    public float obstacleCheckRadius = 5f;
    public float maxNavMeshAngle = 45f;
    public float rotationSpeed = 10f;
    public float startDelay = 1f; // Delay before the car starts moving

    private NavMeshAgent agent;
    private Vector3 destination;
    private bool isWaiting;
    private bool isTurning;
    private bool isAvoidingCollision;
    private float originalMaxSpeed;
    private Transform carToAvoid;
    private float avoidDistance = 5f;
    private float currentSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.angularSpeed = 360f;
        originalMaxSpeed = maxSpeed;
        currentSpeed = maxSpeed;
        StartCoroutine(StartMoving());
    }

    private IEnumerator StartMoving()
    {
        isWaiting = true;
        yield return new WaitForSeconds(startDelay);
        isWaiting = false;
        FindRandomDestination();
    }

    private void OnEnable()
    {
        NavMesh.onPreUpdate += ResetPath;
    }

    private void OnDisable()
    {
        NavMesh.onPreUpdate -= ResetPath;
    }

    private void Update()
    {
        if (!isWaiting)
        {
            if (agent.isOnNavMesh && agent.remainingDistance <= brakeDistance)
            {
                if (agent.hasPath)
                {
                    if (isTurning)
                    {
                        isTurning = false;
                        StartCoroutine(TurnAround());
                    }
                    else if (!isAvoidingCollision)
                    {
                        FindRandomDestination();
                    }
                }
            }
            else
            {
                agent.speed = maxSpeed;
            }
        }
        else
        {
            agent.speed = 0f;
        }

        if (isTurning)
        {
            Vector3 targetDir = destination - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        if (isAvoidingCollision)
        {
            AvoidCollision();
        }
    }

    private IEnumerator TurnAround()
    {
        yield return new WaitForSeconds(Random.Range(0f, maxTurnDelay));
        transform.rotation *= Quaternion.Euler(0f, turnAngle, 0f);
        FindRandomDestination();
    }

    private void FindRandomDestination()
    {
        NavMeshHit hit;
        Vector3 randomPoint = Random.insideUnitSphere * 100f;
        randomPoint += transform.position;
        if (NavMesh.SamplePosition(randomPoint, out hit, 100f, NavMesh.AllAreas))
        {
            destination = hit.position;
            SetDestination(destination);
        }
        else
        {
            Debug.LogError("Failed to find a valid random destination on the NavMesh.");
            StartCoroutine(RetryFindingDestination());
        }
    }

    private IEnumerator RetryFindingDestination()
    {
        yield return new WaitForSeconds(1f);
        FindRandomDestination();
    }

    private void SetDestination(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path))
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetPath(path);
                isTurning = false;
            }
            else if (!isTurning)
            {
                StartCoroutine(TurnAround());
            }
        }
        else
        {
            Debug.LogError("Failed to find a valid path to the destination.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == obstacleLayer)
        {
            agent.speed = 0f;
            StartCoroutine(WaitForObstacleToClear());
        }
    }

    private IEnumerator WaitForObstacleToClear()
    {
        yield return new WaitUntil(() => agent.isOnNavMesh && agent.remainingDistance <= brakeDistance);
        agent.speed = maxSpeed;
        FindRandomDestination();
    }

    private void ResetPath()
    {
        if (agent.enabled && agent.pathPending && agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            agent.ResetPath();
        }
    }

    private void AvoidCollision()
    {
        if (carToAvoid != null)
        {
            Vector3 direction = carToAvoid.position - transform.position;
            Vector3 avoidPoint = transform.position - direction.normalized * avoidDistance;
            SetDestination(avoidPoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (!isAvoidingCollision)
            {
                isAvoidingCollision = true;
                carToAvoid = other.transform;
                maxSpeed /= 2f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && other.transform == carToAvoid)
        {
            isAvoidingCollision = false;
            carToAvoid = null;
            maxSpeed = originalMaxSpeed;
            FindRandomDestination();
        }
    }
}
