using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Tieto.TSU.Framework.EventDataSolution;
using Tieto.TSU.Framework.EventDataSolution.Helpers;
using Tieto.TSU.Framework.EventDataSolution.Interface;

namespace Tieto.TSU.EventReceiver
{
	public class SubscriptionCreator
	{
		private readonly IComponentContext componentContext;
		public SubscriptionCreator(IComponentContext componentContext)
		{
			this.componentContext = componentContext;
		}

		public void Create()
		{
			// created for metadatareload event
			componentContext.Resolve<IMessageBusCreator>().CreateSubscription("TestPlatform", "Test", "Platform");
			
		}

	}
}
