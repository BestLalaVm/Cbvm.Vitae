<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Master.Master" AutoEventWireup="true" CodeBehind="MessageList.aspx.cs" Inherits="Cbvm.Vitae.Template.MessageList" %>

<%@ Register Src="~/Template/UserControl/MessageList.ascx" TagName="MessageList" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
  <uc:MessageList ID="mgmList" runat="server"></uc:MessageList>
</asp:Content>
