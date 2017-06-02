<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master" AutoEventWireup="true" CodeBehind="ActivityList.aspx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.ActivityList" Theme="FrontStudent" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/ActivityList.ascx" TagName="ActivityList" TagPrefix="uc" %>


<%--<asp:Content ID="Content2" ContentPlaceHolderID="contentLeftContainer" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentRight" runat="server">
    <uc:NewestTopProjectWidget ID="projectWidget" runat="server"></uc:NewestTopProjectWidget>
    <uc:NewestTopExercitationWidget ID="exercitationList" runat="server"></uc:NewestTopExercitationWidget>
    <uc:NewestTopProfessionalWidget ID="professionList" runat="server"></uc:NewestTopProfessionalWidget>
    <uc:NewestTopDailyEssayWidget ID="dailyEssayList" runat="server"></uc:NewestTopDailyEssayWidget>
</asp:Content>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <uc:ActivityList ID="actiList" runat="server"></uc:ActivityList>
    </div>
</asp:Content>
