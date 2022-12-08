using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnderDove
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot RightHandSlot;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }

                else if(weaponSlot.isRightHandSlot)
                {
                    RightHandSlot= weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
            }
            else
            {
                RightHandSlot.LoadWeaponModel(weaponItem);
            }
        }
    }
}