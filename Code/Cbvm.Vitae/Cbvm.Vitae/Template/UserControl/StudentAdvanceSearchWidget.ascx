<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentAdvanceSearchWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControl.StudentAdvanceSearchWidget" %>
<div style="padding: 15px;">
    <table>
        <tr>
            <th align="right">专业名称:</th>
            <td colspan="4">
                <select id="drpMarjorName" style="width: 300px; height: 25px;">
                    <option value="">全部</option>
                    <% foreach (var marjor in Marjors)
                       {
                    %>
                    <option value="<%=marjor.Code %>" <%=(marjor.Code==MarjorCode?"selected='selected'":"") %>><%=marjor.Name %></option>
                    <%
                       } %>
                </select></td>
        </tr>
        <tr>
            <th align="right">性别:</th>
            <td>
                <select id="drpSex" style="height: 25px;">
                    <option value="">全部</option>
                    <option value="1" <%=(Sex=="1"?"selected='selected'":"") %>>男</option>
                    <option value="0" <%=(Sex=="0"?"selected='selected'":"") %>>女</option>
                </select>
            </td>
            <th align="right">学号/姓名:</th>
            <td class="keyword">
                <input type="text" id="txtKeyword"  EmptyMessage="学号/姓名" style="height: 20px;width: 162px;" value="<%=Keyword %>" /></td>
            <td>
                <input type="button" value="搜索" style="height: 30px;" onclick="Go()" /></td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    function Go() {
        var queryString = "";
        //var drpMarjorName = document.getElementById("drpMarjorName");
        var marjorCode = $("#drpMarjorName option:selected").attr("value");
        if (marjorCode) {
            queryString = "marjorCode=" + marjorCode;
        }
        //var drpSex = document.getElementById("drpSex");
        var sexValue = $("#drpSex option:selected").attr("value");
        if (sexValue) {
            if (queryString) {
                queryString += "&";
            }
            queryString += "sex=" + sexValue;
        }
        var txtKeyword = document.getElementById("txtKeyword");
        if (txtKeyword.value) {
            if (queryString) {
                queryString += "&";
            }
            queryString += "keyword=" + txtKeyword.value;
        }
        var url = "/Student/1";
        if (queryString) {
            url += "?" + queryString;
        }
        window.parent.location.href = url;
    }
</script>
