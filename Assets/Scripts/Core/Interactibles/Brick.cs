using UnityEngine;
using Zenject;

namespace LeglessDriving
{
    public class Brick : MonoBehaviour, IInteractible
    {
        [Inject]
        private Player player;

        [SerializeField]
        private BasePedal pedal;

        public void Interact(bool hasBrick)
        {
            if (pedal != null)
            {
                pedal.InteractWithBrick(hasBrick);
            }
            else
            {
                player.PickupBrick();
            }
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
