using UnityEngine;

public class SetHook : MonoBehaviour
{
    public DistanceJoint2D hookLine;
    public LineRenderer line;

    private bool _swinging;
    private Rigidbody2D _target;
    public void Update()
    {
        if (_swinging)
            line.SetPositions(new Vector3[]
            {
                transform.position,
                _target.position
            });
    }

    public void Hook(Rigidbody2D target, float distance)
    {
        hookLine.enabled = true;
        line.enabled = true;
        hookLine.connectedBody = target;
        hookLine.distance = distance;
        _swinging = true;
        _target = target;
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
