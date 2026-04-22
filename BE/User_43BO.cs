using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace BE
{
    public class User_43BO
    {
		private string _nombre;

		public string Nombre
		{
			get { return _nombre; }
			set {	_nombre = value; }
		}

		private int _dni;

		public int DNI
		{
			get { return _dni; }
			set { _dni = value; }
		}

		private string  _apellido;

		public string  Apellido
		{
			get { return _apellido; }
			set { _apellido = value; }
		}

		private string _email;

		public string  Email 
		{
			get { return _email; }
			set { _email = value; }
		}

		private string _rol;

		public string  Rol
		{
			get { return _rol; }
			set { _rol = value; }
		}

		private string _contraseña;

		public string Contraseña 
		{
			get { return _contraseña; }
			set { _contraseña = value; }
		}

		private bool  _bloqueado;

		public bool  Bloqueado
		{
			get { return _bloqueado; }
			set { _bloqueado = value; }
		}


		private bool _activo;

		public bool Activo
		{
			get { return _activo; }
			set { _activo = value; }
		}

        public User_43BO()
        {
				
        }



    }
}
