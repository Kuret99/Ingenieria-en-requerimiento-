using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Servicios
{
    public class User_43BO
    {
		private string _nombre;

		public string Nombre_43BO
        {
			get { return _nombre; }
			set {	_nombre = value; }
		}

		private int _dni;

		public int DNI_43BO
        {
			get { return _dni; }
			set { _dni = value; }
		}

		private string  _apellido;

		public string Apellido_43BO
        {
			get { return _apellido; }
			set { _apellido = value; }
		}

		private string _email;

		public string Email_43BO
        {
			get { return _email; }
			set { _email = value; }
		}

		private string _rol;

		public string Rol_43BO
        {
			get { return _rol; }
			set { _rol = value; }
		}
        private string _hash;

        public string Hash_43BO
        {
            get { return _hash; }
            set { _hash = value; }
        }

        private bool  _bloqueado;

		public bool Bloqueado_43BO
        {
			get { return _bloqueado; }
			set { _bloqueado = value; }
		}


		private bool _activo;

		public bool Activo_43BO
        {
			get { return _activo; }
			set { _activo = value; }
		}

        public User_43BO()
        {
				
        }

		public string UserName_43BO		
		{
			get { return _nombre + _dni.ToString();  } 

        }



    }
}
