<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseDetailWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControlV2.EnterpriseDetailWidget" %>
<style type="text/css">
    .company-logo {
        min-height:20px;
    }
</style>
<%if (this.Presentation != null)
  { %>
<div class="qylbContent clearfix">
    <div class="content-left">
        <dl class="company">
            <dt>职位发布企业</dt>
            <dd>
                <h3 class="company-logo">
                    <%if(!String.IsNullOrEmpty(Presentation.Image)){ %>
                    <img src="/content/images/logolink3.jpg">
                    <%} %>
                </h3>
                <h4><%=Presentation.Name %></h4>
                <p>
                    <span>行业：</span> <%=Presentation.IndustryName %>
                </p>
                <p>
                    <span>规模：</span> <%=Presentation.ScopeName %>
                </p>
                <p>
                    <span>性质：</span> <%=Presentation.TypeName %>
                </p>
                <p>
                    <span>地址：</span> <%=Presentation.Address %>
                </p>
            </dd>
        </dl>
        <%if (Presentation.Jobs.Count>0){ %>
        <div class="stu-detail clearfix style-1">
            <h4><span>最新岗位</span></h4>
            <%foreach(var job in Presentation.Jobs){ %>
            <p><a href="<%=String.Format("{0}&id={1}",LkDataContext.AppConfig.JobDetailPageLink,job.Code)  %>"><%=job.Name %></a> </p>
            <%} %>
        </div>
        <%} %>
    </div>
    <div class="content-right">
        <div class="grxmLocal">
            <a href="/Template/EnterpriseList.aspx">企业列表 </a> &gt; <a href="#">企业详情</a>
        </div>
        <div class="grxmListContent">
            <div class="qylbListTit">
                <div class="qylbTitName"><a href="#">企业详情</a></div>
            </div>
            <p class="grxmDocContent">
                <%=Presentation.Description %>
            </p>
        </div>
    </div>
</div>
<%}
  else { Response.Redirect("/Template/EnterpriseList.aspx"); }%>