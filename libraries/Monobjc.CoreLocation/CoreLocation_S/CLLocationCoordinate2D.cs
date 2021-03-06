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
using System.Runtime.InteropServices;
using CLLocationDegrees = System.Double;

namespace Monobjc.CoreLocation
{
    /// <summary>
    /// <para>A structure that contains a geographical coordinate using the WGS 84 reference frame.</para>
    /// <para>Available in Mac OS X v10.6 and later.</para>
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CLLocationCoordinate2D
    {
        /// <summary>
        /// <para>The latitude in degrees. Positive values indicate latitudes north of the equator. Negative values indicate latitudes south of the equator.</para>
        /// <para>Available in Mac OS X v10.6 and later.</para>
        /// </summary>
        public CLLocationDegrees latitude;

        /// <summary>
        /// <para>The longitude in degrees. Measurements are relative to the zero meridian with positive values extending east of the meridian and negative values extending west of the meridian.</para>
        /// <para>Available in Mac OS X v10.6 and later.</para>
        /// </summary>
        public CLLocationDegrees longitude;
    }
}