using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Services
{
	/// <summary>
	/// Deletes the expired refresh tokens from the database.
	/// </summary>
	/// <param name="logger">The logger instance.</param>
	/// <param name="tokenRepository">The <see cref="IRefreshTokenRepository"/> implementation.</param>
	public class RefreshTokenService(ILogger<RefreshTokenService> logger, IServiceProvider serviceProvider) : BackgroundService
	{
		private readonly TimeSpan delay = TimeSpan.FromSeconds(30);

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				// TODO: How to gracefully stop background services?
				await Task.Delay(delay, cancellationToken);
				await PurgeExpiredTokensAsync();
			}
		}

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("Refresh Token Hosted Service is launching.");
			await base.StartAsync(cancellationToken);
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("Timed Hosted Service is stopping.");
			await base.StopAsync(cancellationToken);
		}

		/// <summary>
		/// Collects all expired refresh tokens and deletes them from the database.
		/// </summary>
		private async Task PurgeExpiredTokensAsync()
		{
			using var scope = serviceProvider.CreateScope();
			var tokenRepository = scope.ServiceProvider.GetRequiredService<IRefreshTokenRepository>();

			var expiredTokens = await tokenRepository.GetExpiredTokensAsync();

			tokenRepository.Remove(expiredTokens);
			await tokenRepository.CommitAsync();

			if (expiredTokens.Count > 0)
			{
				logger.LogInformation("Refresh Token Hosted Service is working. Tokens removed: {Count}", expiredTokens.Count);
			}
		}
	}
}
