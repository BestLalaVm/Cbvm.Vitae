<%@ Page Title="" Language="C#" MasterPageFile="~/Template/StudentTemplate/NewMaster.master"
    AutoEventWireup="true" CodeBehind="Professional.aspx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.Professional" %>

<%@ Register Src="~/Template/StudentTemplate/UserControl/ProfessionalDetailControl.ascx"
    TagName="ProfessionalDetailControl" TagPrefix="uc" %>

<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" runat="server">
    <div class="grxmListContent">
        <div class="block-center">
            <uc:ProfessionalDetailControl ID="professionalDetail" runat="server">
            </uc:ProfessionalDetailControl>
        </div>
    </div>
</asp:Content>
