using MassTransit;
using MassTransit.Definition;

using Microsoft.Extensions.Configuration;

namespace MT.Backend.Begehungen
{
  public class BegehungSagaAsStateMachineConfig : SagaDefinition<Begehungen>
  {
    public BegehungSagaAsStateMachineConfig(IConfiguration configuration)
    {
      EndpointName = configuration.GetSection("MassTransit")
                                  .GetSection("Queues")
                                  .GetValue<string>("Begehungen");
    }

    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator,
                                          ISagaConfigurator<Begehungen> sagaConfigurator)
    {
      endpointConfigurator.UseInMemoryOutbox();
    }
  }
}
