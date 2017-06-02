<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master"
    AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.Activity" Theme="FrontStudent" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/ActivityDetailControl.ascx"
    TagName="ActivityDetailControl" TagPrefix="uc" %>

<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <div class="block-center">
            <uc:ActivityDetailControl ID="actControl" runat="server">
            </uc:ActivityDetailControl>
        </div>
    </div>
</asp:Content>
