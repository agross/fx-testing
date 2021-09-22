using System.Threading.Tasks;

using Domain.Models;

using Microsoft.AspNetCore.Mvc;

using NServiceBus;

namespace Web.Pages
{
  public class BegehungApi : Controller
  {
    readonly IMessageSession _bus;

    public BegehungApi(IMessageSession bus)
    {
      _bus = bus;
    }

    // GET
    public async Task<IActionResult> Index(string id)
    {
      await _bus.Send(new StarteBegehung { BegehungId = id });

      return Redirect("/Begehungen");
    }
  }
}
