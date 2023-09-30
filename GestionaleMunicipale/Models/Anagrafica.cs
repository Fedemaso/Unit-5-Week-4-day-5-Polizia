using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Municipale.Models
{
    public class Anagrafica
    {
        public int IDAnagrafica { get; set; }

        [Required]
        [StringLength(20)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(20)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(20)]
        public string Citta { get; set; }

        [StringLength(5)]
        public string CAP { get; set; }

        [Required]
        [StringLength(16)]
        [Display(Name = "Codice Fiscale")]
        public string Cod_Fisc { get; set; }
       
        [Display(Name = "Verbali")]
        public int totVerbali {  get; set; }

        [Display(Name = "totale punti decurtati")]
        public int totPuntiDecurtati { get; set; }

        public Anagrafica() { }
        public Anagrafica(int id,string nome,string cognome, string indirizzo,string citta, string cap,string codFiscale) 
        { 
            IDAnagrafica = id;
            Nome = nome;
            Cognome = cognome;
            Indirizzo = indirizzo;
            Citta = citta;
            CAP = cap;
            Cod_Fisc= codFiscale;
        }


    }
}