using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Tieto.TSU.Framework.Bootstrap.Autofac.AzureWebJob;
using Autofac;

namespace Tieto.TSU.EventReceiver
{
	public class Program
	{
		static void Main(string[] args)
		{
			AutofacAzureWebJobBootstrapper bootstrapper = new AutofacAzureWebJobBootstrapper()
			{
				EnableCaching = false,
				ApplicationName = "EventSourcing",
				ModuleName = "Platform",
				ServiceName = "EventSourcing",
				ServiceDescription = "This service will store all events into event source and store deviation events in to deviation store.",
				ServiceDisplayName = "EventSourcing",
				EnableLogging=false

			};
			bootstrapper.InitializeCompleted += Bootstrapper_InitializeCompleted;

			// The following code ensures that the WebJob will be running continuously
			bootstrapper.RunAndBlock();

		}

		public static void Bootstrapper_InitializeCompleted(Framework.Bootstrap.Bootstrapper bootstrapper)
		{
			// Create Azure Service Bus command and evnet subscription when web job starts.
			bootstrapper.Container.Resolve<SubscriptionCreator>().Create();
		}
	}
}
