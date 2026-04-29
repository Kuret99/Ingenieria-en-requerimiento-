using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public enum Modulo_43BO { Usuario, Ventas, Compras, Maestro, Perfiles } //
    public enum Evento_43BO { Login, Logout, Crear, Desactivar,modificar}
    public  class Bitacora_43BO
    {
		private User_43BO _user;

		private DateTime _fecha;

		private int _criticidad;


		public Evento_43BO Evento
		{
			get; set;
		}


		public Modulo_43BO  Modulo
		{
			get; set;
		}


		public DateTime Fecha_43BO
        {
			get { return _fecha; }
			set { _fecha = value; }
		}

		public User_43BO log_43BO
        {
			get { return _user; }
			set { _user = value; }
		}

	
		public int Criticidad_43BO
        {
			get { return _criticidad; }
			set { _criticidad = value; }
		}

	}
}
