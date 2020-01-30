using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Makeey.UI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreen : MonoBehaviour
    {
        [SerializeField]
        private Selectable selectable;
        [SerializeField]
        private UnityEvent onShowScreenEvent;
        [SerializeField]
        private UnityEvent onCloseScreenEvent;
        private Animator animator;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            if (selectable)
            {
                EventSystem.current.SetSelectedGameObject(selectable.gameObject);
            }
        }

        public void ShowScreen()
        {
            onShowScreenEvent?.Invoke();
            HandleAnimation("show");
        }

        public void CloseScreen()
        {
            onCloseScreenEvent?.Invoke();
            HandleAnimation("close");
        }

        private void HandleAnimation(string trigger)
        {
            if (animator)
            {
                animator.SetTrigger(trigger);
            }
        }
    }
}
