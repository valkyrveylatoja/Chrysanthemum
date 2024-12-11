using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance; // Singleton for easier access
    public GameObject menuContent; // Parent object that contains the menu UI
    public GameObject selectionArrow; // The arrow GameObject for selection
    public Transform[] menuOptions; // Array to hold the menu options (assigned in Inspector)
    public int selectedIndex = 0; // To track the selected index

    [Header("Input Actions")]
    public InputAction moveSelection; // Input action for navigating the menu
    public InputAction interactAction; // Input action for interacting with units (replacement for Z)

    [Header("Menu Settings")]
    public float inputDelay = 0.2f; // Delay between inputs during menu navigation
    private float nextInputTime = 0f; // Tracks when the next input is allowed

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
    }

    void Start()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(false); // Hide menu initially
        }

        UpdateArrowPosition(); // Set the arrow to the default position (top)
    }

    private void OnEnable()
    {
        moveSelection.Enable();
        interactAction.Enable();
    }

    private void OnDisable()
    {
        moveSelection.Disable();
        interactAction.Disable();
    }

    void Update()
    {
        // Handle menu input when the menu is active
        if (menuContent.activeSelf)
        {
            HandleArrowMovement();
        }

        // Handle interaction with units
        if (interactAction.triggered && !menuContent.activeSelf)
        {
            ShowMenuContent(); // Trigger the menu when interacting with a unit
        }
    }

    void HandleArrowMovement()
    {
        // Prevent rapid input by enforcing a delay
        if (Time.time < nextInputTime)
        {
            return;
        }

        // Read movement input
        Vector2 moveInput = moveSelection.ReadValue<Vector2>();

        // Move the selection up
        if (moveInput.y > 0.5f) // Detect upward input
        {
            selectedIndex = (selectedIndex - 1 + menuOptions.Length) % menuOptions.Length; // Wrap around the top
            UpdateArrowPosition();
            nextInputTime = Time.time + inputDelay; // Set the next allowed input time
        }

        // Move the selection down
        if (moveInput.y < -0.5f) // Detect downward input
        {
            selectedIndex = (selectedIndex + 1) % menuOptions.Length; // Wrap around the bottom
            UpdateArrowPosition();
            nextInputTime = Time.time + inputDelay; // Set the next allowed input time
        }
    }

    // Update the arrow's position based on the selected index with offset
    void UpdateArrowPosition()
    {
        if (selectionArrow != null && menuOptions.Length > 0)
        {
            // Define the offset (adjust x value to move the arrow to the right)
            Vector3 offset = new Vector3(1.5f, 0, 0); // Adjust this as needed for arrow placement

            // Update the arrow's position with the offset
            selectionArrow.transform.position = menuOptions[selectedIndex].position + offset;
        }
    }

    // Show the menu content
    public void ShowMenuContent()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(true); // Display the menu content
        }
    }

    // Hide the menu content
    public void HideMenuContent()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(false); // Hide the menu content
        }
    }
}
