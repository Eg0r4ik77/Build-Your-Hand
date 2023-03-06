using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VictimFlasher
{
    public static IEnumerator Flash(List<Renderer> renderers, Material flashMaterial, float flashTimeInSeconds)
    {
        var materials = new List<Material>();

        foreach (var meshRenderer in renderers)
        {
            materials.Add(meshRenderer.material);
            meshRenderer.material = flashMaterial;
        }
            
        yield return new WaitForSeconds(flashTimeInSeconds);
            
        for (int i = 0; i < renderers.Count; i++)
            renderers[i].material = materials[i];
    }
}