<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/login.aspx.cs" Inherits="SohhScrape.login"
    MasterPageFile="~/MobileSohhMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="preXsltContent" runat="server">
    <div id="bodyLogin">
        <a name="top"></a>
        <script type="text/javascript" src="scripts/vbulletin_md5.js"></script>
        <div id="headerToolbar" class="toolbar">
            <h1>
                Log In</h1>
            <a class="leftButton" href="./index.aspx"><span>Cancel</span></a>
        </div>
        <form class="login" action="login.aspx" method="post" onsubmit="md5hash(password, vb_login_md5password, vb_login_md5password_utf, 0)"
        runat="server">
<asp:Label ID="errorMessage" CssClass="loginError" runat="server" Text=""></asp:Label>
            
            <input type="hidden" name="vb_login_md5password" value="">
            <input type="hidden" name="vb_login_md5password_utf" value="">
            <p>
                User Name:</p>
            <input class="loginInput" type="text" name="username" value="">
            <p>
                Password:</p>
            <input class="loginInput" type="password" name="password" value="">
            <p class="loginText">
                For your security, Javascript is required. <a href="privacy.aspx">Privacy Policy</a></p>
            <input class="loginSubmit" type="submit" name="submit" value="Log In" id="submit">
            <a name="bottom"></a>
        </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="postXsltContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerBarContent" runat="server">
    </div>
</asp:Content>
