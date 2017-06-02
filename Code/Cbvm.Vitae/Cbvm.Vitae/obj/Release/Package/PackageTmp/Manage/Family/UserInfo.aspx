<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true"
    CodeBehind="UserInfo.aspx.cs" Inherits="Cbvm.Vitae.Manage.Family.UserInfo" Theme="FamilyManage" %>

<%@ Register Src="UserControl/UserInfo.ascx" TagName="UserInfo" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .user-info .container .user-left > table {
            width: 100%;
        }

        html body .RadInput .riTextBox, html body .RadInputMgr {
            border-style: none !important;
            background: none !important;
        }

        label {
            line-height: 25px;
        }

        .body-content .main .wrap-right .file-Upload input[type="file"] {
            width: 40px !important;
            right: 90px  !important;
            height: 33px;
            cursor: pointer;
        }

        .file-Upload input[type="text"] {
            margin-right: 4px;
        }
        .body-content .main .wrap-container .wrap-right .user-info .action {
            padding-right: 30px !important;
            padding-bottom: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:UserInfo ID="ucUserInfo" runat="server"></uc:UserInfo>
</asp:Content>
