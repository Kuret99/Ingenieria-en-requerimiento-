using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Servicios;
using BLL;

namespace GUI_43BO
{
    internal static class AsignadorPermisos_43BO
    {
        public static void Aplicar(Control raiz, Dictionary<string, Permisos_43BO> mapa)
        {

            if (SessionManager_43BO.Instancia == null || SessionManager_43BO.Instancia.Permisos == null)
                return;

            var permisosUsuario = SessionManager_43BO.Instancia.Permisos;

            foreach (var elemento in mapa)
            {
                var encontrados = raiz.Controls.Find(elemento.Key, true);
                if (encontrados.Length > 0)
                {
                    encontrados[0].Enabled = permisosUsuario.Contains(elemento.Value.ToString());
                }
            }
        }

        public static void AplicarMenu(MenuStrip menuStrip, Dictionary<string, Permisos_43BO> mapa)
        {
            if (SessionManager_43BO.Instancia == null || SessionManager_43BO.Instancia.Permisos == null)
                return;

            var permisosUsuario = SessionManager_43BO.Instancia.Permisos;

            foreach (var elemento in mapa)
            {
                // Buscamos el ítem en TODO el menú
                var itemMenu = EncontrarMenuItem(menuStrip.Items, elemento.Key);

                if (itemMenu != null)
                {
                    // Verificamos si tiene el permiso
                    bool tienePermiso = permisosUsuario.Contains(elemento.Value.ToString());

                    // Aplicamos la visibilidad
                    itemMenu.Visible = tienePermiso;

                    // Si es un padre, ocultar también sus hijos para mayor seguridad
                    if (itemMenu is ToolStripMenuItem subMenu)
                    {
                        foreach (ToolStripItem hijo in subMenu.DropDownItems)
                        {
                            hijo.Visible = tienePermiso;
                        }
                    }
                }
            }
        }

        private static ToolStripMenuItem EncontrarMenuItem(ToolStripItemCollection items, string name)
        {
            foreach (ToolStripItem item in items)
            {
                if (item.Name == name) return item as ToolStripMenuItem;
                if (item is ToolStripMenuItem subMenu && subMenu.DropDownItems.Count > 0)
                {
                    var encontrado = EncontrarMenuItem(subMenu.DropDownItems, name);
                    if (encontrado != null) return encontrado;
                }
            }
            return null;
        }
    }
}