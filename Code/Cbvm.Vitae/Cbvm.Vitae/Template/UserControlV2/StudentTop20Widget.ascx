<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentTop20Widget.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControlV2.StudentTop20Widget" %>
<script type="text/javascript" src="/Scripts/marquee/marquee.js"></script>
<style type="text/css">
    #TopStudentList {
        height:800px;
    }
</style>
<div class="widget">
    <div class="clickRankTit">
        点击排行
    </div>
    <div class="clickRankContent" id="TopStudentList">
        <asp:Repeater ID="rptStudent" runat="server">
            <ItemTemplate>
                <div class="widget-item">
                    <div class="clickRankImg">
                        <a href='<%#Eval("Url") %>' title='<%#Eval("Title") %>'>
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("Photo") %>' /></a>
                    </div>
                    <div class="clickRankWord">
                        <p>
                            <a href='<%#Eval("Url") %>' title='<%#Eval("Title") %>'>
                                <%#Eval("NameZh")%>
                            </a>
                        </p>
                        <p ><a href="#" style="font-size:10px;"><%#Eval("MarjorName")%></a></p>
                    </div>
                    <div class="clickRankNum">
                        <p>点击量</p>
                        <p class="clickNumPStyle"><%#Eval("VisitedCount") %></p>
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
