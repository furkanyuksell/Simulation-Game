    using System.Collections;
    using UnityEngine;
     
    public class AnimalMovement : MonoBehaviour
    {
        public float speed;
        public float randomX;
        public float randomY;
        public float minWaitTime;
        public float maxWaitTime;
        private Vector2 currentRandomPos;
        
        
        void Start()
        {
            PickPosition();
        }
        
        void PickPosition()
        {
            currentRandomPos = new Vector2(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY));
            StartCoroutine ( MoveToRandomPos());
        }
        
        IEnumerator MoveToRandomPos()
        {
            float i = 0.0f;
            float rate = 1.0f / speed;
            Vector2 currentPos = transform.position;
            
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                transform.position = Vector2.Lerp( currentPos, currentPos + currentRandomPos, i);
                yield return null;
            }
            
            float randomFloat = Random.Range(0.0f,1.0f); // Create %50 chance to wait
            if(randomFloat < 0.5f)
                StartCoroutine ( WaitForSomeTime());
            else
                PickPosition();
        }
            
        IEnumerator WaitForSomeTime()
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            PickPosition();
        }
    }
