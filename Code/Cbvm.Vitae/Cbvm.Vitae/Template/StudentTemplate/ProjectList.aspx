<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.ProjectList" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/ProjectList.ascx" TagName="ProjectList" TagPrefix="uc" %>

<asp:Content ID="Content4" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <uc:ProjectList ID="project" runat="server"></uc:ProjectList>
    </div>

</asp:Content>
