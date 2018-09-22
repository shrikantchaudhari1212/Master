using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Tieto.TSU.EventReceiver.Handlers;
using System.Reflection;
using Tieto.TSU.Framework.EventDataSolution.Interface;
using Tieto.TSU.EventReceiver.Processors;

namespace Tieto.TSU.EventReceiver
{
	public class AzureWebJobModule: Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<SubscriptionCreator>();
			builder.RegisterType<DefaultHandler>();
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Named<IMessageProcessor>(x => x.Name.Replace("Processor", string.Empty)).As<IMessageProcessor>();
			//builder.RegisterType<SmokeTestHandler>();
			//Registed MetadataReload event processor
			//builder.RegisterType<MetadataReloadProcessor>();
			//
		}
	}
}
