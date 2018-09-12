using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool MouseLeftClick;
    public bool EscapeKey;
	public bool SpaceKey;
	public bool NumberOneKey;
	public bool NumberTwoKey;
	public bool NumberThreeKey;
	public bool NumberFourKey;

    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		MouseLeftClick = Input.GetButtonDown("Fire1");
		EscapeKey = Input.GetButtonDown("Cancel");
		SpaceKey = Input.GetButton("Jump");
		NumberOneKey = Input.GetKeyDown(KeyCode.Alpha1);
        NumberTwoKey = Input.GetKeyDown(KeyCode.Alpha2);
        NumberThreeKey = Input.GetKeyDown(KeyCode.Alpha3);
        NumberFourKey = Input.GetKeyDown(KeyCode.Alpha4);
    }
}
