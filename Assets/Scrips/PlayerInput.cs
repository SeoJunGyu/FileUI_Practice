using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static readonly string Vertical = "Vertical";
    public static readonly string Horizontal = "Horizontal";

    public float Ver { get; private set; }
    public float Hor { get; private set; }

    private void Update()
    {
        Ver = Input.GetAxis(Vertical);
        Hor = Input.GetAxis(Horizontal);
    }
}
