//
// This file is part of Monobjc, a .NET/Objective-C bridge
// Copyright (C) 2007-2013 - Laurent Etiemble
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Monobjc.Tools.Generators;

namespace Monobjc.Foundation
{
	public partial class NSData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NSData"/> class.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		public NSData (byte[] bytes)
            : this(bytes, (NSUInteger) bytes.Length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NSData"/> class.
		/// </summary>
		/// <param name="bytes">The bytes.</param>
		/// <param name="length">The length.</param>
		public NSData (byte[] bytes, NSUInteger length)
            : this(ObjectiveCRuntime.SendMessage<IntPtr>(NSDataClass, "alloc"))
		{
			IntPtr pointer = Marshal.AllocHGlobal ((int)length);
			Marshal.Copy (bytes, 0, pointer, (int)length);
			
			if (ObjectiveCRuntime.Is64Bits) {
				this.NativePointer = ObjectiveCRuntime.SendMessage<IntPtr> (this, "initWithBytes:length:", pointer, (ulong)length);
			} else {
				this.NativePointer = ObjectiveCRuntime.SendMessage<IntPtr> (this, "initWithBytes:length:", pointer, (uint)length);
			}
			
			Marshal.FreeHGlobal (pointer);
		}

		/// <summary>
		/// Gets a managed byte buffer from this instance.
		/// </summary>
		/// <value>The byte buffer.</value>
		public byte[] GetBuffer ()
		{
			byte[] buffer = new byte[(int)this.Length];
			Marshal.Copy (this.Bytes, buffer, 0, (int)this.Length);
			return buffer;
		}

		/// <summary>
		/// Create a <see cref="NSData"/> from a stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns>A autoreleased <see cref="NSData"/> instance</returns>
		public static NSData DataFromStream (Stream stream)
		{
			NSData data = null;
			if (stream != null) {
				byte[] buffer = new byte[stream.Length];
				stream.Read (buffer, 0, (int)stream.Length);
				data = new NSData (buffer);
				data.Autorelease ();
			}
			return data;
		}

		/// <summary>
		/// Create a <see cref="NSData"/> from a manifest resource stream.
		/// </summary>
		/// <param name="type">The type whose assembly contains the manifest resource.</param>
		/// <param name="resourceName">Name of the resource.</param>
		/// <returns>A autoreleased <see cref="NSData"/> instance</returns>
		public static NSData DataFromResource (Type type, String resourceName)
		{
			Assembly assembly = type.Assembly;
			NSData data = null;
			using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
				if (stream != null) {
					data = DataFromStream (stream);
					stream.Close ();
				}
			}
			return data;
		}

		/// <summary>
		///   Returns a <see cref = "System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		///   A <see cref = "System.String" /> that represents this instance.
		/// </returns>
		public override string ToString ()
		{
			return String.Format (CultureInfo.CurrentCulture, "Length = {0:x}", this.Length);
		}

		/// <summary>
		/// Decrypts the given data by using the <see cref="ArtworkEncrypter"/> tool.
		/// </summary>
		/// <param name="encryptedData">The encrypted artwork data.</param>
		/// <param name="encryptionSeed">The encryption seed to use.</param>
		/// <returns>The decrypted artwork data.</returns>
		public static NSData DecryptArtworkData (NSData encryptedData, NSString encryptionSeed)
		{
			Aes aes = FileEncrypter.GetProvider (encryptionSeed);
			byte[] encryptedBytes = encryptedData.GetBuffer ();
			byte[] decryptedBytes = FileEncrypter.Decrypt (encryptedBytes, aes);
			NSData result = new NSData (decryptedBytes);
			return result.Autorelease<NSData> ();
		}
	}
}
