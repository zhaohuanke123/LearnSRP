using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP.Rumtime
{
    public class CameraRender
    {
        public void Render(ScriptableRenderContext context, Camera cameras)
        {
            m_context = context;
            m_camera = cameras;

            if (!Cull())
            {
                return;
            }

            SetUp();
            DrawVisibleGeometry();
            Submit();
        }

        private void SetUp()
        {
            m_context.SetupCameraProperties(m_camera);
            m_buffer.ClearRenderTarget(true, true, Color.clear);
            m_buffer.BeginSample(BufferName);
            ExecuteBuffer();
        }

        private void DrawVisibleGeometry()
        {
            var sortingSettings = new SortingSettings(m_camera);
            var drawingSettings = new DrawingSettings(
                unlitShaderTagId, sortingSettings
            );
            var filteringSettings = new FilteringSettings(RenderQueueRange.all);

            m_context.DrawRenderers(m_cullingResults, ref drawingSettings, ref filteringSettings);

            m_context.DrawSkybox(m_camera);
        }

        private void Submit()
        {
            m_buffer.EndSample(BufferName);
            ExecuteBuffer();
            m_context.Submit();
        }

        private void ExecuteBuffer()
        {
            m_context.ExecuteCommandBuffer(m_buffer);
            m_buffer.Clear();
        }

        private bool Cull()
        {
            if (m_camera.TryGetCullingParameters(out ScriptableCullingParameters p))
            {
                m_cullingResults = m_context.Cull(ref p);
                return true;
            }

            return false;
        }

        private ScriptableRenderContext m_context;
        private Camera m_camera;

        private const string BufferName = "Render Camera";
        private static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

        private CommandBuffer m_buffer = new CommandBuffer()
        {
            name = BufferName
        };

        CullingResults m_cullingResults;
    }
}