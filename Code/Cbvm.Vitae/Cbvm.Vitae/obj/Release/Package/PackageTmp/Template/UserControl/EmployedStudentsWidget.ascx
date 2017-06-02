<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmployedStudentsWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControl.EmployedStudentsWidget" %>
<div class="widget block-widget employed-student-widget">
    <div class="bar">
        <div class="caption">近期被录用前20名学生</div>
    </div>
    <div class="container">
        <div id="TopEmployedStudentList">
            <asp:Repeater ID="rptStudent" runat="server">
                <ItemTemplate>
                    <div class="widget-item">
                        <div class="image">
                            <a href='<%#Eval("Url") %>' title='<%#Eval("MarjorName") %>'>
                                <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("Photo") %>' /></a>
                        </div>
                        <div class="detail">
                            <div class="row name">
                                <a href='<%#Eval("Url") %>' title='<%#Eval("MarjorName") %>'>
                                    <%#Eval("NameZh")%>(<%#Eval("EmployedTime") %>)
                                </a>
                            </div>
                            <div class="row">
                                <%#Eval("MarjorName")%>
                            </div>
                            <div class="row">
                                <a href='<%#Eval("EnterpriseUrl") %>' title='<%#Eval("EnterpriseName") %>'><%#Eval("EnterpriseName") %></a>
                            </div>
                                                        <div class="row">
                                <a href='<%#Eval("JobNameUrl") %>' title='<%#Eval("JobName") %>'><%#Eval("JobName") %></a>
                            </div>
                            <div class="row" style="color: red;">
                                <%#Eval("ReferralName")%>
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
                obj: 'TopEmployedStudentList',
                mode: 'y'
            });
        });
    </script>
</div>
