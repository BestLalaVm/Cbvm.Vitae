<%@ Page Title="" Language="C#" MasterPageFile="~/Template/NewMaster.Master" AutoEventWireup="true" CodeBehind="EnterpriseDetail.aspx.cs" Inherits="Cbvm.Vitae.Template.EnterpriseDetail" %>
<%@ Register Src="~/Template/UserControlV2/EnterpriseDetailWidget.ascx" TagName="EnterpriseDetailWidget" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link type="text/css" href="/Content/style/qyxq.css" rel="Stylesheet" />
  <style type="text/css">
    .job-request
    {
       width:120px;
       margin-left:auto;
       margin-right:auto;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
  <uc:EnterpriseDetailWidget id="eptDetail" runat="server"></uc:EnterpriseDetailWidget>
</asp:Content>
