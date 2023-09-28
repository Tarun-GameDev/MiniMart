using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed = 100f;
    Rigidbody rb;
    Collider col;
    [SerializeField] Joystick joyStick;


    [SerializeField] Animator animator;
    [SerializeField] float animatorSpeedMultiplier = 1f;
    [SerializeField] float xPos = -2f;
    [SerializeField] float sideMoveSpeed = 1f;



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

        audioManager = AudioManager.instance;

        animator.SetFloat("speed", animatorSpeedMultiplier);
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


}
