using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;
using MongoDB.Driver;
using Newtonsoft.Json;
using Tieto.TSU.Framework.AzureClients.Interface;
using Tieto.TSU.Framework.EventDataSolution;
using Tieto.TSU.Framework.EventDataSolution.Interface;
using Tieto.TSU.Framework.MongoDb.Clients;
using Tieto.TSU.Framework.EventDataSolution.Events;
using Tieto.TSU.EventReceiver.Processors;

namespace Tieto.TSU.EventReceiver.Handlers
{
	public class DefaultHandler
	{
		private readonly IComponentContext componentContext;
		private static List<string> _userVerbs = new List<string> { "Reload", "Updated", "Deleted", "Deactivated" };
		public DefaultHandler(IComponentContext componentContext)
		{
			this.componentContext = componentContext;
		}

		public async Task TestPlatform([ServiceBusTrigger("eventdatasolution", "TestPlatform")] BrokeredMessage brokeredMessage, TextWriter log)
		{
			string noun = brokeredMessage.Properties[Constants.Noun].ToString();
			string verb = brokeredMessage.Properties[Constants.Verb].ToString();
			try
			{
				IMessageProcessor messageProcessor = componentContext.ResolveNamed<IMessageProcessor>(noun + verb);
				await messageProcessor.Process(brokeredMessage);
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}
	}
}
