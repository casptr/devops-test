using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Foodtruck.Shared.Reservations;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatusDto { 
    Voorgesteld,
    Bevestigd,
    Betaald
}

