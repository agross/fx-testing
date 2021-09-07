using System.Linq;

using Domain.Models;

using Infrastructure;

namespace Web
{
  class Stammdaten
  {
    public static void Initialize(DomainDbContext context)
    {
      Begehungsobjekte(context);
      Mitarbeiter(context);
      Prüflinge(context);

      context.SaveChanges();
    }

    static void Begehungsobjekte(DomainDbContext context)
    {
      if (context.Begehungsobjekt.Any())
      {
        return;
      }

      context
        .Begehungsobjekt
        .AddRange(new Begehungsobjekt { Id = "000000000000011O", Bauwerk = "Südstraße 10" },
                  new Begehungsobjekt { Id = "0000000000000D3I", Bauwerk = "Bahnhofstrasse 3" },
                  new Begehungsobjekt { Id = "0000000000000J0O", Bauwerk = "Weststraße 41" },
                  new Begehungsobjekt { Id = "0000000000002BUH", Bauwerk = "Südstraße 8 (MicroStation)" },
                  new Begehungsobjekt { Id = "0000000000002PB4", Bauwerk = "Bürogebäude EZA" },
                  new Begehungsobjekt { Id = "0000000000002Q65", Bauwerk = "Bürohaus Klostergasse (Schlüssel)" },
                  new Begehungsobjekt { Id = "00000000000034PL", Bauwerk = "Bürogebäude Nonnenstrasse 01" },
                  new Begehungsobjekt { Id = "000000000000371F", Bauwerk = "Brandstraße 21" },
                  new Begehungsobjekt { Id = "000000000000371H", Bauwerk = "Meusdorfer Straße 33" },
                  new Begehungsobjekt { Id = "0000000000003FDY", Bauwerk = "Werksgelände Fischerwerke" },
                  new Begehungsobjekt { Id = "0000000000003H79", Bauwerk = "Dresdner Str. 8" },
                  new Begehungsobjekt { Id = "0000000000003MWR", Bauwerk = "Verwaltungsgebäude" },
                  new Begehungsobjekt
                  {
                    Id = "0000000000003W6J", Bauwerk = "Bürogebäude Dresdner Str. 10 (Vermietung)",
                  },
                  new Begehungsobjekt { Id = "0000000000004J0J", Bauwerk = "Energiemanagement Zentrum " },
                  new Begehungsobjekt { Id = "0000000000004KNH", Bauwerk = "Energiemanagement Zentrum Außenstelle" },
                  new Begehungsobjekt { Id = "023413####2R8###", Bauwerk = "Südstraße 9" },
                  new Begehungsobjekt { Id = "0000000000005M34", Bauwerk = "Buttergasse 10" },
                  new Begehungsobjekt { Id = "0000000000006TBG", Bauwerk = "Dö - Grundschule Schillerstraße 8" },
                  new Begehungsobjekt { Id = "0000000000006TBJ", Bauwerk = "Dö - Sporthalle Burgstraße 8" },
                  new Begehungsobjekt { Id = "0000000000006TBM", Bauwerk = "Dö - 1. Oberschule Burgstraße 8" },
                  new Begehungsobjekt { Id = "0000000000006TBS", Bauwerk = "Dö - Gymnasium " },
                  new Begehungsobjekt { Id = "0000000000006TBV", Bauwerk = "Dö - Mehrzweckhalle" },
                  new Begehungsobjekt { Id = "0000000000006TBZ", Bauwerk = "Dö - Stadtverwaltung" },
                  new Begehungsobjekt { Id = "000000000000HJBU", Bauwerk = "Bürogebäude Dresdner Str. 9" });
    }

    static void Mitarbeiter(DomainDbContext context)
    {
      if (context.Mitarbeiter.Any())
      {
        return;
      }

      context
        .Mitarbeiter
        .AddRange(new Mitarbeiter { Id = "00000000000000U2", Name = "Meyer, Karin" },
                  new Mitarbeiter { Id = "00000000000000U6", Name = "Weiner, Uta" },
                  new Mitarbeiter { Id = "000000000000010Q", Name = "Müller, Max" });
    }

