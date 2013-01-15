//
// This file is part of Monobjc, a .NET/Objective-C bridge
// Copyright (C) 2007-2012 - Laurent Etiemble
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Monobjc
{
	partial class ObjectiveCRuntime
	{
		/// <summary>
		///   Convert a native exception into a managed one.
		///   If a managed exception has been previously wrapped, then use it as inner exception.
		/// </summary>
		/// <param name = "data">The serialized inner exception.</param>
		internal static Exception UnWrap (byte[] data)
		{
			Logger.Debug ("ObjectiveCRuntime", "Unwrapping exception");

			Exception ex;

			// De-serialize the wrapped exception
			using (MemoryStream stream = new MemoryStream(data)) {
				BinaryFormatter formatter = new BinaryFormatter ();
				ex = (Exception)formatter.Deserialize (stream);
			}

			return ex;
		}

		/// <summary>
		///   Wraps the given exception.
		/// </summary>
		/// <param name = "exception">The exception.</param>
		public static byte[] Wrap (Exception exception)
		{
			byte[] data;

			// Serialize the exception using a binary format
			using (MemoryStream stream = new MemoryStream()) {
				BinaryFormatter formatter = new BinaryFormatter ();
				formatter.Serialize (stream, exception);
				data = stream.ToArray ();
			}

			return data;
		}
	}
}