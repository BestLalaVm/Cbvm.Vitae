<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="ChildCourseScoreList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Family.Children.ChildCourseScoreList" Theme="FamilyManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main {
            width: 810px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadGrid ID="grdCourseScore" runat="server" OnNeedDataSource="grdCourseScore_NeedDataSource" AutoGenerateColumns="false">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                             <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CourseName" HeaderText="课程名称">
                    <HeaderStyle Width="450px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="450px" HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Score" HeaderText="分数">
                    <HeaderStyle Width="40px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="40px" HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ExamineTime" HeaderText="考试时间">
                    <HeaderStyle Width="60px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="60px" HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CourseScoreType" HeaderText="考试类型">
                    <HeaderStyle Width="50px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle Width="50px" HorizontalAlign="Left"></ItemStyle>
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
