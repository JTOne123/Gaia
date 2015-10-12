﻿/*
 Code copy from https://github.com/dimaKudr/Unity.WF
 */

namespace Gaia.Core.Workflows.Unity
{
	public enum InjectionTypes
	{
		/// <summary>
		///   Activity should pull its dependencies from workflow extension.
		/// </summary>
		Pull,

		/// <summary>
		///   Unity container will push dependencies into activity's properties
		///   marked with [Dependency] attribute.
		/// </summary>
		Push
	}
}