<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyEssayDetailControl.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.DailyEssayDetailControl" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/CommentWidget.ascx" TagName="CommentWidget" TagPrefix="uc" %>

<div class="detail-widget">
    <div class="container">
        <asp:PlaceHolder runat="server" ID="phContainer">
            <div class="caption">
                <h1>
                    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h1>
                <div class="content-time">
                    <div class="left">
                        <asp:Literal ID="ltlTime" runat="server"></asp:Literal>
                    </div>
                    <br class="clear" />
                </div>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
            </div>
            <div>
                <uc:commentwidget id="cmtWidget" runat="server" commenttype="DailyEssay"></uc:commentwidget>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
