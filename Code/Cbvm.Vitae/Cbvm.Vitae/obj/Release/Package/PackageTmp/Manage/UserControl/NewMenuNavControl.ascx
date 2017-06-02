<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewMenuNavControl.ascx.cs" Inherits="Cbvm.Vitae.Manage.UserControl.NewMenuNavControl" %>
<%@ Import Namespace="Presentation" %>

<a href="<%=LkDataContext.AppConfig.HomePageLink %>">
    <img src="/content/images/back-to-index.png"></a>
<ul id="ulMainmenu" class="newMenu-container" >
    <asp:Repeater ID="rptNavigateStudent" runat="server" OnItemDataBound="rptNavigate_ItemDataBound">
        <ItemTemplate>
            <li class="<%#(((bool)Eval("HasSubMenu"))?"hasSubItems":"") %>">
                <a class="head" title='<%#Eval("HelpTip") %>' navigatetype='<%#Eval("NavigateType") %>' value='<%#Eval("LinkUrl") %>' href='<%# ((NavigateItemType)Eval("NavigateType")==NavigateItemType.Front)?Eval("LinkUrl"):"#" %>' target='<%# ((NavigateItemType)Eval("NavigateType")==NavigateItemType.Front)?"_blank":"_self" %>'>
                    <span class="nav-flag <%#(((bool)Eval("HasSubMenu"))?"show":"hide") %>">+</span>
                    <span class="text"><%#Eval("Text")%></span>                        
                </a>
                <asp:Repeater ID="rptNavigateItems" runat="server">
                    <HeaderTemplate>
                        <ul class="sub-container hide" style="width:100%;">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <a title='<%#Eval("HelpTip") %>' class="head" navigatetype='<%#Eval("NavigateType") %>' value='<%#Eval("LinkUrl") %>' href='<%# ((NavigateItemType)Eval("NavigateType")==NavigateItemType.Front)?Eval("LinkUrl"):"#" %>'>
                                <span class="text"><%#Eval("Text")%></span></a>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
<script type="text/javascript">
    $(function () {
        $("#ulMainmenu .hasSubItems > a").click(function () {
            var current = this;
            $("ul.newMenu-container > li>a.head").each(function () {
                if (this != current) {
                    $(this).next(".sub-container").addClass("hide");
                    toggleNavFlag($(this));
                }
            });
            $(current).next(".sub-container").toggleClass("hide");

            toggleNavFlag($(current));
        });
    })
</script>

<asp:HiddenField ID="hdfMenuStatus" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        var menuList = $("#ulMainmenu > li a");
        var lbNavCurrent = $("#lbNavCurrent");
        menuList.click(function () {
            if ($(this).parent().hasClass("hasSubItems")) {

                toggleNavFlag($(this));
                return;
            }

            menuList.each(function () {
                $(this).parent().removeClass("active");
            })
            $(this).parent().addClass("active");
            $("#<%=hdfMenuStatus.ClientID %>").val($(this).attr("value"));
            var iframe = $("iframe#frame");

            if ($(this).attr("href") == "#") {
                BeginRequest();
                iframe.attr("src", $(this).attr("value"));
            } else {
                return;
            }

            var navigateType = $(this).attr("navigateType");

            $(".body-content .main").css("width", "1120px");
            var contentWindow = iframe[0].contentWindow;
            $(iframe).load(function () {
                if (typeof contentWindow.menuItemEx == "function") {
                    contentWindow.menuItemEx(navigateType, $(this).attr("value"), ".body-content .main", "iframe#frame");
                }
            });

            var preTitle = $(this).parent().parent(".sub-container").prev(".head").text();
            if (preTitle) {
                preTitle = preTitle + " - ";
            }
            lbNavCurrent.html(preTitle + $(this).attr("title"));
        });

        var href = $("#<%=hdfMenuStatus.ClientID %>").val();
        if (!href) {
            href = $("#frame").attr("src");
        }
        menuList.each(function () {
            if ($(this).attr("value") == href) {
                $(this).parent().addClass("active");
                $(this).parent().parent(".sub-container").removeClass("hide");
                toggleNavFlag($(this).parent().parent(".sub-container").prev(".head"));

                var preTitle = $(this).parent().parent(".sub-container").prev(".head").text();
                if (preTitle) {
                    preTitle = preTitle + " - ";
                }
                lbNavCurrent.html(preTitle+$(this).attr("title"));
            }
        })
    })

    function toggleNavFlag($alink) {
        if ($alink.next(".sub-container").hasClass("hide")) {
            $alink.find(".nav-flag").text("+");
        } else {
            $alink.find(".nav-flag").text("-");
        }
    }
</script>
