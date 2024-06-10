using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

    }

    private void OnDrawGizmos()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.DrawRay(mousePosition, Vector2.zero);
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(mousePosition, Vector2.one);
    }

}