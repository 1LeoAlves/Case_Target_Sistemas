using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Case_Target_System_DTO
{
    [Serializable]
    public class DailyBilling
    {
        [JsonPropertyName("dia")]
        public int Day {  get; set; }
        [JsonPropertyName("valor")]
        public double Value { get; set; }
    }
}
