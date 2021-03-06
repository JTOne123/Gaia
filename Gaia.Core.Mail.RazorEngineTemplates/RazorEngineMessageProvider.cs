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

using Gaia.Core.Mail.Configuration;
using RazorEngine.Templating;
using System;
using System.Threading.Tasks;

namespace Gaia.Core.Mail.RazorEngineTemplates
{
	public class RazorEngineMessageProvider : IMessageProvider
	{
		#region Private members
		private readonly IRazorEngineService _engineService;
		private readonly IEmailTemplateConfiguration _emailTemplateConfiguration;
		#endregion

		#region Constructor

		public RazorEngineMessageProvider(IEmailTemplateConfiguration templateConfiguration)
		{
			_emailTemplateConfiguration = templateConfiguration ?? throw new ArgumentNullException(nameof(templateConfiguration));
			_engineService = RazorEngineService.Create(new RazorEngineEmailTemplateConfiguration(_emailTemplateConfiguration));
		}
		#endregion

		#region  Public members
		public async Task<string> GetCompiledMessageAsync<T>(string templateKey, string messageTemplate, T model)
		{
			var res = _engineService.IsTemplateCached(templateKey, typeof(T))
				? _engineService.Run(templateKey, typeof(T), model)
				: _engineService.RunCompile(messageTemplate, templateKey, typeof(T), model);

			return await Task.FromResult(res);
		}
		#endregion
	}
}
