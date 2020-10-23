// #define DIAGNOSTIC_MODE
using System.Collections;
using UnityEngine;
using WOB.Enemy.Type;
using WOB.Player;

public enum ButtonType
{
    None,
    Door,
    Activate
}
public class BigPressableButton : MonoBehaviour
{
    #region Serialized Fields
#pragma warning disable CS0649
    public Sprite normal;
    public Sprite depressed;
    public SpriteRenderer spriteRenderer;
    [Header("Door")]
    public LockedDoor door;
    [Header("One Complicated Puzzle")]
    public HookTarget zipline;
    public Transform blockingDoor;
#pragma warning restore CS0649
    #endregion

    public ButtonType buttonType;


    #region Monobehaviour
    private void Start()
    {
        spriteRenderer.sprite = normal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BasePlayer>() == null)
            return;
        switch (buttonType)
        {
            case ButtonType.Door:
                door.UnlockDoor();
                spriteRenderer.sprite = depressed;
                break;
            case ButtonType.Activate:
                zipline.partOfPuzzle = !zipline.partOfPuzzle;
                var dist = zipline.partOfPuzzle ? -2 : 2;
                blockingDoor.position = new Vector3(
                    blockingDoor.position.x,
                    blockingDoor.position.y + dist);
                spriteRenderer.sprite = depressed;
                StartCoroutine(WaitAndSwitch());
                break;
            default:
                break;
            
        }
    }
    #endregion

    private IEnumerator WaitAndSwitch()
    {
        yield return new WaitForSeconds(3f);
        spriteRenderer.sprite = normal;
    }

#if DIAGNOSTIC_MODE
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 25),$"");
    }
#endif
}
