<%@ Page Title="" Language="C#" MasterPageFile="~/Template/NewMaster.Master" AutoEventWireup="true" CodeBehind="EnterpriseList.aspx.cs" Inherits="Cbvm.Vitae.Template.EnterpriseList" %>

<%@ Register Src="~/Template/UserControlV2/EnterpriseListWidget.ascx" TagName="EnterpriseListWidget" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link type="text/css" rel="Stylesheet" href="/Content/style/qylb.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <uc:EnterpriseListWidget ID="entPriseList" runat="server"></uc:EnterpriseListWidget>
</asp:Content>
