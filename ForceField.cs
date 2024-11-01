using System.Collections;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [Header("Force Field Settings")]
    public float activeDuration = 2f;  // Duration the shield stays active
    public string shieldToggleButton = "Shield";  // Input button name for toggling shield
    public Material activeMaterial;  // Material when shield is active
    public Material inactiveMaterial;  // Material when shield is inactive
    public Color activeColor = Color.cyan;  // Color when active
    public Color inactiveColor = Color.gray;  // Color when inactive

    private Collider shieldCollider;  // Reference to the collider of the shield
    private Renderer shieldRenderer;  // Renderer to control appearance
    private bool isActive = false;  // Track if the shield is active

    void Start()
    {
        shieldCollider = GetComponent<Collider>();
        shieldRenderer = GetComponent<Renderer>();

        shieldCollider.enabled = false;
        UpdateAppearance(false);  // Set initial inactive appearance
    }

    void Update()
    {
        // Toggle shield on button press
        if (Input.GetButtonDown(shieldToggleButton))
        {
            if (isActive)
                DeactivateShield();
            else
                ActivateShield();
        }
    }

    void ActivateShield()
    {
        isActive = true;
        shieldCollider.enabled = true;
        UpdateAppearance(true);  // Show active appearance
        StartCoroutine(ShieldTimer());
    }

    void DeactivateShield()
    {
        isActive = false;
        shieldCollider.enabled = false;
        UpdateAppearance(false);  // Show inactive appearance
    }

    IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(activeDuration);
        if (isActive)
            DeactivateShield();
    }

    void UpdateAppearance(bool active)
    {
        if (shieldRenderer != null)
        {
            shieldRenderer.material = active ? activeMaterial : inactiveMaterial;
            shieldRenderer.material.color = active ? activeColor : inactiveColor;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Block specific objects from passing through the shield
        if (isActive)
        {
            if (other.CompareTag("Asteroid") || other.CompareTag("AsteroidShooter") || other.CompareTag("BoltEnemy1"))
            {
                // Destroy or deflect the incoming object
                Destroy(other.gameObject);  // or apply custom effect like deflection
                Debug.Log($"{other.name} blocked by ForceField.");
            }
        }
    }
}
