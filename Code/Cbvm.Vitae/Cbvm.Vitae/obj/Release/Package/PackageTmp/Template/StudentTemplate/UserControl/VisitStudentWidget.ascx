<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisitStudentWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.VisitStudentWidget" %>
<script type="text/javascript" src="/Scripts/marquee/marquee.js"></script>
<div class="widget block-widget visit-student-widget">
    <div class="bar">
        <div class="caption">最近访客(学生)</div>
    </div>
    <div class="container">
        <div id="VisitStudentList">
            <asp:Repeater ID="rptStudent" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="widget-item">
                        <div class="image">
                            <a href='<%#Eval("Url") %>' title='<%#Eval("MarjorName") %>'>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Photo") %>' /></a>
                        </div>
                        <div class="detail">
                            <div class="row name">
                                <a href='<%#Eval("Url") %>' title='<%#Eval("MarjorName") %>'>
                                    <%#Eval("NameZh")%>
                                </a>
                            </div>
<%--                            <div class="row">
                                <%#Eval("MarjorName")%>
                            </div>--%>
                                                        <div class="row">
                                <%#Eval("VisitTime")%>
                            </div>
                        </div>
                        <br class="clear" />
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
<%--    <script type="text/javascript">
        $(function () {
            var marqueeObj = new Marquee({
                obj: 'VisitStudentList',
                mode: 'y'
            });
        });
    </script>--%>