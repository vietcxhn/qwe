using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
    public class DownSlotButton :  MonoBehaviour , IPointerEnterHandler 
    {
        private bool defOK;

        private bool Def ;

        private bool Down ;
        private bool Up ;

        public event Action<bool , bool,bool> downSlotEvent;


        private void Awake() {
            var defenseButton = GameObject.FindGameObjectWithTag("DefenseUI").GetComponent<DefenseButton>();            
            defenseButton.ONdefenseEvent += DefenseEvent;
        }

        private void DefenseEvent(bool defEvent, bool UpEvent)
        {
            Def = defEvent;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {   
            if (Def) {
                Down = true;
                downSlotEvent?.Invoke(Def ,Down , Up);
            }  
            Def = false;
            Down = false;
        }

    }

}