<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Login.Master" AutoEventWireup="true"  EnableViewStateMac="false"
CodeBehind="Login.aspx.cs" Inherits="Cbvm.Vitae.Template.Login1" Theme="Login"%>

<%@ Register Src="~/Template/UserControl/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
      .login-block .data-detail .login-textBox, .login-block .data-detail .action {
          margin-left: 60px;
      }
      .enter-block {
          margin-left: 60px;
          margin-top: 5px;
      }
      .login-textBox {
          margin-bottom: 5px;
      }
      .login-block .data-detail .user-type {
          width: 340px;
          margin-left: auto;
      }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <uc:LoginControl ID="LoginControl" runat="server"></uc:LoginControl>
</asp:Content>
