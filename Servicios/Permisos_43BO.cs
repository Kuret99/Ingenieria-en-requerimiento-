using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    
    public enum Permisos_43BO
    {
        GestionUsuarios_Acceso = 1,
        GestionUsuarios_Alta = 2,
        GestionUsuarios_Desbloquear = 3,
        GestionUsuarios_Modificar = 4,
        GestionUsuarios_ActivarDesactivar = 5,
        GestionUsuarios_Listar = 6,
        Auditoria_Acceso = 7,
        Auditoria_Consultar = 8,
        Auditoria_Imprimir = 9,


      
         Menu_SeccionAdmin = 10,
        Menu_SeccionMaster = 11,
        Menu_SeccionVenta = 12,
        Menu_SeccionCompra = 13,
        Menu_SeccionReporte = 14,

    
        GestionPerfiles_Acceso = 15,
        GestionPerfiles_AsignarRelaciones = 16, // Botones <<-- y -->>
        GestionPerfiles_ConfigurarEstructura = 17 
    }
}