    static void Prüflinge(DomainDbContext context)
    {
      if (context.Prüfling.Any())
      {
        return;
      }

      context
        .Prüfling
        .AddRange(new Prüfling
                  {
                    Id = "0000000000000125",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "002 Küche",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000126",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "003 Küche",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000127",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "004 WC",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000128",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "005 Flur",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000129",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "006 Technik",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012A",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "007 Abstellraum",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012B",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "008 Flur",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012C",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "009 WC",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012D",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "010 Büro",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012E",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "011 Besprechung",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012F",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "012 Archiv",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012G",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "013 Büro",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012H",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "014 Büro",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012I",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "015 Büro",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012J",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "016 Büro",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012K",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "017 Treppen",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "000000000000012L",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "018 Büro",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000002OMS",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "001 Büro",
                    Straße = "Südstraße 10",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "00000000000007H8",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "-101 Abstellraum",
                    Straße = "Südstraße 10",
                    Ort = "KG",
                  },
                  new Prüfling
                  {
                    Id = "00000000000007H9",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "-102 Heizung",
                    Straße = "ung	Südstraße 10",
                    Ort = "KG",
                  },
                  new Prüfling
                  {
                    Id = "00000000000007HB",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "-103 Elektrotechnik",
                    Straße = "Südstraße 10",
                    Ort = "KG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000002MQI",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "-104 Aufzugsraum",
                    Straße = "Südstraße 10",
                    Ort = "KG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000002UVY",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "Technikstellplatz",
                    Straße = "Südstraße 9",
                    Ort = "Dach",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000KLV",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "-111 Techik",
                    Straße = "Südstraße 9",
                    Ort = "KG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000N1C",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "-109 Abstellraum",
                    Straße = "Südstraße 9",
                    Ort = "KG",
                  },
                  new Prüfling
                  {
                    Id = "0000000000002MQH",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "-110 Aufzugsraum",
                    Straße = "Südstraße 9",
                    Ort = "KG",
                  },
                  new Prüfling
                  {
                    Id = "020813####2RK###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "017b Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####2TM###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "003 Speiseraum",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####2Y8###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "002 Vorraum",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####2ZY###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "001 Treppen",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####314###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "012 Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####34Q###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "011 Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3A4###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "004 Flur",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3CU###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "013 Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3GW###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "014 Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3KI###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "015 Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3NK###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "016 Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3PI###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "005 WC-H",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3QK###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "017a Büro",
                    Straße = "Südstraße 9",
                    Ort = "EG",
                  },
                  new Prüfling
                  {
                    Id = "020813####3SL###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "119 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####3WZ###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "118 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####41X###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "120 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####45N###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "124a Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####47T###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "121 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4EN###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "122 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4HP###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "123 Technik",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4LR###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "104 Flur",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4P9###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "105 WC-D",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4QB###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "101 Treppen",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4R9###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "102 Vorraum",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4SN###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "124b Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 01",
                  },
                  new Prüfling
                  {
                    Id = "020813####4UW###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "201 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 02",
                  },
                  new Prüfling
                  {
                    Id = "020813####4V4###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "202 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 02",
                  },
                  new Prüfling
                  {
                    Id = "020813####4V8###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "203 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 02",
                  },
                  new Prüfling
                  {
                    Id = "020813####4VC###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "204 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 02",
                  },
                  new Prüfling
                  {
                    Id = "0000000000002PAL",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "302 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 03",
                  },
                  new Prüfling
                  {
                    Id = "0000000000002PAP",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "303 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 03",
                  },
                  new Prüfling
                  {
                    Id = "0000000000002PAQ",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "304 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 03",
                  },
                  new Prüfling
                  {
                    Id = "020813####4VJ###",
                    Typ = Prüflingstyp.Raum,
                    Bezeichnung = "301 Büro",
                    Straße = "Südstraße 9",
                    Ort = "OG 03",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000N0U",
                    Typ = Prüflingstyp.TechnischeAnlage,
                    Bezeichnung = "Abwasserhebeanlage mini compakta UZ 7",
                    Straße = "Südstraße 9",
                    Ort = "Haus 9	KG	-111 Technik",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000N0Q",
                    Typ = Prüflingstyp.TechnischeAnlage,
                    Bezeichnung = "Personenaufzug  Südstr.9",
                    Straße = "Südstraße 9",
                    Ort = "Haus 9	KG	-110 Aufzugsraum",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000CBI",
                    Typ = Prüflingstyp.TechnischeAnlage,
                    Bezeichnung = "Verladekran  50t",
                    Straße = "Südstraße 10",
                    Ort = "Haus 10	Außenfläche	Technikstellplatz 01",
                  },
                  new Prüfling
                  {
                    Id = "0000000000000N11",
                    Typ = Prüflingstyp.TechnischeAnlage,
                    Bezeichnung = "Personenaufzug Südstr.10",
                    Straße = "Südstraße 10",
                    Ort = "Haus 10	KG	-104 Aufzugsraum",
                  },
                  new Prüfling
                  {
                    Id = "00000000000007HA",
                    Typ = Prüflingstyp.TechnischeAnlage,
                    Bezeichnung = "Heizungsanlage  Vertomat VSB 22 (Südstr.10)",
                    Straße = "Südstraße 10",
                    Ort = "Haus 10	KG	-102 Heizung",
                  },
                  new Prüfling
                  {
                    Id = "00000000000004BY",
                    Typ = Prüflingstyp.TechnischeAnlage,
                    Bezeichnung = "Bearbeitungszentrum Klöckner-Moeller",
                    Straße = "Südstraße 10",
                    Haus = "Haus 10",
                    Ort = "EG	018 Büro",
                  });
    }
  }
}
