using Ornithology.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornithology.Domain
{
    public class AveDomain : IAveDomain
    {
        public bool Validar(Ave objeto, ref List<string> mensajes)
        {
            if (objeto == null)
            {
                mensajes.Add("Ingrese datos del Ave");
            }
            else {
                if (string.IsNullOrEmpty(objeto.Codigo))
                {
                    mensajes.Add("Codigo del Ave es obligatorio");
                }
                if (string.IsNullOrEmpty(objeto.NombreComun))
                {
                    mensajes.Add("Nombre común del Ave es obligatorio");
                }
                if (string.IsNullOrEmpty(objeto.NombreCientifico))
                {
                    mensajes.Add("Nombre científico del Ave es obligatorio");
                }

                if (objeto.AvesPais.Count == 0)
                {
                    mensajes.Add("Ingrese al menos un país para el Ave");
                }
            }

            return mensajes.Count == 0;
        }
    }
}
