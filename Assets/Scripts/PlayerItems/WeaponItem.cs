using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnderDove
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
<<<<<<< HEAD

=======
        public bool isUnarmed;

        [Header("One Handed Attack Animations")]
        public string OH_light_Attack;
        public string OH_heavy_Attack;
>>>>>>> Vladimir
    }
}
