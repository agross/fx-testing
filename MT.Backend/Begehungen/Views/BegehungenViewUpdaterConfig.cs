using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

using Microsoft.Extensions.Configuration;

namespace MT.Backend.Begehungen.Views
{
  public class BegehungenViewUpdaterConfig : ConsumerDefinition<BegehungenViewUpdater>
  {
    public BegehungenViewUpdaterConfig(IConfiguration configuration)
    {
      EndpointName = "begehungen-view";
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
                                              IConsumerConfigurator<BegehungenViewUpdater> consumerConfigurator)
    {
    }
  }
}
