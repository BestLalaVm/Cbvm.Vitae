<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UploadControl.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.UserControl.UploadControl" %>
<div class="file-Upload">
    <asp:FileUpload ID="uploadFile" runat="server"  Width="70px" />
    <asp:TextBox ID="txtFile" runat="server" Width="128px" ReadOnly="true" CssClass="upload-edit-text"></asp:TextBox>
    <asp:Button ID="btnSelect" runat="server" OnClientClick="return false;" Text="添加" />
    <asp:Button ID="btnUpload" runat="server" Text="上传" OnClick="btnUpload_Click" ValidationGroup="file-upload" />
</div>
<script type="text/javascript">
<%--    function fileChange() {
        var upload = $("#<%=uploadFile.ClientID %>");
        var txt = upload.next();
        txt.val(upload.val());
    }
    $("#<%=txtFile.ClientID %>").val("");--%>

    $(function() {
        $(".file-Upload input[type='submit']").jqxButton({ width: "40px", height: "26px" });

        $("#<%=uploadFile.ClientID %>").change(function () {
            var txt = $(this).next();

            txt.val($(this).val());
        });
    });
</script>