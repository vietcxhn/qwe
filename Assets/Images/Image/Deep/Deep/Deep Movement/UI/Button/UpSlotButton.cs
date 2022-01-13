using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
    public class UpSlotButton : MonoBehaviour,IPointerEnterHandler
    {
        public event Action<bool,bool,bool> upSlotEvent;
        
        private bool Def;
        private bool Up;
        private bool Down;
        private void Awake() {
            var defenseButton = GameObject.FindGameObjectWithTag("DefenseUI").GetComponent<DefenseButton>();            
            // DefenseButton defenseButton = GetComponent<DefenseButton>();
            // Debug.Log(defenseButton);
            defenseButton.ONdefenseEvent += DefenseEvent;
        }

        private void DefenseEvent(bool defEvent, bool UpEvent)
        {
            Def = defEvent;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {   
            if (Def) {
                Up = true;
                upSlotEvent?.Invoke(Def ,Down , Up);
            }  
            Def = false;
            Up = false;
        }
        

    }

}