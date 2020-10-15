using UnityEngine;
using WOB.Enemy.Type;

namespace WOB.Enemy
{
    [RequireComponent(typeof(IEnemy))]
    public class BaseEnemy : MonoBehaviour
    {

        private IEnemy _type;
        
        // Start is called before the first frame update
        void Start()
        {
            _type = GetComponent<IEnemy>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _type.Move();
        }
    }
}