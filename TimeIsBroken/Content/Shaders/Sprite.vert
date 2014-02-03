#version 330 core

layout(location = 0) in vec2 vertex;

uniform mat4 mMVP;

uniform vec2 Size;
uniform vec2 Offset;

out vec2 tc;

void main(){
	gl_Position = mMVP * vec4(vertex - vec2(0.5, 0.5), 0.0, 1.0);
	tc = Offset + min(vertex.xy * Size, 1.0);
}

