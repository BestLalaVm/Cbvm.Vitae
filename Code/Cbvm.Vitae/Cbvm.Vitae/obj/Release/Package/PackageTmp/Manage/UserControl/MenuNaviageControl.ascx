<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuNaviageControl.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.UserControl.MenuNaviageControl" %>
<%@ Import Namespace="Presentation" %>

<div class="main-menu">
    <div class="navigate-container">
        <asp:Repeater ID="rptNavigate" runat="server" OnItemDataBound="rptNavigate_ItemDataBound">
            <ItemTemplate>
                <a title='<%#Eval("HelpTip") %>' navigatetype='<%#Eval("NavigateType") %>' value='<%#Eval("LinkUrl") %>' href='<%# ((NavigateItemType)Eval("NavigateType")==NavigateItemType.Front)?Eval("LinkUrl"):"#" %>' target='<%# ((NavigateItemType)Eval("NavigateType")==NavigateItemType.Front)?"_blank":"_self" %>'><span>
                    <%#Eval("Text")%>
                </span></a>
                <div class="sub-container">
                    <asp:Repeater ID="rptNavigateItems" runat="server">
                        <ItemTemplate>
                            <a title='<%#Eval("HelpTip") %>' navigatetype='<%#Eval("NavigateType") %>' value='<%#Eval("LinkUrl") %>' href='<%# ((NavigateItemType)Eval("NavigateType")==NavigateItemType.Front)?Eval("LinkUrl"):"#" %>'><span>
                                <%#Eval("Text")%>
                            </span></a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<asp:HiddenField ID="hdfMenuStatus" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        var menuList = $(".main-menu .navigate-container a");
        menuList.click(function () {
            menuList.each(function () {
                $(this).removeClass("selected");
            })
            $(this).addClass("selected");
            $("#<%=hdfMenuStatus.ClientID %>").val($(this).attr("value"));
            var iframe = $("iframe#frame");
            if (iframe.attr("src") == $(this).attr("value")) {
                //return;
            }
            
            if ($(this).attr("href") == "#") {
                iframe.attr("src", $(this).attr("value"));
            }
            var navigateType = $(this).attr("navigateType");

            $(".body-content .main").css("width", "1120px");
            var contentWindow = iframe[0].contentWindow;
            $(iframe).load(function() {
                if (typeof contentWindow.menuItemEx == "function") {
                    contentWindow.menuItemEx(navigateType, $(this).attr("value"), ".body-content .main", "iframe#frame");
                }
            });
        });

        var href = $("#<%=hdfMenuStatus.ClientID %>").val();
        menuList.each(function () {
            if ($(this).data("value") == href) {
                $(this).addClass("selected");
            }
        })
    })
</script>
