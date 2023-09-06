Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraTab
Imports DevExpress.XtraTab.ViewInfo
Imports System

Namespace XtraTabControl_CustomButtons

    Public Partial Class Form1
        Inherits XtraForm

        Public Sub New()
            InitializeComponent()
            xtraTabControl1.CustomHeaderButtons(0).ToolTip = "Add Image..."
            xtraTabControl1.CustomHeaderButtons(1).ToolTip = "Remove Image"
            AddImage("Start Page", ResourceImageHelper.CreateImageFromResources("XtraTabControl_CustomButtons.08.jpg", GetType(Form1).Assembly))
            xtraTabControl1.TabPages(0).ShowCloseButton = DefaultBoolean.False
        End Sub

        Private Sub xtraTabControl1_CustomHeaderButtonClick(ByVal sender As Object, ByVal e As CustomHeaderButtonEventArgs)
            If e.Button.Kind = ButtonPredefines.Plus Then OnAddImageBtnClick()
            If e.Button.Kind = ButtonPredefines.Minus Then OnRemoveImageClick()
        End Sub

        Private Sub OnAddImageBtnClick()
            Using ofd As OpenFileDialog = New OpenFileDialog()
                ofd.Filter = "All Picture Files |*.bmp;*.gif;*.jpg;*.jpeg;*.ico;*.png|Text Files |*.txt;*.rtf"
                ofd.Title = "Open"
                ofd.Filter = ""
                If ofd.ShowDialog() = DialogResult.OK Then
                    LoadImage(ofd.FileName)
                End If
            End Using
        End Sub

        Private Sub OnRemoveImageClick()
            xtraTabControl1.SelectedTabPage.Dispose()
            UpdateButtons()
        End Sub

        Private Sub UpdateButtons()
        'xtraTabControl1.CustomHeaderButtons[1].Enabled = xtraTabControl1.TabPages.Count > 1;
        End Sub

        Private Sub LoadImage(ByVal path As String)
            If File.Exists(path) Then
                AddImage(path, Image.FromFile(path))
            End If
        End Sub

        Private Sub AddImage(ByVal caption As String, ByVal img As Image)
            Dim page As XtraTabPage = New XtraTabPage()
            page.Text = caption
            Dim picEdit As PictureEdit = New PictureEdit()
            picEdit.Image = img
            picEdit.Dock = DockStyle.Fill
            picEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            picEdit.Properties.PictureAlignment = ContentAlignment.MiddleCenter
            picEdit.Properties.SizeMode = PictureSizeMode.Squeeze
            picEdit.Parent = page
            xtraTabControl1.TabPages.Add(page)
            xtraTabControl1.SelectedTabPage = page
            UpdateButtons()
        End Sub

        Private Sub xtraTabControl1_CloseButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            Dim ea As ClosePageButtonEventArgs = TryCast(e, ClosePageButtonEventArgs)
            CType(ea.Page, IDisposable).Dispose()
        End Sub

        Private Sub xtraTabControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As TabPageChangedEventArgs)
        'xtraTabControl1.CustomHeaderButtons[1].Enabled = (e.Page.Text != "Start Page");
        End Sub
    End Class
End Namespace
