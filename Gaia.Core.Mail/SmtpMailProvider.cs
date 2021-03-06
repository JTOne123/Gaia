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
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Gaia.Core.Mail.Configuration;
using Microsoft.Extensions.Logging;

namespace Gaia.Core.Mail
{
	/// <summary>
	///   SmtpMail provider sends mail message using standard SMTP protocol with configured SMTP server
	/// </summary>
	public class SmtpMailProvider : IMailProvider, IDisposable
	{
		#region Constructors

		/// <summary>
		///   Creates default instance of object
		/// </summary>
		public SmtpMailProvider(ILogger<SmtpMailProvider> logger, ISmtpClientSettings smtpClientSettings = null)
		{
			_logger = logger;
			_smtpClient = new SmtpClient();

			if (smtpClientSettings != null)
			{
				_smtpClient.Host = smtpClientSettings.Host;
				_smtpClient.Port = smtpClientSettings.Port;
				_smtpClient.EnableSsl = smtpClientSettings.EnableSsl;
				if (smtpClientSettings.Credentials != null
				    && !string.IsNullOrEmpty(smtpClientSettings.Credentials.UserName)
				    && !string.IsNullOrEmpty(smtpClientSettings.Credentials.Password)
				)
				{
					_smtpClient.Credentials = new NetworkCredential(smtpClientSettings.Credentials.UserName,
						smtpClientSettings.Credentials.Password);
				}
			}
		}

		#endregion Constructors

		#region Fields and constants

		private readonly ILogger _logger;
		private SmtpClient _smtpClient;

		#endregion Fields and constants

		#region Interface Implementations

		/// <summary>
		///   Default Dispose
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///   Send mail message
		/// </summary>
		/// <param name="message"></param>
		/// <param name="objectId"></param>
		/// <param name="sendTime"></param>
		public async Task SendAsync(MailMessage message, object objectId = null, DateTime? sendTime = null)
		{
			await _smtpClient.SendMailAsync(message);
			_logger.LogInformation($"Message to {message.To} has been sent");
		}

		#endregion Interface Implementations

		#region Private and protected

		/// <summary>
		///   Default object destructor
		/// </summary>
		~SmtpMailProvider()
		{
			Dispose(false);
		}

		/// <summary>
		///   Default dispose
		/// </summary>
		/// <param name="disposing"></param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_smtpClient != null)
				{
					_smtpClient.Dispose();
					_smtpClient = null;
				}
			}
		}

		#endregion Private and protected
	}
}