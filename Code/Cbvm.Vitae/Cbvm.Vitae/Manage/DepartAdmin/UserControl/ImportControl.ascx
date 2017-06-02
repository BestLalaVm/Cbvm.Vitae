<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportControl.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.DepartAdmin.UserControl.ImportControl" %>
<style type="text/css">
    .body-content .main .wrap-right .file-Upload input[type="file"] {
        left:170px !important;
        right:auto;
    }
</style>
<div class="file-Upload import-data">
    <asp:FileUpload ID="uploadFile" runat="server" onchange="fileChange()" />
    <asp:TextBox ID="txtFile" runat="server" ReadOnly="true" CssClass="edit-text"></asp:TextBox>
    <asp:Button ID="btnSelect" runat="server" OnClientClick="return false;" Text="添加文件" />
    <span><asp:Button ID="btnUpload" runat="server" Text="开始导入" OnClick="btnUpload_Click" /></span>
    <span><a href="../../../Resource/09-11在校本科生名单2011年12月更新.xls" target="_blank">下载导入文件格式</a></span>
    <br style="clear: both;" />
</div>
<script type="text/javascript">
    function fileChange() {
        var upload = $("#<%=uploadFile.ClientID %>");
        var txt = upload.next();
        txt.val(upload.val());
    }

    function GetComponentSize(component, size) {
        return {
            Width: "70px"
        };
    }

    $(document).ready(function() {
        $("input.edit-text").jqxInput({ height: 25, width: 180 });
    });
</script>
