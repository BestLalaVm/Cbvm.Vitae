<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ThickBox.Master" AutoEventWireup="true" CodeBehind="UniversityMessageDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Common.UniversityMessageDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .condition > .caption {
            display: none;
        }
        .customize h4 {
            padding-bottom:15px;
            border-bottom:1px solid #ddd;
        }
        .main {
            min-height:660px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
        <div class="caption customize">
        <h4><%=this.MessageItem.Title %></h4>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <div style="margin-bottom:10px;">时间: <%=this.MessageItem.CreateTime.ToString("yyyy-MM-dd hh:mm:ss") %></div>
    <div class="desc">
        <%=this.MessageItem.Content %>
    </div>
</asp:Content>
