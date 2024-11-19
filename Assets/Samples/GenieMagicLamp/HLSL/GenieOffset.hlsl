float3 Wave(in float time)
{
  float x= lerp(0.3,-.3,(sin(time * 3)+1)*0.5);
  float y= lerp(-0.1,0.1,(sin(time * 4)+1)*0.5);
  float z= lerp(0.3,-0.3,(sin(time * 2)+1)*0.5);

  float3 wave = float3(x, y, z);
  return wave;
}