using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [HideInInspector] public PlayerInput _pInput;
    [HideInInspector] public InputAction leftAttack;
    [HideInInspector] public InputAction rightAttack;
    [SerializeField] GameObject leftWing;
    [SerializeField] GameObject rightWing;
    public GameObject leftWeaponContainer;
    public GameObject rightWeaponContainer;

    private void Start(){
        _pInput = GetComponent<PlayerInput>();
        leftAttack = _pInput.actions["LeftShoot"];
        rightAttack = _pInput.actions["RightShoot"];
    }
    private void Update(){
        if(leftAttack.IsPressed()){
            var leftWeapon = leftWeaponContainer.GetComponentInChildren<IShootable>();
            if(leftWeapon != null){
                leftWeapon.Shot();
            }
            else{
                ErrorShot();
            }
        }
        else{
            var leftWeapon = leftWeaponContainer.GetComponentInChildren<IReleasable>();
            if(leftWeapon != null){
                leftWeapon.ShotReleased();
            }
        }
        if(rightAttack.IsPressed()){
            var rightWeapon = rightWeaponContainer.GetComponentInChildren<IShootable>();
            if (rightWeapon != null){
                rightWeapon.Shot();
            }
            else{
                ErrorShot();
            }
        }
        else{
            var rightWeapon = rightWeaponContainer.GetComponentInChildren<IReleasable>();
            if (rightWeapon != null){
                rightWeapon.ShotReleased();
            }
        }
    }
    public void ErrorShot(){
        //Idk play a sound or something here?
        Debug.Log("No Weapon Found");
    }
}
