using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [HideInInspector] public PlayerInput _pInput;
    [HideInInspector] public InputAction leftAttack;
    [HideInInspector] public InputAction rightAttack;
    public GameObject leftWeaponContainer;
    public GameObject rightWeaponContainer;

    private void Start(){
        _pInput = GetComponent<PlayerInput>();
        leftAttack = _pInput.actions["LeftShoot"];
        rightAttack = _pInput.actions["RightShoot"];
    }
    private void Update(){
        if(leftAttack.IsPressed()){
            var leftWeapon = leftWeaponContainer.GetComponentsInChildren<IShootable>();
            if(leftWeapon != null){
                foreach(IShootable weapon in leftWeapon)
                {
                    //leftWeapon.Shot();
                    weapon.Shot();
                }
                
            }
            else{
                ErrorShot();
            }
        }
        else{
            var leftWeapon = leftWeaponContainer.GetComponentsInChildren<IReleasable>();
            if(leftWeapon != null){
                foreach(IReleasable weapon in leftWeapon)
                {
                    weapon.ShotReleased();
                }
                //leftWeapon.ShotReleased();
            }
        }
        if(rightAttack.IsPressed()){
            var rightWeapon = rightWeaponContainer.GetComponentsInChildren<IShootable>();
            if (rightWeapon != null){
                foreach(IShootable weapon in rightWeapon)
                {
                    weapon.Shot();
                }
               // rightWeapon.Shot();
            }
            else{
                ErrorShot();
            }
        }
        else{
            var rightWeapon = rightWeaponContainer.GetComponentsInChildren<IReleasable>();
            if (rightWeapon != null){
                foreach(IReleasable weapon in rightWeapon)
                {
                    weapon.ShotReleased();
                }
                //rightWeapon.ShotReleased();
            }
        }
    }
    public void ErrorShot(){
        //Idk play a sound or something here?
        Debug.Log("No Weapon Found");
    }
}
