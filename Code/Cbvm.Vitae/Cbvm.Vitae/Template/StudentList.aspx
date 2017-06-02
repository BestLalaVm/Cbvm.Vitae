<%@ Page Title="" Language="C#" MasterPageFile="~/Template/NewMaster.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentList" %>

<%@ Register Src="~/Template/UserControlV2/StudentList.ascx" TagName="StudentList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" rel="Stylesheet" href="/Content/style/zxsxslb.css" />
    <style type="text/css">
        .dialog-container {
            overflow: hidden !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <uc:StudentList ID="stuList" runat="server"></uc:StudentList>
</asp:Content>
