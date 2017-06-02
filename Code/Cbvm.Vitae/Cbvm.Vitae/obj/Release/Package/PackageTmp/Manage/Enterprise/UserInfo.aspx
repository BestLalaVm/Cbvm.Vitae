<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true"
    CodeBehind="UserInfo.aspx.cs" Inherits="Cbvm.Vitae.Manage.Enterprise.UserInfo"  Theme="EnterpriseManage"%>

<%@ Register Src="~/Manage/Enterprise/UserControl/UserInfo.ascx" TagName="UserInfo"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        html body .RadInput .riTextBox, html body .RadInputMgr
        {
            border-style: none !important;
            background: none !important;
        }
        label
        {
            line-height: 25px;
        }
        .body-content .main > table.wrap-container .wrap-right .container .course-data
        {
            border-bottom: 1px solid #DDD;
        }
        .body-content .main > table.wrap-container .wrap-right .container .course-detail
        {
            border: 1px solid #DDD;
            padding: 5px;
        }
        .body-content .main .wrap-right .user-detail td
        {
            padding-top: 0px !important;
            padding-bottom: 0px !important;
        }
        .jqTransformSelectWrapper > ul
        {
            z-index: 1000 !important;
        }
        th
        {
            width: 120px;
        }
        .body-content .main .wrap-container .wrap-right .action {
            margin-right: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc:UserInfo ID="UserInfo1" runat="server">
    </uc:UserInfo>
</asp:Content>
