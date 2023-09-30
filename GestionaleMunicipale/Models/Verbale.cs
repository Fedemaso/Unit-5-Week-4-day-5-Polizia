using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Municipale.Models
{
    public class Verbale
    {
        public int IDVerbali { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Data Violazione")]
        public DateTime DataViolazione { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Indirizzo Violazione")]
        public string IndirizzoViolazione { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Agente")]
        public string Nominativo_Agente { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Data Verbale")]
        public DateTime DataTrascrizioneVerbale { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Importo { get; set; }

        [Required]
        [Display(Name = "Punti decurtati")]
        public int DecrementoPunti { get; set; }

        [Required]
        public int IDAnagrafica { get; set; }

        [Required]
        public int IDViolazione { get; set; }

        [Display(Name="Nome")]
        public string NomeAnagrafica { get; set; }
        
        [Display(Name = "Cognome")]
        public string  CognomeAnagrafica { get; set; }
        
        
        [Display(Name = "Violazione")]
        public string TipoViolazione {get; set; }

        public Verbale() { }
        public Verbale(int idVerbali, DateTime dataViolazione, string indirizzoViolazione, string nominativoAgente, DateTime dataTrascrizioneVerbale, decimal importo, int decrementoPunti, int idAnagrafica, int idViolazione)
        {
            IDVerbali = idVerbali;
            DataViolazione = dataViolazione;
            IndirizzoViolazione = indirizzoViolazione;
            Nominativo_Agente = nominativoAgente;
            DataTrascrizioneVerbale = dataTrascrizioneVerbale;
            Importo = importo;
            DecrementoPunti = decrementoPunti;
            IDAnagrafica = idAnagrafica;
            IDViolazione = idViolazione;
        }

    }
}