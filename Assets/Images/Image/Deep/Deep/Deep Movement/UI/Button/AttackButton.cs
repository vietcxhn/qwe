using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using LF2;
namespace UnityEngine.InputSystem.OnScreen
{
    public class AttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDropHandler
    {

        static readonly Vector3 k_DownScale = new Vector3(0.95f, 0.95f, 0.95f);

        public event Action<TypeSkills> classAttackComboEvent;

        public event Action<StateType> AttackAction;        
        private bool Up;
        private bool Def;
        private bool Down;


        private void Awake() {
            var slotUpButton = GameObject.FindGameObjectWithTag("UpSlotUI").GetComponent<UpSlotButton>();            
            var slotDownButton = GameObject.FindGameObjectWithTag("DownSlotUI").GetComponent<DownSlotButton>();       
            
            var defenseButton = GameObject.FindGameObjectWithTag("DefenseUI").GetComponent<DefenseButton>();            

            slotUpButton.upSlotEvent += SetSlotEvent;
            slotDownButton.downSlotEvent += SetSlotEvent;
            defenseButton.ResetComboEvent += SetSlotEvent;

        }

        private void SetSlotEvent(bool defEvent ,bool downEvent , bool upEvent)
        {
            Down = downEvent;
            Def = defEvent;
            Up = upEvent;
        }



        public void OnPointerUp(PointerEventData eventData)
        {
            // SendValueToControl(0.0f);
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
            AttackAction?.Invoke(StateType.Attack);
            
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (Up && Def) 
            {
                AttackAction?.Invoke(StateType.DUA);
                Def = false;
                Up = false;
            }

            else if (Down && Def) 
            {
                AttackAction?.Invoke(StateType.DDA);
                Def = false;
                Down = false;
            }
        }


    }

}
