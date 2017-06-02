<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master" AutoEventWireup="true" CodeBehind="ProfessionalList.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.ProfessionalList" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/NewestTopProjectWidget.ascx" TagName="NewestTopProjectWidget" TagPrefix="uc" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/NewestTopActivityWidget.ascx" TagName="NewestTopActivityWidget" TagPrefix="uc" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/NewestTopDailyEssayWidget.ascx" TagName="NewestTopDailyEssayWidget" TagPrefix="uc" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/ProfessionalList.ascx" TagName="ProfessionalList" TagPrefix="uc" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/NewestTopExercitationWidget.ascx"
    TagName="NewestTopExercitationWidget" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <uc:ProfessionalList ID="professList" runat="server"></uc:ProfessionalList>
    </div>
</asp:Content>
