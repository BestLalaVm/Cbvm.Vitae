<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentTop20Widget.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControl.StudentTop20Widget" %>
<script type="text/javascript" src="/Scripts/marquee/marquee.js"></script>
<div class="top-student show-widget widget block-widget">
    <div class="bar">
        <div class="caption">
            <%=Caption%>
        </div>
    </div>
    <div class="container">
        <div id="TopStudentList">
            <asp:Repeater ID="rptStudent" runat="server">
                <ItemTemplate>
                    <div class="widget-item">
                        <div class="image">
                            <a href='<%#Eval("Url") %>' title='<%#Eval("Title") %>'>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Photo") %>' /></a>
                        </div>
                        <div class="detail">
                            <div class="row name">
                                <a href='<%#Eval("Url") %>' title='<%#Eval("Title") %>'>
                                    <%#Eval("NameZh")%>
                                </a>
                                (点击量:<%#Eval("VisitedCount") %>)
                            </div>
                            <div class="row">
                                <%#Eval("MarjorName")%>
                            </div>
                        </div>
                        <br class="clear" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            var marqueeObj = new Marquee({
                obj: 'TopStudentList',
                mode: 'y'
            });
        });
    </script>
</div>
