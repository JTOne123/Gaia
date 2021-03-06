﻿/*
The MIT License (MIT)

Copyright (c) 2012 Stepan Fryd (stepan.fryd@gmail.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/
using System;
using System.Runtime.Serialization;
using System.Security;

namespace Gaia.Core.Exceptions
{
	/// <summary>
	/// Gaia ecosystem base exception and all exceptions thrown by core libraries should inherit from it
	/// </summary>
	[Serializable]
	public class GaiaBaseException : ApplicationException
	{
		/// <summary>
		/// Create instatnce of Gaia base exception
		/// </summary>
		public GaiaBaseException() {}

		/// <summary>
		/// Create instatnce of Gaia base exception
		/// </summary>
		/// <param name="message"></param>
		public GaiaBaseException(string message) : base(message) {}

		/// <summary>
		/// Create instatnce of Gaia base exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="innerException"></param>
		public GaiaBaseException(string message, Exception innerException) : base(message, innerException) {}

		[SecuritySafeCritical]
#pragma warning disable 1591
		protected GaiaBaseException(SerializationInfo info, StreamingContext context) : base(info, context) {}
#pragma warning restore 1591
	}
}