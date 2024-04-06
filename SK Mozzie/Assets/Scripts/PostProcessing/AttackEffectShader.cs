using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectShader : MonoBehaviour
{

    public Shader shader = null;
    private Material m_renderMaterial; 

    void Start()
    {
        if(shader != null)
        {
            Debug.Log("Shader Missing");
            m_renderMaterial = null;
            return;
        }

        m_renderMaterial = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_renderMaterial);
    }
}
