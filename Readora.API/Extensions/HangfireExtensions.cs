using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Readora.Application.Common;
using Readora.Application.Features.Borrowings.Commands;

namespace Readora.API.Extensions;

public static class HangfireExtensions
{
	public static IApplicationBuilder UseBackgroundJobs(this IApplicationBuilder app)
	{
		var dashboardOptions = new DashboardOptions
		{
			Authorization = new[] { new LocalRequestsOnlyAuthorizationFilter() }
		};

		app.UseHangfireDashboard("/hangfire", dashboardOptions);
		
		RecurringJob.AddOrUpdate<IMediator>(
			"process-overdue-books", 
			m => m.Send(new ProcessOverdueBorrowingsCommand(), CancellationToken.None), 
			Cron.Daily);

		return app;
	}
}
