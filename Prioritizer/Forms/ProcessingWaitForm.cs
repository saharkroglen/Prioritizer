using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;

namespace Prioritizer.Forms
{
    public partial class ProcessingWaitForm : WaitForm {
        public ProcessingWaitForm() {
            InitializeComponent();
        }

        public override void SetCaption(string caption) {
            base.SetCaption(caption);
            progressPanel1.Caption = caption;
        }

        public override void SetDescription(string description) {
            base.SetDescription(description);
            progressPanel1.Description = description;
        }
    }
}
