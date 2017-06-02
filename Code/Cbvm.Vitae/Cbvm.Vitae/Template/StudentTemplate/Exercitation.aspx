<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master" AutoEventWireup="true" CodeBehind="Exercitation.aspx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.Exercitation" Theme="FrontStudent" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/ExercitationDetailControl.ascx" TagName="ExercitationDetailControl" TagPrefix="uc" %>

<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <div class="block-center">
            <uc:ExercitationDetailControl ID="extDetail" runat="server"></uc:ExercitationDetailControl>
        </div>
    </div>
</asp:Content>
