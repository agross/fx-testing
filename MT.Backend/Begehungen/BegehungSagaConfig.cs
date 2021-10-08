// using MassTransit;
// using MassTransit.Definition;
// using MassTransit.Saga;
//
// using Microsoft.Extensions.Configuration;
//
// using MT.Backend.Messages.Begehungen;
//
// namespace MT.Backend.Begehungen
// {
//   public class BegehungSagaConfig : SagaDefinition<BegehungSaga>
//   {
//     public BegehungSagaConfig(IConfiguration configuration)
//     {
//       EndpointName = configuration.GetSection("MassTransit")
//                                   .GetSection("Queues")
//                                   .GetValue<string>("Begehungen");
//     }
//
//     protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator,
//                                           ISagaConfigurator<BegehungSaga> sagaConfigurator)
//     {
//       endpointConfigurator.UseInMemoryOutbox();
//     }
//   }
// }
