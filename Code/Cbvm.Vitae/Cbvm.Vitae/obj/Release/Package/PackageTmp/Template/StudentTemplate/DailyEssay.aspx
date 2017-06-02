<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master"
    AutoEventWireup="true" CodeBehind="DailyEssay.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.DailyEssay" Theme="FrontStudent" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/DailyEssayDetailControl.ascx"
    TagName="DailyEssayDetailControl" TagPrefix="uc" %>

<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <div class="block-center">
            <uc:DailyEssayDetailControl ID="dailyDetail" runat="server">
            </uc:DailyEssayDetailControl>
        </div>
    </div>
</asp:Content>
