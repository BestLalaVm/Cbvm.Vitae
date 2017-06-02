<%@ Page Title="" Language="C#" MasterPageFile="~/Template/NewMaster.Master" AutoEventWireup="true" CodeBehind="EnterpriseRegister.aspx.cs" 
    Inherits="Cbvm.Vitae.Template.EnterpriseRegister" %>

<%@ Register Src="~/Template/UserControlV2/EnterpriseRegister.ascx" TagName="EnterpriseRegister" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="/Content/style/qyzc.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <uc:EnterpriseRegister ID="entRegister" runat="server"></uc:EnterpriseRegister>
</asp:Content>
