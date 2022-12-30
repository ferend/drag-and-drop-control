Shader "Custom/CardBorder"
{
 Properties
  {
    // [MainTexture] allow Material.mainTexture to use
    // the correct properties.
    [MainTexture] _BaseMap("Image (RGB)", 2D) = "white" {}
    
    _FrameTex("Frame (RGBA)", 2D) = "white" {}
    _FrameColor("Frame Color", Color) = (0, 0, 0, 1)
  }

  SubShader
  {
    Tags { "RenderType" = "Opaque" "Queue" = "Geometry" "RenderPipeline" = "UniversalPipeline" }

    Pass
    {
      HLSLPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
      #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"

      struct MeshData
      {
        float4 vertex : POSITION;
        float2 uv     : TEXCOORD0; // Material texture UVs.
      };

      struct Varyings
      {
        float4 position : SV_POSITION;
        float2 uv       : TEXCOORD0; // Material texture UVs.
      };

      // This is automatically set by Unity.
      // Used in TRANSFORM_TEX to apply UV tiling.
      float4 _BaseMap_ST;

      // Defines the Frame texture, sampler and color.
      // _BaseMap is already defined in SurfaceInput.hlsl.
      TEXTURE2D(_FrameTex);
      SAMPLER(sampler_FrameTex);
      float4 _FrameColor;
      
      Varyings vert(MeshData input)
      {
        Varyings output = (Varyings)0;

        const VertexPositionInputs positionInputs = GetVertexPositionInputs(input.vertex.xyz);
        output.position = positionInputs.positionCS;
        output.uv = TRANSFORM_TEX(input.uv, _BaseMap);

        return output;
      }

      half4 frag(Varyings input) : SV_Target
      {
        // Sample the textures.
        // const half4 image = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);
        // const half4 frame = SAMPLE_TEXTURE2D(_FrameTex, sampler_FrameTex, input.uv) * _FrameColor;

        // Interpolates between image and frame according
        // to the transparency of the frame.
        // half4 pixel = lerp(image, frame, frame.a);

        // And the result is interpolated with the frame
        // transparency.
        // pixel = lerp(pixel, frame, frame.a);

        return _FrameColor;

        // return pixel;
      }
      ENDHLSL
    }
  }
}
