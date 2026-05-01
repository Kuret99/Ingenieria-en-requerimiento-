using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLBitacora_43BO
    {
        DALBitacora_43BO dal = new DALBitacora_43BO();

        public void GuardarLog_43BO(User_43BO usaurio, Modulo_43BO modulo, Evento_43BO evento, int criticidad)
        {
            Bitacora_43BO bi = new Bitacora_43BO();
          
            //como todavia no tengo el login tuve que mporvisar un dni de un usaurio para testear
           
            
            bi.log_43BO = SessionManager_43BO.Instancia.Usuario;

            bi.Modulo = modulo;
            bi.Evento = evento;
            bi.Fecha_43BO = DateTime.Now;
            bi.Criticidad_43BO = criticidad;
            
            dal.RegistrarEvento_43BO(bi);
        }
    }
}
