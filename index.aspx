<%@ Page Language="C#" AutoEventWireup="true" Inherits="SohhScrape.index" MasterPageFile="~/MobileSohhMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="preXsltContent" runat="server">
    <div id="bodyIndex">
        <a name="top"></a>
        <div id="headerToolbar" class="toolbar">
            <h1>Mpiff.com</h1>
            <a class="leftButton" href="about.aspx"><span>About</span></a> <a class="rightButton"
                href="settings.aspx"><span>Settings</span></a>
        </div>
        <div id="logoHeader">
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="postXsltContent" runat="server">
    <%if (Session["username"] != String.Empty) { %>
    <a href="logout.aspx" class="genericButton">Log Out</a>
    <%} else { %>
    <a href="login.aspx" class="genericButton">Log In</a>
    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerBarContent" runat="server">
    </div> <a name="bottom"></a>
</asp:Content>
