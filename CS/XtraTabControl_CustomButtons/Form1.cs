using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using System;

namespace XtraTabControl_CustomButtons {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();
            xtraTabControl1.CustomHeaderButtons[0].ToolTip = "Add Image...";
            xtraTabControl1.CustomHeaderButtons[1].ToolTip = "Remove Image";
            AddImage("Start Page",
                ResourceImageHelper.CreateImageFromResources("XtraTabControl_CustomButtons.08.jpg", typeof(Form1).Assembly));
            xtraTabControl1.TabPages[0].ShowCloseButton = DefaultBoolean.False;
        }
        void xtraTabControl1_CustomHeaderButtonClick(object sender, CustomHeaderButtonEventArgs e) {
            if (e.Button.Kind == ButtonPredefines.Plus)
                OnAddImageBtnClick();
            if (e.Button.Kind == ButtonPredefines.Minus)
                OnRemoveImageClick();
        }
        void OnAddImageBtnClick() {
            using (OpenFileDialog ofd = new OpenFileDialog()) {
                ofd.Filter = "All Picture Files |*.bmp;*.gif;*.jpg;*.jpeg;*.ico;*.png|Text Files |*.txt;*.rtf";
                ofd.Title = "Open";
                ofd.Filter = "";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    LoadImage(ofd.FileName);
                }
            }
        }
        void OnRemoveImageClick() {
            xtraTabControl1.SelectedTabPage.Dispose();
            UpdateButtons();
        }
        void UpdateButtons() {
            //xtraTabControl1.CustomHeaderButtons[1].Enabled = xtraTabControl1.TabPages.Count > 1;
        }
        void LoadImage(string path) {
            if (File.Exists(path)) {
                AddImage(path, Image.FromFile(path));
            }
        }
        void AddImage(string caption, Image img) {
            XtraTabPage page = new XtraTabPage();
            page.Text = caption;
            PictureEdit picEdit = new PictureEdit();
            picEdit.Image = img;
            picEdit.Dock = DockStyle.Fill;
            picEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            picEdit.Properties.PictureAlignment = ContentAlignment.MiddleCenter;
            picEdit.Properties.SizeMode = PictureSizeMode.Squeeze;
            picEdit.Parent = page;
            xtraTabControl1.TabPages.Add(page);
            xtraTabControl1.SelectedTabPage = page;
            UpdateButtons();
        }
        void xtraTabControl1_CloseButtonClick(object sender, System.EventArgs e) {
            ClosePageButtonEventArgs ea = e as ClosePageButtonEventArgs;
            ((IDisposable)ea.Page).Dispose();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e) {
            //xtraTabControl1.CustomHeaderButtons[1].Enabled = (e.Page.Text != "Start Page");
        }
    }
}
