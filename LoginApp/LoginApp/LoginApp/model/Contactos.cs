using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginApp.model
{
    public class Contactos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), NotNull]
        public string Nombres { get; set; }

        [NotNull]
        public int Edad { get; set; }

        [NotNull]
        public string Pais { get; set; }

        [MaxLength(100), NotNull]
        public string Nota { get; set; }

        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
}
