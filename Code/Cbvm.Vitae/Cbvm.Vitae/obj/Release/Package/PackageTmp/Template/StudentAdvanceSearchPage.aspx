<%@ Page Title="" Language="C#" MasterPageFile="~/Template/ThickBox.Master" AutoEventWireup="true" CodeBehind="StudentAdvanceSearchPage.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentAdvanceSearchPage" %>
<%@ Register src="~/Template/UserControl/StudentAdvanceSearchWidget.ascx" TagName="StudentAdvanceSearchWidget" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body {
            overflow: hidden;
            background: url("") !important;
        }
        .keyword label.hintText {
            top: 8px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
   <uc:StudentAdvanceSearchWidget ID="saWidget" runat="server"></uc:StudentAdvanceSearchWidget>
</asp:Content>
