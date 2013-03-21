<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/post.aspx.cs" Inherits="SohhScrape.post" MasterPageFile="~/MobileSohhMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="preXsltContent" runat="server">
    <div id="bodyPost">
        <a name="top"></a>
        <div id="headerToolbar" class="toolbar">
            <h1><%=Page.Title %></h1>
            <a class="leftButton" 
                href="<%= (Request.QueryString["do"] == "newthread") ? "forumdisplay.aspx?" + Request.QueryString : "showthread.aspx?" + Request.QueryString  %>">
                <span>Cancel</span>
            </a> 
            <a class="rightButton" href="#" onclick="submitForm(); return false;">
                <span><%= (Request.QueryString["do"] == "editpost") ? "Update" : "Post" %></span></a>
        </div>
        <form action="post.aspx" method="post" name="postform" id="postform" runat="server">
            <asp:HiddenField ID="hSecurityToken" runat="server" />
            <asp:HiddenField ID="hThreadId" runat="server" />
            <asp:HiddenField ID="hLoggedInUserId" runat="server" />
            <asp:HiddenField ID="hReferredPostId" runat="server" />
            <asp:HiddenField ID="hPageNumber" runat="server" />
            <asp:HiddenField ID="hForumId" runat="server" />
            <asp:HiddenField ID="hDo" runat="server" />

            <div class="postButtonBar">
                <a class="vbBtn" href="#" onclick="insertBold(); return false;"><img src="images/btnB.gif" alt="Bold" border="0"></a>
                <a class="vbBtn" href="#" onclick="insertQuote(); return false;"><img src="images/btnQuote.gif" alt="Quote" border="0"></a>
                <a class="vbBtn" href="#" onclick="insertList(); return false;"><img src="images/btnList.gif" alt="List" border="0"></a>
                <a class="vbBtn" href="#" onclick="insertOrderList(); return false;"><img src="images/btnOrderList.gif" alt="Ordered List" border="0"></a>

                <a class="vbBtn" href="#" onclick="showHideMore(); return false;"><img src="images/btnMore.gif" id="btnMore" alt="More" border="0"></a>
            </div>
            <div id="postMoreButtons" class="postButtonBar" style="display: none;">
                <a class="vbBtn" href="#" onclick="insertURL(); return false;"><img src="images/btnURL.gif" alt="URL" border="0"></a>
                <a class="vbBtn" href="#" onclick="insertCode(); return false;"><img src="images/btnCode.gif" alt="Code" border="0"></a>
            </div>
            <div id="postMoreOptions" class="postMoreOptions" style="display: none;">
                Subscription: 
                <asp:DropDownList ID="ddlEmailUpdate" runat="server">
                    <asp:ListItem Selected="True" Text="Don't Subscribe" Value="9999"></asp:ListItem>
                    <asp:ListItem Text="With No Emails" Value="0"></asp:ListItem>
                    <asp:ListItem Text="With Instant Emails" Value="1"></asp:ListItem>
                    <asp:ListItem Text="With Daily Emails" Value="2"></asp:ListItem>
                    <asp:ListItem Text="With Weekly Emails" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div id="loggedInName" class="postInput" style="display: none;">
                Logged in as: <span class="postThreadTitle"><%=Session["username"]%></span>
            </div>
            <div class="postInput" style="<%= (Request.QueryString["do"] == "newthread") ? "display:none" : "display:block" %>">
                Thread: <span class="postThreadTitle"><%=ThreadName %></span>
            </div>
            <div class="postInput" style="<%= (Request.QueryString["do"] != "newthread") ? "display:none" : "display:block" %>">
                Title: <asp:TextBox ID="txtTitle" CssClass="postInput" MaxLength="85" runat="server"></asp:TextBox>
            </div>
            <div class="postMessageArea">
                <asp:TextBox ID="message" Columns="20" Rows="0" CssClass="postMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </form>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="postXsltContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footerBarContent" runat="server">
    </div> <a name="bottom"></a>
</asp:Content>
