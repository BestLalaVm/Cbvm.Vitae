<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true"
    CodeBehind="ImportStudent.aspx.cs" Inherits="Cbvm.Vitae.Manage.DepartAdmin.ImportStudent" Theme="DepartAdmin" %>

<%@ Register Src="~/Manage/DepartAdmin/UserControl/ImportControl.ascx" TagName="ImportControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .upload-manage .upload .import-data.file-Upload input[type="file"] {
            right: 96px;
            width: 80px;
        }

        .file-Upload input[type="submit"][id$="btnUpload"] {
            margin-left: 0px !important;
        }

        .body-content .main > table.wrap-container .wrap-right .caption {
            background-color: #F1F1F1 !important;
            border-bottom: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="upload-manage">
        <div class="caption">
            学生导入信息
        </div>
        <div class="container">
            <div class="upload">
                <uc:importcontrol id="btnImport" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
