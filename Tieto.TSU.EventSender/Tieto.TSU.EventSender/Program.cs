using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tieto.TSU.Framework.EventDataSolution.Interface;
using Autofac;
using Tieto.TSU.Framework.Bootstrap.Autofac.WebApi;
using Tieto.TSU.Domain.Platform.Localization;
using Tieto.TSU.Platform.Events.EventPayload;
namespace Tieto.TSU.EventSender
{
	public class Program
	{	
		static void Main(string[] args)
		{   
		    AutoFacStandaloneBootstrapper bootstrapper = new AutoFacStandaloneBootstrapper()
			{
				EnableCaching = false,
				ApplicationName = "Tieto.TSU.EventSender",
				EnableLogging = false,
				ApplicationType = Framework.Definitions.ApplicationType.Standalone,
				BuiltInCacheLoadTypesToInclude = new Type[] { }
			};

			bootstrapper.Initialize();
			var container = bootstrapper.Container;
			var sender = container.Resolve<IEventSender>();
			var id = Guid.NewGuid().ToString();
			LocalizationKey obj = new LocalizationKey();
			obj.Key = "Platform";
			obj.Id = Guid.NewGuid();
			//Message<LocalizationKey> messageReload = new Message<LocalizationKey>()
			//{
			//	Header = new MessageHeader()
			//	{
			//		MessageID = id,
			//		Noun = "Test",
			//		Verb = "Platform"
			//	},
			//	Payload = obj
			//};

			//Event<LocalizationKey> eventmsg = new Event<LocalizationKey>(messageReload);
			//sender.SendEvent(eventmsg);

			UserCreatedEventPayload obj1 = new UserCreatedEventPayload();
			List<string> roles = new List<string>();
			roles.Add("Administrator");
			roles.Add("GlobalAdmin");
			obj1.RoleNames = roles;
			obj1.FirstName = "FirstName";
			obj1.LastName = "LastName";
			obj1.UserName = "AP\\chaudshr";
			obj1.Domain = "AP";


			Message<UserCreatedEventPayload> messageReload = new Message<UserCreatedEventPayload>()
			{
				Header = new MessageHeader()
				{
					Noun = "User",
					Verb = "Created"
				},
				Payload = obj1
			};

			Event<UserCreatedEventPayload> eventmsg = new Event<UserCreatedEventPayload>(messageReload);
			sender.SendEvent(eventmsg);
		}
	}
}
