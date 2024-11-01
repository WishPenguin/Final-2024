using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Missile : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] public GameObject _target;

    [Header("MOVEMENT")]
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _rotateSpeed = 95f;

    [Header("PREDICTION")]
    [SerializeField] private float _maxDistancePredict = 100f;
    [SerializeField] private float _minDistancePredict = 5f;
    [SerializeField] private float _maxTimePrediction = 5f;
    private Vector3 _standardPrediction, _deviatedPrediction;

    [Header("DEVIATION")]
    [SerializeField] private float _deviationAmount = 50f;
    [SerializeField] private float _deviationSpeed = 2f;

    private Rigidbody _targetRb;

    private void Start()
    {
        // Check if the current scene is "_Scenes 1" and disable the missile if true
        if (SceneManager.GetActiveScene().name == "_Scenes 1")
        {
            gameObject.SetActive(false);
            return;
        }

        if (_target != null)
        {
            _targetRb = _target.GetComponent<Rigidbody>();
            if (_targetRb == null)
            {
                Debug.LogWarning("Target does not have a Rigidbody component.");
            }
        }
        else
        {
            Debug.LogError("Missile has no target assigned at the start.");
        }
    }

    private void FixedUpdate()
    {
        if (_target == null) return;

        float leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target.transform.position));
        PredictMovement(leadTimePercentage);
        AddDeviation(leadTimePercentage);

        _rb.velocity = transform.forward * _speed;
        RotateRocket();
    }

    private void PredictMovement(float leadTimePercentage)
    {
        if (_targetRb == null) return;

        float predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
        _standardPrediction = _targetRb.position + _targetRb.velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage)
    {
        if (_target == null) return;

        Vector3 deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, Mathf.Sin(Time.time * _deviationSpeed));
        Vector3 predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;
        _deviatedPrediction = _standardPrediction + predictionOffset;
    }

    private void RotateRocket()
    {
        if (_target == null) return;

        Vector3 heading = _deviatedPrediction - transform.position;
        Quaternion rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (_target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _standardPrediction);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
        }
    }
}
