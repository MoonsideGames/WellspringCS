/* WellspringCS - C# bindings for the Wellspring font rendering Library
 *
 * Copyright (c) 2022 Evan Hemsley
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Evan "cosmonaut" Hemsley <evan@moonside.games>
 *
 */

using System;
using System.Runtime.InteropServices;

namespace WellspringCS
{
	public static class Wellspring
	{
		private const string nativeLibName = "Wellspring";

		// Version

		public const uint WELLSPRING_MAJOR_VERSION = 1;
		public const uint WELLSPRING_MINOR_VERSION = 0;
		public const uint WELLSPRING_PATCH_VERSION = 0;

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern uint Wellspring_LinkedVersion();

		[StructLayout(LayoutKind.Sequential)]
		public struct FontRange
		{
			public uint FirstCodepoint;
			public uint NumChars;
			public byte OversampleH;
			public byte OversampleV;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Color
		{
			public byte R;
			public byte G;
			public byte B;
			public byte A;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Vertex
		{
			public float X;
			public float Y;
			public float Z;
			public float U;
			public float V;
			public byte R;
			public byte G;
			public byte B;
			public byte A;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Rectangle
		{
			public float X;
			public float Y;
			public float W;
			public float H;
		}

		public enum HorizontalAlignment
		{
			Left,
			Center,
			Right
		}

		public enum VerticalAlignment
		{
			Baseline,
			Top,
			Middle,
			Bottom
		}

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Wellspring_CreateFont(
			IntPtr fontBytes,
			uint fontBytesLength,
			IntPtr atlasJsonBytes,
			uint atlasJsonBytesLength,
			out float pixelsPerEm,
			out float distanceRange
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Wellspring_CreateTextBatch();

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Wellspring_StartTextBatch(
			IntPtr textBatch,
			IntPtr font
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte Wellspring_TextBounds(
			IntPtr font,
			int pixelSize,
			HorizontalAlignment horizontalAlignment,
			VerticalAlignment verticalAlignment,
			IntPtr strBytes, /* UTF-8 bytes */
			uint strLengthInBytes,
			out Rectangle rectangle
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern byte Wellspring_AddToTextBatch(
			IntPtr textBatch,
			int pixelSize,
			in Color color,
			HorizontalAlignment horizontalAlignment,
			VerticalAlignment verticalAlignment,
			IntPtr strBytes, /* UTF-8 bytes */
			uint strLengthInBytes
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Wellspring_GetBufferData(
			IntPtr textBatch,
			out uint vertexCount,
			out IntPtr vertexDataPointer,
			out uint vertexDataLengthInBytes,
			out IntPtr indexDataPointer,
			out uint indexDataLengthInBytes
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Wellspring_DestroyTextBatch(
			IntPtr textBatch
		);

		[DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Wellspring_DestroyFont(
			IntPtr font
		);
	}
}
