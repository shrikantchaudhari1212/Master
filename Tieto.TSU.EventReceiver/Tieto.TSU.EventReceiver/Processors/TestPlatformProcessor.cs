using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tieto.TSU.Framework.EventDataSolution.Interface;
using Tieto.TSU.Domain.Platform.Localization;

namespace Tieto.TSU.EventReceiver.Processors
{
	public class TestPlatformProcessor: IMessageProcessor
	{
		private readonly IMessageBusHelper messageBusHelper;
		private readonly IEventSender eventSender;

		public TestPlatformProcessor(IMessageBusHelper messageBusHelper, IEventSender eventSender)
		{
			this.messageBusHelper = messageBusHelper;
			this.eventSender = eventSender;
		}

		public async Task Process(BrokeredMessage brokeredMessage)
		{
			string payloadUri = string.Empty;
			try
			{
				// This is to create current method into async and test async scenario.
				// await Task.Delay(1000);

				LocalizationKey eventDataSolutionTestEntity = messageBusHelper.GetPayloadFromMessage<LocalizationKey>(brokeredMessage, out payloadUri);
			}
			catch (Exception ex)
			{
				//throw new Exception();
				eventSender.SendErrorDeviationEvent(brokeredMessage, ex, payloadUri);
			}
		}
	}
}
