using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace Prioritizer.Shared
{

    public class ClientMessage
    {
        [JsonProperty("PokeList")]
        public List<Poke> PokeList = new List<Poke>();
        [JsonProperty("UserID")]
        public Guid UserID;
        [JsonProperty("UpdatedOn")]
        public DateTime UpdatedOn { get; set; }

        //public bool Dirty { get; set; }

        public void AddPoke(Poke p)
        {
            PokeList.Add(p);
            this.UpdatedOn = DateTime.UtcNow;
            //Dirty = true;
        }
    }

    public class Poke
    {
        enPokeMood _pokeMood = enPokeMood.friendly;
        [JsonProperty("From")]
        public Guid From { get; set; }
        [JsonProperty("To")]
        public Guid To { get; set; }
        [JsonProperty("PokeMood")]
        public enPokeMood PokeMood
        {
            get
            {
                return _pokeMood;
            }
            set
            {
                _pokeMood = value;
            }
        } 
        [JsonProperty("Comment")]
        public string Comment { get; set; }
        [JsonProperty("SentOn")]
        public DateTime SentOn { get; set; }
        [JsonProperty("Type")]
        public enPokeType Type { get; set; }
        [JsonProperty("TaskID")]
        public Guid TaskID { get; set; }
        [JsonProperty("SendEmail")]
        public bool SendEmail { get; set; }
        [JsonProperty("DeliveredByMailAfterTimeout")]
        public bool DeliveredByMailAfterTimeout { get; set; }

    }

    public enum enPokeType
    {
        Invoker,
        Reply,
        PlainMessage
    }

    }
