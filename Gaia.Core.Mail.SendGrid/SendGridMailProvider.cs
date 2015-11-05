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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Gaia.Core.Mail.SendGrid.Configuration;
using SendGrid;
using SendGridWeb = SendGrid.Web;

namespace Gaia.Core.Mail.SendGrid
{
	public class SendGridMailProvider : IMailProvider
	{
		#region Fields and constants

		private readonly SendGridWeb _sendGridClient;

		#endregion

		#region Constructors

		public SendGridMailProvider(SendGridSettings sendGridSettings = null)
		{
			var settings = sendGridSettings ?? new Settings().SendGrid;

			if (settings == null) throw new SendGridSettingsException();

			_sendGridClient =
				new SendGridWeb(
					new NetworkCredential(settings.UserName, settings.Password));
		}

		#endregion

		#region Interface Implementations

		public async void Send(MailMessage message)
		{
			var sendGridMessage = new SendGridMessage
			{
				From = message.From,
				To = message.To.Select(a => a).ToArray(),
				Subject = message.Subject,
				Text = message.Body
			};

			var stream = message.AlternateViews[0].ContentStream;
			using (var reader = new StreamReader(stream))
			{
				sendGridMessage.Html = reader.ReadToEnd();
			}

			foreach (var att in message.Attachments)
			{
				sendGridMessage.AddAttachment(att.ContentStream, att.Name);
			}

			await _sendGridClient.DeliverAsync(sendGridMessage);
		}

		#endregion
	}
}