using System.Windows.Forms;

namespace MiniER
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        protected SettingsDTO _settings;

        public SettingsDTO Settings
        {
            get {
                if (_settings == null) return null;
                return _settings.Clone() as SettingsDTO; 
            }
            set {
                _settings = (SettingsDTO)value.Clone();
                cb_chow_datatype.DataBindings.Add("Checked", _settings, "ShowDataType");
                cb_show_schema.DataBindings.Add("Checked", _settings, "ShowSchema");
            }
        }
    }
}
