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

    private void Start(){
        _pInput = GetComponent<PlayerInput>();
        leftAttack = _pInput.actions["LeftShoot"];
        rightAttack = _pInput.actions["RightShoot"];
    }
    private void Update(){
        if(leftAttack.IsPressed()){
            var leftWeapon = leftWing.GetComponentInChildren<IShootable>();
            if(leftWeapon != null){
                leftWeapon.Shot();
            }
            else{
                ErrorShot();
            }
        }
        else{
            var leftWeapon = leftWing.GetComponentInChildren<IReleasable>();
            if(leftWeapon != null){
                leftWeapon.ShotReleased();
            }
        }
        if(rightAttack.IsPressed()){
            var rightWeapon = rightWing.GetComponentInChildren<IShootable>();
            if (rightWeapon != null){
                rightWeapon.Shot();
            }
            else{
                ErrorShot();
            }
        }
        else{
            var rightWeapon = rightWing.GetComponentInChildren<IReleasable>();
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
