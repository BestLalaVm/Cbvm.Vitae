<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ThickBox.Master" AutoEventWireup="true" CodeBehind="UniversityMessageTopList.aspx.cs" Inherits="Cbvm.Vitae.Manage.Common.UniversityMessageTopList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .condition > .caption {
            display: none;
        }

        .customize h4 {
            padding-bottom: 15px;
            border-bottom: 1px solid #ddd;
        }

        .top-messages {
            list-style-type: decimal;
        }

        .message-container {
            min-height: 265px;
        }

        .message-item {
            margin: 10px 0px;
            font-size: 18px;
        }

        .customize-footer .block-item {
            display: inline-block;
            width: 200px;
            height: 80px;
            background-color: #ddd;
            margin-bottom: 35px;
            position: relative;
        }

            .customize-footer .block-item a {
                position: absolute;
                top: 30px;
                color: black;
                text-decoration: none;
            }

                .customize-footer .block-item a span {
                    width: 200px;
                    text-align: center;
                    display: block;
                }

        .list {
            height: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <div class="caption customize">
        <h4>重要通知</h4>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <div class="message-container">
        <ul class="top-messages">
            <%foreach (var item in TopMessages)
              { %>
            <li class="message-item"><a href="/Manage/Common/UniversityMessageDetail.aspx?id=<%=item.Id %>"><%=item.Title %></a> <%if (item.CreateTime.Date >= DateTime.Now.AddMonths(-1).Date)
                                                                                                                                   { %><span style="margin-left: 10px;"><img src="/image/new.gif" alt="<%=item.CreateTime.ToString("yyyy-MM-dd hh:mm:ss") %>" title="<%=item.CreateTime.ToString("yyyy-MM-dd hh:mm:ss") %>" /></span><%} %></li>
            <%} %>
        </ul>

        <%if (TopMessages.Count == 0)
          { %>
        <div>无任何通知</div>
        <%} %>
    </div>
    <div class="customize-footer">
        <div class="block-item">
            <a href="http://210.34.215.230:8080/jxq/front/message.jhtml?messageCode=XM1000011117" target="_blank"><span>就业指导</span></a>
        </div>
<%--        <div class="block-item">
            <a href="#" target="_blank"><span>就业政策</span></a>
        </div>--%>
        <div class="block-item">
            <a href="http://210.34.215.230:8080/jxq/front/message.jhtml?messageCode=XM1000011114" target="_blank"><span>手续办理流程</span></a>
        </div>
        <div class="block-item">
            <a href="http://xmut.ncss.org.cn/jixun/JixunLogin.aspx" target="_blank"><span>职业评测</span></a>
        </div>
        <div class="block-item">
            <a href="http://210.34.215.230:8080/jxq/front/message.jhtml?messageCode=XM1000011115" target="_blank"><span>下载中心</span></a>
        </div>
        <div class="block-item">
            <a href="http://210.34.215.230:8080/jxq/front/message.jhtml?messageCode=XM1000011119" target="_blank"><span>升学指导</span></a>
        </div>
<%--        <div class="block-item">
            <a href="#" target="_blank"><span>考公专区</span></a>
        </div>
        <div class="block-item">
            <a href="#" target="_blank"><span>职业生涯规划</span></a>
        </div>--%>
    </div>
</asp:Content>

