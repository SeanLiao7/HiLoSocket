using System;
using System.Windows.Forms;

namespace ClientForm
{
    public static class Extensions
    {
        public static void UpdateUi( this Control control, Action updateAction )
        {
            if ( control.InvokeRequired )
                control.Invoke( updateAction );
            else
                updateAction( );
        }
    }
}