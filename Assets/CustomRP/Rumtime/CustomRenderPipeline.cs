using System.Collections;
using System.Collections.Generic;
using CustomRP.Rumtime;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (var camera in cameras)
        {
            m_render.Render(context, camera);
        }
    }

    CameraRender m_render = new CameraRender();
}