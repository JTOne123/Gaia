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

namespace Gaia.Core.Mail.Configuration
{
	/// <summary>
	///   Email settings interface definion
	/// </summary>
	public interface IEmailSettings
	{
		#region Public members

		/// <summary>
		///   Email is enabled
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		///   In case of Test mode the testing recipient email address
		/// </summary>
		string TestRecipient { get; set; }

		/// <summary>
		///   Defines if emails settings uses testing mode. If true, emails are send to TestRecipient address instead of original
		///   recipient
		/// </summary>
		bool TestMode { get; set; }

		/// <summary>
		///   Specify if the email message is saved/archived to disc
		/// </summary>
		bool SaveCopy { get; set; }

		/// <summary>
		///   Location where to store local copy of messages can be physical or virtual
		/// </summary>
		string CopyLocation { get; set; }

		/// <summary>
		///   Location where to store local copy of messages can be physical or virtual real path
		/// </summary>
		string CopyLocationPath { get; }

		#endregion
	}
}