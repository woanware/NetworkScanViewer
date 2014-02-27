using WeifenLuo.WinFormsUI.Docking;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormDescription : DockContent
    {
        /// <summary>
        /// 
        /// </summary>
        public FormDescription()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SetData(string data)
        {
            txtData.Text = data;
        }
    }
}
