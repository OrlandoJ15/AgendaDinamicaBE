using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ArticuloDetalle
    {
        public decimal Precio { get; set; }
        public string Moneda { get; set; }
        public string Nom_Articulo { get; set; }
        public string Desc_Categoria { get; set; }
        public string Aplic_Desc { get; set; }
        public string Preparacion { get; set; }
        public string Des_Observaciones { get; set; }
        public string Ind_MaxMin { get; set; }
        public decimal Precio_Maximo { get; set; }
        public string Tip_Articulo { get; set; }
        public decimal Programa_Mivida { get; set; }
    }
}
