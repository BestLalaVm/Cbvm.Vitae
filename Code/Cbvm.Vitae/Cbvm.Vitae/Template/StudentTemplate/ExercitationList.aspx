<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master" AutoEventWireup="true" CodeBehind="ExercitationList.aspx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.ExercitationList" Theme="FrontStudent" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/ExercitationList.ascx" TagName="ExercitationList" TagPrefix="uc" %>

<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <uc:ExercitationList ID="exercitationList" runat="server"></uc:ExercitationList>
    </div>
</asp:Content>
