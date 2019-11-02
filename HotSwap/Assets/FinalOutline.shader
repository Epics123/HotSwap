Shader "Custom/FinalOutline"
{
	Properties
	{
		_MainTex("Main Texture (RBG)", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)

		_OutlineWidth("Outline Width", Range(1.0,10.0)) = 1.1

		_BlurRadius("Blur Radius", Range(0.0,20.0)) = 1
		_Intensity("Blur Intensity", Range(0.0,1.0)) = 0.01

		_DistortColor("Distort Color", Color) = (1,1,1,1)
		_BumpAmt("Distortion", Range(0,128)) = 128
		_DistortTex("Distort Texture (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
	}

		SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
		}

		GrabPass{}
		UsePass "Custom/OutlineDistort/OUTLINEDISTORT"
		GrabPass{}
		UsePass "Custom/OutlineBlur/OUTLINEHORIZONTALBLUR"
		GrabPass{}
		UsePass "Custom/OutlineBlur/OUTLINEVERTICALBLUR"

		UsePass "Custom/Outline/OBJECT"
	}
}
