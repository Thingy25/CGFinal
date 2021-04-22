#ifndef MATH_LIBRARY
#define MATH_LIBRARY

inline float Fresnel(float3 viewDir, float3 normal, float intensity, float fade)
{
    float border = intensity * pow(1 - saturate(dot(viewDir, normal)), fade);
    return border;
}

inline float InverseFresnel(float3 viewDir, float3 normal, float intensity, float fade)
{
    float border = intensity * (1 - pow(1 - saturate(dot(viewDir, normal)), fade));
    return border;
}

float2 RandomValueMapped2IN(float2 value)
{
    half3 output = frac(value.xyx * half3(123.34, 234.34, 345.65));
    output += dot(output, output + 34.45);
    return frac(half2(output.x * output.y, output.y * output.z));
}

float2 UvsToPolarCoords(float2 _uvs)
{
    half2 uvs = _uvs - 0.5;
    float2 polarUVs = half2(atan2(uvs.x, uvs.y) / 6.2831 + 0.5, distance(_uvs, half2(0.5, 0.5))); //Map cartesian coordinates to polar coordinates
    return polarUVs;
}

float2 UvRotator(float2 uvs, float uvScaling, float amount)
{
    float2 uvsRotated;
    
    float2x2 rotationMatrix;
    float sinTheta;
    float cosTheta;

    sinTheta = sin(amount);
    cosTheta = cos(amount);
    rotationMatrix = float2x2(cosTheta, -sinTheta, sinTheta, cosTheta);
    uvsRotated = (mul((uvs - 0.5) / uvScaling * (1 / half2(1, 1)), rotationMatrix)) + 0.5;
    
    return uvsRotated;
}

float2 SpritesheetUV(float2 uvs, half columns, half rows, half currentFrame)
{
    // Get single sprite size
    float2 size = float2(1.0f / columns, 1.0f / rows);
    uint totalFrames = columns * rows;

    // Select which frame is being selected
    uint index = currentFrame;

    // Wrap x and y indexes
    uint indexX = index % columns;
    uint indexY = floor((index % totalFrames) / columns);

    // Get offsets to our sprite index
    float2 offset = float2(size.x * indexX, -size.y * indexY);

    // Get single sprite UV
    float2 newUV = uvs * size;

    // Flip Y (to start 0 from top)
    newUV.y = newUV.y + size.y * (rows - 1);

    return newUV + offset;
}
#endif
