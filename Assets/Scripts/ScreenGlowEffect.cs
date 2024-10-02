using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenGlowEffect : MonoBehaviour
{
    public Image glowPanel;  // Referência ao painel de glow (deve ser uma Image)
    private float glowDuration = 0.5f; // Tempo que o glow fica ativo
    public Color glowColor = new Color(0f, 1f, 0f, 0.5f); // Cor verde com transparência
    private Color originalColor;

    void Start()
    {
        // Armazena a cor original do painel
        originalColor = glowPanel.color;
        // Certifica-se de que o painel comece invisível
        glowPanel.color = new Color(glowColor.r, glowColor.g, glowColor.b, 0f);
    }

    void Update()
    {
        // Verifica se o botão é pressionado
        // if (Input.GetKeyDown(KeyCode.Space))  // Troque "Space" pelo botão desejado
        // {
        //     StartCoroutine(ActivateGlow());
        // }
    }

    public void StartGlow() {
        StartCoroutine(ActivateGlow());
    }

    IEnumerator ActivateGlow()
    {
        // Ativa o glow gradualmente
        glowPanel.color = glowColor;

        // Aguarda o tempo de duração do glow
        yield return new WaitForSeconds(glowDuration);

        // Gradualmente desativa o glow
        glowPanel.color = new Color(glowColor.r, glowColor.g, glowColor.b, 0f);
    }
}
