using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0.5f; // Duração do tremor
    private float shakeMagnitude = 1f; // Intensidade do tremor
    private float dampingSpeed = 1.0f; // Velocidade de atenuação do tremor
    private Vector3 initialPosition; // Posição inicial da câmera

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration)
        {
            // Gera um deslocamento aleatório baseado na magnitude do tremor
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;

            // Aplica o deslocamento à posição da câmera
            transform.localPosition = initialPosition + randomOffset;

            // Incrementa o tempo decorrido
            elapsedTime += Time.deltaTime;

            // Atenua gradualmente a intensidade do tremor
            shakeMagnitude = Mathf.Lerp(shakeMagnitude, 0f, elapsedTime / shakeDuration);

            yield return null;
        }

        // Restaura a posição inicial da câmera
        transform.localPosition = initialPosition;
    }
}
