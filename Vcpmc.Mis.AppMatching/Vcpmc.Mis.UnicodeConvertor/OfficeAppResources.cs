
namespace Vcpmc.Mis.UnicodeConverter
{
    public abstract class OfficeAppResources
    {
        /// <summary>
        /// Releases COM object (Word/Excel/PowerPoint) resources. 
        /// http://support2.microsoft.com/kb/317109/en-us
        /// </summary>
        /// <param name="o">COM object</param>
        public void NAR(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch { }
            finally
            {
                o = null;
            }
        }
    }
}
