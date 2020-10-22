using UnityEngine;
using WOB.Enemy.Type;

public class SetHook : MonoBehaviour
{
    public DistanceJoint2D hookLine;
    public LineRenderer line;

    private bool _swinging;
    private Vector3 _potentialPosition;
    private HookTarget _potentialHTarget;
    public void Update()
    {
        if (_potentialHTarget != null)
        {
            _potentialPosition = _potentialHTarget.hookPoint.position;
        }
        if (_swinging)
            line.SetPositions(new Vector3[]
            {
                transform.position,
                _potentialPosition
            });
    }

    public void Hook(Rigidbody2D target, float distance)
    {
        hookLine.enabled = true;
        line.enabled = true;
        hookLine.connectedBody = target;
        hookLine.distance = distance;
        _swinging = true;

        _potentialHTarget = target.GetComponent<HookTarget>();
        if (_potentialHTarget != null)
        {
            _potentialHTarget.Moving = true;
            hookLine.connectedAnchor = _potentialHTarget.hookPoint.localPosition;
        }
        else
        {
            hookLine.connectedAnchor = Vector2.zero;
            _potentialPosition = target.position;
        }
    }

    public void UnHook()
    {
        _swinging = false;
        hookLine.enabled = false;
        line.enabled = false;
    }

    public void SetDistance(float adjustment) =>
        hookLine.distance += adjustment;
}
