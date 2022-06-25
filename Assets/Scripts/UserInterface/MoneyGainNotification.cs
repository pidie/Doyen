using System.Collections;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class MoneyGainNotification : MonoBehaviour
    {
        [SerializeField] private float timeUntilDestroy;
        [SerializeField] private float timeToStayOpaque;
        [SerializeField] private TMP_Text notificationText;
        [SerializeField] private float moveSpeed;
        [Range(1.001f, 1.8f)]
        [SerializeField] private float acceleration;

        private float FadeTime => timeUntilDestroy - timeToStayOpaque;
        
        private void Awake() => StartCoroutine(WaitToDestroy());

        private void Update()
        {
            transform.position += new Vector3(0, -moveSpeed * Time.deltaTime);
            moveSpeed *= acceleration;
        }

        private IEnumerator WaitToDestroy()
        {
            yield return new WaitForSeconds(timeToStayOpaque);

            StartCoroutine(FadeText());
            
            yield return new WaitForSeconds(FadeTime);
            Destroy(gameObject);
        }

        private IEnumerator FadeText()
        {
            notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, 1);
            while (notificationText.color.a > 0.0f)
            {
                notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, notificationText.color.a - (Time.deltaTime / FadeTime));
                yield return null;
            }
        }
    }
}
