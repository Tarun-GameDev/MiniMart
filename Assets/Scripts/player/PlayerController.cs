using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 100f;
    Rigidbody rb;
    Collider col;
    [SerializeField] Joystick joyStick;
    [SerializeField] Transform pizzaBoxPivot;
    [SerializeField] int pizzasCollected = 1;
    [SerializeField] Rig characterRig;
    [SerializeField] Animator animator;
    [SerializeField] float animatorSpeedMultiplier = 1f;
    [SerializeField] float xPos = -2f;
    [SerializeField] float sideMoveSpeed = 1f;
    [SerializeField] GameObject pizzaBoxPrefab;
    [SerializeField] List<GameObject> pizzaBoxeArray = new List<GameObject>();


    GameObject delivaryPizzaPos;

    public bool pizzasDelivered = false;
    bool move = false;
    AudioManager audioManager;

    [SerializeField] ParticleSystem upgradeParticleEff;
    [SerializeField] ParticleSystem degradeParticleEff;
    [SerializeField] ParticleSystem powerUpPartcleEff;
    void Start()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody>();
        if(col == null)
            col = GetComponent<Collider>();


        if(characterRig != null)
            characterRig.weight = 1f;

        audioManager = AudioManager.instance;

        animator.SetFloat("speed", animatorSpeedMultiplier);

        pizzasCollected = 1;
        //LevelManager.instance.currentPizzasCollected = pizzasCollected;
    }

    private void FixedUpdate()
    {
        #region PcControllers

        //float _verti = Input.GetAxis("Vertical");
        //float _horz = Input.GetAxis("Horizontal");
        float _verti = joyStick.Vertical;
        float _horz = joyStick.Horizontal;
        #endregion

        #region MObileCOmtrollers

        #endregion

        Vector3 _movement = new Vector3(_horz, 0, _verti).normalized;


        if(_movement != Vector3.zero)
        {
            Quaternion _targerRot = Quaternion.LookRotation(_movement);
            _targerRot = Quaternion.RotateTowards(transform.rotation, _targerRot, 360 * Time.fixedDeltaTime);

            rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(_targerRot);
        }
        
    }



    #region powerUps
    public void IncreaseSpeed(float _multiplier)
    {
        moveSpeed += _multiplier;
        animatorSpeedMultiplier += .1f;
        animator.SetFloat("speed", animatorSpeedMultiplier);
        if (powerUpPartcleEff != null)
            powerUpPartcleEff.Play();
    }
    #endregion
}
