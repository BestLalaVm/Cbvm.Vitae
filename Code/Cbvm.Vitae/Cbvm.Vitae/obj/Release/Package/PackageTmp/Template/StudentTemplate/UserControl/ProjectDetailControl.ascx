<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectDetailControl.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.ProjectDetailControl" %>
<%@ Register Src="~/UserControl/GradeControl.ascx" TagName="GradeControl" TagPrefix="uc" %>
<%@ Register Src="~/Template/StudentTemplate/UserControl/CommentWidget.ascx" TagName="CommentWidget"
    TagPrefix="uc" %>
<div class="detail-widget">
    <div class="container">
        <asp:PlaceHolder runat="server" ID="phContainer">
            <div class="caption">
                <h1>
                    <asp:Literal ID="ltlName" runat="server"></asp:Literal></h1>
            </div>
            <div class="row-caption">
                <h3>职责及项目时间</h3>
            </div>
            <div class="row-content">
                <div class="left">
                    <asp:Literal ID="ltlPosition" runat="server"></asp:Literal>
                </div>
                <div class="right">
                    <asp:Literal ID="ltlTime" runat="server"></asp:Literal>
                </div>
                <br class="clear" />
            </div>
            <div class="row-caption">
                <h3>职责描述</h3>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlPositionDescription" runat="server"></asp:Literal>
            </div>
            <div class="row-caption">
                <h3>项目自评</h3>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlEvaluate" runat="server"></asp:Literal>
            </div>
            <div class="row-caption">
                <h3>团队描述</h3>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlTeamDescription" runat="server"></asp:Literal>
            </div>
            <div class="row-caption">
                <h3>项目描述</h3>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
            </div>
            <div class="row-caption">
                <h3>导师评价</h3>
            </div>
            <div class="row-content">
                <div class="rate-list">
                    <uc:gradecontrol id="gradeUsable" runat="server" caption="可用性" enabled="false">
                </uc:gradecontrol>
                    <uc:gradecontrol id="gradeSkill" runat="server" caption="技术性" enabled="false">
                </uc:gradecontrol>
                    <uc:gradecontrol id="gradeDreative" runat="server" caption="创意性" enabled="false">
                </uc:gradecontrol>
                    <br class="clear" />
                </div>
            </div>
            <div class="row-content">
                <asp:Literal ID="ltlEvaluateFromTeacher" runat="server"></asp:Literal>
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
                <uc:commentwidget id="comment" runat="server" commenttype="Project">
            </uc:commentwidget>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
<script type="text/javascript">
    $(function () {
        return;
        $(".attachment-list").slides({
            preload: true,
            preloadImage: '/Image/loadingAnimation.gif',
            play: 2000,
            pause: 2000,
            hoverPause: true,
            pagination: false,
            generatePagination: false,
            crossfade: true
        });
    })
</script>
