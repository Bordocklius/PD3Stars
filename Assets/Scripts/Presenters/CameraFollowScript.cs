using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [Header("Follow Settings")]
    [SerializeField]
    private float _smoothTime = 0.12f;
    [SerializeField]
    private bool _lookAtTarget = true;
    [SerializeField]
    private Vector3 _offset = new Vector3(0f, 5f, -7f);

    private Vector3 _velocity = Vector3.zero;

    // Public read-only access to the current follow target
    public Transform Target => _target;

    // Set the follow target at runtime. When set, the offset will be preserved
    // unless you pass useDefaultOffset = true.
    public void SetTarget(Transform target, bool useDefaultOffset = false)
    {
        _target = target;
        if (_target != null && !useDefaultOffset)
        {
            _offset = transform.position - _target.position;
        }
    }

    // Clear the follow target so the camera stops following
    public void ClearTarget() => _target = null;

    // LateUpdate is preferred for camera movement so it runs after all character movement
    private void LateUpdate()
    {
        if (_target == null)
            return;

        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothTime);

        if (_lookAtTarget)
        {
            Vector3 lookPos = _target.position;
            lookPos.y = transform.position.y; // keep camera level if desired
            Quaternion targetRotation = Quaternion.LookRotation(lookPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}