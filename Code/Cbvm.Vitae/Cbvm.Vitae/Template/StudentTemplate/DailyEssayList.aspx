<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master" AutoEventWireup="true" CodeBehind="DailyEssayList.aspx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.DailyEssayList" Theme="FrontStudent" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/DailyEssayList.ascx" TagName="DailyEssayList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <uc:DailyEssayList ID="dailyList" runat="server"></uc:DailyEssayList>
    </div>
</asp:Content>
