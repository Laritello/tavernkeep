﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tavernkeep.Application.UseCases.Glossary.Query.GetAncestries;
using Tavernkeep.Application.UseCases.Glossary.Query.GetClasses;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Server.Controllers
{
	/// <summary>
	///  The <see cref="GlossaryController"/> class that returns glossary data.
	/// </summary>
	/// <param name="mediator">The <see cref="IMediator"/> instance.</param>
	[ApiController]
	[Route("api/[controller]")]
	public class GlossaryController(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Get all classes.
		/// </summary>
		/// <returns><see cref="List{T}"/> of all available classes.</returns>
		[HttpGet("classes")]
		public async Task<List<ClassTemplate>> GetClassesAsync()
		{
			var classes = await mediator.Send(new GetClassesQuery());
			return classes;
		}

		/// <summary>
		/// Get all ancestries.
		/// </summary>
		/// <returns><see cref="List{T}"/> of all available ancestries.</returns>
		[HttpGet("ancestries")]
		public async Task<List<AncestryTemplate>> GetAncestriesAsync()
		{
			var ancestries = await mediator.Send(new GetAncestriesQuery());
			return ancestries;
		}
	}
}
