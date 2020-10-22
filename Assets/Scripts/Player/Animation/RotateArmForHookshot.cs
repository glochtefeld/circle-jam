using UnityEngine;

public class RotateArmForHookshot : MonoBehaviour
{    
    public static Quaternion Angle { private set; get; }
    private void Update()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var armPosition = transform.position;
        var angle = FindAngle(armPosition, mouse);
        transform.rotation 
            = Angle 
            = Quaternion.Euler(new Vector3(0f, 0f, angle + 180));

    }

    private float FindAngle(Vector2 a, Vector2 b)
        => Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;


}
/* Attaches directly to the arm prefab of the player. */