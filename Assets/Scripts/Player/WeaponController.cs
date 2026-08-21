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
    public GameObject systemWeaponContainer;

    private void Start(){
        _pInput = GetComponent<PlayerInput>();
        leftAttack = _pInput.actions["LeftShoot"];
        rightAttack = _pInput.actions["RightShoot"];
    }
    private void Update(){
        if(leftAttack.IsPressed()){
            var leftWeapon = leftWeaponContainer.GetComponentsInChildren<IShootable>();
            if(leftWeapon != null && leftWeapon.Length > 0){
                foreach(IShootable weapon in leftWeapon)
                {
                    weapon.Shot();
                    
                }
            }
            else
            {
                SystemShot(1);
            }
        }
        else{
            var leftWeapon = leftWeaponContainer.GetComponentsInChildren<IReleasable>();
            if(leftWeapon != null && leftWeapon.Length > 0){
                foreach(IReleasable weapon in leftWeapon)
                {
                    weapon.ShotReleased();
                    
                }
            }
            else
            {
                SystemShot(0);
            }
        }
        if(rightAttack.IsPressed()){
            var rightWeapon = rightWeaponContainer.GetComponentsInChildren<IShootable>();
            if (rightWeapon != null && rightWeapon.Length > 0){
                foreach(IShootable weapon in rightWeapon)
                {
                    weapon.Shot();
                    
                }
            }
            else
            {
                SystemShot(1);
            }
 
        }
        else{
            var rightWeapon = rightWeaponContainer.GetComponentsInChildren<IReleasable>();
            if (rightWeapon != null && rightWeapon.Length > 0){
                foreach(IReleasable weapon in rightWeapon)
                {
                    weapon.ShotReleased();
                    
                }
            }
            else
            {
                SystemShot(0);
            }
        }
    }
    public void SystemShot(int action){
        if(action == 1)
        {
            var systemWeapon = systemWeaponContainer.GetComponentsInChildren<IShootable>();
            if(systemWeapon == null)
            {
                Debug.LogWarning("No System Weapon Found");
                return;
            }
            foreach(IShootable weapon in systemWeapon)
            {
                weapon.Shot();
            }
            
        }
        if(action == 0)
        {
            var systemWeapon = systemWeaponContainer.GetComponentsInChildren<IReleasable>();
            if(systemWeapon == null)
            {
                Debug.LogWarning("No System Weapon Found");
                return;
            }
            foreach(IReleasable weapon in systemWeapon)
            {
                weapon.ShotReleased();
                return;
            }
            
        }

    }
}
