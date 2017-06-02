<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfessionalDetailControl.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.ProfessionalDetailControl" %>
<div class="detail-widget">

    <div class="container">
        <asp:PlaceHolder runat="server" ID="phContainer">
            <div class="row-caption">
                <h3>证书名称</h3>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlName" runat="server"></asp:Literal>
            </div>
            <div class="row-caption">
                <h3>获取时间</h3>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlObtainTime" runat="server"></asp:Literal>
            </div>
            <div class="row-caption">
                <h3>描述</h3>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
            </div>
            <asp:PlaceHolder ID="phAttachmentList" runat="server">
                <fieldset>
                    <legend>
                        <h3>附件列表</h3>
                    </legend>
                    <div class="attachment-list">
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
                </fieldset>
            </asp:PlaceHolder>
        </asp:PlaceHolder>
    </div>

</div>
