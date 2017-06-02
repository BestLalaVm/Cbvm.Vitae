<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExercitationDetailControl.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.ExercitationDetailControl" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/CommentWidget.ascx" TagName="CommentWidget"
    TagPrefix="uc" %>
<div class="detail-widget">

    <div class="container">
        <asp:PlaceHolder runat="server" ID="phContainer">
            <div class="caption">
                <h1>
                    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h1>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
            </div>
            <br />
            <br />
            <div class="row-content">
                <asp:Literal ID="ltlAddress" runat="server"></asp:Literal>
            </div>
            <div class="row-content data-time">
                <span><strong>实习时间:</strong></span>
                <asp:Literal ID="ltlTime" runat="server"></asp:Literal>
            </div>
            <asp:PlaceHolder ID="phAttachmentList" runat="server">
                <div class="attachment-list">
                    <div class="caption">
                        <h3>附件列表</h3>
                    </div>
                    <asp:Repeater ID="rptAttachment" runat="server">
                        <HeaderTemplate>
                            <div class="slides_container">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class='attachment-item slide'>
                                <div class="image">
                                    <a href='<%#Eval("Path") %>' target="_blank">
                                        <img src='<%#Eval("ThumbPath") %>' />
                                    </a>
                                </div>
                                <div class="label">
                                    <%#Eval("FileTip")%>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </asp:PlaceHolder>
            <div>
                <uc:commentwidget id="comment" runat="server" commenttype="Exercitation">
            </uc:commentwidget>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
