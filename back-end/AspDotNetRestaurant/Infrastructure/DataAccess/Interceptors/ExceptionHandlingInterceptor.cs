using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Interceptors;
public class ExceptionHandlingInterceptor : DbCommandInterceptor
{
	public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
	{
		return base.CommandCreated(eventData, result);
	}
}
