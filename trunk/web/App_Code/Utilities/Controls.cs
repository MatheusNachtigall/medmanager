using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Controls
/// </summary>
namespace Utilities
{
    public static class Controls
    {
        public static List<Control> GetAll(ControlCollection controls)
        {
            List<Control> returnControls = new List<Control>();

            foreach (Control control in controls)
            {
                returnControls.Add(control);

                List<Control> subControls = Utilities.Controls.GetAll(control.Controls);

                returnControls.AddRange(subControls);
            }

            return returnControls;
        }

        public static List<T> GetAll<T>(ControlCollection controls)
        {
            List<T> returnControls = new List<T>();

            foreach (Control control in controls)
            {
                if (control is T)
                {
                    returnControls.Add((T)(object)control);
                }

                List<T> subControls = Utilities.Controls.GetAll<T>(control.Controls);

                returnControls.AddRange(subControls);
            }

            return returnControls;
        }
    }
}