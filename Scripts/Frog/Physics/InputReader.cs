using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode AbilityKey = KeyCode.Q;
    private const KeyCode JumpKey = KeyCode.Space;
    private const string CommandHorizontal = "Horizontal";
    private const int LeftMouseButtonCode = 0;

    private bool _isJump = false;
    private bool _isAbilityPressed = false;
    private bool _isShooting = false;

    public float Direction {get; private set;} = 0f;

    private void Update()
    {
        Direction = Input.GetAxis(CommandHorizontal);

        if (Input.GetKeyDown(JumpKey))
            _isJump = true;

        if (Input.GetMouseButtonDown(LeftMouseButtonCode))
            _isShooting = true;

        if (Input.GetKeyDown(AbilityKey))
            _isAbilityPressed = true;
    }

    public bool IsJump()
    {
        return GetBoolAsTrigger(ref _isJump);
    }

    public bool IsAbilityPressed()
    {
        return GetBoolAsTrigger(ref _isAbilityPressed);
    }

    public bool IsShooting()
    {
        return GetBoolAsTrigger(ref _isShooting);
    }

    private bool GetBoolAsTrigger(ref bool isEnabled)
    {
        bool newIsEnabled = isEnabled;
        isEnabled = false;
        return newIsEnabled;
    }
}
