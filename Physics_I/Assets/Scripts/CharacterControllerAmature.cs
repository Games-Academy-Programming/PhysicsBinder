using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AABBNS
{
    public class CharacterControllerAmature : MonoBehaviour
    {
        [field: SerializeField]
        public MyCharacterController Controller {get; private set;}
        
        [field: SerializeField] 
        public MyInputModule InputModule { get; private set; }
    }
}