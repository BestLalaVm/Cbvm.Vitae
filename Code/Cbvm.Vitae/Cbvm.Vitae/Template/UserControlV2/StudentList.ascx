<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentList.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControlV2.StudentList" %>
<%@ Register Src="~/Template/UserControlV2/StudentTop20Widget.ascx" TagName="StudentTop20Widget" TagPrefix="uc" %>
<%@ Register Assembly="CustomControl" Namespace="CustomControl" TagPrefix="custom" %>

<style type="text/css">
    .query ul li dl {
        width: 100px !important;
    }

    .page {
        margin-top: 20px;
        margin-left: 18px !important;
    }

    .headDown + .activity {
        display: none;
    }
</style>
<div class="wrap-container">
    <div class="container">
        <div class="activity">
            <a href="###">
                <img src="/Content/images/activity.jpg" width="1130"></a>
        </div>
        <div class="query" id="studentSearch">
            <div style="width:860px; margin:0px auto;">
                <ul class="clearfix">
                    <li class="keyword">
                        <label>关键词</label>
                        <dl>
                            <dd>求职意向/姓名</dd>
                        </dl>
                        <input type="text" data-bind="value: keyword">
                    </li>
<%--                    <li>
                        <label>所属系</label>
                        <input type="text" data-bind="value: depart">
                    </li>--%>
                    <li>
                        <label>专业</label>
                        <input type="text" data-bind="value: marjor">
                    </li>
                </ul>
                <div class="xszzwSearchButton">
                    <a href="#" data-bind="click: events.search">
                        <img src="/content/images/xszzwSearchButton.jpg"></a>
                </div>
                <div class="xszzwSearchClear">
                    <a href="#" data-bind="click: events.reset">清空搜索条件</a>
                </div>
                <div class="searchNotic">
                    当前搜索：未输入搜索条件
                </div>
            </div>

        </div>
        <div style="width: 100%; overflow: hidden;">
            <div class="contentLeft">
                <uc:StudentTop20Widget id="stutopwidget" runat="server"></uc:StudentTop20Widget>
            </div>
            <div class="contentRight">
                <div class="clickRankTit">
                    为您找到 <font style="color: #F28F18;"><%=TotalCount %> </font>个学生
                </div>
                <div class="zxslist">
                    <asp:Repeater ID="rptStudent" runat="server">
                        <ItemTemplate>
                            <div class="zxsDetailInfo">
                                <div class="zxsDetailLeft">
                                    <p class="zxsDetailImg">
                                        <a href='<%#Eval("Url") %>' title='查看明细'>
                                            <img src="<%#Eval("Photo") %>"></a>
                                    </p>
                                    <p class="zxsDetailP"><%#Eval("VisitedCount") %></p>
                                    <p class="zxsDetailNumPStyle">23</p>
                                </div>
                                <div class="zxsDetailRight">
                                    <div style="float: left; width: 90px;">
                                        <p>姓名</p>
                                        <p>性别</p>
                        <%--                <p>所属系别</p>--%>
                                        <p>所属专业</p>
                                        <p>个人主页</p>
                                    </div>
                                    <div style="float: left;">
                                        <p><a href="#"><%#Eval("NameZh") %></a></p>
                                        <p><a href="#"><%#Eval("Sex") %></a></p>
                <%--                        <p><a href="#"><%#Eval("DepartName") %></a></p>--%>
                                        <p><a href="#"><%#Eval("MarjorName") %> &nbsp;</a></p>
                                        <p><a style="color: #6699CC;" href="<%#Eval("Url") %>">查看个人主页</a></p>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <br class="clear" />
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="page">
                    <custom:CustomPager ID="CustomPager" runat="server" PageCountPerPage="15" OnRepeaterDataItemPropertying="CustomPager_RepeaterDataItemPropertying">
                        <headertemplate>
                            <div class="pageTopLast">
                                <a href="#"><img src="/content/images/pageTopButton.jpg"></a>
                            </div>
                            <div class="pageUpDown">
                                <a href="#">上一页</a>
                            </div>
                        </headertemplate>
                        <itemtemplate>
                            <div class="pageNum"> <a href='<%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"Url")%>' class='<%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"CssClass")%>' ><%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"Text")%></a></div>
                         </itemtemplate>
                        <footertemplate>
                            <div class="pageUpDown">
                                <a href="#">下一页</a>
                            </div>
                            <div class="pageTopLast">
                                <a href="#">
                                    <img src="/content/images/pageLastButton.jpg">
                                </a>
                            </div>
                            <br class="clear" />
                        </footertemplate>
                    </custom:CustomPager>
                </div>
                <div id="divMsg" runat="server" visible="false" class="empty-container">
                    没有任何记录!
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    var searchViewModel = function () {
        var self = this;

        self.keyword = ko.observable("<%=Request.QueryString["keyword"]%>");
<%--        self.depart = ko.observable("<%=Request.QueryString["depart"]%>");--%>
        self.marjor = ko.observable("<%=Request.QueryString["marjor"]%>");

        self.events = {
            search: function () {
                var param = "";
                if (self.keyword()) {
                    param = "keyword=" + self.keyword();
                }

                //if (self.depart()) {
                //    if (param) {
                //        param = param + "&";
                //    }
                //    param = param + "depart=" + self.depart();
                //}

                if (self.marjor()) {
                    if (param) {
                        param = param + "&";
                    }
                    param = param + "marjor=" + self.marjor();
                }
                if (param) {
                    param = "?" + param;
                }

                window.location.href = "/Template/StudentList.aspx" + param;
            },
            reset: function () {
                self.keyword("");
                self.depart("");
                self.marjor("");
            }
        }
    }

    $(function () {
        var model = searchViewModel();
        ko.applyBindings(model, document.getElementById("studentSearch"));
    });
</script>
