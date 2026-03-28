using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CombatInputHandler : MonoBehaviour
{
    [Header("Input")]
    private StarterAssetsInputs starterAssetsInputType;
    private PlayerInput playerInput;
    private InputAction attackInput;

    [Header("Aiming")]
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform BulletSpawnPoint;

    [Header("Handlers/Manager")]
    private GunManager gunManager;
    private MeleeManager meleeManager;
    private Camera mainCamera;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        starterAssetsInputType = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        attackInput = playerInput.actions["MeleeAttack"];

        gunManager = GetComponent<GunManager>();
        meleeManager = GetComponent<MeleeManager>();

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (starterAssetsInputType == null) return;

        

        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPositon = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenterPositon);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            if (debugTransform != null) debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }


        if (starterAssetsInputType.aim)
        {
            if (gunManager.CheckWeaponExists())
            {
                meleeManager.HideWeapon();
                gunManager.SetAiming(true, mouseWorldPosition);

                if (starterAssetsInputType.attack)
                {
                    Debug.Log("lol");
                    Vector3 spawnPosition = BulletSpawnPoint.position;
                    Vector3 AimDir = (mouseWorldPosition - spawnPosition).normalized;
                    gunManager.PullTrigger(spawnPosition, AimDir);

                }
            }          
    
        }
        //Not aiming stuff
        else
        {
            gunManager.SetAiming(false, mouseWorldPosition);
            //Melee stuff
            if (starterAssetsInputType.sprint)
            {
                meleeManager.HideWeapon();
            }
            else if (attackInput.triggered) //Basic check to see if mosue button was pressed for attack
            {
                Debug.Log("Melee Attack Triggered");
                meleeManager.PerformAttack();
            }
        }
    }
}