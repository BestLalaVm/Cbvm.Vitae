<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Master.Master" AutoEventWireup="true"
    CodeBehind="Message.aspx.cs" Inherits="Cbvm.Vitae.Template.Message" Theme="BaseFront" %>

<%@ Register Src="~/Template/UserControl/UserNavigateControl.ascx" TagName="UserNavigateControl" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMain" runat="server">
    <div class="article widget">
        <div class="bar">
            <div class="caption">
                <uc:usernavigatecontrol id="ucNaviageControl" runat="server"></uc:usernavigatecontrol>
            </div>
        </div>
        <div class="container">
            <asp:PlaceHolder runat="server" ID="phContainer">
                <div class="title">
                    <h1>
                        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
                    </h1>
                </div>
                <div class="content-time">
                    <span class='label'>发表时间:</span>
                    <asp:Literal ID="ltlTime" runat="server"></asp:Literal>
                </div>
                <div class="content">
                    <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
                </div>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>

