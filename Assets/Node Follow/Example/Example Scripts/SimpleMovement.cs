using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [Header("Offset BoxCollider2D of Node Follow to red trigger box")]

    [Header("Press Q to move the Player box left")]

    [Space(5)]

    [Header("Press E to move the Player box right")]

    [Space(10)]

    public GameObject player;

	void Update ()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            DoMovementLeft();
        }
        if (Input.GetKey(KeyCode.E))
        {
            DoMovementRight();
        }
    }

    void DoMovementLeft()
    {
        player.transform.Translate(Vector3.left * Time.deltaTime);
    }

    void DoMovementRight()
    {
        player.transform.Translate(Vector3.right * Time.deltaTime);
    }
}
