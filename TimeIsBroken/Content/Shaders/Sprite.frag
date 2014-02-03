#version 330 core

in vec2 tc;

out vec4 color;

uniform sampler2D Texture;
 
void main(){
	vec4 c = texture( Texture, tc );
	//c = vec4(c.b, c.g, c.r, c.a); // Fix the damn colors
	//c = vec4(c.a,c.a,c.a,c.a);
	if(c.a < 0.5) discard;
	color = c;
	//color = vec4(1);
}
