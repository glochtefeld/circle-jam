using UnityEngine;

public class CheckHookable : MonoBehaviour
{
    public LayerMask hookLayer;
    public bool Hooked { private set; get; } = false;
    public GameObject @object { private set; get; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Hookable>()
            != null)
        {
            Hooked = true;
            @object = collision.gameObject;
        }
    }
}
