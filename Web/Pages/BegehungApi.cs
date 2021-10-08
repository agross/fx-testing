using System.Threading.Tasks;

using MassTransit;

using Microsoft.AspNetCore.Mvc;

using MT.Backend.Messages.Begehungen;

namespace Web.Pages
{
  public class BegehungApi : Controller
  {
    readonly IBus _bus;
    readonly ISendEndpointProvider _sendEndpointProvider;

    public BegehungApi(IBus bus, ISendEndpointProvider sendEndpointProvider)
    {
      _bus = bus;
      _sendEndpointProvider = sendEndpointProvider;
    }

    // GET
    public async Task<IActionResult> Index(string id)
    {
      // https://stackoverflow.com/questions/62713786/masstransit-endpointconvention-azure-service-bus/62714778#62714778
      await _bus.Send<StarteBegehung>(new { BegehungId = id });

      return Redirect("/Begehungen");
    }

    public async Task<IActionResult> Abschließen(string id)
    {
      await _bus.Send<SchließeBegehungAb>(new { BegehungId = id });

      return Redirect("/Begehungen");
    }

    public async Task<IActionResult> Verwerfen(string id)
    {
      await _bus.Send<VerwerfeBegehung>(new { BegehungId = id });

      return Redirect("/Begehungen");
    }
  }
}
