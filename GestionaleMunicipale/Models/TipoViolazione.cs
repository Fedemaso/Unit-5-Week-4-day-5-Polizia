using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Municipale.Models
{
    public class TipoViolazione
    {
        public int IdViolazione { get; set; }

        [Required]
        public string Descrizione { get; set; }

        public TipoViolazione(int id, string descrizione) 
        { 
            IdViolazione = id;
            Descrizione = descrizione;
        }

        public TipoViolazione() { }
    }
}