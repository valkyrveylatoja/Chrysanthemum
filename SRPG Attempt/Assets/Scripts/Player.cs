using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f; // Speed of the player
    public Transform movePoint; // The object for movement reference (for the player)
    public LayerMask whatStopsMovement; // Layers where the player should stop moving (obstacles)
    public GameObject menuContent; // The parent object for the menu content
    public Tilemap unitTilemap; // Reference to the Tilemap for detecting unit tiles
    public LayerMask unitLayerMask; // LayerMask for detecting unit sprites
    public InputAction playerControls; // Input action for player movement
    public InputAction interactAction; // Input action for interacting with units
    public InputAction cancelAction; // Input action for closing the menu

    private bool isHoveringUnit = false; // To track if player is hovering over a unit

    void Start()
    {
        movePoint.parent = null; // Detach movePoint from the player for independent control
        if (menuContent != null)
        {
            menuContent.SetActive(false); // Ensure the menu is hidden initially
        }
    }

    private void OnEnable()
    {
        playerControls.Enable();
        interactAction.Enable();
        cancelAction.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        interactAction.Disable();
        cancelAction.Disable();
    }

    void Update()
    {
        // Check if the movePoint is hovering over a unit
        CheckHoveringUnit();

        // If the menu is active, handle cancellation
        if (menuContent.activeSelf)
        {
            if (cancelAction.triggered)
            {
                HideMenuContent();
            }
            return; // Skip movement logic while the menu is displayed
        }

        // Move the player toward movePoint
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            HandleMovement();
        }

        // Show menu only if the movePoint is over a valid unit tile
        if (interactAction.triggered && isHoveringUnit)
        {
            ShowMenuContent();
        }
    }

    // Check if the movePoint is over a unit tile or sprite using OverlapCircle
    void CheckHoveringUnit()
    {
        // Check for colliders in the unit layer using Physics2D.OverlapCircle
        Collider2D hit = Physics2D.OverlapCircle(movePoint.position, 0.2f, unitLayerMask); // Check a small radius around movePoint

        // Tilemap detection: Check if movePoint is over a valid tile in the unitTilemap
        Vector3Int cellPosition = unitTilemap.WorldToCell(movePoint.position);
        TileBase tile = unitTilemap.GetTile(cellPosition);

        // Set isHoveringUnit to true if there is a sprite or a valid tile
        isHoveringUnit = (hit != null || tile != null);
    }

    void HandleMovement()
    {
        Vector2 moveDirection = playerControls.ReadValue<Vector2>();

        if (Mathf.Abs(moveDirection.x) == 1f)
        {
            // Move horizontally if there’s no obstacle
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveDirection.x, 0f, 0f), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(moveDirection.x, 0f, 0f);
            }
        }

        if (Mathf.Abs(moveDirection.y) == 1f)
        {
            // Move vertically if there’s no obstacle
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveDirection.y, 0f), .2f, whatStopsMovement))
            {
                movePoint.position += new Vector3(0f, moveDirection.y, 0f);
            }
        }
    }

    // Show the menu content
    void ShowMenuContent()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(true); // Show the menu content
        }
    }

    // Hide the menu content
    void HideMenuContent()
    {
        if (menuContent != null)
        {
            menuContent.SetActive(false); // Hide the menu content
        }
    }

    // Optional: Visualize the detection radius in the Scene view for debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(movePoint.position, 0.2f); // Visualize the detection area
    }
}
