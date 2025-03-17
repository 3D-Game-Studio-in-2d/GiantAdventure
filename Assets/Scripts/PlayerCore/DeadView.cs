using UnityEngine;

public class DeadView : MonoBehaviour
{
        [SerializeField] private GameObject deadUI;
        private Health _healthPlayer;
        
        public void Initialize(Health healthPlayer)
        {
                _healthPlayer = healthPlayer;
                _healthPlayer.OnDeath += Death;
        }

        private void Death()
        {
                Instantiate(deadUI, Vector3.zero, Quaternion.identity);
        }
}